using System;
using SephiraBundle_Se21341.Models;

namespace SephiraBundle_Se21341.Util
{
    public class MechUtilBase
    {
        private readonly MechUtilBaseModel _model;

        public MechUtilBase(MechUtilBaseModel model)
        {
            _model = model;
            if (model.HasEgo && model.EgoCardId != null) model.Owner.personalEgoDetail.AddCard(model.EgoCardId);
        }

        public virtual void EgoActive()
        {
            if (_model.Owner.bufListDetail.HasAssimilation()) return;
            _model.EgoActivated = false;
            if (_model.IsSummonEgo && BattleObjectManager.instance.GetAliveList(Faction.Player).Count > 1)
            {
                _model.Owner.personalEgoDetail.AddCard(_model.SecondaryEgoCardId);
                return;
            }

            if (!string.IsNullOrEmpty(_model.SkinName)) _model.Owner.view.SetAltSkin(_model.SkinName);
            _model.Owner.bufListDetail.AddBufWithoutDuplication(
                (BattleUnitBuf)Activator.CreateInstance(_model.EgoType));
            _model.Owner.cardSlotDetail.RecoverPlayPoint(_model.Owner.cardSlotDetail.GetMaxPlayPoint());
            if (_model.HasEgoAttack) _model.Owner.personalEgoDetail.AddCard(_model.EgoAttackCardId);
        }

        public virtual void OnUseExpireCard(LorId cardId)
        {
            if (_model.LorIdArray != null && _model.LorIdArray.Contains(cardId))
                _model.Owner.personalEgoDetail.RemoveCard(cardId);
            if (_model.EgoAttackCardExpire && _model.EgoAttackCardId == cardId)
                _model.Owner.personalEgoDetail.RemoveCard(_model.EgoAttackCardId);
            if (!_model.HasEgo || _model.EgoCardId != cardId) return;
            if (_model.EgoCardId != null) _model.Owner.personalEgoDetail.RemoveCard(_model.EgoCardId);
            if (_model.HasAdditionalPassive) _model.Owner.passiveDetail.AddPassive(_model.AdditionalPassiveId);
            _model.Owner.breakDetail.ResetGauge();
            _model.Owner.breakDetail.RecoverBreakLife(1, true);
            _model.Owner.breakDetail.nextTurnBreak = false;
            _model.EgoActivated = true;
        }

        public virtual void ChangeToEgoMap(LorId cardId)
        {
            if (cardId != _model.EgoAttackCardId ||
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            _model.MapUsed = true;
            MapUtil.ChangeMap(new MapModel
            {
                Stage = _model.EgoMapName,
                StageIds = _model.OriginalMapStageIds,
                OneTurnEgo = true,
                IsPlayer = true,
                Component = _model.EgoMapType,
                Bgy = _model.BgY ?? 0.5f,
                Fy = _model.FlY ?? 407.5f / 1080f
            });
        }

        public virtual void ReturnFromEgoMap()
        {
            if (!_model.MapUsed) return;
            _model.MapUsed = false;
            MapUtil.ReturnFromEgoMap(_model.EgoMapName, _model.OriginalMapStageIds);
        }

        public virtual bool EgoCheck()
        {
            return _model.EgoActivated;
        }
    }
}