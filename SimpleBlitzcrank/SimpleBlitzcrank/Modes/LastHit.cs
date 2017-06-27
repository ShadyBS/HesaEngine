using System.Linq;
using HesaEngine.SDK;
using static SimpleTemplate.MenuManager;
using static SimpleTemplate.SpellManager;

namespace SimpleTemplate.Modes
{
    public static class LastHit
    {
        //public static void DoLastHit()
        //{
        //    var q = LasthitMenu.GetCheckbox("useQ") && Q.IsReady();
        //    var w = LasthitMenu.GetCheckbox("useW") && W.IsReady();
        //    var e = LasthitMenu.GetCheckbox("useE") && E.IsReady();
        //    var r = LasthitMenu.GetCheckbox("useR") && R.IsReady();
        //    var minion = ObjectManager.MinionsAndMonsters.Enemy.Where(x => x.IsValidTarget(W.Range));

        //    foreach (var m in minion)
        //    {
        //        if (w && W.GetDamage(m) >= m.Health)
        //        {
        //            W.PredictionCast(m);
        //        }
        //    }
        //}
    }
}
