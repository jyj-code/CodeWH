using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class B_Sys_Announcement : BaseBLL<Sys_Announcement>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new D_Sys_Announcement();
        }
    }
}
