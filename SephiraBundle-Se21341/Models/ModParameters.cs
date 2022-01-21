using System;
using System.Collections.Generic;
using UnityEngine;

namespace SephiraBundle_Se21341.Models
{
    public static class ModParameters
    {
        public const string PackageId = "SephirahBundleSe21341.Mod";
        public static readonly Dictionary<string, Sprite> ArtWorks = new Dictionary<string, Sprite>();
        public static readonly List<int> CustomSkinTrue = new List<int> { 10000001, 10000003 };

        public static readonly Dictionary<string, EffectTextModel> EffectTexts =
            new Dictionary<string, EffectTextModel>();

        public static Dictionary<int, LorId> DialogList = new Dictionary<int, LorId>
        {
            { 10000001, new LorId(10) }, { 10000002, new LorId(6) }, { 10000003, new LorId(8) }
        };

        public static readonly List<int> KeypageIds = new List<int>
        {
            10000001,10000002,10000003,10000004
        };
        public static readonly List<int> NoInventoryCardList = new List<int>
            { 9910001, 9910002, 9910003, 9910004, 9910005 };

        public static readonly List<int> UntransferablePassives = new List<int> { 1, 2, 3, 4 };

        public static readonly Dictionary<int, Tuple<string, SephirahType>> DynamicNames =
            new Dictionary<int, Tuple<string, SephirahType>>
            {
                { 10000001, new Tuple<string, SephirahType>("RolandName_Se21341", SephirahType.Keter) },
                { 10000002, new Tuple<string, SephirahType>("GeburaName_Se21341", SephirahType.Gebura) },
                { 10000003, new Tuple<string, SephirahType>("BinahName_Se21341", SephirahType.Binah) },
                { 10000004, new Tuple<string, SephirahType>("AngelaName_Se21341", SephirahType.None) }
            };

        public static readonly List<int> NoEgoFloorUnit = new List<int> { 10000004, 10000002 };

        public static readonly Dictionary<string, List<int>> SpritePreviewChange = new Dictionary<string, List<int>>
        {
            { "AngelaDefault_Se21341", new List<int> { 10000004 } }
        };

        public static readonly Dictionary<string, List<int>> DefaultSpritePreviewChange =
            new Dictionary<string, List<int>>
            {
                { "Sprites/Books/Thumb/102", new List<int> { 10000001 } },
                { "Sprites/Books/Thumb/250022", new List<int> { 10000002 } },
                { "Sprites/Books/Thumb/8", new List<int> { 10000003 } }
            };
        public static string Path { get; set; }
        public static string Language { get; set; }
    }
}