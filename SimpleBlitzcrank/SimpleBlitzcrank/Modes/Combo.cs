using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using SharpDX;
using static SimpleTemplate.DarkPrediction;
using static SimpleTemplate.SpellManager;
using static SimpleTemplate.MenuManager;

namespace SimpleTemplate.Modes
{
	public static class Combo
	{
		public static void DoCombo()
		{
			var q = ComboMenu.GetCheckbox("useQ") && Q.IsReady();
			var e = ComboMenu.GetCheckbox("useE") && E.IsReady();
			var r = ComboMenu.GetCheckbox("useR") && R.IsReady();
			var rc = MiscMenu.GetCheckbox("useRafterQ") && R.IsReady();
			AIHeroClient target;
			
			if (q)
			{
				target = TargetSelector.GetTarget(QMenu.GetSlider("rangeQ"), TargetSelector.DamageType.Magical);
				if (MiscMenu.GetCheckbox("useDP"))
				{
					var location = LinearPrediction(ObjectManager.Player.Position, Q, (AIHeroClient)target);
					if (target != null && (target.Distance(ObjectManager.Me) > QMenu.GetSlider("minQ")) && !QMenu.GetCheckbox("blq" + target.ChampionName) && !Functions.HasSpellShield(target) && location != DarkPrediction.empt && !DarkPrediction.CollisionChecker(location, ObjectManager.Me.Position, Q))
					{
						Q.Cast(location);
					}
				}
				else
				{
					if (target != null && (target.Distance(ObjectManager.Me) > QMenu.GetSlider("minQ")) && !QMenu.GetCheckbox("blq" + target.ChampionName) && !Functions.HasSpellShield(target))
					{
						Q.CastIfHitchanceEquals(target, HitChance.VeryHigh);
					}
				}
			}
			if (r)
			{
				target = TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Magical);
				if (target != null && !Functions.HasSpellShield(target))
				{
					if (ObjectManager.Player.CountEnemiesInRange(550f) > ComboMenu.GetSlider("minR"))
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
				target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Physical);
				if (target != null && !Functions.HasSpellShield(target))
				{
					E.Cast();
					Orbwalker.ResetAutoAttackTimer();
				}
			}
		}
	}
}