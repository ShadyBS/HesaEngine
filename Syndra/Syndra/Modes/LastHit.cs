using System.Linq;
using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using static Syndra.MenuManager;
using static Syndra.SpellManager;

namespace Syndra.Modes
{
	public static class LastHit
	{
		public static void DoLastHit()
		{
			var q = LasthitMenu.GetCheckbox("useQ") && Q.IsReady();
			var w = LasthitMenu.GetCheckbox("useW") && W.IsReady();
			var minion = ObjectManager.MinionsAndMonsters.Enemy.Where(x => x.IsValidTarget(Q.Range));
			Obj_AI_Minion mex = null;

			foreach (var m in minion)
			{
				if (q && Q.GetDamage(m) >= m.Health)
				{
					Q.Cast(m);
					mex = m;
				}
			}
			minion = ObjectManager.MinionsAndMonsters.Enemy.Where(x => x.IsValidTarget(W.Range));
			foreach (var m in minion)
			{
				if (w && W.GetDamage(m) >= m.Health && mex != m)
				{
					if (W.Instance.ToggleState == 1) // Not holding
					{
						W.Cast(m);
						W.Cast(Game.MousePos);
					}
					else
					{
						W.Cast(m);
					}
				}
			}
		}
	}
}