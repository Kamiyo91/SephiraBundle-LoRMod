using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.BaseClass;
using KamiyoStaticUtil.Utils;
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
            SephiraUtil.PrepareBlackSilenceDeck(_model.Owner);
        }

        public void ChangeToEgoMap(LorId cardId)
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

        public void ReturnFromEgoMap()
        {
            if (!_model.MapUsed) return;
            _model.MapUsed = false;
            MapUtil.ReturnFromEgoMap(_model.EgoMapName, _model.OriginalMapStageIds);
        }
    }
}