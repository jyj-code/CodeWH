using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Sys_Config
    {
        private string iD;
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string tYPE;
        public string TYPE
        {
            get { return tYPE; }
            set { tYPE = value; }
        }

        private int sTATUS;
        public int STATUS
        {
            get { return sTATUS; }
            set { sTATUS = value; }
        }

        private string kEY;
        public string KEY
        {
            get { return kEY; }
            set { kEY = value; }
        }

        private string vALUE;
        public string VALUE
        {
            get { return vALUE; }
            set { vALUE = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private DateTime dataTime;
        public DateTime DataTime
        {
            get { return dataTime; }
            set { dataTime = value; }
        }
    }
}
