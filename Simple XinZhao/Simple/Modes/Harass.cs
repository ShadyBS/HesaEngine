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
			var w = HarassMenu.GetCheckbox("useW") && W.IsReady();
			var e = HarassMenu.GetCheckbox("useE") && E.IsReady();
			var r = HarassMenu.GetCheckbox("useR") && R.IsReady();
			AIHeroClient target;

			if (w)
			{
				target = TargetSelector.GetTarget(W.Range, TargetSelector.DamageType.Physical);
				if (ObjectManager.Player.CountEnemiesInRange(300f) >= 1)
				{
					W.Cast();
				}
			}
			if (e)
			{
				target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Physical);
				if (target != null && (target.Distance(ObjectManager.Me) > HarassMenu.Get<MenuSlider>("rangeE").CurrentValue))

				{
					E.CastOnUnit(target);
				}
			}
			if (r)
			{
				target = TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Physical);
				if (ObjectManager.Player.CountEnemiesInRange(500f) >= HarassMenu.Get<MenuSlider>("enemiesR").CurrentValue)
				{
					R.Cast();
				}
			}

		}
	}
}
