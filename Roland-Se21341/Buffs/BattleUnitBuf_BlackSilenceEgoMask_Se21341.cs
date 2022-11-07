using BigDLL4221.Buffs;
using SephiraModInit.Models;
using Sound;

namespace SephiraModInit.Roland_Se21341.Buffs
{
    public class BattleUnitBuf_BlackSilenceEgoMask_Se21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        public BattleUnitBuf_BlackSilenceEgoMask_Se21341() : base(lastOneScene: false, infinite: true)
        {
        }

        public override int MinStack => 1;
        public override int MaxStack => 3;
        protected override string keywordId => "BlackSilenceEgo_Se21341";
        public override int paramInBufDesc => 0;
        protected override string keywordIconId => "BlackFrantic";
        public override bool isAssimilation => true;

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }

        public override void OnRoundStartAfter()
        {
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, stack, _owner);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel card)
        {
            if (card.card.GetID() == new LorId(SephiraModParameters.PackageId, 30)) OnAddBuf(1);
        }
    }
}