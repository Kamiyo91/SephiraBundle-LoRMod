using System;
using System.IO;
using System.Reflection;
using SephiraBundle_Se21341.Models;
using SephiraBundle_Se21341.Util;

namespace SephiraBundle_Se21341.Harmony
{
    public class ModInit_Se21341 : ModInitializer
    {
        public override void OnInitializeMod()
        {
            ModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(HarmonyPatch_Se21341), "LOR.SephirahBundleSe21341_MOD");
            ModParameters.Language = GlobalGameManager.Instance.CurrentOption.language;
            SephiraUtil.GetArtWorks(new DirectoryInfo(ModParameters.Path + "/ArtWork"));
            SephiraUtil.ChangeCardItem(ItemXmlDataList.instance);
            SephiraUtil.ChangePassiveItem();
            SephiraUtil.AddLocalize();
            SephiraUtil.RemoveError();
        }
    }
}