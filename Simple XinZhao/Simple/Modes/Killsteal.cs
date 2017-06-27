using System.Linq;
using HesaEngine.SDK;
using static Simple.SpellManager;
using static Simple.MenuManager;

namespace Simple.Modes
{
    public static class Killsteal
    {
        public static void DoKs()
        {
            var e = E.IsReady() && KillstealMenu.GetCheckbox("useE");
			var r = R.IsReady() && KillstealMenu.GetCheckbox("useR");
			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x=> x.IsValidTarget(E.Range) && !x.IsDead && !x.IsZombie))
            {
                if (e && E.GetDamage(enemy) >= enemy.Health)
                    E.CastOnUnit(enemy);
            }
			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(R.Range) && !x.IsDead && !x.IsZombie))
			{
				if (r && R.GetDamage(enemy) >= enemy.Health)
					R.Cast();
			}
		}
    }
}
