using System;

namespace ProjectBase.Core.Util
{
    public class ConvertUtil
    {
        public static string FormatSize(long byteSize)
        {
            if (byteSize == 0)
                return "0 K";

            string unit = "BKMG";
            if (byteSize >= 1024)
            {
                double size = byteSize;
                int indx = 0;
                do
                {
                    size = size / 1024.00;
                    indx++;
                } while (size >= 1024 && indx < unit.Length - 1);
                return Math.Round(size, 1).ToString() + " " + unit[indx];
            }
            else
                return byteSize.ToString() + " B";
        }

        public static long GetFileSizeValue(long byteSize)
        {
            if (byteSize == 0)
                return 0;

            string unit = "BKMG";
            if (byteSize >= 1024)
            {
                double size = byteSize;
                int indx = 0;
                do
                {
                    size = size / 1024.00;
                    indx++;
                } while (size >= 1024 && indx < unit.Length - 1);
                return (long)Math.Round(size, 1);
            }
            else
                return byteSize;
        }
    }
}