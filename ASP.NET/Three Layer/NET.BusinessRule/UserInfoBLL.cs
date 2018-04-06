using NET.Architect.Model;
using NET.DataAccessLayer;

namespace NET.BusinessRule
{
    public class UserInfoBLL : Base<UserInfo>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new UserInfoDAL();
        }
    }
}
