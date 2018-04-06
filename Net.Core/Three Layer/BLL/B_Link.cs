using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using DAL;
using Interface;

namespace BLL
{
    public class B_Link :BaseBLL<Link>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new D_Link();
        }
    }
}
