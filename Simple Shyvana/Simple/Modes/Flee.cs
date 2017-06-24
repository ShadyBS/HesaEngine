using HesaEngine.SDK;
using static Simple.SpellManager;
using static Simple.MenuManager;

namespace Simple.Modes
{
    public static class Flee
    {
        public static void DoFlee()
        {

            var w = FleeMenu.GetCheckbox("useW") && W.IsReady();

            var target = TargetSelector.GetTarget(W.Range);
			if (w)
				W.Cast();
            
        }
    }
}
