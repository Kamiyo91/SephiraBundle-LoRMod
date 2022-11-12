using System.Linq;
using BigDLL4221.Models;

namespace SephiraModInit.CommonPassives
{
    public class PassiveAbility_Dialog_Se21341 : PassiveAbility_10008
    {
        private BattleDialogueModel _dlg;

        public override void OnWaveStart()
        {
            base.OnWaveStart();
            if (!ModParameters.KeypageOptions.TryGetValue(owner.Book.BookId.packageId, out var keypageOptions)) return;
            var keypageItem = keypageOptions.FirstOrDefault(x => x.KeypageId == owner.Book.BookId.id);
            if (keypageItem?.BookCustomOptions == null) return;
            _dlg = owner.UnitData.unitData.battleDialogModel;
            if (keypageItem.BookCustomOptions.CustomDialogId != null)
            {
                owner.UnitData.unitData.InitBattleDialogByDefaultBook(keypageItem.BookCustomOptions.CustomDialogId);
                return;
            }

            if (keypageItem.BookCustomOptions.CustomDialog == null) return;
            owner.UnitData.unitData.battleDialogModel =
                new BattleDialogueModel(keypageItem.BookCustomOptions.CustomDialog);
        }

        public override void OnBattleEnd()
        {
            base.OnBattleEnd();
            if(_dlg != null) owner.UnitData.unitData.battleDialogModel = _dlg;
        }
    }
}