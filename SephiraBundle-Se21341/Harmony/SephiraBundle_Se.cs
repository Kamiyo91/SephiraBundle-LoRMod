using System;
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
            method = typeof(SephiraBundle_Se).GetMethod("General_GetThumbSprite");
            harmony.Patch(typeof(BookModel).GetMethod("GetThumbSprite", AccessTools.all), null,
                new HarmonyMethod(method));
            harmony.Patch(typeof(BookXmlInfo).GetMethod("GetThumbSprite", AccessTools.all), null,
                new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("General_SetBooksData");
            harmony.Patch(typeof(UISettingInvenEquipPageListSlot).GetMethod("SetBooksData", AccessTools.all),
                null, new HarmonyMethod(method));
            harmony.Patch(typeof(UIInvenEquipPageListSlot).GetMethod("SetBooksData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UISpriteDataManager_GetStoryIcon");
            harmony.Patch(typeof(UISpriteDataManager).GetMethod("GetStoryIcon", AccessTools.all),
                new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("TextDataModel_InitTextData");
            harmony.Patch(typeof(TextDataModel).GetMethod("InitTextData", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UnitDataModel_EquipBookPrefix");
            var methodPostfix = typeof(SephiraBundle_Se).GetMethod("UnitDataModel_EquipBookPostfix");
            harmony.Patch(typeof(UnitDataModel).GetMethod("EquipBook", AccessTools.all),
                new HarmonyMethod(method), new HarmonyMethod(methodPostfix));
            method = typeof(SephiraBundle_Se).GetMethod("General_SetOperatingPanel");
            harmony.Patch(typeof(UIInvenEquipPageSlot).GetMethod("SetOperatingPanel", AccessTools.all),
                null, new HarmonyMethod(method));
            harmony.Patch(typeof(UIInvenLeftEquipPageSlot).GetMethod("SetOperatingPanel", AccessTools.all),
                null, new HarmonyMethod(method));
            harmony.Patch(typeof(UISettingInvenEquipPageSlot).GetMethod("SetOperatingPanel", AccessTools.all),
                null, new HarmonyMethod(method));
            harmony.Patch(typeof(UISettingInvenEquipPageLeftSlot).GetMethod("SetOperatingPanel", AccessTools.all),
                null, new HarmonyMethod(method));
            method = typeof(SephiraBundle_Se).GetMethod("UILibrarianAppearanceInfoPanel_OnClickCustomizeButton");
            harmony.Patch(typeof(UILibrarianAppearanceInfoPanel).GetMethod("OnClickCustomizeButton", AccessTools.all),
                new HarmonyMethod(method));
            ModParameters.Language = GlobalGameManager.Instance.CurrentOption.language;
            SephiraUtil.GetArtWorks(new DirectoryInfo(ModParameters.Path + "/ArtWork"));
            SephiraUtil.ChangeCardItem(ItemXmlDataList.instance);
            SephiraUtil.ChangePassiveItem();
            SephiraUtil.AddLocalize();
            SephiraUtil.RemoveError();
        }

        public static void General_GetThumbSprite(object __instance, ref Sprite __result)
        {
            switch (__instance)
            {
                case BookXmlInfo bookInfo:
                    SephiraUtil.GetThumbSprite(bookInfo.id, ref __result);
                    break;
                case BookModel bookModel:
                    SephiraUtil.GetThumbSprite(bookModel.BookId, ref __result);
                    break;
            }
        }

        public static void BookModel_SetXmlInfo(BookModel __instance, BookXmlInfo ____classInfo,
            ref List<DiceCardXmlInfo> ____onlyCards)
        {
            if (__instance.BookId.packageId == ModParameters.PackageId)
                ____onlyCards.AddRange(____classInfo.EquipEffect.OnlyCard.Select(id =>
                    ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, id))));
        }

        public static void General_SetOperatingPanel(object __instance,
            UICustomGraphicObject ___button_Equip, TextMeshProUGUI ___txt_equipButton, BookModel ____bookDataModel)
        {
            var uiOrigin = __instance as UIOriginEquipPageSlot;
            SephiraUtil.SetOperationPanel(uiOrigin, ___button_Equip, ___txt_equipButton, ____bookDataModel);
        }

        public static void UnitDataModel_EquipBookPrefix(UnitDataModel __instance, BookModel newBook,
            ref BookModel __state, bool force)
        {
            if (force) return;
            __state = newBook;
            if (ModParameters.PackageId != __instance.bookItem.ClassInfo.id.packageId ||
                !ModParameters.DynamicNames.ContainsKey(__instance.bookItem.ClassInfo.id.id)) return;
            __instance.ResetTempName();
            __instance.customizeData.SetCustomData(true);
        }

        public static void UnitDataModel_EquipBookPostfix(UnitDataModel __instance,
            BookModel __state, bool isEnemySetting, bool force)
        {
            if (force) return;
            if (__state == null || ModParameters.PackageId != __state.ClassInfo.workshopID ||
                !ModParameters.DynamicNames.ContainsKey(__state.ClassInfo.id.id)) return;
            if (!ModParameters.CustomSkinTrue.Contains(__state.ClassInfo.id.id))
                __instance.customizeData.SetCustomData(false);
            __instance.EquipCustomCoreBook(null);
            __instance.workshopSkin = "";
            ModParameters.DynamicNames.TryGetValue(__state.ClassInfo.id.id, out var name);
            __instance.SetTempName(ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals(name?.Item1)).Value.Name);
            __instance.EquipBook(__state, isEnemySetting, true);
        }

        public static void TextDataModel_InitTextData(string currentLanguage)
        {
            ModParameters.Language = currentLanguage;
            SephiraUtil.AddLocalize();
        }

        public static bool UILibrarianAppearanceInfoPanel_OnClickCustomizeButton(
            UILibrarianAppearanceInfoPanel __instance)
        {
            if (__instance.unitData.bookItem.BookId.packageId != ModParameters.PackageId ||
                !ModParameters.DynamicNames.ContainsKey(__instance.unitData.bookItem.BookId.id)) return true;
            UIAlarmPopup.instance.SetAlarmText(ModParameters.EffectTexts
                .FirstOrDefault(x =>
                    x.Key.Equals(ModParameters.DynamicNames
                        .FirstOrDefault(y => __instance.unitData.bookItem.BookId.id == y.Key).Value.Item1)).Value.Desc);
            return false;
        }

        public static void General_SetBooksData(object __instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            var uiOrigin = __instance as UIOriginEquipPageList;
            SephiraUtil.SetBooksData(uiOrigin, books, storyKey);
        }

        public static void UISpriteDataManager_GetStoryIcon(UISpriteDataManager __instance, ref string story)
        {
            if (story.Contains("Binah_Se21341"))
                story = "Chapter1";
        }
    }
}