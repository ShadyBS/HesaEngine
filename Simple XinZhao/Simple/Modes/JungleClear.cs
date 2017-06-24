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
            var w = JungleclearMenu.GetCheckbox("useW") && W.IsReady();
            var e = JungleclearMenu.GetCheckbox("useE") && E.IsReady();

            var minion = ObjectManager.MinionsAndMonsters.NeutralCamps.Where(x => x.IsValidTarget(Q.Range));

			foreach (var m in minion)
            {
                if (w && m.IsInRange(ObjectManager.Me, 300f))
                {
					W.Cast();
                }
				if (e && m.IsInRange(ObjectManager.Me, 600f))
				{
					E.CastOnUnit(m);
				}
			}
        }
    }
}
