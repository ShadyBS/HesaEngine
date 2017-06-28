using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static SimpleTemplate.SpellManager;
using static SimpleTemplate.MenuManager;
using static HesaEngine.SDK.Orbwalker;

namespace SimpleTemplate.Modes
{
    public static class Interrupt
    {
        public static void DoInterrupt(AIHeroClient Source, Interrupter.InterruptableTargetEventArgs args)
        {
            if (Source.IsMe || Source.IsAlly || args.DangerLevel < Interrupter.DangerLevel.Medium) return;

            var q = MiscMenu.GetCheckbox("inQ") && Q.IsReady() && Source.IsValidTarget(Q.Range) && args.MovementInterrupts;
            var e = MiscMenu.GetCheckbox("inE") && E.IsReady() && Orbwalker.InAutoAttackRange(Source) && args.MovementInterrupts;
            var r = MiscMenu.GetCheckbox("inR") && R.IsReady() && Source.IsValidTarget(R.Range);

			if (e)
			{
				E.Cast(Source);
			}

			if (q)
			{
				Q.PredictionCast(Source);
			}

			if (r)
			{
				R.Cast();
			}
            
        }
    }
}
