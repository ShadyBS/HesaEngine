using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Olaf.DarkPrediction;
using static Olaf.SpellManager;
using static Olaf.MenuManager;

namespace Olaf.Modes
{
	public static class Harass
	{
		public static void DoHarass()
		{
			var q = HarassMenu.GetCheckbox("useQ") && Q.IsReady();
			var e = HarassMenu.GetCheckbox("useE") && E.IsReady();

			if (q)
			{
				var target = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Magical);
				var location = LinearPrediction(ObjectManager.Player.Position, Q, (AIHeroClient)target);
				if (target != null && (target.Distance(ObjectManager.Me) < HarassMenu.GetSlider("maxQ")) && location != DarkPrediction.empt)
				{
					Q.Cast(location);
				}
			}
			if (e)
			{
				var target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
				if (target != null && ObjectManager.Me.HealthPercent > HarassMenu.GetSlider("minE"))
				{
					E.CastOnUnit(target);
				}
			}
		}
	}
}
