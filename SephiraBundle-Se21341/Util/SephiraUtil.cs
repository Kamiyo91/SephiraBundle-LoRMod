using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HarmonyLib;
using LOR_XML;
using Mod;
using SephiraBundle_Se21341.Models;
using UnityEngine;

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
            ModParameters.NameTexts.Clear();
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
                            ModParameters.NameTexts.Add(enemy.id.id, enemy.name);
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
        }
        public static void RemoveError()
        {
            var list = new List<string>();
            var list2 = new List<string>();
            list.Add("0Harmony");
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
    }
}
