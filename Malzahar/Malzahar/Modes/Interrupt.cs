using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Malzahar.SpellManager;
using static Malzahar.MenuManager;
using static HesaEngine.SDK.Orbwalker;

namespace Malzahar.Modes
{
	public static class Interrupt
	{
		public static void DoInterrupt(AIHeroClient Source, Interrupter.InterruptableTargetEventArgs args)
		{
			if (Source.IsMe || Source.IsAlly || args.DangerLevel < Interrupter.DangerLevel.Medium) return;

			var q = MiscMenu.GetCheckbox("inQ") && Q.IsReady() && Source.IsValidTarget(Q.Range);
			var r = MiscMenu.GetCheckbox("inR") && R.IsReady() && Source.IsValidTarget(R.Range);


			if (q)
			{
				Q.Cast(Source);
			}

			if (r)
			{
				R.Cast(Source);
			}

		}
	}
}
