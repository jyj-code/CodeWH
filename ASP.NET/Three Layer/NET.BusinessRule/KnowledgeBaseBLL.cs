using NET.Architect.Model;
using NET.DataAccessLayer;

namespace NET.BusinessRule
{
    public class KnowledgeBaseBLL : BaseBLL<KnowledgeBase>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new KnowledgeBaseDAL(); 
        }
    }

}
