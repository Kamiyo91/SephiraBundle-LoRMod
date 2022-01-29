using System;
using System.Collections.Generic;

namespace SephiraBundle_Se21341.Models
{
    public class MechUtilBaseModel
    {
        public BattleUnitModel Owner { get; set; }
        public string EgoMapName { get; set; }
        public float? BgY { get; set; }
        public float? FlY { get; set; }
        public List<int> OriginalMapStageIds { get; set; }
        public bool HasEgo { get; set; }
        public bool HasEgoAttack { get; set; }
        public bool EgoActivated { get; set; }
        public bool IsSummonEgo { get; set; }
        public bool EgoAttackCardExpire { get; set; }
        public bool HasAdditionalPassive { get; set; }
        public string SkinName { get; set; }
        public bool MapUsed { get; set; }
        public Type EgoType { get; set; }
        public Type EgoMapType { get; set; }
        public LorId[] LorIdArray { get; set; } = null;
        public LorId EgoCardId { get; set; }
        public LorId SecondaryEgoCardId { get; set; }
        public LorId EgoAttackCardId { get; set; }
        public LorId AdditionalPassiveId { get; set; }
    }
}