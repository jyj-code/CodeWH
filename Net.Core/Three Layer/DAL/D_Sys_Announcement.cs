using DatabaseOperation;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
   public class D_Sys_Announcement : Interface.IDBOperating<Model.Sys_Announcement>
    {
        public string Sql { get; set; }

        public int Add(List<Sys_Announcement> _entity)
        {
            Sql = "INSERT INTO Sys_Announcement(ID,AnnouncementContent,date)VALUES(@ID,@AnnouncementContent,@date)";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<Sys_Announcement> _entity)
        {
            Sql = "DELETE Sys_Announcement WHERE ID=@ID";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Excute(string _sql)
        {
            return DBHelper.ExcuteSQL(_sql, null);
        }

        public string ExecuteScalarString(string _sql)
        {
            return DBHelper.ExecuteScalarString(_sql, null);
        }

        public List<Sys_Announcement> Find()
        {
            Sql = "select * from Sys_Announcement order by date desc";
            return DBHelper.ReadCollection<Sys_Announcement>(Sql, null);
        }

        public List<Sys_Announcement> Find(string _sql)
        {
            return DBHelper.ReadCollection<Sys_Announcement>(_sql, null);
        }

        public int Update(List<Sys_Announcement> _entity)
        {
            Sql = "UPDATE Sys_Announcement SET AnnouncementContent=@AnnouncementContent,date=@date WHERE ID=@ID";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
