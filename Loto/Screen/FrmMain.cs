using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YANF.Script;
using static Loto.Script.Common;
using static Loto.Script.Constant;
using static System.Math;
using static System.Threading.Tasks.Task;

namespace Loto.Screen
{
    public partial class FrmMain : Form
    {
        #region Fields
        private readonly Random _rnd = new();
        #endregion

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
            btnScan.Select();
        }
        #endregion

        #region Events
        // frm shown
        private void FrmMain_Shown(object sender, EventArgs e) => this.FadeIn();

        // Scan click
        private void BtnScan_Click(object sender, EventArgs e)
        {
            var arr = rtxData.Text.Split('\n');
            for (var i = 0; i < arr.Length; i++)
            {
                _nbHeads[i].Value = decimal.TryParse(arr[i].Split('\t')[0].Split(' ')[0], out var head) ? head : 1;
                _nbHeadCons[i].Value = decimal.TryParse(arr[i].Split('\t')[1], out var headCon) ? headCon : 0;
                _nbTails[i].Value = decimal.TryParse(arr[i].Split('\t')[4].Split(' ')[0], out var tail) ? tail : 1;
                _nbTailCons[i].Value = decimal.TryParse(arr[i].Split('\t')[5], out var tailCon) ? tailCon : 0;
            }
        }

        // btn guess click
        private void BtnGes_Click(object sender, EventArgs e)
        {
            // trans data from ctrl
            var numHeads = _nbHeads.Select(x => (int)x.Value).ToList();
            var numHeadCls = new List<int>(numHeads);
            var numTails = _nbTails.Select(x => (int)x.Value).ToList();
            var numTailCls = new List<int>(numTails);
            var numHeadCons = _nbHeadCons.Select(x => (int)x.Value).ToList();
            var numHeadConCls = new List<int>(numHeadCons);
            var numTailCons = _nbTailCons.Select(x => (int)x.Value).ToList();
            var numTailConCls = new List<int>(numTailCons);
            // spin
            var cpls = new List<string>();
            for (var i = 0; i < RND_SIZE; i++)
            {
                SpinWiz(numHeadCls, numTailCls, numHeadConCls, numTailConCls, out var numHead, out var numTail);
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
                SpinWiz(numHeadCls, numTailCls, numHeadConCls, numTailConCls, out var numHead, out var numTail);
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
            var numHeadUps = RvtList(numHeads);
            var numTailUps = RvtList(numTails);
            var numHeadConUps = RvtList(numHeadCons);
            var numTailConUps = RvtList(numTailCons);
            FusList(numHeadUps, numHeadConUps);
            FusList(numTailUps, numTailConUps);
            lblNumUp.Text = $"{numHeadUps.IndexOf(numHeadUps.Max())}{numTailUps.IndexOf(numTailUps.Max())}";
            var numHeadDowns = new List<int>(numHeads);
            var numTailDowns = new List<int>(numTails);
            var numHeadConDowns = new List<int>(numHeadCons);
            var numTailConDowns = new List<int>(numTailCons);
            FusList(numHeadDowns, numHeadConDowns);
            FusList(numTailDowns, numTailConDowns);
        ChkPtDown:
            var cplDown = $"{numHeadDowns.IndexOf(numHeadDowns.Max())}{numTailDowns.IndexOf(numTailDowns.Max())}";
            if (cpls.Contains(cplDown))
            {
                if (numHeadDowns.Max() > numTailDowns.Max())
                {
                    numHeadDowns.Remove(numHeadDowns.Max());
                }
                else
                {
                    numTailDowns.Remove(numTailDowns.Max());
                }
                if (numHeadDowns.Count == 0 || numTailDowns.Count == 0)
                {
                    BtnGes_Click(sender, new EventArgs());
                }
                else
                {
                    goto ChkPtDown;
                }
            }
            lblNumDown.Text = cplDown;
        }
        #endregion

        #region Methods
        // Revert list
        private List<int> RvtList(List<int> nums)
        {
            var sum = nums.Sum();
            var rvNums = nums.Select(x => sum - x).ToList();
            var gcd = GCD(rvNums.ToArray());
            return rvNums.Select(x => x / gcd).ToList();
        }

        // Fusion list
        private void FusList(List<int> nums, List<int> numCons)
        {
            var sumCon = numCons.Sum();
            for (var i = 0; i < nums.Count; i++)
            {
                nums[i] = (int)Ceiling((double)(nums[i] * numCons[i] / sumCon));
            }
            var gcd = GCD(nums.ToArray());
            nums = nums.Select(x => x / gcd).ToList();
        }

        // Re-new list
        private List<int> RenewList(List<int> nums, List<int> numCons, out int rng)
        {
            // add probability
            var cmpcts = RvtList(nums);
            var cmpctCons = RvtList(numCons);
            FusList(cmpcts, cmpctCons);
            // fill range
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
        private void Spin(List<int> numHeads, List<int> numTails, List<int> numHeadCons, List<int> numTailCons, List<int> heads, List<int> tails, int rngHead, int rngTail, out int numHead, out int numTail)
        {
            numHead = heads.IndexOf(heads.First(x => x > _rnd.Next(rngHead)));
            numHeads[numHead] = numHeads[numHead] + 1;
            numHeadCons[numHead] = numHeadCons[numHead] + 1;
            numTail = tails.IndexOf(tails.First(x => x > _rnd.Next(rngTail)));
            numTails[numTail] = numTails[numTail] + 1;
            numTailCons[numTail] = numTailCons[numTail] + 1;
        }

        // Spin wizard
        private void SpinWiz(List<int> numHeads, List<int> numTails, List<int> numHeadCons, List<int> numTailCons, out int numHead, out int numTail)
        {
            var rngHead = 0;
            var rngTail = 0;
            var taskHead = Run(() => RenewList(numHeads, numHeadCons, out rngHead));
            var taskTail = Run(() => RenewList(numTails, numTailCons, out rngTail));
            WaitAll(taskHead, taskTail);
            Spin(numHeads, numTails, numHeadCons, numTailCons, taskHead.Result, taskTail.Result, rngHead, rngTail, out numHead, out numTail);
        }
        #endregion
    }
}
