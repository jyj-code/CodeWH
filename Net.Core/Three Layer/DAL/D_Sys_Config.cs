using System;
using System.Collections.Generic;
using Interface;
using Model;
using DatabaseOperation;

namespace DAL
{
    public class D_Sys_Config : IDBOperating<Sys_Config>
    {
        public string Sql { get; set; }

        public int Add(List<Sys_Config> _entity)
        {
            Sql = "  INSERT INTO Sys_Config(ID,[TYPE],[STATUS],[KEY],VALUE,Remark,DataTime)VALUES(@ID,@TYPE,@STATUS,@KEY,@VALUE,@Remark,@DataTime)";
            return DBHelper.SaveCollection(Sql,_entity);
        }

        public int Delete(List<Sys_Config> _entity)
        {
            return DBHelper.SaveCollection("DELETE Sys_Config WHERE ID=@ID", _entity);
        }

        public int Excute(string _sql)
        {
            return DBHelper.ExcuteSQL(_sql, null);
        }

        public string ExecuteScalarString(string _sql)
        {
            return DBHelper.ExecuteScalarString(_sql, null);
        }

        public List<Sys_Config> Find()
        {
            Sql = "SELECT ID,TYPE,STATUS,[KEY],VALUE,Remark,DataTime FROM Sys_Config";
            return DBHelper.ReadCollection<Sys_Config>(Sql, null);
        }

        public List<Sys_Config> Find(string _sql)
        {
            return DBHelper.ReadCollection<Sys_Config>(_sql, null);
        }

        public int Update(List<Sys_Config> _entity)
        {
            Sql = "UPDATE Sys_Config SET ID=@ID,[TYPE]=@TYPE,[STATUS]=@STATUS,[KEY]=@KEY,VALUE=@VALUE,Remark=@Remark,DataTime=@DataTime";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
