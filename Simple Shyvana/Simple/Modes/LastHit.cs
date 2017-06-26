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
            var e = LasthitMenu.GetCheckbox("useE") && E.IsReady();
            var minion = ObjectManager.MinionsAndMonsters.Enemy.Where(x => x.IsValidTarget(E.Range));

            foreach (var m in minion)
            {
                if (e && E.GetDamage(m) >= m.Health)
                {
                    E.CastIfHitchanceEquals(m, HitChance.High);
                }
            }
        }
    }
}
