using System;
using System.Linq;

namespace ConsoleApplication
{
    public class WanDaInfoInterView
    {
        public void InterView()
        {
            var interview = GetRange();
            GetIsPassString();
            new C().F();
        }
        public object GetRange()
        {
            var seq = Enumerable.Range(0, 9).ToList();
            return seq.Where(o => o > 5);
        }
        void GetIsPassString()
        {
            int[] tmp = new int[10];
            for (int i = 0; i < 10; i++)
            {
                tmp[i] = i;
            }
        }
    }
    public class A {
        public virtual void F() {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Module.FullyQualifiedName+ " A.F");
        }
    }
    public abstract class B : A {
        public abstract override void F();
    }
    public class C : B
    {
        public C() { }
        public override void F()
        {
          
        }
    }
}
