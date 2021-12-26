using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using HarmonyLib;
using LOR_DiceSystem;
using LOR_XML;
using Mod;
using SephiraBundle_Se21341.Models;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace SephiraBundle_Se21341.Util
{
    public static class SephiraUtil
    {
        public static void AddLocalize()
        {
            var dictionary =
                typeof(BattleEffectTextsXmlList).GetField("_dictionary", AccessTools.all)
                    ?.GetValue(Singleton<BattleEffectTextsXmlList>.Instance) as Dictionary<string, BattleEffectText>;
            var files = new DirectoryInfo(ModParameters.Path + "/Localize/" + ModParameters.Language + "/EffectTexts")
                .GetFiles();
            ModParameters.EffectTexts.Clear();
            foreach (var t in files)
                using (var stringReader = new StringReader(File.ReadAllText(t.FullName)))
                {
                    var battleEffectTextRoot =
                        (BattleEffectTextRoot)new XmlSerializer(typeof(BattleEffectTextRoot))
                            .Deserialize(stringReader);
                    foreach (var battleEffectText in battleEffectTextRoot.effectTextList)
                    {
                        dictionary.Remove(battleEffectText.ID);
                        dictionary?.Add(battleEffectText.ID, battleEffectText);
                        ModParameters.EffectTexts.Add(battleEffectText.ID,
                            new EffectTextModel { Name = battleEffectText.Name, Desc = battleEffectText.Desc });
                    }
                }

            files = new DirectoryInfo(ModParameters.Path + "/Localize/" + ModParameters.Language + "/Books").GetFiles();
            foreach (var t in files)
                using (var stringReader4 = new StringReader(File.ReadAllText(t.FullName)))
                {
                    var bookDescRoot =
                        (BookDescRoot)new XmlSerializer(typeof(BookDescRoot)).Deserialize(stringReader4);
                    using (var enumerator4 =
                           Singleton<BookXmlList>.Instance.GetAllWorkshopData()[ModParameters.PackageId]
                               .GetEnumerator())
                    {
                        while (enumerator4.MoveNext())
                        {
                            var bookXml = enumerator4.Current;
                            bookXml.InnerName = bookDescRoot.bookDescList.Find(x => x.bookID == bookXml.id.id)
                                .bookName;
                        }
                    }

                    using (var enumerator5 = Singleton<BookXmlList>.Instance.GetList()
                               .FindAll(x => x.id.packageId == ModParameters.PackageId).GetEnumerator())
                    {
                        while (enumerator5.MoveNext())
                        {
                            var bookXml = enumerator5.Current;
                            bookXml.InnerName = bookDescRoot.bookDescList.Find(x => x.bookID == bookXml.id.id)
                                .bookName;
                            Singleton<BookXmlList>.Instance.GetData(bookXml.id).InnerName = bookXml.InnerName;
                        }
                    }

                    (typeof(BookDescXmlList).GetField("_dictionaryWorkshop", AccessTools.all)
                            .GetValue(Singleton<BookDescXmlList>.Instance) as Dictionary<string, List<BookDesc>>)
                        [ModParameters.PackageId] = bookDescRoot.bookDescList;
                }

            files = new DirectoryInfo(ModParameters.Path + "/Localize/" + ModParameters.Language + "/CharactersName")
                .GetFiles();
            foreach (var t in files)
                using (var stringReader3 = new StringReader(File.ReadAllText(t.FullName)))
                {
                    var charactersNameRoot =
                        (CharactersNameRoot)new XmlSerializer(typeof(CharactersNameRoot)).Deserialize(
                            stringReader3);
                    using (var enumerator3 =
                           Singleton<EnemyUnitClassInfoList>.Instance.GetAllWorkshopData()[ModParameters.PackageId]
                               .GetEnumerator())
                    {
                        while (enumerator3.MoveNext())
                        {
                            var enemy = enumerator3.Current;
                            enemy.name = charactersNameRoot.nameList.Find(x => x.ID == enemy.id.id).name;
                            Singleton<EnemyUnitClassInfoList>.Instance.GetData(enemy.id).name = enemy.name;
                        }
                    }
                }

            files = new DirectoryInfo(ModParameters.Path + "/Localize/" + ModParameters.Language + "/BattleDialog")
                .GetFiles();
            var dialogDictionary =
                (Dictionary<string, BattleDialogRoot>)BattleDialogXmlList.Instance.GetType()
                    .GetField("_dictionary", AccessTools.all)
                    ?.GetValue(BattleDialogXmlList.Instance);
            foreach (var t in files)
                using (var stringReader = new StringReader(File.ReadAllText(t.FullName)))
                {
                    var battleDialogList =
                        ((BattleDialogRoot)new XmlSerializer(typeof(BattleDialogRoot))
                            .Deserialize(stringReader)).characterList;
                    foreach (var dialog in battleDialogList)
                    {
                        dialog.workshopId = ModParameters.PackageId;
                        dialog.bookId = int.Parse(dialog.characterID);
                    }

                    if (!dialogDictionary.ContainsKey("Workshop")) continue;
                    dialogDictionary["Workshop"].characterList
                        .RemoveAll(x => x.workshopId.Equals(ModParameters.PackageId));
                    if (dialogDictionary.ContainsKey("Workshop"))
                    {
                        dialogDictionary["Workshop"].characterList.AddRange(battleDialogList);
                    }
                    else
                    {
                        var battleDialogRoot = new BattleDialogRoot
                        {
                            groupName = "Workshop",
                            characterList = battleDialogList
                        };
                        dialogDictionary.Add("Workshop", battleDialogRoot);
                    }
                }

            files = new DirectoryInfo(ModParameters.Path + "/Localize/" + ModParameters.Language + "/PassiveDesc")
                .GetFiles();
            foreach (var t in files)
                using (var stringReader7 = new StringReader(File.ReadAllText(t.FullName)))
                {
                    var passiveDescRoot =
                        (PassiveDescRoot)new XmlSerializer(typeof(PassiveDescRoot)).Deserialize(stringReader7);
                    using (var enumerator9 = Singleton<PassiveXmlList>.Instance.GetDataAll()
                               .FindAll(x => x.id.packageId == ModParameters.PackageId).GetEnumerator())
                    {
                        while (enumerator9.MoveNext())
                        {
                            var passive = enumerator9.Current;
                            passive.name = passiveDescRoot.descList.Find(x => x.ID == passive.id.id).name;
                            passive.desc = passiveDescRoot.descList.Find(x => x.ID == passive.id.id).desc;
                        }
                    }
                }

            files = new DirectoryInfo(ModParameters.Path + "/Localize/" + ModParameters.Language + "/DropBooks")
                .GetFiles();
            foreach (var t in files)
                using (var stringReader5 = new StringReader(File.ReadAllText(t.FullName)))
                {
                    var charactersNameRoot2 =
                        (CharactersNameRoot)new XmlSerializer(typeof(CharactersNameRoot)).Deserialize(
                            stringReader5);
                    using (var enumerator6 =
                           Singleton<DropBookXmlList>.Instance.GetAllWorkshopData()[ModParameters.PackageId]
                               .GetEnumerator())
                    {
                        while (enumerator6.MoveNext())
                        {
                            var dropBook = enumerator6.Current;
                            dropBook.workshopName =
                                charactersNameRoot2.nameList.Find(x => x.ID == dropBook.id.id).name;
                        }
                    }

                    using (var enumerator7 = Singleton<DropBookXmlList>.Instance.GetList()
                               .FindAll(x => x.id.packageId == ModParameters.PackageId).GetEnumerator())
                    {
                        while (enumerator7.MoveNext())
                        {
                            var dropBook = enumerator7.Current;
                            dropBook.workshopName =
                                charactersNameRoot2.nameList.Find(x => x.ID == dropBook.id.id).name;
                            Singleton<DropBookXmlList>.Instance.GetData(dropBook.id).workshopName =
                                dropBook.workshopName;
                        }
                    }
                }

            files = new DirectoryInfo(ModParameters.Path + "/Localize/" + ModParameters.Language + "/StageName")
                .GetFiles();
            foreach (var t in files)
                using (var stringReader6 = new StringReader(File.ReadAllText(t.FullName)))
                {
                    var charactersNameRoot3 =
                        (CharactersNameRoot)new XmlSerializer(typeof(CharactersNameRoot)).Deserialize(
                            stringReader6);
                    using (var enumerator8 =
                           Singleton<StageClassInfoList>.Instance.GetAllWorkshopData()[ModParameters.PackageId]
                               .GetEnumerator())
                    {
                        while (enumerator8.MoveNext())
                        {
                            var stage = enumerator8.Current;
                            stage.stageName = charactersNameRoot3.nameList.Find(x => x.ID == stage.id.id).name;
                        }
                    }
                }
        }

        public static void RemoveError()
        {
            var list = new List<string>();
            var list2 = new List<string>();
            list.Add("0Harmony");
            list.Add("Mono.Cecil");
            list.Add("MonoMod.Utils");
            list.Add("MonoMod.RuntimeDetour");
            using (var enumerator = Singleton<ModContentManager>.Instance.GetErrorLogs().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var errorLog = enumerator.Current;
                    if (list.Exists(x => errorLog.Contains(x))) list2.Add(errorLog);
                }
            }

            foreach (var item in list2) Singleton<ModContentManager>.Instance.GetErrorLogs().Remove(item);
        }

        public static void GetArtWorks(DirectoryInfo dir)
        {
            if (dir.GetDirectories().Length != 0)
            {
                var directories = dir.GetDirectories();
                foreach (var t in directories) GetArtWorks(t);
            }

            foreach (var fileInfo in dir.GetFiles())
            {
                var texture2D = new Texture2D(2, 2);
                texture2D.LoadImage(File.ReadAllBytes(fileInfo.FullName));
                var value = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height),
                    new Vector2(0f, 0f));
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                ModParameters.ArtWorks[fileNameWithoutExtension] = value;
            }
        }

        public static void ChangePassiveItem()
        {
            foreach (var passive in Singleton<PassiveXmlList>.Instance.GetDataAll().Where(passive =>
                         passive.id.packageId == ModParameters.PackageId &&
                         ModParameters.UntransferablePassives.Contains(passive.id.id)))
                passive.CanGivePassive = false;
        }
        public static void ChangeCardItem(ItemXmlDataList instance)
        {
            var dictionary = (Dictionary<LorId, DiceCardXmlInfo>)instance.GetType()
                .GetField("_cardInfoTable", AccessTools.all).GetValue(instance);
            var list = (List<DiceCardXmlInfo>)instance.GetType()
                .GetField("_cardInfoList", AccessTools.all).GetValue(instance);
            foreach (var item in dictionary.Where(x => string.IsNullOrEmpty(x.Key.packageId)).Where(item => ModParameters.NoInventoryCardList.Contains(item.Key.id)).ToList())
            {
                SetCustomCardOption(CardOption.NoInventory, item.Key, false, ref dictionary, ref list);
            }
        }
        private static void SetCustomCardOption(CardOption option, LorId id, bool keywordsRequired,
            ref Dictionary<LorId, DiceCardXmlInfo> cardDictionary, ref List<DiceCardXmlInfo> cardXmlList)
        {
            var diceCardXmlInfo2 = CardOptionChange(cardDictionary[id], new List<CardOption> { option });
            cardDictionary[id] = diceCardXmlInfo2;
            cardXmlList.Add(diceCardXmlInfo2);
        }
        private static DiceCardXmlInfo CardOptionChange(DiceCardXmlInfo cardXml, List<CardOption> option)
        {
            return new DiceCardXmlInfo(cardXml.id)
            {
                workshopID = cardXml.workshopID,
                workshopName = cardXml.workshopName,
                Artwork = cardXml.Artwork,
                Chapter = cardXml.Chapter,
                category = cardXml.category,
                DiceBehaviourList = cardXml.DiceBehaviourList,
                _textId = cardXml._textId,
                optionList = option.Any() ? option : cardXml.optionList,
                Priority = cardXml.Priority,
                Rarity = cardXml.Rarity,
                Script = cardXml.Script,
                ScriptDesc = cardXml.ScriptDesc,
                Spec = cardXml.Spec,
                SpecialEffect = cardXml.SpecialEffect,
                SkinChange = cardXml.SkinChange,
                SkinChangeType = cardXml.SkinChangeType,
                SkinHeight = cardXml.SkinHeight,
                MapChange =  cardXml.MapChange,
                PriorityScript = cardXml.PriorityScript,
                Keywords = cardXml.Keywords
            };
        }
        public static void SetOperationPanel(UIOriginEquipPageSlot instance,
            UICustomGraphicObject button_Equip, TextMeshProUGUI txt_equipButton, BookModel bookDataModel)
        {
            if (bookDataModel.ClassInfo.id.packageId != ModParameters.PackageId) return;
            if (instance.BookDataModel == null || instance.BookDataModel.owner != null) return;
            var currentUnit = UI.UIController.Instance.CurrentUnit;
            if (currentUnit == null || !IsLockedCharacter(currentUnit) ||
                !ModParameters.DynamicNames.ContainsKey(bookDataModel.ClassInfo.id.id)) return;
            button_Equip.interactable = true;
            txt_equipButton.text = TextDataModel.GetText("ui_bookinventory_equipbook", Array.Empty<object>());
        }

        private static bool IsLockedCharacter(UnitDataModel unitData)
            => unitData.isSephirah && (unitData.OwnerSephirah == SephirahType.Binah || unitData.OwnerSephirah == SephirahType.Keter);
        public static void SetBooksData(UIOriginEquipPageList instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            if (storyKey.workshopId != ModParameters.PackageId) return;
            var textMeshProUGUI = (TextMeshProUGUI)instance.GetType().GetField("txt_StoryName", AccessTools.all)
                .GetValue(instance);
            if (books.Count < 0) return;
            textMeshProUGUI.text = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("ModName_Re21341")).Value
                .Name;
        }
        public static void GetThumbSprite(LorId bookId, ref Sprite result)
        {
            if (bookId.packageId != ModParameters.PackageId) return;
            var sprite = ModParameters.SpritePreviewChange.FirstOrDefault(x => x.Value.Contains(bookId.id));
            if (!string.IsNullOrEmpty(sprite.Key) && sprite.Value.Any())
            {
                result = ModParameters.ArtWorks[sprite.Key];
                return;
            }

            var defaultSprite =
                ModParameters.DefaultSpritePreviewChange.FirstOrDefault(x => x.Value.Contains(bookId.id));
            if (!string.IsNullOrEmpty(defaultSprite.Key) && defaultSprite.Value.Any())
                result = Resources.Load<Sprite>(defaultSprite.Key);
        }
    }
}