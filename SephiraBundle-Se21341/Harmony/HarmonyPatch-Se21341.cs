using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using GameSave;
using HarmonyLib;
using LOR_DiceSystem;
using SephiraBundle_Se21341.Models;
using SephiraBundle_Se21341.Util;
using TMPro;
using UI;
using UnityEngine;

namespace SephiraBundle_Se21341.Harmony
{
    [HarmonyPatch]
    public class HarmonyPatch_Se21341
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BookModel), "GetThumbSprite")]
        [HarmonyPatch(typeof(BookXmlInfo), "GetThumbSprite")]
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

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BookModel), "SetXmlInfo")]
        public static void BookModel_SetXmlInfo(BookModel __instance, BookXmlInfo ____classInfo,
            ref List<DiceCardXmlInfo> ____onlyCards)
        {
            if (__instance.BookId.packageId == ModParameters.PackageId)
                ____onlyCards.AddRange(____classInfo.EquipEffect.OnlyCard.Select(id =>
                    ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, id))));
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIInvenEquipPageSlot), "SetOperatingPanel")]
        [HarmonyPatch(typeof(UIInvenLeftEquipPageSlot), "SetOperatingPanel")]
        [HarmonyPatch(typeof(UISettingInvenEquipPageSlot), "SetOperatingPanel")]
        [HarmonyPatch(typeof(UISettingInvenEquipPageLeftSlot), "SetOperatingPanel")]
        public static void General_SetOperatingPanel(object __instance,
            UICustomGraphicObject ___button_Equip, TextMeshProUGUI ___txt_equipButton, BookModel ____bookDataModel)
        {
            var uiOrigin = __instance as UIOriginEquipPageSlot;
            SephiraUtil.SetOperationPanel(uiOrigin, ___button_Equip, ___txt_equipButton, ____bookDataModel);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UnitDataModel), "EquipBook")]
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

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnitDataModel), "EquipBook")]
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

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIBookStoryChapterSlot), "SetEpisodeSlots")]
        public static void UIBookStoryChapterSlot_SetEpisodeSlots(UIBookStoryChapterSlot __instance,
            UIBookStoryPanel ___panel, List<UIBookStoryEpisodeSlot> ___EpisodeSlots)
        {
            SephiraUtil.SetEpisodeSlots(__instance, ___EpisodeSlots);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UnitDataModel), "LoadFromSaveData")]
        public static void UnitDataModel_LoadFromSaveData(UnitDataModel __instance, SaveData data)
        {
            if (!__instance.isSephirah || __instance.OwnerSephirah != SephirahType.Keter) return;
            var bookInstanceId = data.GetInt("bookInstanceId");
            if (bookInstanceId <= 0) return;
            var bookModel = Singleton<BookInventoryModel>.Instance.GetBookByInstanceId(bookInstanceId);
            if (bookModel != null && bookModel.ClassInfo.id.packageId == ModParameters.PackageId &&
                ModParameters.DynamicNames.ContainsKey(bookModel.ClassInfo.id.id))
                __instance.EquipBook(bookModel);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TextDataModel), "InitTextData")]
        public static void TextDataModel_InitTextData(string currentLanguage)
        {
            ModParameters.Language = currentLanguage;
            SephiraUtil.AddLocalize();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UILibrarianAppearanceInfoPanel), "OnClickCustomizeButton")]
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

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UISettingInvenEquipPageListSlot), "SetBooksData")]
        [HarmonyPatch(typeof(UIInvenEquipPageListSlot), "SetBooksData")]
        public static void General_SetBooksData(object __instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            var uiOrigin = __instance as UIOriginEquipPageList;
            SephiraUtil.SetBooksData(uiOrigin, books, storyKey);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UISpriteDataManager), "GetStoryIcon")]
        public static void UISpriteDataManager_GetStoryIcon(ref string story)
        {
            if (story.Contains("Binah_Se21341"))
                story = "Chapter1";
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BookInventoryModel), "LoadFromSaveData")]
        public static void BookInventoryModel_LoadFromSaveData(BookInventoryModel __instance)
        {
            foreach (var keypageId in ModParameters.KeypageIds.Where(keypageId =>
                         !Singleton<BookInventoryModel>.Instance.GetBookListAll().Exists(x =>
                             x.GetBookClassInfoId() == new LorId(ModParameters.PackageId, keypageId))))
                __instance.CreateBook(new LorId(ModParameters.PackageId, keypageId));
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(BattleUnitCardsInHandUI), "UpdateCardList")]
        public static IEnumerable<CodeInstruction> BattleUnitCardsInHandUI_UpdateCardList(
            IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var patchSuccess = false;
            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode != OpCodes.Ldloc_2 || codes[i + 1].opcode != OpCodes.Callvirt ||
                    codes[i + 2].opcode != OpCodes.Callvirt || codes[i + 3].opcode != OpCodes.Ldc_I4 ||
                    (int)codes[i + 3].operand != 250022 || codes[i + 4].opcode != OpCodes.Call ||
                    codes[i + 5].opcode != OpCodes.Brfalse) continue;
                patchSuccess = true;
                var addedCodes = new List<CodeInstruction>();
                foreach (var codeToAdd in ModParameters.NoEgoFloorUnit.Select(unitId => new List<CodeInstruction>
                         {
                             codes[i],
                             codes[i + 1],
                             codes[i + 2],
                             new CodeInstruction(OpCodes.Ldstr, ModParameters.PackageId),
                             new CodeInstruction(OpCodes.Ldc_I4, unitId),
                             new CodeInstruction(OpCodes.Newobj,
                                 AccessTools.Constructor(typeof(LorId), new[] { typeof(string), typeof(int) })),
                             new CodeInstruction(OpCodes.Call,
                                 AccessTools.Method(typeof(LorId), "op_Inequality",
                                     new[] { typeof(LorId), typeof(LorId) })),
                             codes[i + 5]
                         }))
                    addedCodes.AddRange(codeToAdd);
                codes.InsertRange(i + 6, addedCodes);
                Debug.Log("Patched");
                break;
            }

            if (!patchSuccess)
                HarmonyLib.Harmony.CreateAndPatchAll(typeof(FailSafePatch_Se21341), "LOR.SephirahBundleSe21341_MOD");
            return codes.AsEnumerable();
        }
    }
}