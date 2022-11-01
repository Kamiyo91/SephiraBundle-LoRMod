using System;
using UnityEngine;

namespace SephiraModInit.Roland_Se21341.DiceEffects
{
    public class BehaviourAction_BlackSilenceCustomEgoAreaStrongFinal_Se21341 : BehaviourActionBase
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            _self = self;
            var farAreaeffectBlackSilence4ThAreaStrongFinal =
                new GameObject().AddComponent<FarAreaEffect_BlackSilenceCustomEgoAreaStrongFinal_Se21341>();
            farAreaeffectBlackSilence4ThAreaStrongFinal.Init(self, Array.Empty<object>());
            return farAreaeffectBlackSilence4ThAreaStrongFinal;
        }
    }
}