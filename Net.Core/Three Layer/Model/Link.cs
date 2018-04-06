using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Link
    {
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string nAME;
        public string NAME
        {
            get { return nAME; }
            set { nAME = value; }
        }

        private string uRL;
        public string URL
        {
            get { return uRL; }
            set { uRL = value; }
        }
        private int sort;
        public int SORT
        {
            get { return sort; }
            set { sort = value; }
        }
        private string tYPES;
        public string TYPES
        {
            get { return tYPES; }
            set { tYPES = value; }
        }
        
        private string rEMARK;
        public string REMARK
        {
            get { return rEMARK; }
            set { rEMARK = value; }
        }
    }
}
