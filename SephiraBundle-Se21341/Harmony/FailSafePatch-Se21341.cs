using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using SephiraBundle_Se21341.Models;
using SephiraBundle_Se21341.Util;

namespace SephiraBundle_Se21341.Harmony
{
    [HarmonyPatch]
    public class FailSafePatch_Se21341
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BattleUnitCardsInHandUI), "UpdateCardList")]
        public static void BattleUnitCardsInHandUI_UpdateCardList(BattleUnitCardsInHandUI __instance,
            List<BattleDiceCardUI> ____activatedCardList, ref float ____xInterval)
        {
            if (__instance.CurrentHandState != BattleUnitCardsInHandUI.HandState.EgoCard) return;
            var unit = __instance.SelectedModel ?? __instance.HOveredModel;
            if (unit.UnitData.unitData.bookItem.BookId.packageId != ModParameters.PackageId ||
                !ModParameters.NoEgoFloorUnit.Contains(unit.UnitData.unitData.bookItem.BookId.id)) return;
            var list = SephiraUtil.ReloadEgoHandUI(__instance, __instance.GetCardUIList(), unit, ____activatedCardList,
                ref ____xInterval).ToList();
            __instance.SetSelectedCardUI(null);
            for (var i = list.Count; i < __instance.GetCardUIList().Count; i++)
                __instance.GetCardUIList()[i].gameObject.SetActive(false);
        }
    }
}