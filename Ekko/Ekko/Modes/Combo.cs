using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using HesaEngine.SDK.Enums;
using static HesaEngine.SDK.Utility;
using static Ekko.SpellManager;
using static Ekko.MenuManager;
using System.Linq;

namespace Ekko.Modes
{
    public static class Combo
    {
        public static void DoCombo()
        {
            var q = ComboMenu.GetCheckbox("useQ") && Q.IsReady();
            var w = ComboMenu.GetCheckbox("useW") && W.IsReady();
			var w2 = ComboMenu.GetCheckbox("useW2") && W.IsReady();
			var e = ComboMenu.GetCheckbox("useE") && E.IsReady();
			var r = ComboMenu.GetCheckbox("useR") && E.IsReady();
			var nodivesE = ComboMenu.GetCheckbox("nodiveE");
			var miniR = ComboMenu.GetSlider("minR");

			foreach (var target in ObjectManager.Heroes.Enemies.Where(k => k.IsValidTarget(E.Range + 325) && !k.IsDead && !k.IsZombie))
			{
				if (e)
				{
					if (Main.WArea != null && Main.WArea.Position.CountEnemiesInRange(200) > 0 && Main.WArea.Position.Distance(ObjectManager.Player.ServerPosition) < E.Range)
					{
							E.Cast(Main.WArea.Position);
					}
					if (nodivesE && ObjectManager.Player.Distance(target.Position) >= 175)
					{
						if (!target.IsUnderEnemyTurret())
						{
							E.Cast(target.Position);
							Orbwalker.ResetAutoAttackTimer();
							Core.DelayAction(() => ObjectManager.Player.IssueOrder(HesaEngine.SDK.Enums.GameObjectOrder.AttackUnit, target), 100);
						}
					}
					if (!nodivesE && ObjectManager.Player.Distance(target.Position) >= 175)
					{
						E.Cast(target.Position);
						Orbwalker.ResetAutoAttackTimer();
						Core.DelayAction(() => ObjectManager.Player.IssueOrder(HesaEngine.SDK.Enums.GameObjectOrder.AttackUnit, target), 100);
					}
				}	
			}


			foreach (var target in ObjectManager.Heroes.Enemies.Where(s => s.IsValidTarget(W.Range) && !s.IsDead && !s.IsZombie))
			{
				if (q)
				{
					Q.CastIfHitchanceEquals(target, HitChance.Low);
				}

				if (w || w2)
				{
					if (ComboMenu.GetCheckbox("useW2"))
					{
						W.Cast(target.Position);
					}
					else
					{
							W.CastIfHitchanceEquals(target, HitChance.VeryHigh);
					}
				}
			}

			if (r)
			{
				if (Main.EkkoRGhost != null)
				{
					if (Main.EkkoRGhost.Position.CountEnemiesInRange(R.Range) >= miniR)
					{
							R.Cast();
					}
				}
			}
		} 
    }
}
