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
            var transaction= new TransactionHistory()
            {
            
                Amount = amount
            };
            
            
            await _unitOfWork.TransactionHistoryRepository.CreateAsync(transaction);

            if ( await _unitOfWork.SaveChangesAsync())
            {
                await _unitOfWork.CustomerBalanceRepository.MustUpdateBalanceAsync("fahimforhad@gmail.com", amount);
            };

        }
    }
}
