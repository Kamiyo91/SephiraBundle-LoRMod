using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Models;
using LOR_DiceSystem;

namespace SephiraModInit.Roland_Se21341
{
    public class MechUtil_Roland : MechUtilBase
    {
        public new MechUtilBaseModel Model;

        public MechUtil_Roland(MechUtilBaseModel model) : base(model)
        {
            Model = model;
        }

        public override void EgoActive()
        {
            base.EgoActive();
            Model.Owner.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S13);
            Model.Owner.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S13);
            PrepareBlackSilenceDeck(Model.Owner);
        }

        public static void PrepareBlackSilenceDeck(BattleUnitModel owner)
        {
            var furiosoCard = owner.personalEgoDetail.GetCardAll()
                .FirstOrDefault(x => x.GetID().IsBasic() && x.GetID().id == 702010);
            if (furiosoCard != null)
            {
                furiosoCard.CopySelf();
                var num = 0;
                foreach (var furiosoDice in furiosoCard.GetBehaviourList())
                {
                    if (num == 0)
                    {
                        furiosoDice.MotionDetail = MotionDetail.J2;
                        furiosoDice.EffectRes = "BS4DurandalDown_J2";
                        furiosoDice.ActionScript = "BlackSilence_SpecialDurandal_Ego_Se21341";
                    }
                    else
                    {
                        ChangeCardDiceEffect(furiosoDice);
                    }

                    num++;
                }
            }

            foreach (var card in owner.allyCardDetail.GetAllDeck())
            {
                card.CopySelf();
                foreach (var dice in card.GetBehaviourList())
                    ChangeCardDiceEffect(dice);
            }
        }

        private static void ChangeCardDiceEffect(DiceBehaviour dice)
        {
            switch (dice.MotionDetail)
            {
                case MotionDetail.Z:
                    dice.MotionDetail = MotionDetail.S11;
                    dice.EffectRes = "BlackSilence_4th_Lance_S11";
                    break;
                case MotionDetail.S10:
                    dice.MotionDetail = MotionDetail.S7;
                    dice.EffectRes = "BlackSilence_4th_GreatSword_S7";
                    break;
                case MotionDetail.S8:
                case MotionDetail.S9:
                    dice.MotionDetail = MotionDetail.S5;
                    dice.EffectRes = "BlackSilence_4th_MaceAxe_S5";
                    break;
                case MotionDetail.H:
                    dice.MotionDetail = MotionDetail.S6;
                    dice.EffectRes = "BlackSilence_4th_Hammer_S6";
                    break;
                case MotionDetail.S4:
                    dice.MotionDetail = MotionDetail.S2;
                    dice.ActionScript = "";
                    dice.EffectRes = "BlackSilence_4th_LongSword_S2";
                    break;
                case MotionDetail.S5:
                case MotionDetail.S6:
                    dice.MotionDetail = MotionDetail.S2;
                    dice.EffectRes = "BlackSilence_4th_Gauntlet_S3";
                    break;
                case MotionDetail.S7:
                    dice.MotionDetail = MotionDetail.S4;
                    dice.EffectRes = "BlackSilence_4th_ShortSword_S4";
                    break;
                case MotionDetail.J:
                    dice.MotionDetail = MotionDetail.S9;
                    dice.EffectRes = "BlackSilence_4th_DualWield1_S9";
                    break;
                case MotionDetail.S15:
                    dice.MotionDetail = MotionDetail.S10;
                    dice.EffectRes = "BlackSilence_4th_DualWield2_S10";
                    break;
                case MotionDetail.S2:
                    dice.MotionDetail = MotionDetail.S1;
                    break;
                case MotionDetail.S11:
                    dice.MotionDetail = MotionDetail.S8;
                    dice.EffectRes = "BlackSilence_4th_Shotgun_S8";
                    break;
                case MotionDetail.S12:
                    dice.MotionDetail = MotionDetail.J;
                    dice.EffectRes = "BS4DurandalUp_J";
                    break;
                case MotionDetail.S13:
                    dice.MotionDetail = MotionDetail.J2;
                    dice.EffectRes = "BS4DurandalDown_J2";
                    break;
            }
        }
    }
}