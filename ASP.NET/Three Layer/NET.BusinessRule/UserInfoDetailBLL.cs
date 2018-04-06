using NET.Architect.Model;
using NET.DataAccessLayer;

namespace NET.BusinessRule
{
    public class UserInfoDetailBLL : Base<UserInfoDetail>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new UserInfoDetailDAL();
        }
    }
}
