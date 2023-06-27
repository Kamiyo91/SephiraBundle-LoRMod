using UtilLoader21341.Util;

namespace SephiraModInit.CommonPassives
{
    public class PassiveAbility_Dialog_Se21341 : PassiveAbility_10008
    {
        private BattleDialogueModel _dlg;

        public override void OnWaveStart()
        {
            base.OnWaveStart();
            this.OnWaveStartChangeDialog(ref _dlg);
        }

        public override void OnBattleEnd()
        {
            base.OnBattleEnd();
            if (_dlg != null) owner.UnitData.unitData.battleDialogModel = _dlg;
        }
    }
}