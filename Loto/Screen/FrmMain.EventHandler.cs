using System;
using System.Windows.Forms;

namespace Loto.Screen
{
    public partial class FrmMain
    {
        #region Nud
        // nud enter
        internal static void Nud_Enter(object sender, EventArgs e) => ((NumericUpDown)sender).ResetText();
        #endregion
    }
}
