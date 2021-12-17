using SephiraBundle_Se21341.Models;

namespace Angela_Se21341.Passives
{
    public class PassiveAbility_AngelaUnit_Se21341 : PassiveAbilityBase
    {
        private BattleDialogueModel _dlg;

        public override void OnWaveStart()
        {
            _dlg = owner.UnitData.unitData.battleDialogModel;
            owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(ModParameters.PackageId, 10000006));
            AddCardsWaveStart();
        }

        private void AddCardsWaveStart()
        {
            if (owner.emotionDetail.EmotionLevel == 3)
            {
                owner.personalEgoDetail.AddCard(9910011);
                owner.personalEgoDetail.AddCard(9910012);
                owner.personalEgoDetail.AddCard(9910013);
            }

            if (owner.emotionDetail.EmotionLevel == 4)
            {
                owner.personalEgoDetail.AddCard(9910011);
                owner.personalEgoDetail.AddCard(9910012);
                owner.personalEgoDetail.AddCard(9910013);
                owner.personalEgoDetail.AddCard(9910014);
                owner.personalEgoDetail.AddCard(9910015);
                owner.personalEgoDetail.AddCard(9910016);
            }

            if (owner.emotionDetail.EmotionLevel != 5) return;
            owner.personalEgoDetail.AddCard(9910011);
            owner.personalEgoDetail.AddCard(9910012);
            owner.personalEgoDetail.AddCard(9910013);
            owner.personalEgoDetail.AddCard(9910014);
            owner.personalEgoDetail.AddCard(9910015);
            owner.personalEgoDetail.AddCard(9910016);
            owner.personalEgoDetail.AddCard(9910017);
            owner.personalEgoDetail.AddCard(9910018);
            owner.personalEgoDetail.AddCard(9910019);
        }

        private void AddCardOnLvUpEmotion()
        {
            if (owner.emotionDetail.EmotionLevel == 3)
            {
                owner.personalEgoDetail.AddCard(9910011);
                owner.personalEgoDetail.AddCard(9910012);
                owner.personalEgoDetail.AddCard(9910013);
            }

            if (owner.emotionDetail.EmotionLevel == 4)
            {
                owner.personalEgoDetail.AddCard(9910014);
                owner.personalEgoDetail.AddCard(9910015);
                owner.personalEgoDetail.AddCard(9910016);
            }

            if (owner.emotionDetail.EmotionLevel != 5) return;
            owner.personalEgoDetail.AddCard(9910017);
            owner.personalEgoDetail.AddCard(9910018);
            owner.personalEgoDetail.AddCard(9910019);
        }

        public override void OnLevelUpEmotion()
        {
            AddCardOnLvUpEmotion();
        }

        public override void OnBattleEnd()
        {
            owner.UnitData.unitData.battleDialogModel = _dlg;
        }
    }
}