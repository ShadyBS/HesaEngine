using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Syndra.SpellManager;
using static Syndra.MenuManager;
using SharpDX;
using static HesaEngine.SDK.Orbwalker;

namespace Syndra.Modes
{
	public static class Interrupt
	{
		public static void DoInterrupt(AIHeroClient Source, Interrupter.InterruptableTargetEventArgs args)
		{
			if (Source.IsMe || Source.IsAlly || args.DangerLevel < Interrupter.DangerLevel.Medium) return;
			var e = MiscMenu.GetCheckbox("inE") && E.IsReady() && Source.IsValidTarget(E.Range);
			var eq = MiscMenu.GetCheckbox("inEQ") && Q.IsReady() && E.IsReady() && Source.IsValidTarget(EQ.Range);


			if (eq)
			{
				var pred = EQ.GetPrediction(Source);
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

			if (e)
			{
				E.Cast(Source);
			}
		}
	}
}
