using System.Linq;
using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using HesaEngine.SDK.Enums;
using SharpDX;
using static Ekko.MenuManager;
using static Ekko.SpellManager;

namespace Ekko.Modes
{
    public static class JungleClear
    {
        public static void DoJungleClear()
        {
            var q = JungleclearMenu.GetCheckbox("useQ") && Q.IsReady();
			var minQ = JungleclearMenu.GetSlider("minQ");
			var e = JungleclearMenu.GetCheckbox("useE") && E.IsReady();

			var minionQ = ObjectManager.MinionsAndMonsters.NeutralCamps.Where(x => x.IsValidTarget(Q.Range));
			var qpos = MinionManager.GetBestLineFarmLocation(minionQ.Select(x => x.Position.To2D()).ToList(), Q.Width, Q.Range);

			foreach (var m in minionQ)
            {
				if (q && qpos.MinionsHit >= minQ)
				{
					Q.Cast(qpos.Position);
				}
				if (e)
				{
					if (m.IsInRange(ObjectManager.Me, E.Range + 375))
					{
						ObjectManager.Player.Spellbook.CastSpell(SpellSlot.E, m.Position);
						Orbwalker.ResetAutoAttackTimer();
						Core.DelayAction(() => ObjectManager.Player.IssueOrder(HesaEngine.SDK.Enums.GameObjectOrder.AttackUnit, m), 100);
						return;
					}
				}
			}
        }
    }
}
