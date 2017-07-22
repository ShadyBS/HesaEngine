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


			foreach (var target in ObjectManager.Heroes.Enemies.Where(k => k.IsValidTarget(Q.Range) && !k.IsDead && !k.IsZombie))
			{
				if (q)
				{
					Q.CastIfHitchanceEquals(target, HitChance.VeryHigh);
					//Chat.Print((Main.MyBalls.Count() + 3) * R.GetDamage(target)/3);
				}
			}

			foreach (var target in ObjectManager.Heroes.Enemies.Where(k => k.IsValidTarget(EQ.Range) && !k.IsDead && !k.IsZombie))
			{
				if (eq && !Functions.HasSpellShield(target))
				{
					var pred = EQ.GetPrediction(target);
					Vector3 Qpos = pred.CastPosition;
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
				//else if (e && !q && !Functions.HasSpellShield(target))
				//{
				//	if (!Main.DoEQ)
				//	{
				//		var ePred = Eex.GetPrediction(target);
				//		if (ePred.Hitchance >= HitChance.VeryHigh)
				//		{
				//			var playerToCP = ObjectManager.Player.Distance(ePred.CastPosition);
				//			foreach (var ball in Main.MyBalls.Where(ball => ObjectManager.Player.Distance(ball.Position) < E.Range))
				//			{
				//				var ballFinalPos = ObjectManager.Player.ServerPosition.Extend(ball.Position, playerToCP);
				//				if (ballFinalPos.Distance(ePred.CastPosition) < 50)
				//					E.Cast(ball.Position);
				//			}
				//		}
				//	}
				//}
			}
			foreach (var target in ObjectManager.Heroes.Enemies.Where(k => k.IsValidTarget(W.Range) && !k.IsDead && !k.IsZombie))
			{
				if (w)
				{
					if (target.IsValidTarget(W.Range) && W.GetPrediction(target).Hitchance == HitChance.VeryHigh)
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
								W.CastIfHitchanceEquals(target, HitChance.VeryHigh);
							}
						}
						else //holding
						{
							W.CastIfHitchanceEquals(target, HitChance.VeryHigh);
						}
					}
				}
			}


			foreach (var target in ObjectManager.Heroes.Enemies.Where(k => k.IsValidTarget(Eex.Range) && !k.IsDead && !k.IsZombie))
			{
				if (e && !Main.DoEQ && !Functions.HasSpellShield(target))
				{
					var ePred = Eex.GetPrediction(target);
					if (ePred.Hitchance >= HitChance.Low)
					{
						var playerToCP = ObjectManager.Player.Distance(ePred.CastPosition);
						foreach (var eball in Main.MyBalls.Where(ball => ball.Position.Distance(ObjectManager.Player) < E.Range))
						{
							var ballFinalPos = ObjectManager.Player.ServerPosition.Extend(eball.Position, playerToCP);
							if (ballFinalPos.Distance(ePred.CastPosition) < 50)
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
