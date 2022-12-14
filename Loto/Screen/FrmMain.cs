using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YANF.Script;

namespace Loto.Screen
{
    public partial class FrmMain : Form
    {
        #region Constructors
        public FrmMain()
        {
            InitializeComponent();
            InitItems();
            // nud event
            foreach (var nud in this.GetAllObjs(typeof(NumericUpDown)))
            {
                nud.Enter += Nud_Enter;
            }
            // option
            btnRun.Select();
        }
        #endregion

        #region Events
        // frm shown
        private void FrmMain_Shown(object sender, EventArgs e) => this.FadeIn();

        // btn Run click
        private void BtnRun_Click(object sender, EventArgs e)
        {
            var rnd = new Random();
            // num up-down head
            var heads = new List<int>();
            _nbHeads.ForEach(x => heads.Add((int)x.Value));
            // num up-down tail
            var tails = new List<int>();
            _nbTails.ForEach(x => tails.Add((int)x.Value));
            // random head
            var numHeads = new List<int>();
            var sum = 0;
            heads.ForEach(x =>
            {
                sum += x;
                numHeads.Add(sum);
            });
            // random tail
            var numTails = new List<int>();
            sum = 0;
            tails.ForEach(x =>
            {
                sum += x;
                numTails.Add(sum);
            });
            // running
            var rslt = string.Empty;
            var rslts = new List<string>();
        ChkPt:
            rslt = $"{numHeads.IndexOf(numHeads.First(x => x > rnd.Next(sum)))}{numTails.IndexOf(numTails.First(x => x > rnd.Next(sum)))}";
            if (!rslts.Contains(rslt))
            {
                rslts.Add(rslt);
                goto ChkPt;
            }
            // display
            lblArDown.Visible = true;
            lblRslt.Text = rslt;
            lblNumUp.Text = $"{heads.IndexOf(heads.Min())}{tails.IndexOf(tails.Min())}";
            lblNumDown.Text = $"{heads.IndexOf(heads.Max())}{tails.IndexOf(tails.Max())}";
        }
        #endregion
    }
}
