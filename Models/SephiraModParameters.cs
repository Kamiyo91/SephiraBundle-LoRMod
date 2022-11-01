using System.Collections.Generic;
using BigDLL4221.Models;
using SephiraModInit.Roland_Se21341;
using SephiraModInit.Roland_Se21341.Buffs;

namespace SephiraModInit.Models
{
    public static class SephiraModParameters
    {
        public const string PackageId = "SephirahBundleSe21341.Mod";

        public static MapModel BlackSilenceMap = new MapModel(typeof(BlackSilence_Se21341MapManager),
            "BlackSilenceMassEgo_Se21341", fy: 0.285f);

        public static MechUtil_Roland BlackSilenceUtil = new MechUtil_Roland(new MechUtilBaseModel(
            new Dictionary<int, EgoOptions>
            {
                {
                    1, new EgoOptions(new BattleUnitBuf_BlackSilenceEgoMask_Se21341(), "BlackSilence4",
                        egoMaps: new Dictionary<LorId, MapModel>
                        {
                            { new LorId(PackageId, 30), BlackSilenceMap }
                        })
                }
            }, personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                { new LorId(PackageId, 31), new PersonalCardOptions(activeEgoCard: true) },
                { new LorId(PackageId, 30), new PersonalCardOptions(true, egoPhase: 1) }
            }));

        public static string Path { get; set; }
    }
}