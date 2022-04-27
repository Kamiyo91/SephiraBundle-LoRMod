using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using MonoMod.Utils;
using SephiraBundle_Se21341.Models;

namespace SephiraBundle_Se21341.Harmony
{
    public class ModInit_Se21341 : ModInitializer
    {
        public override void OnInitializeMod()
        {
            InitParameters();
            MapStaticUtil.GetArtWorks(new DirectoryInfo(SephiraModParameters.Path + "/ArtWork"));
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance, SephiraModParameters.PackageId);
            UnitUtil.ChangePassiveItem(SephiraModParameters.PackageId);
            LocalizeUtil.AddLocalLocalize(SephiraModParameters.Path, SephiraModParameters.PackageId);
            LocalizeUtil.RemoveError();
        }

        private static void InitParameters()
        {
            ModParameters.PackageIds.Add(SephiraModParameters.PackageId);
            SephiraModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            ModParameters.Language = GlobalGameManager.Instance.CurrentOption.language;
            ModParameters.CustomSkinTrue.AddRange(new List<LorId>
            {
                new LorId(SephiraModParameters.PackageId, 10000001), new LorId(SephiraModParameters.PackageId, 10000003)
            });
            ModParameters.DialogList.AddRange(new Dictionary<LorId, LorId>
            {
                { new LorId(SephiraModParameters.PackageId, 10000001), new LorId(10) },
                { new LorId(SephiraModParameters.PackageId, 10000002), new LorId(6) },
                { new LorId(SephiraModParameters.PackageId, 10000003), new LorId(8) }
            });
            ModParameters.KeypageIds.AddRange(new List<LorId>
            {
                new LorId(SephiraModParameters.PackageId, 10000001),
                new LorId(SephiraModParameters.PackageId, 10000002),
                new LorId(SephiraModParameters.PackageId, 10000003),
                new LorId(SephiraModParameters.PackageId, 10000004),
                new LorId(SephiraModParameters.PackageId, 10000005),
                new LorId(SephiraModParameters.PackageId, 10000006)
            });
            ModParameters.OriginalNoInventoryCardList.AddRange(new List<LorId>
            {
                new LorId(9910001), new LorId(9910002), new LorId(9910003), new LorId(9910004), new LorId(9910005)
            });
            ModParameters.PersonalCardList.AddRange(new List<LorId>
            {
                new LorId(SephiraModParameters.PackageId, 31)
            });
            ModParameters.EgoPersonalCardList.AddRange(new List<LorId>
            {
                new LorId(SephiraModParameters.PackageId, 30)
            });
            ModParameters.UntransferablePassives.AddRange(new List<LorId>
            {
                new LorId(SephiraModParameters.PackageId, 1),
                new LorId(SephiraModParameters.PackageId, 2),
                new LorId(SephiraModParameters.PackageId, 3),
                new LorId(SephiraModParameters.PackageId, 4),
                new LorId(SephiraModParameters.PackageId, 35)
            });
            ModParameters.DynamicSephirahNames.AddRange(new Dictionary<LorId, Tuple<string, SephirahType>>
            {
                {
                    new LorId(SephiraModParameters.PackageId, 10000001),
                    new Tuple<string, SephirahType>("RolandName_Se21341", SephirahType.Keter)
                },
                {
                    new LorId(SephiraModParameters.PackageId, 10000002),
                    new Tuple<string, SephirahType>("GeburaName_Se21341", SephirahType.Gebura)
                },
                {
                    new LorId(SephiraModParameters.PackageId, 10000003),
                    new Tuple<string, SephirahType>("BinahName_Se21341", SephirahType.Binah)
                },
                {
                    new LorId(SephiraModParameters.PackageId, 10000004),
                    new Tuple<string, SephirahType>("AngelaName_Se21341", SephirahType.None)
                }
            });
            ModParameters.NoEgoFloorUnit.AddRange(new List<LorId>
            {
                new LorId(SephiraModParameters.PackageId, 10000002),
                new LorId(SephiraModParameters.PackageId, 10000004)
            });
            ModParameters.SpritePreviewChange.AddRange(new Dictionary<string, List<LorId>>
            {
                { "AngelaDefault_Se21341", new List<LorId> { new LorId(SephiraModParameters.PackageId, 10000004) } },
                {
                    "FragmentDefault_Se21341",
                    new List<LorId>
                    {
                        new LorId(SephiraModParameters.PackageId, 10000005),
                        new LorId(SephiraModParameters.PackageId, 10000006)
                    }
                }
            });
            ModParameters.DefaultSpritePreviewChange.AddRange(new Dictionary<string, List<LorId>>
            {
                { "Sprites/Books/Thumb/102", new List<LorId> { new LorId(SephiraModParameters.PackageId, 10000001) } },
                {
                    "Sprites/Books/Thumb/250022",
                    new List<LorId> { new LorId(SephiraModParameters.PackageId, 10000002) }
                },
                { "Sprites/Books/Thumb/8", new List<LorId> { new LorId(SephiraModParameters.PackageId, 10000003) } }
            });
            ModParameters.UniquePassives.AddRange(new List<Tuple<LorId, List<LorId>>>
            {
                new Tuple<LorId, List<LorId>>(new LorId(SephiraModParameters.PackageId, 26),
                    new List<LorId> { new LorId(10010) }),
                new Tuple<LorId, List<LorId>>(new LorId(SephiraModParameters.PackageId, 27),
                    new List<LorId> { new LorId(10012) })
            });
        }
    }
}