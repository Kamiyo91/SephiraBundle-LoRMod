using System.Linq;
using LOR_DiceSystem;
using SephiraBundle_Se21341.Models;
using SephiraBundle_Se21341.Util;

namespace Roland_Se21341
{
    public class MechUtil_Roland : MechUtilBase
    {
        private readonly MechUtilBaseModel _model;

        public MechUtil_Roland(MechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public override void EgoActive()
        {
            base.EgoActive();
            _model.Owner.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S13);
            _model.Owner.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S13);
            if (_model.Owner.bufListDetail.GetActivatedBufList()
                    .Find(x => x is PassiveAbility_10012.BattleUnitBuf_blackSilenceSpecialCount) is PassiveAbility_10012
                    .BattleUnitBuf_blackSilenceSpecialCount)
                _model.Owner.bufListDetail.RemoveBufAll(
                    typeof(PassiveAbility_10012.BattleUnitBuf_blackSilenceSpecialCount));
            if (_model.Owner.passiveDetail.PassiveList.Find(x => x is PassiveAbility_10012) is PassiveAbility_10012
                passiveAbilityBaseOriginal) _model.Owner.passiveDetail.DestroyPassive(passiveAbilityBaseOriginal);
            foreach (var battleDiceCardModel in _model.Owner.allyCardDetail.GetAllDeck())
                battleDiceCardModel.RemoveBuf<PassiveAbility_10012.BattleDiceCardBuf_blackSilenceEgoCount>();
            _model.Owner.personalEgoDetail.RemoveCard(702010);
            SephiraUtil.PrepareBlackSilenceDeck(_model.Owner);
        }
    }
}