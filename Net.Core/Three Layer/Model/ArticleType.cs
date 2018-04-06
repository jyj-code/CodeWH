using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ArticleType
    {
        private string iD;
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private bool status;
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        private string firstID;
        public string FirstID
        {
            get { return firstID; }
            set { firstID = value; }
        }

        private int lover;
        public int Lover
        {
            get { return lover; }
            set { lover = value; }
        }

        private int sequence;
        public int Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }
        public string Link_Url { get; set; }
        public string Remark { get; set; }
    }
}
