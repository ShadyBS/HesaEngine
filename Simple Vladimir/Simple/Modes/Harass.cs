using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Simple.SpellManager;
using static Simple.MenuManager;

namespace Simple.Modes
{
	public static class Harass
	{
		public static void DoHarass()
		{
			var q = HarassMenu.GetCheckbox("useQ") && Q.IsReady();
			var e = HarassMenu.GetCheckbox("useE") && E.IsReady();

			AIHeroClient target;
			{
				if (q)
				{
					target = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Magical);
					if (target != null)
					{
						Q.CastOnUnit(target);
					}
				}

				if (e)
				{
					target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
					if (ObjectManager.Player.CountEnemiesInRange(600f) >= 1)
					{
						E.StartCharging();
					}
				}
			}
		}
	}
}
