using System.Collections.Generic;
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
            ChangeToBlackSilenceCards();
        }

        private void ChangeToBlackSilenceCards()
        {
            var handCount = _model.Owner.allyCardDetail.GetHand().Count;
            _model.Owner.allyCardDetail.ExhaustAllCards();
            foreach (var cardId in GetBlackSilenceMaskCardsId())
                _model.Owner.allyCardDetail.AddNewCardToDeck(cardId);
            _model.Owner.allyCardDetail.Shuffle();
            _model.Owner.allyCardDetail.DrawCards(handCount);
        }

        private static List<LorId> GetBlackSilenceMaskCardsId()
        {
            return new List<LorId>
            {
                new LorId(705206), new LorId(705207), new LorId(705208), new LorId(ModParameters.PackageId, 36),
                new LorId(ModParameters.PackageId, 37), new LorId(ModParameters.PackageId, 38),
                new LorId(ModParameters.PackageId, 39), new LorId(702001), new LorId(702004)
            };
        }
    }
}