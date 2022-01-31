using System.Collections.Generic;
using UnityEngine;

namespace Roland_Se21341.DiceEffects
{
    public class BehaviourAction_BlackSilence_SpecialDurandal_Ego_Se21341 : BehaviourActionBase
    {
        private bool _bMoveAfterHammer;

        private bool _bMovedGauntlet1;

        private bool _bMovedLance;

        private bool _bMovedShortSword;

        private bool _bMoveDualWield;

        private bool _bMoveDurandal1;

        private bool _bMoveDurandal2;

        private bool _bMoveGauntler2;

        private BattleUnitModel _target;

        public override bool IsMovable()
        {
            return false;
        }

        public override bool IsOpponentMovable()
        {
            return false;
        }

        public override List<RencounterManager.MovingAction> GetMovingAction(
            ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            var flag = false;
            if (opponent.behaviourResultData != null) flag = opponent.behaviourResultData.IsFarAtk();
            if (self.result != Result.Win || flag) return base.GetMovingAction(ref self, ref opponent);
            _self = self.view.model;
            _target = opponent.view.model;
            var list = new List<RencounterManager.MovingAction>();
            var list2 = new List<RencounterManager.MovingAction>();
            if (opponent.infoList.Count > 0) opponent.infoList.Clear();
            SetRevolver(list, list2);
            SetLance(list, list2);
            SetHammer(list, list2);
            SetLongSword(list, list2);
            SetGauntlet(list, list2);
            SetShortSword(list, list2);
            SetAxe(list, list2);
            SetMace(list, list2);
            SetGreatSword(list, list2);
            SetDualWield(list, list2);
            SetShotgun(list, list2);
            SetDurandal(list, list2);
            opponent.infoList = list2;
            return list;
        }

        private static void SetRevolver(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction = new RencounterManager.MovingAction(ActionDetail.S1, CharMoveState.Stop, 1f, true, 0.5f)
            {
                customEffectRes = "BlackSilence_4th_Revolver_S1"
            };
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NOT_PRINT, EffectTiming.WITHOUT_DMGTEXT);
            self.Add(movingAction);
            var movingAction2 = new RencounterManager.MovingAction(ActionDetail.S1, CharMoveState.Stop, 1f, true, 0.5f)
            {
                customEffectRes = "BlackSilence_4th_Revolver_S1"
            };
            movingAction2.SetEffectTiming(EffectTiming.PRE, EffectTiming.NOT_PRINT, EffectTiming.WITHOUT_DMGTEXT);
            self.Add(movingAction2);
            oppo.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, 1f, true, 0.1f)
            {
                knockbackPower = 5f
            });
            oppo.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, 1f, true, 0.1f)
            {
                knockbackPower = 5f
            });
        }

        private void SetLance(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction =
                new RencounterManager.MovingAction(ActionDetail.S11, CharMoveState.Custom, 1f, false, 0f);
            movingAction.SetCustomMoving(MoveLance);
            movingAction.customEffectRes = "BlackSilence_4th_Lance_S11";
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            var movingAction2 =
                new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.Stop, 0f, false, 0.3f);
            movingAction2.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
            self.Add(movingAction);
            self.Add(movingAction2);
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.1f);
            oppo.Add(item);
            var item2 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.1f);
            oppo.Add(item2);
        }

        private void SetHammer(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction =
                new RencounterManager.MovingAction(ActionDetail.S6, CharMoveState.MoveForward, 1f, true, 0.6f)
                {
                    customEffectRes = "BlackSilence_4th_Hammer_S6"
                };
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            var movingAction2 =
                new RencounterManager.MovingAction(ActionDetail.Move, CharMoveState.Custom, 6f, true, 0.1f);
            movingAction2.SetCustomMoving(MoveAfterHammer);
            movingAction2.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
            self.Add(movingAction);
            self.Add(movingAction2);
            oppo.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, 1f, true, 0.6f)
            {
                knockbackPower = 5f
            });
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.1f);
            oppo.Add(item);
        }

        private static void SetLongSword(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction = new RencounterManager.MovingAction(ActionDetail.S12, CharMoveState.Stop, 0f, true, 0.2f);
            movingAction.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
            var movingAction2 = new RencounterManager.MovingAction(ActionDetail.S2, CharMoveState.Stop, 0f, true, 0.3f)
            {
                customEffectRes = "BlackSilence_4th_LongSword_S2"
            };
            movingAction2.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            var movingAction3 = new RencounterManager.MovingAction(ActionDetail.S3, CharMoveState.Stop, 0f, true, 0.4f);
            movingAction3.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
            self.Add(movingAction2);
            self.Add(movingAction3);
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.2f);
            oppo.Add(item);
            var item2 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.3f);
            oppo.Add(item2);
            var item3 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.4f);
            oppo.Add(item3);
        }

        private void SetGauntlet(ICollection<RencounterManager.MovingAction> self,
            List<RencounterManager.MovingAction> oppo)
        {
            var movingAction =
                new RencounterManager.MovingAction(ActionDetail.S3, CharMoveState.Custom, 1f, false, 0.2f);
            movingAction.SetCustomMoving(MoveGauntlet1);
            movingAction.customEffectRes = "BlackSilence_4th_Gauntlet_S3";
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            var movingAction2 =
                new RencounterManager.MovingAction(ActionDetail.S3, CharMoveState.Custom, 1f, false, 0.2f);
            movingAction2.SetCustomMoving(MoveGauntlet2);
            movingAction2.customEffectRes = "BlackSilence_4th_Gauntlet_S3";
            movingAction2.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            self.Add(movingAction);
            self.Add(movingAction2);
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.2f);
            oppo.Add(item);
            var item2 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.2f);
            oppo.Add(item2);
        }

        private void SetShortSword(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction =
                new RencounterManager.MovingAction(ActionDetail.S4, CharMoveState.Custom, 1f, false, 0.5f);
            movingAction.SetCustomMoving(MoveShortSword);
            movingAction.customEffectRes = "BlackSilence_4th_ShortSword_S4";
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            self.Add(movingAction);
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.5f);
            oppo.Add(item);
        }

        private static void SetAxe(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction = new RencounterManager.MovingAction(ActionDetail.S5, CharMoveState.Stop, 1f, true, 0.5f)
            {
                customEffectRes = "BlackSilence_4th_MaceAxe_S5"
            };
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            self.Add(movingAction);
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.1f);
            oppo.Add(item);
        }

        private static void SetMace(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction = new RencounterManager.MovingAction(ActionDetail.S5, CharMoveState.Stop, 1f, true, 0.5f)
            {
                customEffectRes = "BlackSilence_4th_MaceAxe_S5"
            };
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            self.Add(movingAction);
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.1f);
            oppo.Add(item);
        }

        private static void SetGreatSword(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction = new RencounterManager.MovingAction(ActionDetail.S7, CharMoveState.Stop, 1f, true, 1f)
            {
                customEffectRes = "BlackSilence_4th_GreatSword_S7"
            };
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            self.Add(movingAction);
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.1f);
            oppo.Add(item);
        }

        private void SetDualWield(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction =
                new RencounterManager.MovingAction(ActionDetail.S9, CharMoveState.Custom, 0f, false, 0.01f);
            movingAction.SetCustomMoving(MoveDualWield);
            movingAction.customEffectRes = "BlackSilence_4th_DualWield1_S9";
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            var movingAction2 =
                new RencounterManager.MovingAction(ActionDetail.S10, CharMoveState.Stop, 0f, false, 0.5f);
            movingAction2.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
            var movingAction3 =
                new RencounterManager.MovingAction(ActionDetail.Default, CharMoveState.Stop, 0f, true, 0.01f);
            movingAction3.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
            self.Add(movingAction);
            self.Add(movingAction2);
            self.Add(movingAction3);
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.01f);
            oppo.Add(item);
            var item2 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.5f);
            oppo.Add(item2);
            var item3 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.01f);
            oppo.Add(item3);
        }

        private static void SetShotgun(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction =
                new RencounterManager.MovingAction(ActionDetail.S8, CharMoveState.MoveBack, 3f, true, 0.3f)
                {
                    customEffectRes = "BlackSilence_4th_Shotgun_S8"
                };
            movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            self.Add(movingAction);
            oppo.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, 1f, true, 0.3f)
            {
                knockbackPower = 15f
            });
        }

        private void SetDurandal(ICollection<RencounterManager.MovingAction> self,
            ICollection<RencounterManager.MovingAction> oppo)
        {
            var movingAction =
                new RencounterManager.MovingAction(ActionDetail.Move, CharMoveState.Custom, 0f, true, 0.01f);
            movingAction.SetCustomMoving(MoveDurandal1);
            var movingAction2 =
                new RencounterManager.MovingAction(ActionDetail.Slash2, CharMoveState.Stop, 0f, true, 0.5f)
                {
                    customEffectRes = "BS4DurandalDown_J2"
                };
            movingAction2.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.WITHOUT_DMGTEXT);
            var movingAction3 =
                new RencounterManager.MovingAction(ActionDetail.Slash, CharMoveState.Custom, 0f, false, 0.01f);
            movingAction3.SetCustomMoving(MoveDurandal2);
            movingAction3.customEffectRes = " BS4DurandalUp_J";
            movingAction3.SetEffectTiming(EffectTiming.PRE, EffectTiming.NOT_PRINT, EffectTiming.WITHOUT_DMGTEXT);
            var movingAction4 =
                new RencounterManager.MovingAction(ActionDetail.S12, CharMoveState.Stop, 0f, false, 0.5f);
            movingAction4.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
            var movingAction5 = new RencounterManager.MovingAction(ActionDetail.S7, CharMoveState.Stop, 0f, true, 1f)
            {
                customEffectRes = "BlackSilence_4th_GreatSword_S7"
            };
            movingAction5.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
            self.Add(movingAction);
            self.Add(movingAction2);
            self.Add(movingAction3);
            self.Add(movingAction4);
            self.Add(movingAction5);
            var item = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.01f);
            oppo.Add(item);
            var item2 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.5f);
            oppo.Add(item2);
            var item3 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.01f);
            oppo.Add(item3);
            var item4 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0.5f);
            oppo.Add(item4);
            var item5 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 1f);
            oppo.Add(item5);
        }

        private bool MoveLance(float deltaTime)
        {
            if (_target == null) return true;
            var result = false;
            if (!_bMovedLance)
            {
                var worldPosition = _target.view.WorldPosition;
                var x = _target.view.transform.localScale.x;
                var num = SingletonBehavior<HexagonalMapManager>.Instance.tileSize * x + 6f;
                var num2 = 1;
                if (_self.view.WorldPosition.x < _target.view.WorldPosition.x) num2 = -1;
                var pos = worldPosition - new Vector3(num2 * num, 0f, 0f);
                _self.moveDetail.Move(pos, 400f, false, true);
                _bMovedLance = true;
            }
            else if (_self.moveDetail.isArrived)
            {
                result = true;
            }

            return result;
        }

        private bool MoveAfterHammer(float deltaTime)
        {
            var result = false;
            if (!_bMoveAfterHammer)
            {
                var worldPosition = _target.view.WorldPosition;
                var x = _target.view.transform.localScale.x;
                var num = SingletonBehavior<HexagonalMapManager>.Instance.tileSize * x + 6f;
                var num2 = 1;
                if (_self.view.WorldPosition.x < _target.view.WorldPosition.x) num2 = -1;
                var pos = worldPosition + new Vector3(num2 * num, 0f, 0f);
                _self.moveDetail.Move(pos, 150f);
                _bMoveAfterHammer = true;
            }
            else if (_self.moveDetail.isArrived)
            {
                result = true;
            }

            return result;
        }

        private bool MoveGauntlet1(float deltaTime)
        {
            if (_target == null) return true;
            var result = false;
            if (!_bMovedGauntlet1)
            {
                var worldPosition = _target.view.WorldPosition;
                var x = _target.view.transform.localScale.x;
                var num = SingletonBehavior<HexagonalMapManager>.Instance.tileSize * x + 6f;
                var num2 = 1;
                if (_self.view.WorldPosition.x < _target.view.WorldPosition.x) num2 = -1;
                var pos = worldPosition - new Vector3(num2 * num, 0f, 0f);
                _self.moveDetail.Move(pos, 250f, false);
                _bMovedGauntlet1 = true;
            }
            else if (_self.moveDetail.isArrived)
            {
                result = true;
            }

            return result;
        }

        private bool MoveGauntlet2(float deltaTime)
        {
            if (_target == null) return true;
            var result = false;
            if (!_bMoveGauntler2)
            {
                var worldPosition = _target.view.WorldPosition;
                var x = _target.view.transform.localScale.x;
                var num = SingletonBehavior<HexagonalMapManager>.Instance.tileSize * x + 6f;
                var num2 = 1;
                if (_self.view.WorldPosition.x < _target.view.WorldPosition.x) num2 = -1;
                var pos = worldPosition - new Vector3(num2 * num, 0f, 0f);
                _self.moveDetail.Move(pos, 250f, false);
                _bMoveGauntler2 = true;
            }
            else if (_self.moveDetail.isArrived)
            {
                result = true;
            }

            return result;
        }

        private bool MoveShortSword(float deltaTime)
        {
            if (_target == null) return true;
            var result = false;
            if (!_bMovedShortSword)
            {
                var worldPosition = _target.view.WorldPosition;
                var x = _target.view.transform.localScale.x;
                var num = SingletonBehavior<HexagonalMapManager>.Instance.tileSize * x + 6f;
                var num2 = 1;
                if (_self.view.WorldPosition.x < _target.view.WorldPosition.x) num2 = -1;
                var pos = worldPosition - new Vector3(num2 * num, 0f, 0f);
                _self.moveDetail.Move(pos, 250f, false);
                _bMovedShortSword = true;
            }
            else if (_self.moveDetail.isArrived)
            {
                result = true;
            }

            return result;
        }

        private bool MoveDualWield(float deltaTime)
        {
            if (_target == null) return true;
            var result = false;
            if (!_bMoveDualWield)
            {
                var worldPosition = _target.view.WorldPosition;
                var x = _target.view.transform.localScale.x;
                var num = SingletonBehavior<HexagonalMapManager>.Instance.tileSize * x + 12f;
                var num2 = 1;
                if (_self.view.WorldPosition.x < _target.view.WorldPosition.x) num2 = -1;
                var pos = worldPosition - new Vector3(num2 * num, 0f, 0f);
                _self.moveDetail.Move(pos, 250f, false);
                _bMoveDualWield = true;
            }
            else if (_self.moveDetail.isArrived)
            {
                result = true;
            }

            return result;
        }

        private bool MoveDurandal1(float deltaTime)
        {
            if (_target == null) return true;
            var result = false;
            if (!_bMoveDurandal1)
            {
                var worldPosition = _target.view.WorldPosition;
                var x = _target.view.transform.localScale.x;
                var num = SingletonBehavior<HexagonalMapManager>.Instance.tileSize * x + 6f;
                var num2 = 1;
                if (_self.view.WorldPosition.x < _target.view.WorldPosition.x) num2 = -1;
                var pos = worldPosition + new Vector3(num2 * num, 0f, 0f);
                _self.moveDetail.Move(pos, 250f, false);
                _bMoveDurandal1 = true;
            }
            else if (_self.moveDetail.isArrived)
            {
                result = true;
            }

            return result;
        }

        private bool MoveDurandal2(float deltaTime)
        {
            if (_target == null) return true;
            var result = false;
            if (!_bMoveDurandal2)
            {
                var worldPosition = _target.view.WorldPosition;
                var x = _target.view.transform.localScale.x;
                var num = SingletonBehavior<HexagonalMapManager>.Instance.tileSize * x + 6f;
                var num2 = 1;
                if (_self.view.WorldPosition.x < _target.view.WorldPosition.x) num2 = -1;
                var pos = worldPosition - new Vector3(num2 * num, 0f, 0f);
                _self.moveDetail.Move(pos, 250f, false);
                _bMoveDurandal2 = true;
            }
            else if (_self.moveDetail.isArrived)
            {
                result = true;
            }

            return result;
        }
    }
}