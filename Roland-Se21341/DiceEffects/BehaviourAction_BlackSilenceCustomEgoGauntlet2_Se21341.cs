using System.Collections.Generic;

namespace SephiraModInit.Roland_Se21341.DiceEffects
{
    public class BehaviourAction_BlackSilenceCustomEgoGauntlet2_Se21341 : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(
            ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            if (self.result != Result.Win || self.data.actionType != ActionType.Atk ||
                opponent.behaviourResultData == null || opponent.behaviourResultData.IsFarAtk())
                return base.GetMovingAction(ref self, ref opponent);
            var list = new List<RencounterManager.MovingAction>();
            var movingAction =
                new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.MoveForward, 30f, false, 0.9f);
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
            new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.Stop, 0f, true, 0.1f)
                .SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
            list.Add(movingAction);
            opponent.infoList.Add(
                new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, false, 0.5f));
            return list;
        }
    }
}