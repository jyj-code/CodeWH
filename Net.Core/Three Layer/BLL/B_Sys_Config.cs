using DAL;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class B_Sys_Config : BaseBLL<Sys_Config>
    {
        public override void SetCurrentDAL()
        {
            base.CurrentDAL = new D_Sys_Config();
        }
        public int GetStatus(string key)
        {
            return Find().Where(n => n.KEY == key).ToList().Count <= 0 ? 0 : Find().Where(n => n.KEY == key).ToList()[0].STATUS;
        }
    }
}
