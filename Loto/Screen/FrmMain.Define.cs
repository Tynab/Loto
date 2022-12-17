using System.Collections.Generic;
using YANF.Control;

namespace Loto.Screen
{
    public partial class FrmMain
    {
        private void InitItems()
        {
            InitNbHeads();
            InitNbTails();
            InitNbHeadCons();
            InitNbTailCons();
        }

        #region Nb
        private List<YANNb> _nbHeads;
        // Initialize list nbHead
        private void InitNbHeads() => _nbHeads = new List<YANNb>
            {
                nb100,
                nb101,
                nb102,
                nb103,
                nb104,
                nb105,
                nb106,
                nb107,
                nb108,
                nb109
            };

        private List<YANNb> _nbTails;
        // Initialize list nbTail
        private void InitNbTails() => _nbTails = new List<YANNb>
            {
                nb0,
                nb1,
                nb2,
                nb3,
                nb4,
                nb5,
                nb6,
                nb7,
                nb8,
                nb9
            };

        private List<YANNb> _nbHeadCons;
        // Initialize list nbHeadCon
        private void InitNbHeadCons() => _nbHeadCons = new List<YANNb>
            {
                nbC100,
                nbC101,
                nbC102,
                nbC103,
                nbC104,
                nbC105,
                nbC106,
                nbC107,
                nbC108,
                nbC109
            };

        private List<YANNb> _nbTailCons;
        // Initialize list nbTailCon
        private void InitNbTailCons() => _nbTailCons = new List<YANNb>
            {
                nbC0,
                nbC1,
                nbC2,
                nbC3,
                nbC4,
                nbC5,
                nbC6,
                nbC7,
                nbC8,
                nbC9
            };
        #endregion
    }
}
