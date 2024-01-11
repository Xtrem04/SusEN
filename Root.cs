using System;
using System.Windows.Forms;

namespace SusEN
{
    internal static class Root
    {
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
        }
    }
}
