using System;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public static class Constr
    {
        private static string _path = @"{0}/../../../车牌字符模板/{1}/";
        public static string CurrentPath = AppDomain.CurrentDomain.BaseDirectory;
        public static Bitmap m_Bitmap;//测试图片
        public static Bitmap c_Bitmap;//车牌图像
        public static Bitmap objNewPic;
        public static int[] gray = new int[256];
        public static Bitmap[] charFont;
        public static Bitmap[] provinceFont;
        public static string[] charString;//数字字母存储的路径
        public static string[] provinceString;//省份存储的路径
        public static string[] charDigitalString;
        public static string[] provinceDigitalString;
        public static Bitmap[] array_Bitmap = new Bitmap[7];//最终黑白字体图片
        public static String name;
        public static int flag = 0;
        public static string charSourceBath = string.Format(_path, CurrentPath, "char");
        public static string provinceSourceBath = string.Format(_path,CurrentPath, "font");
        public static string[,] _ChildModule;
        public static int _ModuleButtonHeight = 30;
        public static int _ChildButtonHeight = 25;
        public static string[,] ChildModule
        {
            get { return _ChildModule; }
            set { _ChildModule = value; }
        }

        public static void Init()
        {
            ChildModule = new string[5, 4];
            ChildModule[0, 0] = "打开";
            ChildModule[0, 1] = "重新载入";
            ChildModule[0, 2] = "保存";
            ChildModule[0, 3] = "退出";
            ChildModule[1, 0] = "灰度化";
            ChildModule[1, 1] = "灰度均衡化";
            ChildModule[1, 2] = "均值滤波";
            ChildModule[1, 3] = "";
            ChildModule[2, 0] = "sobel边缘检测";
            ChildModule[2, 1] = "车牌定位";
            ChildModule[2, 2] = "";
            ChildModule[2, 3] = "";
            ChildModule[3, 0] = "灰度化";
            ChildModule[3, 1] = "二值化";
            ChildModule[3, 2] = "精确定位";
            ChildModule[3, 3] = "字符分割";
            ChildModule[4, 0] = "车牌识别";
            ChildModule[4, 1] = "";
            ChildModule[4, 2] = "";
            ChildModule[4, 3] = "";
        }
    }
}
