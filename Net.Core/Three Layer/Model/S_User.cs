using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class S_User
    {
        private string iD;
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }
        
        private string userAccount;
        public string UserAccount
        {
            get { return userAccount; }
            set { userAccount = value; }
        }
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private DateTime? birthday;
        public DateTime? Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        private DateTime registration_Date;
        public DateTime Registration_Date
        {
            get { return registration_Date; }
            set { registration_Date = value; }
        }

        private string role_id;
        public string Role_ID
        {
            get { return role_id; }
            set { role_id = value; }
        }

        private string nation;
        public string Nation
        {
            get { return nation; }
            set { nation = value; }
        }

        private string province;
        public string Province
        {
            get { return province; }
            set { province = value; }
        }

        private string area;
        public string Area
        {
            get { return area; }
            set { area = value; }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string county;
        public string County
        {
            get { return county; }
            set { county = value; }
        }

        private string town;
        public string Town
        {
            get { return town; }
            set { town = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string actual_name;
        public string Actual_name
        {
            get { return actual_name; }
            set { actual_name = value; }
        }

        private string state_license_number;
        public string State_license_number
        {
            get { return state_license_number; }
            set { state_license_number = value; }
        }

        private string photo;
        public string Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        private string image_Log;
        public string Image_Log
        {
            get { return image_Log; }
            set { image_Log = value; }
        }

        private string link_address;
        public string Link_address
        {
            get { return link_address; }
            set { link_address = value; }
        }
        public string Template_ID { get; set; }
    }
}
