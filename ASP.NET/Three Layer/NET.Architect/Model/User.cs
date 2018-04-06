using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Architect.Model
{
    public class User
    {
        public static void AaA(ref int a)
        {
            for (int i = 0; i < 1000000000; i++)
            {
                a = a + i;
            }
        }
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
