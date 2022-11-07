using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BigDLL4221.Enum;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using LOR_DiceSystem;
using SephiraModInit.Models;
using UnityEngine;

namespace SephiraModInit.Harmony
{
    public class ModInit_Se21341 : ModInitializer
    {
        public override void OnInitializeMod()
        {
            OnInitParameters();
            ArtUtil.GetArtWorks(new DirectoryInfo(SephiraModParameters.Path + "/ArtWork"));
            CardUtil.ChangeCardItem(ItemXmlDataList.instance, SephiraModParameters.PackageId);
            PassiveUtil.ChangePassiveItem(SephiraModParameters.PackageId);
            LocalizeUtil.AddGlobalLocalize(SephiraModParameters.PackageId);
            ArtUtil.PreLoadBufIcons();
            LocalizeUtil.RemoveError();
            CardUtil.InitKeywordsList(new List<Assembly> { Assembly.GetExecutingAssembly() });
            ArtUtil.InitCustomEffects(new List<Assembly> { Assembly.GetExecutingAssembly() });
            CustomMapHandler.ModResources.CacheInit.InitCustomMapFiles(Assembly.GetExecutingAssembly());
        }

        private static void OnInitParameters()
        {
            ModParameters.PackageIds.Add(SephiraModParameters.PackageId);
            SephiraModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            ModParameters.Path.Add(SephiraModParameters.PackageId, SephiraModParameters.Path);
            OnInitSprites();
            OnInitKeypages();
            OnInitCards();
            OnInitPassives();
            OnInitRewards();
            OnInitCredenza();
        }

        private static void OnInitSprites()
        {
            ModParameters.SpriteOptions.Add(SephiraModParameters.PackageId, new List<SpriteOptions>
            {
                new SpriteOptions(SpriteEnum.Custom, 10000004, "AngelaDefault_Se21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000005, "FragmentDefault_Se21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000006, "FragmentDefault_Se21341"),
                new SpriteOptions(SpriteEnum.Base, 10000001, "Sprites/Books/Thumb/102"),
                new SpriteOptions(SpriteEnum.Base, 10000002, "Sprites/Books/Thumb/250022"),
                new SpriteOptions(SpriteEnum.Base, 10000003, "Sprites/Books/Thumb/8")
            });
        }

        private static void OnInitRewards()
        {
            ModParameters.StartUpRewardOptions.Add(new RewardOptions(keypages: new List<LorId>
            {
                new LorId(SephiraModParameters.PackageId, 10000001),
                new LorId(SephiraModParameters.PackageId, 10000002),
                new LorId(SephiraModParameters.PackageId, 10000003),
                new LorId(SephiraModParameters.PackageId, 10000004),
                new LorId(SephiraModParameters.PackageId, 10000005),
                new LorId(SephiraModParameters.PackageId, 10000006)
            }, cards: new Dictionary<LorId, int>
            {
                { new LorId(607003), 3 },
                { new LorId(607004), 3 },
                { new LorId(607005), 3 },
                { new LorId(607006), 3 },
                { new LorId(607007), 3 }
            }));
        }

        private static void OnInitCards()
        {
            ModParameters.CardOptions.Add(SephiraModParameters.PackageId, new List<CardOptions>
            {
                new CardOptions(9910001, CardOption.NoInventory, isBaseGameCard: true),
                new CardOptions(9910002, CardOption.NoInventory, isBaseGameCard: true),
                new CardOptions(9910003, CardOption.NoInventory, isBaseGameCard: true),
                new CardOptions(9910004, CardOption.NoInventory, isBaseGameCard: true),
                new CardOptions(9910005, CardOption.NoInventory, isBaseGameCard: true),
                new CardOptions(30, CardOption.EgoPersonal),
                new CardOptions(31, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.gray, iconColor: new HSVColor(0, 0, 74)))
            });
        }

        private static void OnInitPassives()
        {
            ModParameters.PassiveOptions.Add(SephiraModParameters.PackageId, new List<PassiveOptions>
            {
                new PassiveOptions(1, false),
                new PassiveOptions(2, false, bannedEgoFloorCards: true),
                new PassiveOptions(3, false),
                new PassiveOptions(4,passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
                new PassiveOptions(10008, isBaseGamePassive: true, passiveScriptId: "Dialog_Se21341"),
                new PassiveOptions(35, false),
                new PassiveOptions(26, canBeUsedWithPassivesOne: new List<LorId>
                {
                    new LorId(10010)
                }),
                new PassiveOptions(27, canBeUsedWithPassivesOne: new List<LorId>
                {
                    new LorId(10012)
                }, chainReleasePassives: new List<LorId>
                {
                    new LorId(SephiraModParameters.KamiyoModPackPackageId, 61)
                }, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray))
            });
        }

        private static void OnInitKeypages()
        {
            ModParameters.KeypageOptions.Add(SephiraModParameters.PackageId, new List<KeypageOptions>
            {
                new KeypageOptions(10000001, false, everyoneCanEquip: true, editErrorMessageId: "EditError_Se21341",
                    bookCustomOptions: new BookCustomOptions(nameTextId: 1, customFaceData: false,
                        customDialogId: new LorId(10))),
                new KeypageOptions(10000002, false, everyoneCanEquip: true, editErrorMessageId: "EditError_Se21341",
                    bannedEgoFloorCards: true,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 2, customFaceData: false,
                        customDialogId: new LorId(6))),
                new KeypageOptions(10000003, false, everyoneCanEquip: true, editErrorMessageId: "EditError_Se21341",
                    bookCustomOptions: new BookCustomOptions(nameTextId: 3, customFaceData: false,
                        customDialogId: new LorId(8))),
                new KeypageOptions(10000004, false, everyoneCanEquip: true, editErrorMessageId: "EditError_Se21341",
                    bookCustomOptions: new BookCustomOptions(nameTextId: 4, customFaceData: false,
                        customDialogId: new LorId(SephiraModParameters.PackageId, 10000006))),
                new KeypageOptions(10000006, keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray))
            });
        }

        private static void OnInitCredenza()
        {
            ModParameters.CredenzaOptions.Add(SephiraModParameters.PackageId,
                new CredenzaOptions(credenzaNameId: "SephirahBundleSe21341.Mod"));
        }
    }
}