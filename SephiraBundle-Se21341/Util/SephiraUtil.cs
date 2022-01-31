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

            files = new DirectoryInfo(ModParameters.Path + "/Localize/" + ModParameters.Language + "/BattlesCards")
                .GetFiles();
            foreach (var t in files)
                using (var stringReader2 = new StringReader(File.ReadAllText(t.FullName)))
                {
                    var battleCardDescRoot =
                        (BattleCardDescRoot)new XmlSerializer(typeof(BattleCardDescRoot)).Deserialize(
                            stringReader2);
                    using (var enumerator =
                           ItemXmlDataList.instance.GetAllWorkshopData()[ModParameters.PackageId].GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            var card = enumerator.Current;
                            card.workshopName = battleCardDescRoot.cardDescList.Find(x => x.cardID == card.id.id)
                                .cardName;
                        }
                    }

                    typeof(ItemXmlDataList).GetField("_cardInfoTable", AccessTools.all)
                        .GetValue(ItemXmlDataList.instance);
                    using (var enumerator2 = ItemXmlDataList.instance.GetCardList()
                               .FindAll(x => x.id.packageId == ModParameters.PackageId).GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            var card = enumerator2.Current;
                            card.workshopName = battleCardDescRoot.cardDescList.Find(x => x.cardID == card.id.id)
                                .cardName;
                            ItemXmlDataList.instance.GetCardItem(card.id).workshopName = card.workshopName;
                        }
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

            var cardAbilityDictionary = typeof(BattleCardAbilityDescXmlList).GetField("_dictionary", AccessTools.all)
                    ?.GetValue(Singleton<BattleCardAbilityDescXmlList>.Instance) as
                Dictionary<string, BattleCardAbilityDesc>;
            files = new DirectoryInfo(ModParameters.Path + "/Localize/" + ModParameters.Language +
                                      "/BattleCardAbilities").GetFiles();
            foreach (var t in files)
                using (var stringReader8 = new StringReader(File.ReadAllText(t.FullName)))
                {
                    foreach (var battleCardAbilityDesc in
                             ((BattleCardAbilityDescRoot)new XmlSerializer(typeof(BattleCardAbilityDescRoot))
                                 .Deserialize(stringReader8)).cardDescList)
                    {
                        cardAbilityDictionary.Remove(battleCardAbilityDesc.id);
                        cardAbilityDictionary.Add(battleCardAbilityDesc.id, battleCardAbilityDesc);
                    }
                }
        }

        public static void RemoveError()
        {
            Singleton<ModContentManager>.Instance.GetErrorLogs().RemoveAll(x => new List<string>
            {
                "0Harmony",
                "Mono.Cecil",
                "MonoMod.RuntimeDetour",
                "MonoMod.Utils"
            }.Exists(y => x.Contains("The same assembly name already exists. : " + y)));
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
            foreach (var item in dictionary.Where(x => string.IsNullOrEmpty(x.Key.packageId))
                         .Where(item => ModParameters.NoInventoryCardList.Contains(item.Key.id))
                         .ToList())
                SetCustomCardOption(CardOption.NoInventory, item.Key, ref dictionary, ref list);
            foreach (var item in dictionary.Where(x => x.Key.packageId == ModParameters.PackageId).ToList())
            {
                if (ModParameters.PersonalCardList.Contains(item.Key.id))
                {
                    SetCustomCardOption(CardOption.Personal, item.Key, ref dictionary, ref list);
                    continue;
                }

                if (ModParameters.EgoPersonalCardList.Contains(item.Key.id))
                    SetCustomCardOption(CardOption.EgoPersonal, item.Key, ref dictionary, ref list);
            }
        }

        private static void SetCustomCardOption(CardOption option, LorId id,
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
                MapChange = cardXml.MapChange,
                PriorityScript = cardXml.PriorityScript,
                Keywords = cardXml.Keywords
            };
        }

        public static void SetOperationPanel(UIOriginEquipPageSlot instance,
            UICustomGraphicObject button_Equip, TextMeshProUGUI txt_equipButton, BookModel bookDataModel)
        {
            if (bookDataModel == null || bookDataModel.ClassInfo.id.packageId != ModParameters.PackageId ||
                instance.BookDataModel == null ||
                instance.BookDataModel.owner != null) return;
            var currentUnit = UI.UIController.Instance.CurrentUnit;
            if (currentUnit == null) return;
            if (!ModParameters.DynamicNames.ContainsKey(bookDataModel.ClassInfo.id.id)) return;
            var mainItem = ModParameters.DynamicNames.FirstOrDefault(x => x.Key == bookDataModel.ClassInfo.id.id);
            if (mainItem.Value.Item2 == currentUnit.OwnerSephirah && !currentUnit.isSephirah)
            {
                button_Equip.interactable = false;
                txt_equipButton.text = TextDataModel.GetText("ui_equippage_notequip", Array.Empty<object>());
                return;
            }

            if (!IsLockedCharacter(currentUnit)) return;
            button_Equip.interactable = true;
            txt_equipButton.text = TextDataModel.GetText("ui_bookinventory_equipbook", Array.Empty<object>());
        }

        private static bool IsLockedCharacter(UnitDataModel unitData)
        {
            return unitData.isSephirah && (unitData.OwnerSephirah == SephirahType.Binah ||
                                           unitData.OwnerSephirah == SephirahType.Keter);
        }

        public static void SetBooksData(UIOriginEquipPageList instance,
            List<BookModel> books, UIStoryKeyData storyKey)
        {
            if (storyKey.workshopId != ModParameters.PackageId) return;
            var textMeshProUGUI = (TextMeshProUGUI)instance.GetType().GetField("txt_StoryName", AccessTools.all)
                .GetValue(instance);
            if (books.Count < 0) return;
            textMeshProUGUI.text = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("ModName_Se21341")).Value
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

        public static void SetEpisodeSlots(UIBookStoryChapterSlot instance, List<UIBookStoryEpisodeSlot> episodeSlots)
        {
            if (instance.chapter != 7) return;
            var uibookStoryEpisodeSlot =
                episodeSlots.Find(x => x.books.Find(y => y.id.packageId == ModParameters.PackageId) != null);
            if (uibookStoryEpisodeSlot == null) return;
            var books = uibookStoryEpisodeSlot.books;
            var uibookStoryEpisodeSlot2 = episodeSlots[episodeSlots.Count - 1];
            books.RemoveAll(x => x.id.packageId == ModParameters.PackageId);
            uibookStoryEpisodeSlot2.Init(instance.chapter, books, instance);
        }

        public static IEnumerable<BattleDiceCardModel> ReloadEgoHandUI(BattleUnitCardsInHandUI instance,
            List<BattleDiceCardUI> cardList, BattleUnitModel unit, List<BattleDiceCardUI> activatedCardList,
            ref float xInt)
        {
            var list = unit.personalEgoDetail.GetHand();
            if (list.Count >= 9) xInt = 65f * 8f / list.Count;
            var num = 0;
            activatedCardList.Clear();
            while (num < list.Count)
            {
                cardList[num].gameObject.SetActive(true);
                cardList[num].SetCard(list[num], Array.Empty<BattleDiceCardUI.Option>());
                cardList[num].SetDefault();
                cardList[num].ResetSiblingIndex();
                activatedCardList.Add(cardList[num]);
                num++;
            }

            for (var i = 0; i < activatedCardList.Count; i++)
            {
                var navigation = default(Navigation);
                navigation.mode = Navigation.Mode.Explicit;
                if (i > 0)
                    navigation.selectOnLeft = activatedCardList[i - 1].selectable;
                else if (activatedCardList.Count >= 2)
                    navigation.selectOnLeft = activatedCardList[activatedCardList.Count - 1].selectable;
                else
                    navigation.selectOnLeft = null;
                if (i < activatedCardList.Count - 1)
                    navigation.selectOnRight = activatedCardList[i + 1].selectable;
                else if (activatedCardList.Count >= 2)
                    navigation.selectOnRight = activatedCardList[0].selectable;
                else
                    navigation.selectOnRight = null;
                activatedCardList[i].selectable.navigation = navigation;
                activatedCardList[i].selectable.parentSelectable = instance.selectablePanel;
            }

            return list;
        }

        public static void PrepareBlackSilenceDeck(BattleUnitModel owner)
        {
            var furiosoCard = owner.personalEgoDetail.GetCardAll()
                .FirstOrDefault(x => x.GetID().IsBasic() && x.GetID().id == 702010);
            if (furiosoCard != null)
            {
                furiosoCard.CopySelf();
                var furiosoDice = furiosoCard.GetBehaviourList().FirstOrDefault();
                if (furiosoDice != null)
                {
                    furiosoDice.MotionDetail = MotionDetail.J2;
                    furiosoDice.EffectRes = "BS4DurandalDown_J2";
                    furiosoDice.ActionScript = "BlackSilence_SpecialDurandal_Ego_Se21341";
                }
            }

            foreach (var card in owner.allyCardDetail.GetAllDeck())
            {
                card.CopySelf();
                int num;
                switch (card.GetID().id)
                {
                    case 702001:
                        num = 0;
                        foreach (var dice in card.GetBehaviourList())
                        {
                            switch (num)
                            {
                                case 0:
                                    dice.MotionDetail = MotionDetail.S11;
                                    dice.EffectRes = "BlackSilence_4th_Lance_S11";
                                    break;
                                case 1:
                                    dice.MotionDetail = MotionDetail.S11;
                                    dice.EffectRes = "BlackSilence_4th_Lance_S11";
                                    break;
                            }

                            num++;
                        }

                        break;
                    case 702002:
                        num = 0;
                        foreach (var dice in card.GetBehaviourList())
                        {
                            switch (num)
                            {
                                case 0:
                                    dice.MotionDetail = MotionDetail.S7;
                                    dice.EffectRes = "BlackSilence_4th_GreatSword_S7";
                                    break;
                            }

                            num++;
                        }

                        break;
                    case 702003:
                        num = 0;
                        foreach (var dice in card.GetBehaviourList())
                        {
                            switch (num)
                            {
                                case 0:
                                    dice.MotionDetail = MotionDetail.S5;
                                    dice.EffectRes = "BlackSilence_4th_MaceAxe_S5";
                                    break;
                                case 1:
                                    dice.MotionDetail = MotionDetail.S5;
                                    dice.EffectRes = "BlackSilence_4th_MaceAxe_S5";
                                    break;
                            }

                            num++;
                        }

                        break;
                    case 702004:
                        num = 0;
                        foreach (var dice in card.GetBehaviourList())
                        {
                            switch (num)
                            {
                                case 0:
                                    dice.MotionDetail = MotionDetail.S6;
                                    dice.EffectRes = "BlackSilence_4th_Hammer_S6";
                                    break;
                            }

                            num++;
                        }

                        break;
                    case 702005:
                        num = 0;
                        foreach (var dice in card.GetBehaviourList())
                        {
                            switch (num)
                            {
                                case 0:
                                    dice.MotionDetail = MotionDetail.S2;
                                    dice.ActionScript = "";
                                    dice.EffectRes = "BlackSilence_4th_LongSword_S2";
                                    break;
                            }

                            num++;
                        }

                        break;
                    case 702006:
                        num = 0;
                        foreach (var dice in card.GetBehaviourList())
                        {
                            switch (num)
                            {
                                case 0:
                                    dice.MotionDetail = MotionDetail.S2;
                                    dice.EffectRes = "BlackSilence_4th_Gauntlet_S3";
                                    break;
                                case 1:
                                    dice.MotionDetail = MotionDetail.S2;
                                    dice.EffectRes = "BlackSilence_4th_Gauntlet_S3";
                                    break;
                                case 2:
                                    dice.MotionDetail = MotionDetail.S2;
                                    dice.EffectRes = "BlackSilence_4th_LongSword_S2";
                                    break;
                            }

                            num++;
                        }

                        break;
                    case 702007:
                        num = 0;
                        foreach (var dice in card.GetBehaviourList())
                        {
                            switch (num)
                            {
                                case 1:
                                    dice.MotionDetail = MotionDetail.S9;
                                    dice.EffectRes = "BlackSilence_4th_DualWield1_S9";
                                    break;
                                case 2:
                                    dice.MotionDetail = MotionDetail.S10;
                                    dice.EffectRes = "BlackSilence_4th_DualWield2_S10";
                                    break;
                            }

                            num++;
                        }

                        break;
                    case 702008:
                        num = 0;
                        foreach (var dice in card.GetBehaviourList())
                        {
                            switch (num)
                            {
                                case 1:
                                    dice.MotionDetail = MotionDetail.S1;
                                    break;
                                case 2:
                                    dice.MotionDetail = MotionDetail.S8;
                                    dice.EffectRes = "BlackSilence_4th_Shotgun_S8";
                                    break;
                            }

                            num++;
                        }

                        break;
                    case 702009:
                        num = 0;
                        foreach (var dice in card.GetBehaviourList())
                        {
                            switch (num)
                            {
                                case 0:
                                    dice.MotionDetail = MotionDetail.J;
                                    dice.EffectRes = "BS4DurandalUp_J";
                                    break;
                                case 1:
                                    dice.MotionDetail = MotionDetail.J2;
                                    dice.EffectRes = "BS4DurandalDown_J2";
                                    break;
                            }

                            num++;
                        }

                        break;
                }
            }
        }
    }
}