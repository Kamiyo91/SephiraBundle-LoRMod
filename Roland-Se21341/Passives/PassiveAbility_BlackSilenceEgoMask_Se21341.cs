using System.Collections.Generic;
using CustomMapUtility;
using SephiraModInit.Roland_Se21341.Buffs;
using UtilLoader21341.Models;
using UtilLoader21341.Util;

namespace SephiraModInit.Roland_Se21341.Passives
{
    public class PassiveAbility_BlackSilenceEgoMask_Se21341 : PassiveAbilityBase
    {
        private const string OriginalSkinName = "BlackSilence";
        private const string StarterSkinName = "BlackSilenceMask";
        private const string EgoSkinName = "BlackSilence4";
        private readonly CustomMapHandler _cmh = CustomMapHandler.GetCMU(SephiraModParameters.PackageId);
        private readonly LorId _egoAttackCard = new LorId(SephiraModParameters.PackageId, 30);
        private readonly LorId _egoCard = new LorId(SephiraModParameters.PackageId, 31);
        public bool CustomSkin;
        public bool EgoActive;
        public bool EgoActiveQueue;

        public MapModelRoot MapModel = new MapModelRoot
            { Component = nameof(BlackSilence_Se21341MapManager), Fy = 0.285f, Stage = "BlackSilenceMassEgo_Se21341" };

        public override void OnWaveStart()
        {
            if (UnitUtil.CheckSkinProjection(owner))
            {
                CustomSkin = true;
            }
            else
            {
                owner.UnitData.unitData.bookItem.SetCharacterName(StarterSkinName);
                owner.view.SetAltSkin(StarterSkinName);
                UnitUtil.RefreshCombatUI();
            }

            owner.personalEgoDetail.AddCard(_egoCard);
        }

        public override void OnRoundStart()
        {
            if (!EgoActiveQueue) return;
            EgoActiveQueue = false;
            EgoActived();
        }

        public override void OnRoundEndTheLast()
        {
            if (!EgoActiveQueue) return;
            if (!owner.passiveDetail.HasPassive<PassiveAbility_MaskPower_Se21341>())
                owner.passiveDetail.AddPassive(new LorId(SephiraModParameters.PackageId, 4));
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            if (cardId.packageId != SephiraModParameters.PackageId) return;
            switch (cardId.id)
            {
                case 31:
                    EgoActiveQueue = true;
                    break;
                case 30:
                    MapUtil.ChangeMapGeneric<BlackSilence_Se21341MapManager>(_cmh, MapModel);
                    break;
            }
        }

        public void EgoActived()
        {
            owner.personalEgoDetail.RemoveCard(_egoCard);
            owner.EgoActive<BattleUnitBuf_BlackSilenceEgoMask_Se21341>(ref EgoActive, CustomSkin ? "" : EgoSkinName,
                true,
                true, new List<LorId> { _egoAttackCard });
            if (!CustomSkin)
            {
                owner.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S13);
                owner.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S13);
                owner.PrepareBlackSilenceDeck();
            }
        }

        public override void OnBattleEnd()
        {
            if (!CustomSkin)
                owner.UnitData.unitData.bookItem.SetCharacterName(OriginalSkinName);
        }
    }
}