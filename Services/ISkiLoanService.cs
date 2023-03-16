using Skiverleih.Models;

namespace Skiverleih.Services
{
    public interface ISkiLoanService
    {
        List<Customer> GetCustomers();
        List<Customer> GetCustomersWithLoan();

        List<Article> GetArticles();
        List<Article> GetArticles(int loanStatus);
        List<Article> GetNotLoanedArticles();
        List<Article> GetLoanedArticles();

        List<OnLoan> GetLoans();
        List<OnLoan> GetLoans(int customerID, int articleID, int isReturned);
        void AddLoan(OnLoan loan);
        void ReturnLoan(OnLoan loan);

    }
}
