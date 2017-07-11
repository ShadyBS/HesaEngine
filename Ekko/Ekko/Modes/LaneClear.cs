using System.Linq;
using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using HesaEngine.SDK.Enums;
using SharpDX;
using static Ekko.MenuManager;
using static Ekko.SpellManager;

namespace Ekko.Modes
{
    public static class LaneClear
    {
        public static void DoLaneClear()
        {
            var q = LaneclearMenu.GetCheckbox("useQ") && Q.IsReady();
			var minQ = LaneclearMenu.GetSlider("minQ");
			var e = LaneclearMenu.GetCheckbox("useE") && E.IsReady();

			var minionQ = ObjectManager.MinionsAndMonsters.Enemy.Where(x => x.IsValidTarget(Q.Range));
			var qpos = MinionManager.GetBestLineFarmLocation(minionQ.Select(x => x.Position.To2D()).ToList(), Q.Width, Q.Range);

			foreach (var m in minionQ)
            {
				if (q && qpos.MinionsHit >= minQ)
				{
					Q.Cast(qpos.Position);
				}
				if (e && E.GetDamage(m) + ObjectManager.Player.GetAutoAttackDamage(m) >= m.Health)
				{
					if (m.Distance(ObjectManager.Player.Position) > ObjectManager.Player.GetAutoAttackRange(m) && m.IsValidTarget(E.Range + 375))
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
