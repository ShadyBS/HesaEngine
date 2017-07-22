using System.Linq;
using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using HesaEngine.SDK.Enums;
using SharpDX;
using static Syndra.MenuManager;
using static Syndra.SpellManager;

namespace Syndra.Modes
{
	public static class JungleClear
	{
		public static void DoJungleClear()
		{
			var q = JungleclearMenu.GetCheckbox("useQ") && Q.IsReady();
			var w = JungleclearMenu.GetCheckbox("useW") && E.IsReady();

			var minionQ = ObjectManager.MinionsAndMonsters.NeutralCamps.Where(x => x.IsValidTarget(Q.Range));
			var qpos = MinionManager.GetBestCircularFarmLocation(minionQ.Select(x => x.Position.To2D()).ToList(), Q.Width, Q.Range);

			foreach (var m in minionQ)
			{
				if (q && qpos.MinionsHit >= 1)
				{
					Q.Cast(qpos.Position);
				}
			}

			var minionW = ObjectManager.MinionsAndMonsters.NeutralCamps.Where(x => x.IsValidTarget(W.Range));
			var wpos = MinionManager.GetBestCircularFarmLocation(minionW.Select(x => x.Position.To2D()).ToList(), W.Width, W.Range);

			foreach (var m in minionQ)
			{
				if (w && wpos.MinionsHit >= 1)
				{
					if (W.Instance.ToggleState == 1) // Not holding
					{
						var wball = Main.MyBalls.Find(ball => ball.Distance(ObjectManager.Player) < W.Range - 25);// Look for ball
						var wminion = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, W.Range - 25, MinionTypes.All, MinionTeam.NotAlly, MinionOrderTypes.MaxHealth).FirstOrDefault();//Look for minion
						if (wball != null && !wball.IsMoving)
						{
							W.Cast(wball);

						}
						else if (wminion != null)
						{
							W.Cast(wminion);
						}
					}
					else //holding
					{
						W.Cast(wpos.Position);
					}
				}
			}
		}
	}
}
