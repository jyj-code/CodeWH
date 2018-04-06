using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Calculate.MainFormFunc = (() => { return this; });
        }
        private void clearAllPanel()
        {
            Constr.c_Bitmap = null;
            this.ResultLabel.Text = "";
            this.pictureBox1.Invalidate();
            this.pictureBox2.Invalidate();
            this.pictureBox3.Invalidate();
            this.pictureBox4.Invalidate();
            this.pictureBox5.Invalidate();
            this.pictureBox6.Invalidate();
            this.pictureBox7.Invalidate();
            this.pictureBox8.Invalidate();
            this.pictureBox9.Invalidate();
            this.pictureBox10.Invalidate();
            this.pictureBox11.Invalidate();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Constr.Init();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.button_Click(sender, e);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.button_Click(sender, e);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.button_Click(sender, e);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.button_Click(sender, e);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.button_Click(sender, e);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.button_Click(sender, e);
        }
        private void button_Click(object sender, EventArgs e)
        {
            //标志是否找到用户点击的Button
            bool findOutStatus = false;

            //清除上一次操作加载的子菜单项
            for (int i = 0; i < this.ControlPanel.Controls.Count; i++)
            {
                if (this.ControlPanel.Controls[i].GetType().Name == "Panel")
                {
                    this.ControlPanel.Controls.RemoveAt(i);
                }
            }

            for (int i = 0; i < this.ControlPanel.Controls.Count; i++)
            {
                if (this.ControlPanel.Controls[i].GetType().Name == "Button")
                {
                    //重新定义各个button位置
                    if (!findOutStatus)
                        this.ControlPanel.Controls[i].Top = Constr._ModuleButtonHeight * i;
                    else
                        this.ControlPanel.Controls[i].Top = this.ControlPanel.Height - (Constr._ModuleButtonHeight * (5 - i));

                    //找到所点击的Button,在其下加载子菜单
                    if (this.ControlPanel.Controls[i].Name == ((Button)sender).Name)
                    {
                        findOutStatus = true;
                        Panel panel = new Panel();
                        panel.BackColor = Color.AliceBlue;
                        panel.Top = Constr._ModuleButtonHeight * (i + 1);
                        panel.Width = this.ControlPanel.Width;
                        panel.Height = this.ControlPanel.Height - Constr._ModuleButtonHeight * 5;
                        this.ControlPanel.Controls.Add(panel);

                        for (int j = 0; j < Constr.ChildModule.Length / 5; j++)
                        {
                            if (!string.IsNullOrEmpty(Constr.ChildModule[i, j]))
                            {
                                Button btn = new Button();
                                btn.FlatStyle = FlatStyle.Flat;
                                btn.Top = Constr._ChildButtonHeight * j;
                                btn.Width = this.ControlPanel.Width - 50;
                                btn.Left = 50;
                                btn.Height = Constr._ChildButtonHeight;
                                btn.Name = string.Format("ChildButton{0}_{1}", i.ToString(), j.ToString());
                                btn.Text = Constr.ChildModule[i, j];
                                btn.BackColor = Color.CornflowerBlue;
                                if (i == 0)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            btn.Click += new EventHandler(文件_打开_Click); break;
                                        case 1:
                                            btn.Click += new EventHandler(文件_重新载入_Click); break;
                                        case 2:
                                            btn.Click += new EventHandler(文件_保存_Click); break;
                                        case 3:
                                            btn.Click += new EventHandler(文件_退出_Click);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (i == 1)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            btn.Click += new EventHandler(t灰度化_Click); break;
                                        case 1:
                                            btn.Click += new EventHandler(t灰度均衡化_Click); break;
                                        case 2:
                                            btn.Click += new EventHandler(t均值滤波_Click); break;
                                        case 3:
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (i == 2)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            btn.Click += new EventHandler(sobel边缘检测_Click); break;
                                        case 1:
                                            btn.Click += new EventHandler(车牌定位_Click); break;
                                        case 2:
                                            break;
                                        case 3:
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (i == 3)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            btn.Click += new EventHandler(c灰度化_Click); break;
                                        case 1:
                                            btn.Click += new EventHandler(c二值化_Click); break;
                                        case 2:
                                            btn.Click += new EventHandler(c精确定位_Click); break;
                                        case 3:
                                            btn.Click += new EventHandler(c字符分割_Click); break;
                                        default:
                                            break;
                                    }
                                }
                                if (i == 4)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            btn.Click += new EventHandler(车牌识别_Click); break;
                                        case 1:
                                            break;
                                        case 2:
                                            break;
                                        case 3:
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                panel.Controls.Add(btn);
                            }
                        }
                    }
                }
            }
        }
        //文件按钮事件
        private void 文件_打开_Click(object sender, EventArgs e)
        {
            this.clearAllPanel();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Jpeg文件(*.jpg)|*.jpg|Bitmap文件(*.bmp)|*.bmp| 所有合适文件(*.bmp/*.jpg)|*.bmp/*.jpg";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;//该值指示对话框在关闭前是否还原当前目录

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Constr.name = openFileDialog.FileName;
                Constr.m_Bitmap = (Bitmap)Image.FromFile(Constr.name);
                //this.always_Bitmap = m_Bitmap.Clone(new Rectangle(0, 0, m_Bitmap.Width, m_Bitmap.Height), PixelFormat.DontCare);
                pictureBox1.Image = Constr.m_Bitmap;
            }
            openFileDialog.Dispose();
            Constr.flag = 1;
            Calculate.graydo();
        }
        private void 文件_重新载入_Click(object sender, EventArgs e)
        {
            if (Constr.name != null)
            {
                Constr.m_Bitmap = (Bitmap)Bitmap.FromFile(Constr.name, false);
                pictureBox1.Image = Constr.m_Bitmap;
            }
            Calculate.graydo();
        }
        private void 文件_保存_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bitmap文件(*.bmp)|*.bmp| Jpeg文件(*.jpg)|*.jpg| 所有合适文件(*.bmp/*.jpg)|*.bmp/*.jpg";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                pictureBox1.Image.Save(fileName);
            }
        }
        private void 文件_退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //车辆图片处理事件
        private void t灰度化_Click(object sender, EventArgs e)
        {
            if (Constr.m_Bitmap != null)
            {
                Bitmap bitmap = new Bitmap(pictureBox1.Image);
                Color curColor;
                int ret;
                for (int i = 0; i < Constr.m_Bitmap.Width; i++)
                {
                    for (int j = 0; j < Constr.m_Bitmap.Height; j++)
                    {
                        curColor = Constr.m_Bitmap.GetPixel(i, j);
                        ret = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                        bitmap.SetPixel(i, j, Color.FromArgb(ret, ret, ret));
                    }
                }
                pictureBox1.Image = bitmap;
                Invalidate();
            }
            Constr.flag = 1;
            Calculate.graydo();
        }
        private void t灰度均衡化_Click(object sender, EventArgs e)
        {
            Bitmap curBitmap = (Bitmap)pictureBox1.Image.Clone();
            if (curBitmap != null)
            {
                Bitmap bitmap = new Bitmap(pictureBox1.Image);
                int[] hist = Calculate.getHist(curBitmap, curBitmap.Width, curBitmap.Height);
                Color color = new Color();
                double p = (double)255 / (curBitmap.Width * curBitmap.Height);
                double[] sum = new double[256];
                int[] outg = new int[256];
                sum[0] = hist[0];
                for (int i = 1; i < 256; i++)
                    sum[i] = sum[i - 1] + hist[i];
                for (int i = 0; i < 256; i++)
                    outg[i] = (int)(p * sum[i]);
                for (int j = 0; j < curBitmap.Height; j++)
                {
                    for (int i = 0; i < curBitmap.Width; i++)
                    {
                        int g = (curBitmap.GetPixel(i, j).R);
                        color = Color.FromArgb(outg[g], outg[g], outg[g]);
                        bitmap.SetPixel(i, j, color);
                    }
                }
                pictureBox1.Image = bitmap;
            }
            Constr.flag = 1;
            Calculate.graydo();
        }
        private void t均值滤波_Click(object sender, EventArgs e)
        {
            Bitmap curBitmap = (Bitmap)pictureBox1.Image.Clone();
            if (curBitmap != null)
            {
                Bitmap bitmap = new Bitmap(pictureBox1.Image);
                for (int j = 0; j < curBitmap.Height; j++)
                {
                    for (int i = 0; i < curBitmap.Width; i++)
                    {
                        int sum = 0;
                        for (int k = -1; k <= 1; k++)
                            for (int m = -1; m <= 1; m++)
                                if ((i + k) > 0 && (i + k) < curBitmap.Width && (j + m) > 0 && (j + m) < curBitmap.Height)
                                    sum += (curBitmap.GetPixel(i + k, j + m)).R;
                        int avrage = (int)(sum / 9.0);
                        bitmap.SetPixel(i, j, Color.FromArgb(avrage, avrage, avrage));
                    }
                }
                pictureBox1.Image = bitmap;
            }
            Constr.flag = 1;
            Calculate.graydo();
        }
        //定位处理事件
        private void sobel边缘检测_Click(object sender, EventArgs e)
        {
            if (Constr.m_Bitmap != null)
            {
                Bitmap bitmap = new Bitmap(pictureBox1.Image);
                Color color = new Color();
                int r;
                int w = Constr.m_Bitmap.Width;
                int h = Constr.m_Bitmap.Height;
                int[,] inred = new int[w, h];
                int[,] ingreen = new int[w, h];
                int[,] inblue = new int[w, h];
                int[,] ingray = new int[w, h];
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        color = Constr.m_Bitmap.GetPixel(i, j);
                        inred[i, j] = color.R;
                        ingreen[i, j] = color.G;
                        inblue[i, j] = color.B;
                        ingray[i, j] = (int)((color.R + color.G + color.B) / 3.0);
                    }
                }
                int[,] sobel1 = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
                int[,] sobel2 = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
                int[,] edge1 = Calculate.edgeDetect(ingray, sobel1, w, h);
                int[,] edge2 = Calculate.edgeDetect(ingray, sobel2, w, h);
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        if (Math.Max(edge1[i, j], edge2[i, j]) > 200)
                            r = 255;
                        else
                            r = 0;
                        color = Color.FromArgb(r, r, r);
                        bitmap.SetPixel(i, j, color);
                    }
                }
                pictureBox1.Image = bitmap;
            }
            Constr.flag = 1;
            Calculate.graydo();
        }
        private void 车牌定位_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image.Clone();
            int height = bitmap.Height;
            int width = bitmap.Width;
            //定义上下左右边界
            int up = 0, down = 0, right = 0, left = 0;
            //定义车牌的高和宽
            int h, w;
            int[] array = new int[height];
            Color color1 = new Color();
            Color color2 = new Color();
            int number = 0, m = 0;
            if (bitmap != null)
            {
                //逐行自下而上扫描像素0、1跳变数
                for (int i = height; i > 0; i--)
                {
                    for (int j = 0; j < width - 1; j++)
                    {
                        color1 = bitmap.GetPixel(j, i - 1);
                        color2 = bitmap.GetPixel(j + 1, i - 1);
                        if (Math.Abs(color1.R - color2.R) > 200)
                            array[i - 1]++;
                    }
                }
                //水平定位
                for (int i = height - 1; i > 0; i--)
                {
                    if (array[i] > 16)
                    {
                        if (m == 1)
                            number++;
                        if (m == 0)
                        {
                            m = 1;
                            number++;
                        }
                    }
                    if (array[i] <= 16)
                    {
                        if (m == 1)
                        {
                            m = 0;
                            if (number > 15)
                            {
                                up = i;
                                down = i + number + 3;
                            }
                        }
                    }
                    if (up != 0)
                        break;
                }

                //垂直定位
                h = down - up;
                w = (int)(3.8 * h);
                int[] arraylist = new int[width - w];
                for (int i = 0; i < width - w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        for (int k = 0; k < w - 1; k++)
                        {
                            color1 = bitmap.GetPixel(k + i, j + up);
                            color2 = bitmap.GetPixel(k + i + 1, j + up);
                            if (Math.Abs(color1.R - color2.R) > 200)
                                arraylist[i]++;
                        }
                    }
                }
                int max = Calculate.maxNumber(arraylist);
                left = max;
                right = max + w;
                Rectangle sourceRectangle = new Rectangle(left, up, w, h);
                Constr.c_Bitmap = Constr.m_Bitmap.Clone(sourceRectangle,
                    PixelFormat.DontCare);
                pictureBox3.Image = Constr.c_Bitmap;
                Graphics g = pictureBox1.CreateGraphics();
                Pen pen = new Pen(Color.Red);
                g.DrawImage(bitmap, 0, 0, Constr.m_Bitmap.Width, Constr.m_Bitmap.Height);
                g.DrawLine(pen, left, up, right, up);
                g.DrawLine(pen, left, down, right, down);
                g.DrawLine(pen, left, up, left, down);
                g.DrawLine(pen, right, up, right, down);
                Constr.flag = 2;
                Calculate.graydo();
            }
        }
        //车牌处理事件
        private void c灰度化_Click(object sender, EventArgs e)
        {
            Bitmap curBitmap = (Bitmap)pictureBox3.Image.Clone();
            if (curBitmap != null)
            {
                Color curColor;
                int ret;
                for (int i = 0; i < curBitmap.Width; i++)
                {
                    for (int j = 0; j < curBitmap.Height; j++)
                    {
                        curColor = curBitmap.GetPixel(i, j);
                        ret = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                        curBitmap.SetPixel(i, j, Color.FromArgb(ret, ret, ret));
                    }
                }
                pictureBox3.Image = curBitmap;
                Invalidate();
            }
            Constr.flag = 2;
            Calculate.graydo();
        }
        private void c二值化_Click(object sender, EventArgs e)
        {
            Bitmap curBitmap = (Bitmap)pictureBox3.Image.Clone();
            if (curBitmap != null)
            {
                for (int j = 1; j < curBitmap.Height - 1; j++)
                {
                    for (int i = 1; i < curBitmap.Width - 1; i++)
                    {
                        int r;
                        if (curBitmap.GetPixel(i, j).R > 130)
                            r = 255;
                        else
                            r = 0;
                        curBitmap.SetPixel(i, j, Color.FromArgb(r, r, r));
                    }
                }
                pictureBox3.Image = curBitmap;
            }
            Constr.flag = 2;
            Calculate.graydo();
        }
        private void c精确定位_Click(object sender, EventArgs e)
        {
            if (Constr.c_Bitmap != null)
            {
                Bitmap bitmap = new Bitmap(pictureBox3.Image);
                Color color = new Color();
                int r;
                int w = Constr.c_Bitmap.Width;
                int h = Constr.c_Bitmap.Height;
                int[,] inred = new int[w, h];
                int[,] ingreen = new int[w, h];
                int[,] inblue = new int[w, h];
                int[,] ingray = new int[w, h];
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        color = Constr.c_Bitmap.GetPixel(i, j);
                        inred[i, j] = color.R;
                        ingreen[i, j] = color.G;
                        inblue[i, j] = color.B;
                        ingray[i, j] = (int)((color.R + color.G + color.B) / 3.0);
                    }
                }
                int[,] sobel1 = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
                int[,] sobel2 = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
                int[,] edge1 = Calculate.edgeDetect(ingray, sobel1, w, h);
                int[,] edge2 = Calculate.edgeDetect(ingray, sobel2, w, h);
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        if (Math.Max(edge1[i, j], edge2[i, j]) > 200)
                            r = 255;
                        else
                            r = 0;
                        color = Color.FromArgb(r, r, r);
                        bitmap.SetPixel(i, j, color);
                    }
                }

                int cwidth = bitmap.Width;
                int cheight = bitmap.Height;
                int Height = cheight, Bottom = 0;
                int Lleft = 0, Lright = 0;
                int[] countx = new int[cheight];
                int[] county = new int[cwidth];
                //水平扫描像素值
                for (int j = 0; j < cheight; j++)
                {
                    for (int i = 0; i < cwidth - 1; i++)
                    {
                        color = bitmap.GetPixel(i, j);
                        if (color.R > 200)
                            countx[j]++;
                    }
                }

                //计算车牌号的上边缘
                for (int y = cheight / 2; y > 0; y--)
                {
                    if (countx[y] >= 30 && countx[(y + 1) % cheight] >= 28)
                        if (Height > y)
                            Height = y;
                }

                //计算车牌号的下边缘
                for (int y = cheight / 2; y < cheight; y++)
                {
                    if (countx[y] >= 30 && countx[(y + 1) % cheight] >= 28)
                        if (Bottom < y)
                            Bottom = y;
                }

                //垂直扫描像素值
                for (int i = 0; i < cwidth; i++)
                {
                    for (int j = 0; j < cheight - 1; j++)
                    {
                        color = bitmap.GetPixel(i, j);
                        if (color.R == 255)
                            county[i]++;
                    }
                }

                //计算车牌号的左边缘
                for (int y = 0; y < cwidth; y++)
                {
                    if (county[y] > 12)
                    {
                        Lleft = y + 2;
                        break;
                    }
                }
                //计算车牌号的右边缘
                for (int y = cwidth - 1; y > 0; y--)
                {
                    if (county[y] > 12)
                    {
                        Lright = y;
                        break;
                    }
                }
                Rectangle sourceRectangle = new Rectangle(Lleft, Height, Lright - Lleft, Bottom - Height);
                using (Bitmap curBitmap = (Bitmap)pictureBox3.Image.Clone())
                {
                    pictureBox4.Image = curBitmap.Clone(sourceRectangle, curBitmap.PixelFormat);
                }
            }
        }
        private void c字符分割_Click(object sender, EventArgs e)
        {
            using (Bitmap curBitmap = (Bitmap)pictureBox4.Image.Clone())
            {
                if (curBitmap != null)
                {
                    int cwidth = curBitmap.Width;
                    int cheight = curBitmap.Height;
                    Color color = new Color();
                    int[] county = new int[cwidth];
                    int[] array = new int[50];
                    int flag2 = 0;
                    int n = 0;
                    for (int i = 0; i < cwidth; i++)
                    {
                        for (int j = 0; j < cheight - 1; j++)
                        {
                            color = curBitmap.GetPixel(i, j);
                            if (color.R == 255)
                                county[i]++;
                        }
                    }
                    for (int i = 1; i < cwidth; i++)
                    {
                        if (county[i] > 2)
                        {
                            if (flag2 == 0)
                            {
                                array[n] = i;
                                n++;
                                flag2 = 1;
                            }
                        }
                        else
                        {
                            if (flag2 == 1)
                            {
                                array[n] = i;
                                n++;
                                flag2 = 0;
                            }
                        }
                    }
                    Graphics g = pictureBox4.CreateGraphics();
                    Pen pen = new Pen(Color.Red);
                    g.DrawImage(curBitmap, 0, 0, curBitmap.Width, curBitmap.Height);

                    for (int i = 0; i < 13; i++)
                    {
                        g.DrawLine(pen, array[i], 0, array[i], cheight);
                    }
                    Calculate.SetImage(curBitmap, pictureBox5, 0, array[0], 0, array[1] - array[0], cheight);
                    Calculate.SetImage(curBitmap, pictureBox6, 1, array[2], 0, array[3] - array[2], cheight);
                    Calculate.SetImage(curBitmap, pictureBox7, 2, array[4], 0, array[5] - array[4], cheight);
                    Calculate.SetImage(curBitmap, pictureBox8, 3, array[6], 0, array[7] - array[6], cheight);
                    Calculate.SetImage(curBitmap, pictureBox9, 4, array[8], 0, array[9] - array[8], cheight);
                    Calculate.SetImage(curBitmap, pictureBox10, 5, array[10], 0, array[11] - array[10], cheight);
                    Calculate.SetImage(curBitmap, pictureBox11, 6, array[12], 0, array[13] - array[12], cheight);
                }
            }
        }
        //车牌识别事件
        private void 车牌识别_Click(object sender, EventArgs e)
        {
            int charBmpCount = Calculate.TransformFiles(Constr.charSourceBath);//字母数字资源库中bitmap文件个数
            int provinceBmpCount = Calculate.TransformFiles(Constr.provinceSourceBath);//省份资源库中bitmap文件个数
            int[] charMatch = new int[charBmpCount];//存储当前图片和资源库中图片比对后所得的像素不同的个数
            int[] provinceMatch = new int[provinceBmpCount];

            Constr.charFont = new Bitmap[charBmpCount];//存储字母数字bitmap文件
            Constr.provinceFont = new Bitmap[provinceBmpCount];//存储省份bitmap文件
            for (int i = 0; i < charBmpCount; i++)
            {
                charMatch[i] = 0;
            }
            for (int i = 0; i < provinceBmpCount; i++)
            {
                provinceMatch[i] = 0;
            }
            for (int i = 0; i < charBmpCount; i++)
            {
                Constr.charFont[i] = (Bitmap)Bitmap.FromFile(Constr.charString[i], false);//charString存储的是路径
            }
            for (int i = 0; i < provinceBmpCount; i++)
            {
                Constr.provinceFont[i] = (Bitmap)Bitmap.FromFile(Constr.provinceString[i], false);
            }

            int matchIndex = 0;//最终匹配索引
            string[] digitalFont = new string[7];

            if (Constr.array_Bitmap[0] != null)
            {
                int nWidth = Constr.array_Bitmap[0].Width;
                int nHeight = Constr.array_Bitmap[0].Height;
                for (int i = 0; i < provinceBmpCount; i++)
                {
                    for (int y = 0; y < nHeight; ++y)
                    {
                        for (int x = 0; x < nWidth; ++x)
                        {
                            if ((Constr.array_Bitmap[0].GetPixel(x, y).R - Constr.provinceFont[i].GetPixel(x, y).R) != 0)
                                provinceMatch[i]++;
                        }
                    }
                }
                matchIndex = Calculate.minNumber(provinceMatch);
                digitalFont[0] = Constr.provinceDigitalString[matchIndex].Substring(0, 1);
            }

            if (Constr.array_Bitmap[1] != null && Constr.array_Bitmap[2] != null && Constr.array_Bitmap[3] != null && Constr.array_Bitmap[4] != null && Constr.array_Bitmap[5] != null && Constr.array_Bitmap[6] != null)
            {
                for (int j = 1; j < 7; j++)
                {
                    int nWidth = Constr.array_Bitmap[j].Width;
                    int nHeight = Constr.array_Bitmap[j].Height;
                    for (int i = 0; i < charBmpCount; i++)
                    {
                        charMatch[i] = 0;
                    }
                    for (int i = 0; i < charBmpCount; i++)
                    {
                        for (int y = 0; y < nHeight; ++y)
                        {
                            for (int x = 0; x < nWidth; ++x)
                            {
                                if ((Constr.array_Bitmap[j].GetPixel(x, y).R - Constr.charFont[i].GetPixel(x, y).R) != 0)
                                    charMatch[i]++;
                            }
                        }
                    }
                    matchIndex = Calculate.minNumber(charMatch);
                    digitalFont[j] = Constr.charDigitalString[matchIndex].Substring(0, 1);
                }
            }
            this.ResultLabel.Text = "" + digitalFont[0] + digitalFont[1] + digitalFont[2] + digitalFont[3] + digitalFont[4] + digitalFont[5] + digitalFont[6];
        }
    }
}