using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerFairPlus.Interface
{
    interface UserDetailsInterface
    {
        int GetIDByUserName(string name, string role);
    }
}
