using System;
using UnityEngine;

namespace SephiraModInit.Roland_Se21341.DiceEffects
{
    public class BehaviourAction_BlackSilenceCustomEgoAreaStrong_Se21341 : BehaviourActionBase
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            _self = self;
            var farAreaeffectBlackSilence4ThAreaStrong =
                new GameObject().AddComponent<FarAreaEffect_BlackSilenceCustomEgoAreaStrong_Se21341>();
            farAreaeffectBlackSilence4ThAreaStrong.Init(self, Array.Empty<object>());
            return farAreaeffectBlackSilence4ThAreaStrong;
        }
    }
}