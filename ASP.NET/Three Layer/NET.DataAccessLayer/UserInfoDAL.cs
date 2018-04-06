using System;
using NET.Architect.Interface;
using NET.Architect.Model;

namespace NET.DataAccessLayer
{
    public class UserInfoDAL: Base<UserInfo>
    {
        public override string DbTableName
        {
            get
            {
                return "UserInfo";
            }
            set
            {
                DbTableName = value;
            }
        }
    }
}
