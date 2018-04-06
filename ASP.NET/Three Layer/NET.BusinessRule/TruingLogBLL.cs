using NET.Architect.Model;
using NET.BusinessRule;
using NET.DataAccessLayer;

namespace NET.BusinessRule
{
    public class TruingLogBLL : BaseBLL<TruingLog>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new TruingLogDAL();
        }
    }
}
