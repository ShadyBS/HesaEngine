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
            var e = ComboMenu.GetCheckbox("useE") && E.IsReady();
			var r = ComboMenu.GetCheckbox("useR") && R.IsReady();

			AIHeroClient target;
			{

				if (r)
				{
					target = TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Magical);
					if (target != null)
					{
						var pred = R.GetPrediction(target);
						if (pred.CastPosition.CountEnemiesInRange(350) >= ComboMenu.Get<MenuSlider>("enemiesR").CurrentValue)
						{
							R.Cast(pred.CastPosition);
						}
					}
				}

				if (q && !Main._isE)
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
					if (ObjectManager.Player.CountEnemiesInRange(E.Range) >= 1)
					{
						E.Cast();
					}
				}
			}
        }
    }
}
	
