using UtilLoader21341.Util;

namespace SephiraModInit.Roland_Se21341.Passives
{
    public class PassiveAbility_MaskPower_Se21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            this.OnRoundStartAfterSpecialDraw(SephiraModParameters.BlackSilencePassiveCards);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            this.OnUseCardSpecialDraw(curCard);
        }
    }
}