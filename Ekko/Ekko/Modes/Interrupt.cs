using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Ekko.SpellManager;
using static Ekko.MenuManager;
using static HesaEngine.SDK.Orbwalker;

namespace Ekko.Modes
{
	public static class Interrupt
	{
		public static void DoInterrupt(AIHeroClient Source, Interrupter.InterruptableTargetEventArgs args)
		{
			if (Source.IsMe || Source.IsAlly || args.DangerLevel < Interrupter.DangerLevel.Medium) return;
		}
	}
}
