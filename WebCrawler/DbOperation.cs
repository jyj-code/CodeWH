using DataBase;
using System;

namespace WebCrawler
{
    public class DbOperation
    {
        public int Add(DataReceivedEventArgs _entity)
        {
            string Sql = @"insert into crawler(Id,Url ,Html,Depth,CrawlingTime)values(@Id,@Url ,@Html,@Depth,@CrawlingTime)";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
