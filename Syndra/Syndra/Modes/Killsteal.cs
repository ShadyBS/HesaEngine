using System;
using System.Linq;
using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;
using HesaEngine.SDK.Enums;
using static Syndra.SpellManager;
using static Syndra.MenuManager;

namespace Syndra.Modes
{
    public static class Killsteal
    {
        public static void DoKs()
        {
            var q = Q.IsReady() && KillstealMenu.GetCheckbox("useQ");
			var w = Q.IsReady() && KillstealMenu.GetCheckbox("useW");
            var r = R.IsReady() && KillstealMenu.GetCheckbox("useR");
            var ignite = KillstealMenu.GetCheckbox("useIgnite");

            if (ignite)
                Functions.UseIgnite();

			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(Q.Range) && !x.IsDead && !x.IsZombie && !Functions.IsNotKillable(x)))
			{
				if (q && Q.GetDamage(enemy) >= enemy.Health)
				{
					var qpos = DarkPrediction.CirclerPrediction(Q, (AIHeroClient)enemy, 1);
					if (qpos != DarkPrediction.empt && qpos.Distance(ObjectManager.Player) <= Q.Range)
					Q.Cast(qpos);
				}
			}

			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => x.IsValidTarget(W.Range) && !x.IsDead && !x.IsZombie && !Functions.IsNotKillable(x)))
			{
				if (w && !Main.waitW && W.GetDamage(enemy) >= enemy.Health)
				{
					if (enemy.IsValidTarget(W.Range))
					{
						var wpos = DarkPrediction.LinearPrediction(ObjectManager.Player.ServerPosition, W, (AIHeroClient)enemy);
						if (wpos != DarkPrediction.empt && wpos.Distance(ObjectManager.Player) <= W.Range)
						{
							if (W.Instance.ToggleState == 1) // Not holding
							{
								var wball = Main.MyBalls.Find(ball => ball.Distance(ObjectManager.Player) < W.Range - 25);// Look for ball
								var wminion = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, W.Range - 25, MinionTypes.All, MinionTeam.NotAlly, MinionOrderTypes.MaxHealth).FirstOrDefault();//Look for minion
								if (wball != null && !wball.IsMoving)
								{
									W.Cast(wball);

									W.Cast(wminion);
								}
								else if (wminion != null)
								{
									//W.CastIfHitchanceEquals(target, HitChance.VeryHigh);
								}
							}
							else //holding
							{
								//W.CastIfHitchanceEquals(target, HitChance.VeryHigh);
								W.Cast(DarkPrediction.LinearPrediction(ObjectManager.Player.ServerPosition, W, (AIHeroClient)enemy));
							}
						}
					}
				}
			}

			foreach (var enemy in ObjectManager.Heroes.Enemies.Where(x => KillstealMenu.GetCheckbox("rlist" + x.ChampionName) && x.IsValidTarget(R.Range) && !x.IsDead && !x.IsZombie && !Functions.IsNotKillable(x)))
			{
				if (r && (Main.MyBalls.Count() + 3) * R.GetDamage(enemy) / 3 >= enemy.Health)
				{
					R.Cast(enemy);
				}
			}
		}
    }
}
