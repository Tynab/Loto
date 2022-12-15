using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YANF.Script;
using static Loto.Script.Common;
using static Loto.Script.Constant;

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
            btnGes.Select();
        }
        #endregion

        #region Events
        // frm shown
        private void FrmMain_Shown(object sender, EventArgs e) => this.FadeIn();

        // btn guess click
        private void BtnGes_Click(object sender, EventArgs e)
        {
            var rnd = new Random();
            // num up-down head
            var numHeads = new List<int>();
            _nbHeads.ForEach(x => numHeads.Add((int)x.Value));
            var sumHead = numHeads.Sum();
            numHeads.ForEach(x => x = sumHead - x);
            var gcdhead = GCD(numHeads.ToArray());
            numHeads.ForEach(x => x /= gcdhead);
            // num up-down tail
            var numTails = new List<int>();
            _nbTails.ForEach(x => numTails.Add((int)x.Value));
            var sumTails = numTails.Sum();
            numTails.ForEach(x => x = sumTails - x);
            var gcdTail = GCD(numTails.ToArray());
            numTails.ForEach((x) => x /= gcdTail);
            // random head size
            var heads = new List<int>();
            var rngHead = 0;
            numHeads.ForEach(x =>
            {
                rngHead += x;
                heads.Add(rngHead);
            });
            // random tail size
            var tails = new List<int>();
            var rngTail = 0;
            numTails.ForEach(x =>
            {
                rngTail += x;
                tails.Add(rngTail);
            });
            // spin
            var cpls = new List<string>();
            for (var i = 0; i < RND_SIZE; i++)
            {
                var numHead = heads.IndexOf(heads.First(x => x > rnd.Next(rngHead)));
                numHeads[numHead] = numHeads[numHead]++;
                var numTail = tails.IndexOf(tails.First(x => x > rnd.Next(rngTail)));
                numTails[numTail] = numTails[numTail]++;
                cpls.Add($"{numHead}{numTail}");
            }
            // get top
            var cpl = string.Empty;
            var topCpl = cpls.GroupBy(x => x).OrderByDescending(g => g.Count()).SelectMany(y => y).ToList();
            if (topCpl.Count > 0)
            {
                cpl = topCpl[0];
            }
            else
            {
            ChkPtGes:
                var numHead = heads.IndexOf(heads.First(x => x > rnd.Next(rngHead)));
                numHeads[numHead] = numHeads[numHead]++;
                var numTail = tails.IndexOf(tails.First(x => x > rnd.Next(rngTail)));
                numTails[numTail] = numTails[numTail]++;
                cpl = $"{numHead}{numTail}";
                if (!cpls.Contains(cpl))
                {
                    cpls.Add(cpl);
                    goto ChkPtGes;
                }
            }
            // display
            lblArDown.Visible = true;
            lblRslt.Text = cpl;
            lblNumUp.Text = $"{numHeads.IndexOf(numHeads.Min())}{numTails.IndexOf(numTails.Min())}";
        ChkPtDown:
            var cplDown = $"{numHeads.IndexOf(numHeads.Max())}{numTails.IndexOf(numTails.Max())}";
            if (cpls.Contains(cplDown))
            {
                if (numHeads.Max() > numTails.Max())
                {
                    numHeads.Remove(numHeads.Max());
                }
                else
                {
                    numTails.Remove(numTails.Max());
                }
                goto ChkPtDown;
            }
            lblNumDown.Text = cplDown;
        }
        #endregion
    }
}
