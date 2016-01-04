using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DS
{
    class FactoryDS
    {
        public static IDAL getIDAL()
        {
            return new Dal_imp();
        }
    }
}
