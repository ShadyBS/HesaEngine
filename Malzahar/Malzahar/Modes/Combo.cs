using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Malzahar.SpellManager;
using static Malzahar.MenuManager;

namespace Malzahar.Modes
{
    public static class Combo
    {
        public static void DoCombo()
        {
            var q = ComboMenu.GetCheckbox("useQ") && Q.IsReady();
            var w = ComboMenu.GetCheckbox("useW") && W.IsReady();
            var e = ComboMenu.GetCheckbox("useE") && E.IsReady();



			if (q && !Main._isUlting)
            {
				var target = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Magical);
				if (target != null)
                {
					Q.CastIfHitchanceEquals(target, HitChance.VeryHigh);
                }
            }
			if (w && !Main._isUlting)
			{
				var target = TargetSelector.GetTarget(W.Range, TargetSelector.DamageType.Magical);
				if (target != null)
				{
					W.Cast(target);
				}
			}
			if (e && !Main._isUlting)
			{
				var target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
				if (target != null)
				{
					E.Cast(target);
				}
			}
		}
    }
}
