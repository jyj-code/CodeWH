using NET.Architect.Model;
using NET.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.BusinessRule
{
    public class CustomerUserInfoBLL : BaseBLL<CustomerUserInfo>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new CustomerUserInfoDAL();
        }
        public List<CustomerUserInfo> FindID(string Id)
        {
            return new CustomerUserInfoDAL().FindID(Id);
        }
    }
}
