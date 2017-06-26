using System.Linq;
using HesaEngine.SDK;
using static Simple.MenuManager;
using static Simple.SpellManager;

namespace Simple.Modes
{
    public static class LastHit
    {
        public static void DoLastHit()
        {
            var q = LasthitMenu.GetCheckbox("useQ") && Q.IsReady();
            var minion = ObjectManager.MinionsAndMonsters.Enemy.Where(x => x.IsValidTarget(Q.Range));

            foreach (var m in minion)
            {
                if (q && Q.GetDamage(m) >= m.Health)
                {
                    Q.CastOnUnit(m);
                }
            }
        }
    }
}
