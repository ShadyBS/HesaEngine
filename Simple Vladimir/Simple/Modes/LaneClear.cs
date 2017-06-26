using System.Linq;
using HesaEngine.SDK;
using HesaEngine.SDK.Args;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SharpDX;
using static Simple.MenuManager;
using static Simple.SpellManager;

namespace Simple.Modes
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
				if (q && m.IsInRange(ObjectManager.Me, Q.Range))
				{
					Q.CastOnUnit(m);
				}
				if (e && m.IsInRange(ObjectManager.Me, E.Range))
				{
					E.StartCharging();
				}
			}
		}
    }
}

