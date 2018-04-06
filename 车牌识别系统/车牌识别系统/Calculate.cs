using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Calculate
    {
        public static MainForm MainForm { get { return MainFormFunc(); } }
        public static Func<MainForm> MainFormFunc;
        /// <summary>
        /// 计算数组中的最小值索引
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int minNumber(int[] num)
        {
            int minIndex = 0;
            int minNum = 1000;
            for (int i = 0; i < num.Length; i++)
            {
                if (minNum > num[i])
                {
                    minNum = num[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }
        public static int TransformFiles(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles("*.bmp");
            int j = 0;
            if (path.Equals(Constr.charSourceBath))
            {
                Constr.charString = new string[86];
                Constr.charDigitalString = new string[86];
                try
                {
                    foreach (FileInfo f in files)
                    {
                        Constr.charString[j] = (dir + f.ToString());
                        Constr.charDigitalString[j] = Path.GetFileNameWithoutExtension(Constr.charString[j]);
                        j++;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Constr.provinceString = new string[22];
                Constr.provinceDigitalString = new string[22];

                try
                {
                    foreach (FileInfo f in files)
                    {
                        Constr.provinceString[j] = (dir + f.ToString());
                        Constr.provinceDigitalString[j] = Path.GetFileNameWithoutExtension(Constr.provinceString[j]);
                        j++;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return j;
        }
        /// <summary>
        /// 灰度直方图的调用
        /// </summary>
        public static void graydo()
        {
            switch (Constr.flag)
            {
                case 1:
                    {
                        MainForm.pictureBox2.Refresh();
                        using (Bitmap bitmap = new Bitmap(MainForm.pictureBox1.Image))
                        {
                            int[] hist = getHist(bitmap, bitmap.Width, bitmap.Height);
                            Graphics g = MainForm.pictureBox2.CreateGraphics();
                            g.Clear(MainForm.BackColor);
                            Pen pen = new Pen(Color.Red);
                            int max = hist[0];
                            for (int i = 0; i < bitmap.Width; i++)
                                if (max < hist[i])
                                    max = hist[i];
                            for (int i = 0; i < bitmap.Width; i++)
                                hist[i] = hist[i] * 250 / max;
                            pen.Color = Color.Red;
                            for (int i = 0; i < bitmap.Width; i++)
                                g.DrawLine(pen, i, 255, i, 255 - hist[i]);
                            break;
                        }
                    }

                case 2:
                    {
                        MainForm.pictureBox2.Refresh();
                        using (Bitmap bitmap = new Bitmap(MainForm.pictureBox3.Image))
                        {
                            int[] hist = getHist(bitmap, bitmap.Width, bitmap.Height);
                            Graphics g = MainForm.pictureBox2.CreateGraphics();
                            g.Clear(MainForm.BackColor);
                            Pen pen = new Pen(Color.Red);
                            int max = hist[0];
                            for (int i = 0; i < bitmap.Width; i++)
                                if (max < hist[i])
                                    max = hist[i];
                            for (int i = 0; i < bitmap.Width; i++)
                                hist[i] = hist[i] * 250 / max;
                            pen.Color = Color.Red;
                            for (int i = 0; i < bitmap.Width; i++)
                                g.DrawLine(pen, i, 255, i, 255 - hist[i]);
                            break;
                        }
                    }
                default:
                    break;
            }
        }
        public static int[] getHist(Bitmap bm, int w, int h)
        {
            int[] hist = new int[500];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    int grey = (bm.GetPixel(i, j)).R;
                    hist[grey]++;
                }
            }
            return hist;
        }
        public static int[,] edgeDetect(int[,] ing, int[,] tmp, int w, int h)
        {
            int[,] edge = new int[w, h];
            for (int j = 1; j < h - 1; j++)
            {
                for (int i = 1; i < w - 1; i++)
                {
                    edge[i, j] = Math.Abs(tmp[0, 0] * ing[i - 1, j - 1] +
                        tmp[0, 1] * ing[i - 1, j] +
                        tmp[0, 2] * ing[i - 1, j + 1] +
                        tmp[1, 0] * ing[i, j - 1] +
                        tmp[1, 1] * ing[i, j] +
                        tmp[1, 2] * ing[i, j + 1] +
                        tmp[2, 0] * ing[i + 1, j - 1] +
                        tmp[2, 1] * ing[i + 1, j] +
                        tmp[2, 2] * ing[i + 1, j + 1]);
                }
            }
            return edge;
        }
        /// <summary>
        /// 计算数组中的最大值索引
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int maxNumber(int[] num)
        {
            int maxIndex = 0;
            int maxNum = 0;
            for (int i = 0; i < num.Length; i++)
            {
                if (maxNum < num[i])
                {
                    maxNum = num[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
        public static void SetImage(Bitmap curBitmap, PictureBox image, int index, int x, int y, int width, int height)
        {
            Rectangle rectangle = new Rectangle(x, y, width <= 0 ? 10 : width, height);
            Constr.array_Bitmap[index] = curBitmap.Clone(rectangle, PixelFormat.DontCare);
            image.Image = Constr.array_Bitmap[index];
            Constr.objNewPic = new Bitmap(Constr.array_Bitmap[index], 9, 16);
            Constr.array_Bitmap[index] = Constr.objNewPic;
            Constr.objNewPic.Save(string.Format("{0}\\{1}.bmp", Constr.CurrentPath, System.Guid.NewGuid().ToString("N")));
            Constr.objNewPic = null;
        }
    }
}
