using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Login
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmLogin fl = new FrmLogin();
           if (fl.ShowDialog()== DialogResult.OK)
            {
                    Application.Run(new FrmMain());
            }

        }
    }
}
