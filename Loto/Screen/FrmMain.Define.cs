using System.Collections.Generic;
using YANF.Control;

namespace Loto.Screen
{
    public partial class FrmMain
    {
        private void InitItems()
        {
            InitNbAlls();
            InitNbHeads();
            InitNbTails();
        }

        #region Nb
        private List<YANNb> _nbAlls;
        // Initialize list nbAll
        private void InitNbAlls() => _nbAlls = new List<YANNb>
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
                nb109,
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
        #endregion
    }
}
