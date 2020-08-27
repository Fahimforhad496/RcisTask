using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.DatabaseContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface ICustomerBalanceRepository : IRepositoryBase<CustomerBalance>
    {
        
    }

    public class CustomerBalanceRepository : RepositoryBase<CustomerBalance>,ICustomerBalanceRepository
    {
        public CustomerBalanceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
