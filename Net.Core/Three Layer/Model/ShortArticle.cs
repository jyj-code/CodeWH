using System;

namespace Model
{

    public class ShortArticle
    {
        private string iD;
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string articleContent;
        public string ArticleContent
        {
            get { return articleContent; }
            set { articleContent = value; }
        }

        private string customerID;
        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private string articleType_ID;
        public string ArticleType_ID
        {
            get { return articleType_ID; }
            set { articleType_ID = value; }
        }

        private int getCount;
        public int GetCount
        {
            get { return getCount; }
            set { getCount = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private int readCount;
        public int ReadCount
        {
            get { return readCount; }
            set { readCount = value; }
        }

        private int thumbsUP;
        public int ThumbsUP
        {
            get { return thumbsUP; }
            set { thumbsUP = value; }
        }

        private int state;
        public int State
        {
            get { return state; }
            set { state = value; }
        }

        private DateTime createDate;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        public string Link_Url { get; set; }
        public string Template_ID { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string TagKeyWords { get; set; }
    }
}