using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SephiraBundle_Se21341.Models
{
    public static class ModParameters
    {
        public const string PackageId = "SephirahBundleSe21341.Mod";
        public static string Path { get; set; }
        public static readonly Dictionary<string, Sprite> ArtWorks = new Dictionary<string, Sprite>();
        public static string Language { get; set; }
        public static Dictionary<int, string> NameTexts = new Dictionary<int, string>();
        public static List<int> CustomSkinTrue = new List<int> { 10000001, 10000003 };
        public static Dictionary<string, EffectTextModel> EffectTexts = new Dictionary<string, EffectTextModel>();

        public static Dictionary<int, LorId> DialogList = new Dictionary<int, LorId>
        {
             { 10000001, new LorId(10) }, { 10000002, new LorId(6) }, { 10000003, new LorId(8) }
        };
        public static readonly List<int> UntransferablePassives = new List<int> { 1, 2, 3 };
        public static readonly Dictionary<SephirahType, string> SephirahError = new Dictionary<SephirahType, string>
            { { SephirahType.Binah, "BinahError_Se21341" },{ SephirahType.Keter, "KeterError_Se21341" } };
        public static readonly Dictionary<int, string> DynamicNames = new Dictionary<int, string>
            { { 10000001, "RolandName_Se21341" },{ 10000002, "GeburaName_Se21341" },{ 10000003, "BinahName_Se21341" },{ 10000004, "AngelaName_Se21341" } };
    }
}
