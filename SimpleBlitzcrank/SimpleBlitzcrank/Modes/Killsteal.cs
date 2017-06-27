using System;
using System.Linq;
using HesaEngine.SDK;
using static SimpleTemplate.SpellManager;
using static SimpleTemplate.MenuManager;

namespace SimpleTemplate.Modes
{
    public static class Killsteal
    {
        public static void DoKs()
        {
            var q = Q.IsReady() && KillstealMenu.GetCheckbox("useQ");
            var r = R.IsReady() && KillstealMenu.GetCheckbox("useR");
            var ignite = KillstealMenu.GetCheckbox("useIgnite");

            if (ignite)
                Functions.UseIgnite();

            foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x=> x.IsValidTarget(Q.Range) && !x.IsDead && !x.IsZombie))
            {
                if (q && Q.GetDamage(enemy) >= enemy.Health)
                    Q.CastIfHitchanceEquals(enemy, HitChance.VeryHigh);
            }
			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(R.Range) && !x.IsDead && !x.IsZombie))
			{
				if (q && R.GetDamage(enemy) >= enemy.Health)
					R.Cast();
			}
		}
    }
}
