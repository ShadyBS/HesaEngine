using HesaEngine.SDK;
using static SimpleTemplate.SpellManager;
using static SimpleTemplate.MenuManager;

namespace SimpleTemplate.Modes
{
    public static class Flee
    {
        public static void DoFlee()
        {
            var q = FleeMenu.GetCheckbox("useQ") && Q.IsReady();
            var w = FleeMenu.GetCheckbox("useW") && W.IsReady();
            var e = FleeMenu.GetCheckbox("useE") && E.IsReady();
            var r = FleeMenu.GetCheckbox("useR") && R.IsReady();

            var target = TargetSelector.GetTarget(W.Range);
            if(w)
                W.PredictionCast(target, HitChance.Medium);
            
        }
    }
}
