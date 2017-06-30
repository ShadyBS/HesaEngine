using System.Linq;
using HesaEngine.SDK;
using SharpDX;
using static Malzahar.MenuManager;
using static Malzahar.SpellManager;

namespace Malzahar.Modes
{
    public static class LaneClear
    {
        public static void DoLaneClear()
        {
            var q = LaneclearMenu.GetCheckbox("useQ") && Q.IsReady();
            var w = LaneclearMenu.GetCheckbox("useW") && W.IsReady();
            var e = LaneclearMenu.GetCheckbox("useE") && E.IsReady();

            var minion = ObjectManager.MinionsAndMonsters.Enemy.Where(x => x.IsValidTarget(W.Range));

            foreach (var m in minion)
            {
				if (q)
				{
					Q.Cast(m);
				}
				if (w)
                {
                    W.Cast(m);
                }
				if (e)
				{
					E.Cast(m);
				}
			}
        }
    }
}
