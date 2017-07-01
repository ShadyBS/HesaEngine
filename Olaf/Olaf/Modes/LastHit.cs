using System.Linq;
using HesaEngine.SDK;
using static Olaf.MenuManager;
using static Olaf.SpellManager;

namespace Olaf.Modes
{
	public static class LastHit
	{
		public static void DoLastHit()
		{
			var q = LasthitMenu.GetCheckbox("useQ") && Q.IsReady();
			var e = LasthitMenu.GetCheckbox("useE") && E.IsReady();
			var minion = ObjectManager.MinionsAndMonsters.Enemy.Where(x => x.IsValidTarget(E.Range));

			foreach (var m in minion)
			{
				if (q && ObjectManager.Me.Distance(m) > Orbwalker.GetRealAutoAttackRange(m) && Q.GetDamage(m) >= m.Health)
				{
					Q.Cast(m);
				}
				if (e && ObjectManager.Me.Distance(m) > Orbwalker.GetRealAutoAttackRange(m) && E.GetDamage(m) >= m.Health)
				{
					E.CastOnUnit(m);
				}
			}
		}
	}
}