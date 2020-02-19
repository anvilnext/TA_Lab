using System;
using System.Collections.Generic;
using System.Text;

namespace TA_Lab.Additional
{
    static class Helper
    {
        public static string SetLocation(int num)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            for (int i = 0; i < 3; i++)
            {
                path = System.IO.Directory.GetParent(path).FullName;
            }
            return path + string.Format("\\Screenshots\\Test{0}", num) + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";
        }
    }
}
