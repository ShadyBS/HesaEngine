using System;
using System.Linq;
using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Olaf.SpellManager;
using static Olaf.MenuManager;
using static Olaf.DarkPrediction;

namespace Olaf.Modes
{
    public static class Killsteal
    {
        public static void DoKs()
        {
            var q = Q.IsReady() && KillstealMenu.GetCheckbox("useQ");
            var e = E.IsReady() && KillstealMenu.GetCheckbox("useE");
            var ignite = KillstealMenu.GetCheckbox("useIgnite");

            if (ignite)
                Functions.UseIgnite();

			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(E.Range) && !x.IsDead && !x.IsZombie))
			{
				if (e && E.GetDamage(enemy) >= enemy.Health)
					E.CastOnUnit(enemy);
			}
			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(Q.Range) && !x.IsDead && !x.IsZombie))
			{
				if (q && Q.GetDamage(enemy) >= enemy.Health)
				{
					var location = LinearPrediction(ObjectManager.Player.Position, Q, (AIHeroClient)enemy);
					if (enemy != null && (enemy.Distance(ObjectManager.Me) < HarassMenu.GetSlider("maxQ")) && location != DarkPrediction.empt)
					{
						Q.Cast(location);
					}
				}
			}
        }
    }
}
