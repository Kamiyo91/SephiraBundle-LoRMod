using System.Linq;
using KamiyoStaticBLL.Models;
using SephiraBundle_Se21341.Models;

namespace SephiraBundle_Se21341.CommonPassives
{
    public class PassiveAbility_Dialog_Se21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            Hide();
            if (owner.UnitData.unitData.bookItem.ClassInfo.id.packageId != SephiraModParameters.PackageId) return;
            if (ModParameters.DialogList.Keys.Contains(owner.UnitData.unitData.bookItem.ClassInfo.id))
                owner.UnitData.unitData.InitBattleDialogByDefaultBook(ModParameters.DialogList
                    .FirstOrDefault(x => x.Key == owner.UnitData.unitData.bookItem.ClassInfo.id).Value);
        }
    }
}