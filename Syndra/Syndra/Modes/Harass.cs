using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using HesaEngine.SDK.Enums;
using SharpDX;
using static Syndra.SpellManager;
using static Syndra.MenuManager;
using System.Linq;

namespace Syndra.Modes
{
	public static class Harass
	{
		public static void DoHarass()
		{
			var q = HarassMenu.GetCheckbox("useQ") && Q.IsReady();
			var w = HarassMenu.GetCheckbox("useW") && W.IsReady();
			var eq = HarassMenu.GetCheckbox("useEQ") && E.IsReady() && Q.IsReady();
			var e = HarassMenu.GetCheckbox("useE") && E.IsReady();
			AIHeroClient target = null;

			if (q)
			{
				target = TargetSelector.GetTarget(Q.Range);
				var qpos = DarkPrediction.CirclerPrediction(Q, (AIHeroClient)target, 1);
				if (qpos != DarkPrediction.empt && qpos.Distance(ObjectManager.Player) <= Q.Range)
					Q.Cast(qpos);
			}

			if (eq)
			{
				target = TargetSelector.GetTarget(EQ.Range);
				if (!Functions.HasSpellShield(target))
				{
					Vector3 Qpos = DarkPrediction.LinearPrediction(ObjectManager.Player.Position, EQ, (AIHeroClient)target);
					if (Qpos != DarkPrediction.empt && Qpos.Distance(ObjectManager.Player) <= EQ.Range)
					{
						if (ObjectManager.Player.Distance(Qpos) <= Q.Range)
						{
							Q.Cast(Qpos);
							Main.DoEQ = true;
						}
						else
						{
							Qpos = ObjectManager.Player.Position.Extend(Qpos, Q.Range);
							Q.Cast(Qpos);
							Main.DoEQ = true;
						}
					}
				}
			}

			if (w && !Main.waitW)
			{
				target = TargetSelector.GetTarget(W.Range);
				if (target.IsValidTarget(W.Range))
				{
					var wpos = DarkPrediction.LinearPrediction(ObjectManager.Player.ServerPosition, W, (AIHeroClient)target);
					if (wpos != DarkPrediction.empt && wpos.Distance(ObjectManager.Player) <= W.Range)
					{
						if (W.Instance.ToggleState == 1) // Not holding
						{
							var wball = Main.MyBalls.Find(ball => ball.Distance(ObjectManager.Player) < W.Range - 25);// Look for ball
							var wminion = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, W.Range - 25, MinionTypes.All, MinionTeam.NotAlly, MinionOrderTypes.MaxHealth).FirstOrDefault();//Look for minion
							if (wball != null && !wball.IsMoving)
							{
								W.Cast(wball);

							}
							else if (wminion != null)
							{
								W.Cast(wminion);
								//W.CastIfHitchanceEquals(target, HitChance.VeryHigh);
							}
						}
						else //holding
						{
							//W.CastIfHitchanceEquals(target, HitChance.VeryHigh);
							W.Cast(DarkPrediction.LinearPrediction(ObjectManager.Player.ServerPosition, W, (AIHeroClient)target));
						}
					}
				}
			}

			if (e && !Main.DoEQ)
			{
				target = TargetSelector.GetTarget(Eex.Range);
				if (!Functions.HasSpellShield(target))
				{
					var ePred = Eex.GetPrediction(target);
					var DPePred = DarkPrediction.LinearPrediction(ObjectManager.Player.Position, Eex, (AIHeroClient)target);
					if (DPePred != DarkPrediction.empt && DPePred.Distance(ObjectManager.Player) <= Eex.Range)
					{
						var playerToCP = ObjectManager.Player.Distance(DPePred);
						foreach (var eball in Main.MyBalls.Where(ball => ball.Position.Distance(ObjectManager.Player) < E.Range))
						{
							var ballFinalPos = ObjectManager.Player.ServerPosition.Extend(eball.Position, playerToCP);
							if (ballFinalPos.Distance(DPePred) < 50)
							{
								E.Cast(eball.Position);
							}
						}
					}
				}
			}
		}
	}
}
