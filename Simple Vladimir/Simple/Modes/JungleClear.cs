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
    public static class JungleClear
    {
        public static void DoJungleClear()
        {
            var q = JungleclearMenu.GetCheckbox("useQ") && Q.IsReady();
            var e = JungleclearMenu.GetCheckbox("useE") && E.IsReady();

            var minion = ObjectManager.MinionsAndMonsters.NeutralCamps.Where(x => x.IsValidTarget(Q.Range));

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
