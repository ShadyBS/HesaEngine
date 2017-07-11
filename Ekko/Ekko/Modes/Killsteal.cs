using System;
using System.Linq;
using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using HesaEngine.SDK.Enums;
using static Ekko.SpellManager;
using static Ekko.MenuManager;

namespace Ekko.Modes
{
    public static class Killsteal
    {
        public static void DoKs()
        {
            var q = Q.IsReady() && KillstealMenu.GetCheckbox("useQ");
            var e = E.IsReady() && KillstealMenu.GetCheckbox("useE");
            var r = R.IsReady() && KillstealMenu.GetCheckbox("useR");
            var ignite = KillstealMenu.GetCheckbox("useIgnite");

            if (ignite)
                Functions.UseIgnite();

			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(E.Range + 325) && !x.IsDead && !x.IsZombie))
			{
				if (e && E.GetDamage(enemy) >= enemy.Health)
				{
					E.Cast(enemy.Position);
					Orbwalker.ResetAutoAttackTimer();
					Core.DelayAction(() => ObjectManager.Player.IssueOrder(HesaEngine.SDK.Enums.GameObjectOrder.AttackUnit, enemy), 100);
					return;
				}
			}
			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(Q.Range) && !x.IsDead && !x.IsZombie))
			{
				if (q && Q.GetDamage(enemy) >= enemy.Health)
					Q.CastIfHitchanceEquals(enemy, HitChance.VeryHigh);
			}
			if (Main.EkkoRGhost != null)
			{
				foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.Distance(Main.EkkoRGhost) <= R.Range && !x.IsDead && !x.IsZombie))
				{
					if (r && enemy.Distance(Main.EkkoRGhost.Position) <= R.Range && !Functions.HasSpellShield(enemy) && !Functions.IsNotKillable(enemy) && R.GetDamage(enemy) >= enemy.Health)
						R.Cast();
				}
			}
		}
    }
}
