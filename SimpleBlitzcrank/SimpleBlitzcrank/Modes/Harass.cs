using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static SimpleTemplate.SpellManager;
using static SimpleTemplate.MenuManager;

namespace SimpleTemplate.Modes
{
	public static class Harass
	{
		public static void DoHarass()
		{
			var q = HarassMenu.GetCheckbox("useQ") && Q.IsReady();
			var e = HarassMenu.GetCheckbox("useE") && E.IsReady();
			var r = HarassMenu.GetCheckbox("useR") && R.IsReady();
			var rc = MiscMenu.GetCheckbox("useRafterQ") && R.IsReady();
			AIHeroClient target;

			if (q)
			{
				target = TargetSelector.GetTarget(QMenu.GetSlider("rangeQ"), TargetSelector.DamageType.Magical);
				if (target != null && (target.Distance(ObjectManager.Me) > QMenu.GetSlider("minQ")) && !QMenu.GetCheckbox("blq" + target.ChampionName) && !Functions.HasSpellShield(target))
				{
					Q.CastIfHitchanceEquals(target, HitChance.VeryHigh);
				}
			}
			if (r)
			{
				target = TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Magical);
				if (target != null && !Functions.HasSpellShield(target))
				{
					if (ObjectManager.Player.CountEnemiesInRange(500f) > HarassMenu.GetSlider("minR"))
						R.Cast();

				}
			}
			if (rc)
			{
				target = TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Magical);
				if (target != null && !Functions.HasSpellShield(target) && target.HasBuff("rocketgrab2"))
				{
					R.Cast();
				}
			}
			if (e)
			{
				target = TargetSelector.GetTarget(HarassMenu.GetSlider("rangeE"), TargetSelector.DamageType.Physical);
				if (target != null && !Functions.HasSpellShield(target))
				{
					E.Cast();
					Orbwalker.ResetAutoAttackTimer();
				}
			}
		}
	}
}