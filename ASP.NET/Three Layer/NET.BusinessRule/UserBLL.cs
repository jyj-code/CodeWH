using NET.Architect.Model;
using NET.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.BusinessRule
{
    public class UserBLL : BaseBLL<User>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new UserDAL();
        }
    }
}
