using HesaEngine.SDK;
using HesaEngine.SDK.Args;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using Malzahar.Modes;
using static HesaEngine.SDK.ObjectManager;
using static Malzahar.SpellManager;
using static Malzahar.MenuManager;
using static Malzahar.DrawingManager;

namespace Malzahar
{
    public class Main : IScript
    {
        //Don't forget to change the champion first!
        private const Champion Champion = HesaEngine.SDK.Enums.Champion.Malzahar;

        public string Name => "Malzahar by BadCommand" ;

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
            LoadDrawings();
            Game.OnTick += Game_OnTick;
			Obj_AI_Base.OnBuffGained += OnBuffGain;
			Obj_AI_Base.OnBuffLost += OnBuffLose;
			HesaEngine.SDK.Interrupter.OnInterruptableTarget += Modes.Interrupt.DoInterrupt;
			Chat.Print(Name + " Loaded Successfully");
        }

		public static bool _isUlting = false;

		public static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainedEventArgs args)

		{
			if (args.Buff.Name.ToLower() == "malzaharrsound" || args.Buff.Name.ToLower() == "malzaharr")
			{


				Orbwalker.Move = false;
				Orbwalker.Attack = false;
				_isUlting = true;


			}
		}

		public static void OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLostEventArgs args)
		{
			if (args.Buff.Name.ToLower() == "malzaharrsound" || args.Buff.Name.ToLower() == "malzaharr")
			{

				Orbwalker.Move = true;
				Orbwalker.Attack = true;
				_isUlting = false;

			}
		}

		private static int _currentLevel = 1;

		private static void Game_OnTick()
        {
            var mana = Me.ManaPercent;

			if (HarassMenu.GetCheckbox("autoE") && mana >= HarassMenu.GetSlider("mana"))
			{
				if (E.IsReady() && !Main._isUlting)
				{
					var target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
					if (target != null)
					{
						E.Cast(target);
					}
				}
			}

				if (KillstealMenu.GetCheckbox("enable"))
                Killsteal.DoKs();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Combo && mana >= ComboMenu.GetSlider("mana"))
                Combo.DoCombo();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Harass && mana >= HarassMenu.GetSlider("mana"))
                Harass.DoHarass();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.LaneClear && mana >= LaneclearMenu.GetSlider("mana"))
                LaneClear.DoLaneClear();


            //This is a basic fix, due to OnLevelUp being disabled
            if (_currentLevel == Player.Level) return;
            Leveler();
            _currentLevel = Player.Level;
        }
    }
}
