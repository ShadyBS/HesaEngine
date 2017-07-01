using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Olaf.DarkPrediction;
using static Olaf.SpellManager;
using static Olaf.MenuManager;

namespace Olaf.Modes
{
    public static class Combo
    {
        public static void DoCombo()
        {
            var q = ComboMenu.GetCheckbox("useQ") && Q.IsReady();
            var e = ComboMenu.GetCheckbox("useE") && E.IsReady();

			if (q)
            {
				var target = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Magical);
				var location = LinearPrediction(ObjectManager.Player.Position, Q, (AIHeroClient)target);
				if (target != null && (target.Distance(ObjectManager.Me) < ComboMenu.GetSlider("maxQ")) && location != DarkPrediction.empt)
				{
					Q.Cast(location);
				}
			}
			if (e)
			{
				var target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
				if (target != null && ObjectManager.Me.HealthPercent > ComboMenu.GetSlider("minE"))
				{
					E.CastOnUnit(target);
				}
			}
		}
    }
}
