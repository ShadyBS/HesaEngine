using HesaEngine.SDK;
using static Simple.MenuManager;
using static Simple.SpellManager;

namespace Simple.Modes
{
    public static class Harass
    {
        public static void DoHarass()
        {
            var q = HarassMenu.GetCheckbox("useQ") && Q.IsReady();
            var w = HarassMenu.GetCheckbox("useW") && W.IsReady();
            var e = HarassMenu.GetCheckbox("useE") && E.IsReady();

            var target = TargetSelector.GetTarget(W.Range, TargetSelector.DamageType.Physical);

			if (e)
			{
				target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
				if (target != null)
				{
					Chat.Print("Casting E");
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
