using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Comment
    {
        private string iD;
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string articleID;
        public string ArticleID
        {
            get { return articleID; }
            set { articleID = value; }
        }

        private string commentContent;
        public string CommentContent
        {
            get { return commentContent; }
            set { commentContent = value; }
        }

        private string customerID;
        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }
}
