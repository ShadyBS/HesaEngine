using HesaEngine.SDK;
using HesaEngine.SDK.Args;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using Ekko.Modes;
using static HesaEngine.SDK.ObjectManager;
using static Ekko.SpellManager;
using static Ekko.MenuManager;
using static Ekko.DrawingManager;
using System;
using System.Linq;

namespace Ekko
{
    public class Main : IScript
    {
        //Don't forget to change the champion first!
        private const Champion Champion = HesaEngine.SDK.Enums.Champion.Ekko;

        public string Name => "Ekko by BadCommand" ;

		public string Version => "1.0.0";

        public string Author => "BadCommand";

        public static PredictionInput QPred, WPred, EPred, RPred;

        public static Orbwalker.OrbwalkerInstance Orb => Core.Orbwalker;

		public static GameObject EkkoRGhost;

		public static GameObject WArea2, WArea;

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
            LoadDrawings();
            Game.OnTick += Game_OnTick;
			Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
			HesaEngine.SDK.Interrupter.OnInterruptableTarget += Modes.Interrupt.DoInterrupt;
			WArea = ObjectManager.Get<Obj_GeneralParticleEmitter>().FirstOrDefault(x => x.Name.Equals("Ekko_Base_W_Indicator.troy"));
			Chat.Print(Name + " Loaded Successfully");

		}

		public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
		{
			var savemeR = MiscMenu.GetCheckbox("saveMe");
			var myhpR = MiscMenu.GetSlider("hpR");
			if (!sender.IsEnemy && (!(sender is AIHeroClient) || !(sender is Obj_AI_Turret)))
			{
				return;
			}

			if (savemeR && R.IsReady())
			{
				if (Player.HealthPercent <= myhpR && ObjectManager.Player.CountEnemiesInRange(800f) >= 0)
				{
					R.Cast();
				}

				if (ObjectManager.Player.CountEnemiesInRange(800f) > 1 && sender.BaseAttackDamage >= Player.TotalShieldHealth || sender.BaseAbilityDamage >= Player.TotalShieldHealth)
				{
					R.Cast();
				}

				if (ObjectManager.Player.CountEnemiesInRange(800f) > 1 && sender.GetAutoAttackDamage(Player) >= Player.TotalShieldHealth)
				{
					R.Cast();
				}
			}

			if (args.SData.Name == "ZedR")
			{
				if (R.IsReady())
				{
					Core.DelayAction(() => R.Cast(), 2000 - Game.Ping - 200);
				}
			}
		}

		private static int _currentLevel = 1;

		private static void Game_OnTick()
        {
            var mana = Me.ManaPercent;

			if (EkkoRGhost == null && R.IsReady())
			{
				EkkoRGhost = ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(x => !x.IsEnemy && x.Name.ToLower().Contains("ekko"));
			}

			if (KillstealMenu.GetCheckbox("enable"))
               Killsteal.DoKs();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Combo && mana >= ComboMenu.GetSlider("mana"))
                Combo.DoCombo();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Harass && mana >= HarassMenu.GetSlider("mana"))
                Harass.DoHarass();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.LaneClear && mana >= LaneclearMenu.GetSlider("mana"))
                LaneClear.DoLaneClear();

			if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.JungleClear && mana >= LaneclearMenu.GetSlider("mana"))
				JungleClear.DoJungleClear();


			//This is a basic fix, due to OnLevelUp being disabled
			if (_currentLevel == Player.Level) return;
            Leveler();
            _currentLevel = Player.Level;
        }
    }
}
