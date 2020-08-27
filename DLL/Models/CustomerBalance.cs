using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class CustomerBalance
    {
        public long CustomerBalanceId { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }

    }
}
