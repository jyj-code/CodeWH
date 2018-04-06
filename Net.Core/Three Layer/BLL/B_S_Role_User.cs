using DAL;
using Model;

namespace BLL
{
    public class B_S_Role_User : BaseBLL<S_Role_User>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new D_S_Role_User();
        }
    }
}
