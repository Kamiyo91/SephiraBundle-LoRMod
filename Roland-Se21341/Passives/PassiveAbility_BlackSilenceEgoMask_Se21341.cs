using BigDLL4221.Passives;
using SephiraModInit.Models;

namespace SephiraModInit.Roland_Se21341.Passives
{
    public class PassiveAbility_BlackSilenceEgoMask_Se21341 : PassiveAbility_PlayerMechBase_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new BlackSilenceUtil().Util);
        }

        public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard != null) Util.ExtraMethodCase();
            base.OnEndBattle(curCard);
        }
    }
}