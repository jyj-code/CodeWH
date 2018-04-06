using System;
using System.Threading;

namespace 线程
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayExecute.Execute();
            return;
            for (int i = 0; i < 10; i++)
            {
                Program program_A = new Program();
                Thread threadA = new Thread(program_A.ThreadMethod);
                threadA.Name = "A";

                Program program_B = new Program();
                Thread threadB = new Thread(program_B.ThreadMethod);
                threadB.Name = "B";

                Program program_C = new Program();
                Thread threadC = new Thread(program_C.ThreadMethod);
                threadC.Name = "C";

                Program program_D = new Program();
                Thread threadD = new Thread(program_D.ThreadMethod);
                threadD.Name = "D";

                threadA.Start();
                threadB.Start();
                threadC.Start();
                threadD.Start();
            }
            //StringBuilder threadInfo = new StringBuilder();
            //threadInfo.AppendLine(string.Format("状态：{0}",thread.IsAlive));
            //threadInfo.AppendLine(string.Format("名称：{0}",thread.Name));
            //threadInfo.AppendLine(string.Format("优先级：{0}",thread.Priority));
            //threadInfo.AppendLine(string.Format("状态：{0}",thread.ThreadState));
            //Console.WriteLine(threadInfo);
            Console.ReadKey();
        }
        private static object obj_A = new object();
        private readonly static object obj_B = new object();
        void ThreadMethod(object parameter)
        {
            ThreadMethod_B(parameter);
            return;
            //lock (typeof(Program))
            //lock(this)
            lock (obj_A)
            //lock (obj_B)
            {
                for (int i = 1; i < 6; i++)
                {
                    Console.WriteLine(string.Format("线程：{0} 第{1}次执行", Thread.CurrentThread.Name, i));
                    Thread.Sleep(200);
                }
                Console.WriteLine("……………………………");
            }
            //Console.WriteLine(string.Format("【{0}】开始执行：{1}", Thread.CurrentThread.Name, parameter));
        }
        void ThreadMethod_B(object parameter)
        {

            //Monitor.Enter(obj_B);
            bool IsFlage = Monitor.TryEnter(obj_B, 1000);
            if (IsFlage)
            {
                for (int i = 1; i < 6; i++)
                {
                    Console.WriteLine(string.Format("线程：{0} 第{1}次执行", Thread.CurrentThread.Name, i));
                    Thread.Sleep(200);
                }
                Console.WriteLine("……………………………");
            }
            if (IsFlage)
                Monitor.Exit(obj_B);
        }
    }

    internal class Monster
    {
        public int Blood { get; set; }
        public Monster(int boold)
        {
            this.Blood = boold;
            Console.WriteLine("         我有{0}滴血", Blood);
        }
    }
    internal class Play
    {
        public string Name { get; set; }
        public int Power { get; set; }
        public void Execute(Object monster)
        {
            Monster m = monster as Monster;
            Monitor.Enter(monster);
            while (m.Blood >= 0)
            {
                Monitor.Wait(monster);
                Console.Write("当前：{0}，法术攻击怪物  ", this.Name);
                if (m.Blood >= Power)
                {
                    Console.Write("血量：{0}，本次消耗：{1}  ", m.Blood, Power);
                    m.Blood -= Power;
                }
                else
                {
                    m.Blood = 0;
                }
                Thread.Sleep(300);
                Console.WriteLine("剩余血量：{0} ", m.Blood);
                Monitor.PulseAll(monster);
            }
            Monitor.Exit(monster);
        }
        public void SicsExecute(Object monster)
        {
            Monster m = monster as Monster;
            Monitor.Enter(monster);
            while (m.Blood > 0)
            {
                Monitor.PulseAll(monster);
                if (Monitor.Wait(monster, 1000))
                {
                    Console.Write("当前：{0}，物理攻击怪物  ", this.Name);
                    if (m.Blood >= Power)
                    {
                        Console.Write("血量：{0}，本次消耗：{1}  ", m.Blood, Power);
                        m.Blood -= Power;
                    }
                    else
                    {
                        m.Blood = 0;
                    }
                    Thread.Sleep(300);
                    Console.WriteLine("剩余血量：{0}", m.Blood);
                }
            }
            Monitor.Exit(monster);
        }
    }
    internal class PlayExecute
    {
        public static void Execute()
        {
            Monster monster = new Monster(100);
            new Thread(new Play() { Name = "张无忌", Power = new Random().Next(1, 9) }.Execute).Start(monster);
            new Thread(new Play() { Name = "孙子涵", Power = new Random().Next(1, 9) }.SicsExecute).Start(monster);
            Console.ReadKey();
        }


    }
}
