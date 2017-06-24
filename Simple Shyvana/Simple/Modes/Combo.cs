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
            AIHeroClient target;

            if (e)
            {
                target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
                if (target != null)
                {
					E.CastIfHitchanceEquals(target, HitChance.High);
                }
            }
            
            if (w)
            {
                target = TargetSelector.GetTarget(W.Range, TargetSelector.DamageType.Magical);
				if (ObjectManager.Player.CountEnemiesInRange(325f) >= 1)
				{
					W.Cast();
				}
			}
        }
    }
}
