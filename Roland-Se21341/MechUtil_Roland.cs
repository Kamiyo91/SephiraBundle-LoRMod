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
            _model.Owner.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S13);
            _model.Owner.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S13);
            SephiraUtil.PrepareBlackSilenceDeck(_model.Owner);
        }
    }
}