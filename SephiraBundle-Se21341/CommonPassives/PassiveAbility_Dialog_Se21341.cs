﻿using System.Linq;
using SephiraBundle_Se21341.Models;

namespace SephiraBundle_Se21341.CommonPassives
{
    public class PassiveAbility_Dialog_Se21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            Hide();
            if (owner.UnitData.unitData.bookItem.ClassInfo.id.packageId != ModParameters.PackageId) return;
            if (ModParameters.DialogList.Keys.Contains(owner.UnitData.unitData.bookItem.ClassInfo.id.id))
                owner.UnitData.unitData.InitBattleDialogByDefaultBook(ModParameters.DialogList
                    .FirstOrDefault(x => x.Key == owner.UnitData.unitData.bookItem.ClassInfo.id.id).Value);
        }
    }
}