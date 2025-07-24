using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMech
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashForm());

            LoginForm lobjLogin = new LoginForm();
            do
            {
                lobjLogin.ShowDialog();
                if (lobjLogin.nStatus != 0)
                {
                    lobjLogin.bOKButtonClicked = false;
                    MasterMechUtil.sUserID = lobjLogin.sUserID;
                    MasterMechUtil.sFY = lobjLogin.sFY;
                    MainForm lobjMainForm = new MainForm(MasterMechUtil.sUserID, lobjLogin.sUserType);
                    Application.Run(lobjMainForm);
                    if (lobjMainForm.ExitApp)
                        lobjLogin.nStatus = 0;
                }
            } while (lobjLogin.nStatus != 0);
            Application.Exit();
        }
    }
}
