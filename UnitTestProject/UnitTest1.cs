using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.Architect.Model;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //HomeController control = new HomeController();
            int a=0;
            User.AaA(ref a);
            Assert.IsTrue(a== 1243309312);

        }
    }
}
