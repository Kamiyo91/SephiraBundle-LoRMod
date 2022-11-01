using Sound;
using UnityEngine;

namespace SephiraModInit.Roland_Se21341.DiceEffects
{
    public class FarAreaEffect_BlackSilenceCustomEgoAreaStrongFinal_Se21341 : FarAreaEffect
    {
        private bool _damaged;
        private float _elapsed;
        private bool _ended;
        private BlackSilence_Se21341MapManager _map;

        private BlackSilence_Se21341MapManager Map
        {
            get
            {
                if (_map == null)
                    _map =
                        SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject as
                            BlackSilence_Se21341MapManager;

                return _map;
            }
        }

        public override void OnGiveDamage()
        {
            base.OnGiveDamage();
            var map = Map;
            if (map != null) map.BoomSecond();

            PrintSound();
            isRunning = false;
        }

        public override void Init(BattleUnitModel self, params object[] args)
        {
            base.Init(self, args);
            SoundEffectPlayer.PlaySound("Battle/Roland_Phase4_CryStart");
        }

        public virtual void PrintSound()
        {
            SoundEffectPlayer.PlaySound("Battle/Roland_Phase2_Windblast");
        }

        public override void OnEffectEnd()
        {
            base.OnEffectEnd();
            _isDoneEffect = true;
            gameObject.SetActive(false);
        }

        protected override void Update()
        {
            base.Update();
            _elapsed += Time.deltaTime;
            if (!_damaged && _elapsed >= 0.4f)
            {
                _damaged = true;
                OnGiveDamage();
            }

            if (!_ended && _elapsed >= 1f)
            {
                _ended = true;
                OnEffectEnd();
            }
        }
    }
}