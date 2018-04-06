using NET.Architect.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.Architect.Model;
using NET.DataAccessLayer.Common;
using DataBase;

namespace NET.DataAccessLayer
{
    public class CustomerUserInfoDAL : IDBOperating<Architect.Model.CustomerUserInfo>
    {
        public string Sql { get; set; }

        public int Add(List<CustomerUserInfo> _entity)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Sql = @"insert into CustomerUserInfo(Id,Ip,Isp,Browser,OS,lat,lng,Status,OperationTime,IMG,Remarks,Browser_1,OS_1,modelType)values(@Id,@Ip,@Isp,@Browser,@OS,@lat,@lng,@Status,'" + date + "',@IMG,@Remarks,@Browser_1,@OS_1,@modelType)";
            return DBHelper.SaveCollection(Sql, _entity);
        }
        public int Delete(List<CustomerUserInfo> _entity)
        {
            Sql = "DELETE FROM CustomerUserInfo WHERE Id=@Id";
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

        public List<CustomerUserInfo> Find()
        {
            Sql = "select * from CustomerUserInfo";
            return DBHelper.ReadCollection<CustomerUserInfo>(Sql, null);
        }

        public List<CustomerUserInfo> Find(string Ip)
        {
            Sql = string.Format("select * from CustomerUserInfo where upper(Ip)=upper('{0}')", Ip);
            return DBHelper.ReadCollection<CustomerUserInfo>(Sql, null);
        }

        public List<CustomerUserInfo> FindID(string Id)
        {
            Sql = string.Format("select * from CustomerUserInfo where upper(Id)=upper('{0}')", Id);
            return DBHelper.ReadCollection<CustomerUserInfo>(Sql, null);
        }
        public int Update(List<CustomerUserInfo> _entity)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE CustomerUserInfo SET");
            var data = _entity[0];
            if (data.Isp != null)
            {
                str.Append(" Isp=@Isp,");
            }
            if (data.Browser != null)
            {
                str.Append(" Browser=@Browser,");
            }
            if (data.OS != null)
            {
                str.Append(" OS=@OS,");
            }
            if (data.lat != null)
            {
                str.Append(" lat=@lat,");
            }
            if (data.lng != null)
            {
                str.Append(" lng=@lng,");
            }
            if (data.Ip != null)
            {
                str.Append(" Ip=@Ip,");
            }
            if (data.IMG >= 0 && data.IMG < 12)
            {
                str.Append(" IMG=@IMG,");
            }
            if (data.Status >= 0 && data.Status < 9)
            {
                str.Append(" Status=@Status,");
            }
            if (data.InCount >= 0)
            {
                str.Append(" InCount=@InCount,");
            }
            if (data.Remarks != null)
            {
                str.Append(" Remarks=@Remarks,");
            }
            if (data.@Browser_1 != null)
            {
                str.Append(" @Browser_1=@@Browser_1,");
            }
            if (data.@modelType != null)
            {
                str.Append(" @modelType=@@modelType,");
            }
            if (data.@OS_1 != null)
            {
                str.Append(" @OS_1=@@OS_1,");
            }
         
           str.Append(string.Format(" OperationTime='{0}',", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            Sql = string.Format("{0} WHERE Id=@Id", str.ToString().Substring(0, str.ToString().Length - 1));
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
