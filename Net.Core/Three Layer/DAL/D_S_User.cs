using DatabaseOperation;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class D_S_User : IDBOperating<S_User>
    {
        public string Sql { get; set; }
        public int Add(List<S_User> _entity)
        {
            Sql = @"INSERT INTO S_User(ID,UserAccount,UserName,Password,Gender,Birthday,Registration_Date,Role_ID,Nation,Province,Area,City,County,Town,Address,Actual_name,State_license_number,Photo,Image_Log,link_address,Status,Role_ID,Template)VALUES(@ID,@UserAccount,@UserName,@Password,@Gender,@Birthday,@Registration_Date,@Role_ID,@Nation,@Province,@Area,@City,@County,@Town,@Address,@Actual_name,@State_license_number,@Photo,@Image_Log,@link_address,@Status,@Role_ID,@Template)";
            return DBHelper.SaveCollection(Sql, _entity);
        }

        public int Delete(List<S_User> _entity)
        {
            return DBHelper.SaveCollection("DELETE S_User WHERE ID=@ID", _entity);
        }

        public int Excute(string _sql)
        {
            return DBHelper.ExcuteSQL(_sql, null);
        }

        public string ExecuteScalarString(string _sql)
        {
            return DBHelper.ExecuteScalarString(_sql, null);
        }

        public List<S_User> Find()
        {
            return DBHelper.ReadCollection<S_User>("select * from S_User", null);
        }
        public S_User GetUserObj(string id)
        {
            Sql = @"select * from (SELECT u.ID,UserAccount,UserName,Password,Status,Gender,Registration_Date,Birthday,r.Name as Role_ID,Nation,Province,Area,City,County,Town,Address,Actual_name,State_license_number,Photo,Image_Log,link_address,Template_ID 
                            FROM S_User u 
                            left join S_Role r on u.Role_ID=r.ID) as S_User where S_User.UserAccount='" + id+"'";
            var data = DBHelper.ReadCollection<S_User>(Sql, null);
            return data == null|| data.Count<=0 ? null : data.First();
        }
        public List<S_User> Find(string _sql)
        {
            return DBHelper.ReadCollection<S_User>(_sql, null);
        }

        public int Update(List<S_User> _entity)
        {
            Sql = @" UPDATE S_User SET UserName = @UserName, Password = @Password, Gender = @Gender, Birthday = @Birthday, Registration_Date = @Registration_Date, Role_ID = @Role_ID, Nation = @Nation, Province = @Province, Area = @Area, City = @City, County = @County, Town = @Town, Address = @Address, Actual_name = @Actual_name, State_license_number = @State_license_number, Photo = @Photo, Image_Log = @Image_Log, link_address = @link_address, Status = @Status,Role_ID=@Role_ID WHERE ID = @ID and UserAccount = @UserAccount";
            return DBHelper.SaveCollection(Sql, _entity);
        }
    }
}
