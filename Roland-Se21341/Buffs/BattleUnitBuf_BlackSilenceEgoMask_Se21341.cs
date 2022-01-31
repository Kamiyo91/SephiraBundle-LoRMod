using Sound;

namespace Roland_Se21341.Buffs
{
    public class BattleUnitBuf_BlackSilenceEgoMask_Se21341 : BattleUnitBuf
    {
        public BattleUnitBuf_BlackSilenceEgoMask_Se21341()
        {
            stack = 0;
        }

        protected override string keywordId => "BlackSilenceEgo_Se21341";
        public override int paramInBufDesc => 0;
        protected override string keywordIconId => "BlackFrantic";
        public override bool isAssimilation => true;

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }
    }
}