using System.Collections.Generic;
using BigDLL4221.Models;
using SephiraModInit.Roland_Se21341;
using SephiraModInit.Roland_Se21341.Buffs;

namespace SephiraModInit.Models
{
    public static class SephiraModParameters
    {
        public const string PackageId = "SephirahBundleSe21341.Mod";
        public const string KamiyoModPackPackageId = "LorModPackRe21341.Mod";

        public static MapModel BlackSilenceMap = new MapModel(typeof(BlackSilence_Se21341MapManager),
            "BlackSilenceMassEgo_Se21341", fy: 0.285f);

        public static string Path { get; set; }
    }

    public class BlackSilenceUtil
    {
        public readonly MechUtil_Roland Util = new MechUtil_Roland(new MechUtilBaseModel(
            new Dictionary<int, EgoOptions>
            {
                {
                    0, new EgoOptions(new BattleUnitBuf_BlackSilenceEgoMask_Se21341(), "BlackSilence4",
                        egoMaps: new Dictionary<LorId, MapModel>
                        {
                            { new LorId(SephiraModParameters.PackageId, 30), SephiraModParameters.BlackSilenceMap }
                        })
                }
            }, personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                {
                    new LorId(SephiraModParameters.PackageId, 31),
                    new PersonalCardOptions(activeEgoCard: true, egoPersonalCard: true)
                },
                { new LorId(SephiraModParameters.PackageId, 30), new PersonalCardOptions(true) }
            }, firstEgoFormCard: new LorId(SephiraModParameters.PackageId, 31)));
    }
}