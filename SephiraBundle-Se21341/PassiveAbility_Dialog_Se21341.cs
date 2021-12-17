using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SephiraBundle_Se21341.Models;
using UnityEngine;

namespace SephiraBundle_Se21341
{
    public class PassiveAbility_Dialog_Se21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            Hide();
            if (owner.UnitData.unitData.bookItem.ClassInfo.id.packageId != ModParameters.PackageId) return;
            if (ModParameters.DialogList.Keys.Contains(owner.UnitData.unitData.bookItem.ClassInfo.id.id))
            {
                owner.UnitData.unitData.InitBattleDialogByDefaultBook(ModParameters.DialogList.FirstOrDefault(x => x.Key == owner.UnitData.unitData.bookItem.ClassInfo.id.id).Value);
            }
        }
    }
}
