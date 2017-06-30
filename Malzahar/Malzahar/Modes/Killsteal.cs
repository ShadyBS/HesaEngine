using System;
using System.Linq;
using HesaEngine.SDK;
using static Malzahar.SpellManager;
using static Malzahar.MenuManager;

namespace Malzahar.Modes
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

			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(E.Range) && !x.IsDead && !x.IsZombie))
			{
				if (e && E.GetDamage(enemy) >= enemy.Health)
					E.Cast(enemy);
			}
			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(Q.Range) && !x.IsDead && !x.IsZombie))
			{
				if (q && Q.GetDamage(enemy) >= enemy.Health)
					Q.CastIfHitchanceEquals(enemy, HitChance.VeryHigh);
			}
			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x=> x.IsValidTarget(R.Range) && !x.IsDead && !x.IsZombie))
            {
                if (r && !Functions.HasSpellShield(enemy) && !Functions.IsNotKillable(enemy) && R.GetDamage(enemy) >= enemy.Health)
                    R.Cast(enemy);
            }
        }
    }
}
