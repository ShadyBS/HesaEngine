using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Simple.SpellManager;
using static Simple.MenuManager;

namespace Simple.Modes
{
    public static class Combo
    {
        public static void DoCombo()
        {
			var q = ComboMenu.GetCheckbox("useQ") && Q.IsReady();
            var w = ComboMenu.GetCheckbox("useW") && W.IsReady();
            var e = ComboMenu.GetCheckbox("useE") && E.IsReady();
			var r = ComboMenu.GetCheckbox("useR") && R.IsReady();
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
                if (target != null && (target.Distance(ObjectManager.Me) > ComboMenu.Get<MenuSlider>("rangeE").CurrentValue))

				{
					E.CastOnUnit(target);
                }
            }
			if (r)
			{
				target = TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Physical);
				if (ObjectManager.Player.CountEnemiesInRange(500f) >= ComboMenu.Get<MenuSlider>("enemiesR").CurrentValue)
				{
					R.Cast();
				}
			}

		}
    }
}
