using BigDLL4221.Passives;
using SephiraModInit.Models;

namespace SephiraModInit.Roland_Se21341.Passives
{
    public class PassiveAbility_MaskPower_Se21341 : PassiveAbility_DrawSpecialCards_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetCards(SephiraModParameters.BlackSilencePassiveCards);
        }
    }
}