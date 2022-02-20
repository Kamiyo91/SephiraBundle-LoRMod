namespace Roland_Se21341.Cards
{
    public class DiceCardSelfAbility_BlackSilenceEgoMask_Se21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return (owner.emotionDetail.EmotionLevel >= 3 ||
                    owner.passiveDetail.PassiveList.Exists(x =>
                        !x.destroyed && x.id == new LorId("LorModPackRe21341.Mod", 61))) &&
                   !owner.bufListDetail.HasAssimilation();
        }
    }
}