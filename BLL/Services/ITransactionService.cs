using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.Models;
using DLL.Repositories;

namespace BLL.Services
{
    public interface ITransactionService
    {
        Task FinancialTransaction();
    }

    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task FinancialTransaction()
        {
            var rand = new Random();
            var amount = rand.Next(1000);
            var transaction= new TransactionHistory
            {
            
                Amount = amount
            };
            var customer =
                await _unitOfWork.CustomerBalanceRepository.
                    FindSingleAsync(x => x.Email == "fahimforhad@gmail.com");
            customer.Balance += amount;
            await _unitOfWork.TransactionHistoryRepository.CreateAsync(transaction);
            _unitOfWork.CustomerBalanceRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
