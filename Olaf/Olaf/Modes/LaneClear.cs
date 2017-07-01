using System.Linq;
using HesaEngine.SDK;
using SharpDX;
using static Olaf.MenuManager;
using static Olaf.SpellManager;

namespace Olaf.Modes
{
    public static class LaneClear
    {
        public static void DoLaneClear()
        {
            var q = LaneclearMenu.GetCheckbox("useQ") && Q.IsReady();
            var e = LaneclearMenu.GetCheckbox("useE") && E.IsReady();

            var minion = ObjectManager.MinionsAndMonsters.Enemy.Where(x => x.IsValidTarget(W.Range));

            foreach (var m in minion)
            {
				if (q && ObjectManager.Me.Distance(m) < Q.Range)
				{
					Q.Cast(m);
				}
				if (e && ObjectManager.Me.Distance(m) < E.Range)
				{
					E.CastOnUnit(m);
				}
			}
        }
    }
}
