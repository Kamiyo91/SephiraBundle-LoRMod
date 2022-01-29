using Roland_Se21341.Buffs;
using SephiraBundle_Se21341.Models;

namespace Roland_Se21341.Passives
{
    public class PassiveAbility_BlackSilenceEgoMask_Se21341 : PassiveAbilityBase
    {
        private MechUtil_Roland _util;

        public override void OnWaveStart()
        {
            if (!owner.passiveDetail.HasPassive<PassiveAbility_10012>())
            {
                owner.passiveDetail.DestroyPassive(this);
                return;
            }

            _util = new MechUtil_Roland(new MechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                SkinName = "BlackSilence3",
                EgoMapName = "BlackSilenceMassEgo_Se21341",
                EgoMapType = typeof(BlackSilence_Se21341MapManager),
                FlY = 0.285f,
                EgoType = typeof(BattleUnitBuf_BlackSilenceEgoMask_Se21341),
                EgoCardId = new LorId(ModParameters.PackageId, 31),
                HasEgoAttack = true,
                EgoAttackCardId = new LorId(ModParameters.PackageId, 30)
            });
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
            _util.ChangeToEgoMap(curCard.card.GetID());
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ReturnFromEgoMap();
        }

        public override void OnRoundEndTheLast()
        {
            if (_util.EgoCheck()) _util.EgoActive();
        }
    }
}