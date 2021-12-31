using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrL
{
    public class Account
    {
        internal string password { get; set; }
        internal string UserName { get; set; }
        internal Account(string UserName, string password) { this.UserName = UserName; this.password = password; }
    }
}
