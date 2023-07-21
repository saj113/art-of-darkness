using Core;
using UnityEngine;

namespace GUIScripts.Menu.Talent.UI
{
    public class TalentsListPanel : MonoBehaviour
    {
        [SerializeField] TalentStructurePanel[] _talentStructurePanels;

        public void LoadTalents(ITalentInfo[] talents)
        {
            Contract.Require(_talentStructurePanels.Length <= talents.Length, "talents.Length");
            for (var i = 0; i < _talentStructurePanels.Length; i++)
            {
                _talentStructurePanels[i].SetTalentInfo(talents[i]);
            }
        }
    }
}
