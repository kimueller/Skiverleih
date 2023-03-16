using Microsoft.EntityFrameworkCore;
using Skiverleih.Data;
using Skiverleih.Models;

namespace Skiverleih.Services
{
    public class SkiLoanService : ISkiLoanService
    {
        private readonly ApplicationDbContext dbc;

        public SkiLoanService(ApplicationDbContext dbc)
        {
            this.dbc = dbc; //creates a new instanc of the dbContext
        }

        /// <summary>
        /// Adds an Loan in to the OnLoan table
        /// Loan = Customer + Article + Returned
        /// </summary>
        /// <param name="loan">new loan-object to add to DB</param>
        public void AddLoan(OnLoan loan)
        {
            var article = dbc.Articles
                          .Include(a => a.OnLoan)
                          .Where(a => a.ArticleID == loan.ArticleID)
                          .FirstOrDefault();

            if (article != null)
            {
                article.LoanCount = article.LoanCount + 1;//counts the loan count up
                article.LoanStatusID = 1;//sets the loanstatus to OnLoan
                dbc.Update(article);
            }
            loan.LoanDate = DateTime.Now;
            loan.Returned = false; //sets the loan to not returned
            dbc.Add(loan);
            dbc.SaveChanges();
        }

        /// <summary>
        /// Gets all articles from the DB
        /// </summary>
        /// <returns>List of all Articles</returns>
        public List<Article> GetArticles()
        {
            var articles = dbc.Articles
                           .OrderBy(a => a.ArticleID)
                           .Select(a => new Article
                           {
                               ArticleID = a.ArticleID,
                               CategoryID = a.CategoryID,
                               PricePerDay = a.PricePerDay,
                               LoanStatusID = a.LoanStatusID,
                               OnLoan = a.OnLoan,
                               Category = a.Category,
                               LoanCount = a.LoanCount,
                               LoanStatus = a.LoanStatus,
                               Title = a.Title
                           });
            return articles.ToList();
        }

        /// <summary>
        /// Gets all articles with the specific loanStatus
        /// </summary>
        /// <param name="loanStatus">available or on loan</param>
        /// <returns>List of Articles with given loan status</returns>
        public List<Article> GetArticles(int loanStatus)
        {
            var articles = dbc.Articles
                           .OrderBy(a => a.ArticleID)
                           .Where(a => a.LoanStatusID == loanStatus)
                           .Select(a => new Article
                           {
                               ArticleID = a.ArticleID,
                               CategoryID = a.CategoryID,
                               PricePerDay = a.PricePerDay,
                               LoanStatusID = a.LoanStatusID,
                               OnLoan = a.OnLoan,
                               Category = a.Category,
                               LoanCount = a.LoanCount,
                               LoanStatus = a.LoanStatus,
                               Title = a.Title
                           });
            return articles.ToList();
        }

        /// <summary>
        /// Gets all Customers from the DB
        /// </summary>
        /// <returns>List of all customers</returns>
        public List<Customer> GetCustomers()
        {
            var customers = dbc.Customers
                           .OrderBy(c => c.CustomerID)
                           .Select(c => new Customer
                           {
                               CustomerID = c.CustomerID,
                               FirstName = c.FirstName,
                               LastName = c.LastName,
                               PhoneNumber = c.PhoneNumber,
                               BirthDate = c.BirthDate,
                               OnLoan = c.OnLoan,
                           });
            return customers.ToList();
        }

        /// <summary>
        /// Gets all customers that loaned out an article
        /// </summary>
        /// <returns>List of customers with a loan object</returns>
        public List<Customer> GetCustomersWithLoan()
        {
            var customers = dbc.OnLoans
                           .Select(l => l.CustomerID)
                           .ToList();
            var customersWithLoan = dbc.Customers
                                    //if the customerID matches one customerID of the customer in loan it gets added to the list
                                    .Where(c => customers.Contains(c.CustomerID));
            return customersWithLoan.ToList();
        }

        /// <summary>
        /// Gets all loans
        /// </summary>
        /// <returns>List of loans</returns>
        public List<OnLoan> GetLoans()
        {
            var loans = dbc.OnLoans
                        .Include(l => l.Article)
                        .Include(l => l.Customer)
                        .OrderByDescending(l => l.LoanDate)
                            .Select(l => new OnLoan
                            {
                                OnLoanID = l.OnLoanID,
                                ArticleID = l.ArticleID,
                                CustomerID = l.CustomerID,
                                Article = l.Article,
                                Customer = l.Customer,
                                Returned = l.Returned,
                                LoanDate = l.LoanDate,
                                ReturnDate = l.ReturnDate,
                            });
            return loans.ToList();
        }


        /// <summary>
        /// Gets filtered loans
        /// </summary>
        /// <param name="customerID">customer to filter</param>
        /// <param name="articleID">article to filter</param>
        /// <param name="isReturned">filters the active/inactive loans</param>
        /// <returns>Filtered list of loans</returns>
        public List<OnLoan> GetLoans(int customerID, int articleID, int isReturned)
        {

            var query = dbc.OnLoans
                .Include(l => l.Article)
                .Include(l => l.Customer)
                .OrderByDescending(l => l.LoanDate) 
                .Where(l =>
                    (customerID == 0 || l.CustomerID == customerID) && //if customerID = 0 it gets all customers
                    (articleID == 0 || l.ArticleID == articleID) && //same for articleID
                    (isReturned == 2 || l.Returned == Convert.ToBoolean(isReturned))//2 because we convert the integers to bool (0,false - 1,true)
                )
                .Select(l => new OnLoan
                {
                    OnLoanID = l.OnLoanID,
                    ArticleID = l.ArticleID,
                    CustomerID = l.CustomerID,
                    Article = l.Article,
                    Customer = l.Customer,
                    Returned = l.Returned,
                    LoanDate = l.LoanDate,
                    ReturnDate = l.ReturnDate,
                });

            return query.ToList();
        }

        /// <summary>
        /// Gets a list of all the articles that are not on loan and are available
        /// used for the drop-down-menu to add new loan
        /// </summary>
        /// <returns>List of articles that can be loaned</returns>
        public List<Article> GetNotLoanedArticles()
        {
            var loanedArticles = dbc.OnLoans
                                 .Select(l => l.ArticleID)
                                 .ToList();

            var notLoanedArticles = dbc.Articles
                                    .Where(a => a.LoanStatusID == 2);

            return notLoanedArticles.ToList();
        }

        /// <summary>
        /// Gets all articles that have been loaned out
        /// </summary>
        /// <returns>List of articles</returns>
        public List<Article> GetLoanedArticles()
        {
            var loanedArticles = dbc.OnLoans
                                 .Select(l => l.ArticleID)
                                 .ToList();
            var articles = dbc.Articles
                           .Where(a => loanedArticles.Contains(a.ArticleID));

            return articles.ToList();
        }

        /// <summary>
        /// Returns the loan
        /// it sets the loan status of the article to available(2)
        /// </summary>
        /// <param name="loan">loan to return</param>
        public void ReturnLoan(OnLoan loan)
        {
            var existingState = dbc.OnLoans.Find(loan.OnLoanID);
            if (existingState != null)
            {
                dbc.Entry(existingState).State = EntityState.Detached;
                dbc.Entry(loan).State = EntityState.Modified;
            }
            var article = dbc.Articles
                          .Include(a => a.OnLoan)
                          .Where(a => a.ArticleID == loan.ArticleID)
                          .FirstOrDefault();
            if (article != null)
            {
                article.LoanStatusID = 2; //sets the loanstatus of the article to available
                dbc.Update(article);
            }
            loan.Returned = true; //sets the loan to returned
            loan.ReturnDate = DateTime.Now;
            dbc.Update(loan);
            dbc.SaveChanges();
        }
    }
}
