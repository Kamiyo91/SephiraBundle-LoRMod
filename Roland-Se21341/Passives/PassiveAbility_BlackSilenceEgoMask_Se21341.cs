using BigDLL4221.Passives;
using SephiraModInit.Models;

namespace SephiraModInit.Roland_Se21341.Passives
{
    public class PassiveAbility_BlackSilenceEgoMask_Se21341 : PassiveAbility_PlayerMechBase_DLL4221
    {
        public override void OnWaveStart()
        {
            SetUtil(new BlackSilenceUtil().Util);
            base.OnWaveStart();
        }

        public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard != null) Util.ExtraMethodCase();
        }
    }
}