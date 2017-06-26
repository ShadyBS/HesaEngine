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
			var q = Q.IsReady() && KillstealMenu.GetCheckbox("useQ");
            foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x=> x.IsValidTarget(Q.Range) && !x.IsDead && !x.IsZombie))
            {
				if (q && Q.GetDamage(enemy) >= enemy.Health)
					Q.CastOnUnit(enemy);
            }
        }
    }
}
