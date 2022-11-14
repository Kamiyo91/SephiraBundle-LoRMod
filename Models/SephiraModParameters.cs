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

        public static List<LorId> BlackSilencePassiveCards = new List<LorId>
        {
            new LorId(705206), new LorId(705208), new LorId(705207), new LorId(702312), new LorId(702311),
            new LorId(705218)
        };

        public static string Path { get; set; }
    }

    public class BlackSilenceUtil
    {
        public readonly MechUtil_Roland Util = new MechUtil_Roland(new MechUtilBaseModel(
            new Dictionary<int, EgoOptions>
            {
                {
                    0,
                    new EgoOptions(egoSkinName: "BlackSilenceMask", activeEgoOnStart: true, refreshUI: true,
                        isBaseGameSkin: true)
                },
                {
                    1,
                    new EgoOptions(new BattleUnitBuf_BlackSilenceEgoMask_Se21341(), "BlackSilence4", true,
                        new List<LorId> { new LorId(SephiraModParameters.PackageId, 4) },
                        isBaseGameSkin: true)
                }
            }, originalSkinName: "BlackSilence", originalSkinIsBaseGame: true, egoMaps: new Dictionary<LorId, MapModel>
            {
                { new LorId(SephiraModParameters.PackageId, 30), SephiraModParameters.BlackSilenceMap }
            }, personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                {
                    new LorId(SephiraModParameters.PackageId, 31),
                    new PersonalCardOptions(activeEgoCard: true, egoPersonalCard: true, egoPhase: 1)
                },
                { new LorId(SephiraModParameters.PackageId, 30), new PersonalCardOptions(true, egoPhase: 1) }
            }));
    }
}