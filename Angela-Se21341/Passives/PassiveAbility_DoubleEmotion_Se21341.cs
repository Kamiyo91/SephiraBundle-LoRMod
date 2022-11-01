﻿using System.Linq;

namespace SephiraModInit.Angela_Se21341.Passives
{
    public class PassiveAbility_DoubleEmotion_Se21341 : PassiveAbilityBase
    {
        public override void OnDie()
        {
            RemoveBuffAndAuraToAll();
        }

        private void RemoveBuffAndAuraToAll()
        {
            foreach (var battleUnitModel in BattleObjectManager.instance.GetAliveList(owner.faction)
                         .Where(x => x != owner))
            {
                if (battleUnitModel.bufListDetail.GetActivatedBufList()
                        .Find(x => x is BattleUnitBuf_KeterFinal_LibrarianAura) is
                    BattleUnitBuf_KeterFinal_LibrarianAura
                    bufAura)
                    bufAura.Destroy();
                battleUnitModel.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_KeterFinal_DoubleEmotion));
                battleUnitModel.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_KeterFinal_LibrarianAura));
            }
        }

        public override void OnRoundStart()
        {
            GiveBuf();
        }

        private void GiveBuf()
        {
            foreach (var battleUnitModel in BattleObjectManager.instance.GetAliveList(owner.faction)
                         .Where(battleUnitModel =>
                             battleUnitModel.bufListDetail.GetActivatedBuf(KeywordBuf.KeterFinal_DoubleEmotion) ==
                             null))
            {
                battleUnitModel.bufListDetail.AddBuf(new BattleUnitBuf_KeterFinal_DoubleEmotion());
                battleUnitModel.bufListDetail.AddBuf(new BattleUnitBuf_KeterFinal_LibrarianAura());
            }
        }
    }
}