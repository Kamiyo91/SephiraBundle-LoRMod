using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.Utils;
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
                SkinName = "BlackSilence4",
                EgoMapName = "BlackSilenceMassEgo_Se21341",
                EgoMapType = typeof(BlackSilence_Se21341MapManager),
                FlY = 0.285f,
                EgoType = typeof(BattleUnitBuf_BlackSilenceEgoMask_Se21341),
                EgoCardId = new LorId(SephiraModParameters.PackageId, 31),
                HasEgoAttack = true,
                EgoAttackCardId = new LorId(SephiraModParameters.PackageId, 30)
            });
            if (owner.faction != Faction.Enemy) return;
            if (UnitUtil.SpecialCaseEgo(owner.faction, new LorId(SephiraModParameters.PackageId, 27),
                    typeof(BattleUnitBuf_BlackSilenceEgoMask_Se21341))) _util.ForcedEgo();
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

        public override void OnRoundEnd()
        {
            if (owner.faction != Faction.Enemy) return;
            if (UnitUtil.SpecialCaseEgo(owner.faction, new LorId(SephiraModParameters.PackageId, 27),
                    typeof(BattleUnitBuf_BlackSilenceEgoMask_Se21341))) _util.ForcedEgo();
        }
    }
}