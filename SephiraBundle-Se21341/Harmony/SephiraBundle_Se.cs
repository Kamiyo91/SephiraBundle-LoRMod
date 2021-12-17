﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using LOR_DiceSystem;
using SephiraBundle_Se21341.Models;
using SephiraBundle_Se21341.Util;
using TMPro;
using UI;
using UnityEngine;

namespace SephiraBundle_Se21341.Harmony
{
    public class SephiraBundle_Se : ModInitializer
    {
        public override void OnInitializeMod()
        {
            ModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            var harmony = new HarmonyLib.Harmony("LOR.SephirahBundleSe21341_MOD");
            var method = typeof(SephiraBundle_Se).GetMethod("BookModel_SetXmlInfo");
            harmony.Patch(typeof(BookModel).GetMethod("SetXmlInfo", AccessTools.all), null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("BookModel_GetThumbSprite");
            harmony.Patch(typeof(BookModel).GetMethod("GetThumbSprite", AccessTools.all), null,
                new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("BookXmlInfo_GetThumbSprite");
            harmony.Patch(typeof(BookXmlInfo).GetMethod("GetThumbSprite", AccessTools.all), null,
                new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UISettingInvenEquipPageListSlot_SetBooksData");
            harmony.Patch(typeof(UISettingInvenEquipPageListSlot).GetMethod("SetBooksData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UIInvenEquipPageListSlot_SetBooksData");
            harmony.Patch(typeof(UIInvenEquipPageListSlot).GetMethod("SetBooksData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UISpriteDataManager_GetStoryIcon");
            harmony.Patch(typeof(UISpriteDataManager).GetMethod("GetStoryIcon", AccessTools.all),
                new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UnitDataModel_EquipBook");
            harmony.Patch(typeof(UnitDataModel).GetMethod("EquipBook", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("TextDataModel_InitTextData");
            harmony.Patch(typeof(TextDataModel).GetMethod("InitTextData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UnitDataModel_EquipBook");
            var methodSuffix = typeof(SephiraBundle_Se).GetMethod("UnitDataModel_EquipBook_Postfix");
            harmony.Patch(typeof(UnitDataModel).GetMethod("EquipBook", AccessTools.all),
                new HarmonyMethod(method), new HarmonyMethod(methodSuffix));
            method = typeof(SephiraBundle_Se).GetMethod("UnitDataModel_IsBinahChangeItemLock");
            harmony.Patch(typeof(UnitDataModel).GetMethod("IsBinahChangeItemLock", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UnitDataModel_IsBlackSilenceChangeItemLock");
            harmony.Patch(typeof(UnitDataModel).GetMethod("IsBlackSilenceChangeItemLock", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UILibrarianAppearanceInfoPanel_OnClickCustomizeButton");
            harmony.Patch(typeof(UILibrarianAppearanceInfoPanel).GetMethod("OnClickCustomizeButton", AccessTools.all),
                new HarmonyMethod(method));
            ModParameters.Language = GlobalGameManager.Instance.CurrentOption.language;
            SephiraUtil.GetArtWorks(new DirectoryInfo(ModParameters.Path + "/ArtWork"));
            SephiraUtil.ChangePassiveItem();
            SephiraUtil.AddLocalize();
            SephiraUtil.RemoveError();
        }

        public static void BookXmlInfo_GetThumbSprite(BookXmlInfo __instance, ref Sprite __result)
        {
            if (__instance.id.packageId != ModParameters.PackageId) return;
            switch (__instance.id.id)
            {
                case 10000001:
                    __result = Resources.Load<Sprite>("Sprites/Books/Thumb/102");
                    return;
                case 10000002:
                    __result = Resources.Load<Sprite>("Sprites/Books/Thumb/250022");
                    return;
                case 10000003:
                    __result = Resources.Load<Sprite>("Sprites/Books/Thumb/8");
                    return;
                case 10000004:
                    __result = ModParameters.ArtWorks["AngelaDefault_Se21341"];
                    return;
            }
        }

        public static void BookModel_GetThumbSprite(BookModel __instance, ref Sprite __result)
        {
            if (__instance.BookId.packageId != ModParameters.PackageId) return;
            switch (__instance.BookId.id)
            {
                case 10000001:
                    __result = Resources.Load<Sprite>("Sprites/Books/Thumb/102");
                    return;
                case 10000002:
                    __result = Resources.Load<Sprite>("Sprites/Books/Thumb/250022");
                    return;
                case 10000003:
                    __result = Resources.Load<Sprite>("Sprites/Books/Thumb/8");
                    return;
                case 10000004:
                    __result = ModParameters.ArtWorks["AngelaDefault_Se21341"];
                    return;
            }
        }

        public static void BookModel_SetXmlInfo(BookModel __instance, BookXmlInfo ____classInfo,
            ref List<DiceCardXmlInfo> ____onlyCards)
        {
            if (__instance.BookId.packageId == ModParameters.PackageId)
                ____onlyCards.AddRange(____classInfo.EquipEffect.OnlyCard.Select(id =>
                    ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, id))));
        }

        [HarmonyPriority(0)]
        public static void UnitDataModel_EquipBook(UnitDataModel __instance, ref bool __state, BookModel ____bookItem,
            ref BookModel newBook, bool isEnemySetting,
            bool force)
        {
            if (force) return;
            if (newBook == null && __instance.isSephirah && __instance.OwnerSephirah == SephirahType.Keter &&
                !LibraryModel.Instance.IsBlackSilenceLockedInLibrary())
            {
                __instance.ResetTempName();
                __instance.customizeData.SetCustomData(true);
                newBook = Singleton<BookInventoryModel>.Instance.GetBlackSilenceBook();
                return;
            }

            if (newBook != null && newBook.ClassInfo.id.packageId == ModParameters.PackageId &&
                ModParameters.DynamicNames.ContainsKey(newBook.ClassInfo.id.id))
            {
                if (!ModParameters.CustomSkinTrue.Contains(newBook.ClassInfo.id.id))
                    __instance.customizeData.SetCustomData(false);
                __instance.EquipCustomCoreBook(null);
                ModParameters.DynamicNames.TryGetValue(newBook.ClassInfo.id.id, out var name);
                __instance.SetTempName(ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals(name)).Value.Name);
                return;
            }

            if (__instance.bookItem.ClassInfo.id.packageId == ModParameters.PackageId &&
                ModParameters.DynamicNames.ContainsKey(__instance.bookItem.ClassInfo.id.id))
            {
                __instance.ResetTempName();
                __instance.customizeData.SetCustomData(true);
                return;
            }

            if (newBook != null && (newBook.ClassInfo.id.packageId == ModParameters.PackageId || !newBook.IsWorkshop) &&
                __instance.isSephirah && (__instance.OwnerSephirah == SephirahType.Binah ||
                                          __instance.OwnerSephirah == SephirahType.Keter))
            {
                newBook = ____bookItem;
                __state = false;
                return;
            }

            if (__instance.isSephirah && __instance.OwnerSephirah == SephirahType.Binah)
                __instance.customizeData.SetCustomData(true);
            __state = true;
        }

        public static void UnitDataModel_EquipBook_Postfix(UnitDataModel __instance, ref bool __state,
            ref bool __result)
        {
            if (__state) return;
            __result = false;
        }

        public static void TextDataModel_InitTextData(string currentLanguage)
        {
            ModParameters.Language = currentLanguage;
            SephiraUtil.AddLocalize();
        }

        public static void UnitDataModel_IsBinahChangeItemLock(ref bool __result)
        {
            __result = false;
        }

        public static void UnitDataModel_IsBlackSilenceChangeItemLock(ref bool __result)
        {
            __result = false;
        }

        public static bool UILibrarianAppearanceInfoPanel_OnClickCustomizeButton(
            UILibrarianAppearanceInfoPanel __instance)
        {
            if (__instance.unitData.isSephirah &&
                (__instance.unitData.OwnerSephirah == SephirahType.Binah ||
                 __instance.unitData.OwnerSephirah == SephirahType.Keter &&
                 !LibraryModel.Instance.IsBlackSilenceLockedInLibrary()))
            {
                UIAlarmPopup.instance.SetAlarmText(ModParameters.EffectTexts
                    .FirstOrDefault(x =>
                        x.Key.Equals(ModParameters.SephirahError
                            .FirstOrDefault(y => __instance.unitData.OwnerSephirah == y.Key).Value)).Value.Desc);
                return false;
            }

            if (__instance.unitData.bookItem.BookId.packageId != ModParameters.PackageId ||
                !ModParameters.DynamicNames.ContainsKey(__instance.unitData.bookItem.BookId.id)) return true;
            UIAlarmPopup.instance.SetAlarmText(ModParameters.EffectTexts
                .FirstOrDefault(x =>
                    x.Key.Equals(ModParameters.DynamicNames
                        .FirstOrDefault(y => __instance.unitData.bookItem.BookId.id == y.Key).Value)).Value.Desc);
            return false;
        }

        public static void UIInvenEquipPageListSlot_SetBooksData(UISettingInvenEquipPageListSlot __instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            if (storyKey.workshopId != ModParameters.PackageId) return;
            var textMeshProUGUI = (TextMeshProUGUI)__instance.GetType().GetField("txt_StoryName", AccessTools.all)
                .GetValue(__instance);
            if (books.Count < 0) return;
            textMeshProUGUI.text = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("ModName_Re21341")).Value
                .Name;
        }

        public static void UISettingInvenEquipPageListSlot_SetBooksData(UISettingInvenEquipPageListSlot __instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            if (storyKey.workshopId != ModParameters.PackageId) return;
            var textMeshProUGUI = (TextMeshProUGUI)__instance.GetType().GetField("txt_StoryName", AccessTools.all)
                .GetValue(__instance);
            if (books.Count < 0) return;
            textMeshProUGUI.text = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("ModName_Re21341")).Value
                .Name;
        }

        public static void UISpriteDataManager_GetStoryIcon(UISpriteDataManager __instance, ref string story)
        {
            if (story.Contains("Binah_Se21341"))
                story = "Chapter1";
        }
    }
}