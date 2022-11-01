﻿using BigDLL4221.Passives;
using SephiraModInit.Models;

namespace SephiraModInit.Roland_Se21341.Passives
{
    public class PassiveAbility_BlackSilenceEgoMask_Se21341 : PassiveAbility_PlayerMechBase_DLL4221
    {
        public override void OnWaveStart()
        {
            SetUtil(SephiraModParameters.BlackSilenceUtil);
            base.OnWaveStart();
        }
    }
}