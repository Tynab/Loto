using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YANF.Script;
using static Loto.Script.Common;
using static Loto.Script.Constant;
using static System.Threading.Tasks.Task;

namespace Loto.Screen
{
    public partial class FrmMain : Form
    {
        private readonly Random _rnd = new();
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
            // trans data from ctrl
            var numHeads = new List<int>();
            _nbHeads.ForEach(x => numHeads.Add((int)x.Value));
            var numTails = new List<int>();
            _nbTails.ForEach(x => numTails.Add((int)x.Value));
            // spin
            var cpls = new List<string>();
            for (var i = 0; i < RND_SIZE; i++)
            {
                SpinWiz(numHeads, numTails, out var numHead, out var numTail);
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
                SpinWiz(numHeads, numTails, out var numHead, out var numTail);
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

        #region Methods
        // Convert list
        private List<int> CnvtList(List<int> nums)
        {
            var sum = nums.Sum();
            var rvNums = new List<int>();
            nums.ForEach(x => rvNums.Add(sum - x));
            var gcdhead = GCD(rvNums.ToArray());
            var rslts = new List<int>();
            rvNums.ForEach(x => rslts.Add(x / gcdhead));
            return rslts;
        }

        // Refresh list
        private List<int> RefreshList(List<int> nums, out int rng)
        {
            var cmpcts = CnvtList(nums);
            var rngs = new List<int>();
            rng = 0;
            foreach (var cmpct in cmpcts)
            {
                rng += cmpct;
                rngs.Add(rng);
            }
            return rngs;
        }

        // Spin access
        private void Spin(List<int> numHeads, List<int> numTails, List<int> heads, List<int> tails, int rngHead, int rngTail, out int numHead, out int numTail)
        {
            numHead = heads.IndexOf(heads.First(x => x > _rnd.Next(rngHead)));
            numHeads[numHead] = numHeads[numHead] + 1;
            numTail = tails.IndexOf(tails.First(x => x > _rnd.Next(rngTail)));
            numTails[numTail] = numTails[numTail] + 1;
        }

        // Spin wizard
        private void SpinWiz(List<int> numHeads, List<int> numTails, out int numHead, out int numTail)
        {
            var rngHead = 0;
            var rngTail = 0;
            var taskHead = Run(() => RefreshList(numHeads, out rngHead));
            var taskTail = Run(() => RefreshList(numTails, out rngTail));
            WaitAll(taskHead, taskTail);
            Spin(numHeads, numTails, taskHead.Result, taskTail.Result, rngHead, rngTail, out numHead, out numTail);
        }
        #endregion
    }
}
