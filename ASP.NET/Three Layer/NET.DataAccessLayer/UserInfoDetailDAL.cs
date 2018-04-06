using System;
using NET.Architect.Model;

namespace NET.DataAccessLayer
{
    public class UserInfoDetailDAL : Base<UserInfoDetail>
    {
        public override string DbTableName
        {
            get
            {
               return "UserInfoDetail";
            }
            set
            {
                DbTableName = value;
            }
        }
    }
}
