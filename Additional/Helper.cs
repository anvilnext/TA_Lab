using System;
using System.Collections.Generic;
using System.Text;

namespace TA_Lab.Additional
{
    static class Helper
    {
        public static string SetLocation(int num)
        {
            return GetPath() + string.Format("\\Screenshots\\Test{0}\\Test{1}", num, num) + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";
        }

        public static string GetPath()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            for (int i = 0; i < 3; i++)
            {
                path = System.IO.Directory.GetParent(path).FullName;
            }
            return path;
        }

        public static string SetMany(int num, int k)
        {
            return GetPath() + string.Format("\\Screenshots\\Test{0}\\Test{1}", num, num) + string.Format("_{0}", k) + ".png";
        }
    }
}
