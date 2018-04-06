using DAL;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{

    public class B_S_User : BaseBLL<S_User>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new D_S_User();
        }
        public S_User GetUserObj(string id)
        {
            return new D_S_User().GetUserObj(id);
        }
    }
}
