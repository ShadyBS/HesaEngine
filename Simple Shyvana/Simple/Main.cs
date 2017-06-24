using HesaEngine.SDK;
using HesaEngine.SDK.Args;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using static HesaEngine.SDK.ObjectManager;
using Simple.Modes;
using static Simple.SpellManager;
using static Simple.MenuManager;
using static Simple.DrawingManager;

namespace Simple
{
    public class Main : IScript
    {
        //Don't forget to change the champion first!
        private const Champion Champion = HesaEngine.SDK.Enums.Champion.Shyvana;

        public string Name => "Shyvana by BadCommand";

        public string Version => "1.0.0";

        public string Author => "BadCommand";

        public static PredictionInput QPred, WPred, EPred, RPred;

        public static Orbwalker.OrbwalkerInstance Orb => Core.Orbwalker;

        public void OnInitialize()
        {
            Game.OnGameLoaded += Game_OnGameLoaded;
        }

        private void Game_OnGameLoaded()
        {
            if (Me.Hero != Champion)
                return;
            LoadMenu();
            LoadSpells();
            HesaEngine.SDK.AntiGapcloser.OnEnemyGapcloser += Modes.AntiGapcloser.DoAntigapclose;
            LoadDrawings();
            Game.OnTick += Game_OnTick;
			Orbwalker.AfterAttack += AfterAttack;
			Chat.Print(Name + " Loaded Successfully");
        }

        private static int _currentLevel = 1;
		public static Item THydra;
		public static Item RHydra;
		public static Item Tiamat;
		private static void AfterAttack(AttackableUnit sender, AttackableUnit ArgsTarget)
		{
			var ttarget = TargetSelector.GetTarget(385);
			var rtarget = TargetSelector.GetTarget(385);
			var tiamattarget = TargetSelector.GetTarget(385);
			THydra = new Item(3748, 385);
			RHydra = new Item(3074, 385);
			Tiamat = new Item(3077, 385);
			if (!sender.IsMe || ObjectManager.Me.IsDead)
				return;

			if (ArgsTarget == null || ArgsTarget.IsDead || ArgsTarget.Health <= 0)
				return;

			if (Orb.ActiveMode.Equals(Orbwalker.OrbwalkingMode.Combo))
			{
				var target = ArgsTarget as AIHeroClient;

				if (target != null && !target.IsDead)
				{
					if (ComboMenu.GetCheckbox("useQ"))
					{
						if (Q.IsReady() && target.Distance(ObjectManager.Me) < 280)
						{
							Q.Cast();
							Orbwalker.ResetAutoAttackTimer();
						}
						if (!Q.IsReady() && target.Distance(ObjectManager.Me) < 300)
						{
							if (THydra.IsOwned() && THydra.IsReady())
							{
								THydra.Cast(ttarget);
							}
							if (RHydra.IsOwned() && RHydra.IsReady())
							{
								RHydra.Cast(rtarget);
							}
							if (Tiamat.IsOwned() && Tiamat.IsReady())
							{
								Tiamat.Cast(tiamattarget);
							}
						}
					}
				}
			}
			if (Orb.ActiveMode.Equals(Orbwalker.OrbwalkingMode.Harass))
			{
				var target = ArgsTarget as AIHeroClient;

				if (target != null && !target.IsDead)
				{
					if (HarassMenu.GetCheckbox("useQ"))
					{
						if (Q.IsReady() && target.Distance(ObjectManager.Me) < 280)
						{
							Q.Cast();
							Orbwalker.ResetAutoAttackTimer();
						}
						if (!Q.IsReady() && target.Distance(ObjectManager.Me) < 300)
						{
							if (THydra.IsOwned() && THydra.IsReady())
							{
								THydra.Cast(ttarget);
							}
							if (RHydra.IsOwned() && RHydra.IsReady())
							{
								RHydra.Cast(rtarget);
							}
							if (Tiamat.IsOwned() && Tiamat.IsReady())
							{
								Tiamat.Cast(tiamattarget);
							}
						}
					}
				}
			}
			if (Orb.ActiveMode.Equals(Orbwalker.OrbwalkingMode.LaneClear))
			{
				var target = ArgsTarget as Obj_AI_Minion;

				if (target != null && !target.IsDead)
				{
					if (LaneclearMenu.GetCheckbox("useQ"))
					{
						if (Q.IsReady() && target.Distance(ObjectManager.Me) < 280)
						{
							Q.Cast();
							Orbwalker.ResetAutoAttackTimer();
						}
						if (!Q.IsReady() && target.Distance(ObjectManager.Me) < 300)
						{
							if (THydra.IsOwned() && THydra.IsReady())
							{
								THydra.Cast(ttarget);
							}
							if (RHydra.IsOwned() && RHydra.IsReady())
							{
								RHydra.Cast(rtarget);
							}
							if (Tiamat.IsOwned() && Tiamat.IsReady())
							{
								Tiamat.Cast(tiamattarget);
							}
						}
					}
				}
			}
			if (Orb.ActiveMode.Equals(Orbwalker.OrbwalkingMode.JungleClear))
			{
				var target = ArgsTarget as Obj_AI_Minion;

				if (target != null && !target.IsDead)
				{
					if (JungleclearMenu.GetCheckbox("useQ"))
					{
						if (Q.IsReady() && target.Distance(ObjectManager.Me) < 280)
						{
							Q.Cast();
							Orbwalker.ResetAutoAttackTimer();
						}
						if (!Q.IsReady() && target.Distance(ObjectManager.Me) < 300)
						{
							if (THydra.IsOwned() && THydra.IsReady())
							{
								THydra.Cast(ttarget);
							}
							if (RHydra.IsOwned() && RHydra.IsReady())
							{
								RHydra.Cast(rtarget);
							}
							if (Tiamat.IsOwned() && Tiamat.IsReady())
							{
								Tiamat.Cast(tiamattarget);
							}
						}
					}
				}
			}
		}

		private static void Game_OnTick()
        {
            var mana = Me.ManaPercent;

            if (KillstealMenu.GetCheckbox("enable"))
                Killsteal.DoKs();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Combo)
                Combo.DoCombo();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Harass)
                Harass.DoHarass();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.LaneClear)
                LaneClear.DoLaneClear();

			if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.JungleClear)
				JungleClear.DoJungleClear();

			if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.LastHit)
                LastHit.DoLastHit();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Flee)
                Flee.DoFlee();

            //This is a basic fix, due to OnLevelUp being disabled
            if (_currentLevel == Player.Level) return;
            Leveler();
            _currentLevel = Player.Level;
        }
    }
}
