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
        private const Champion Champion = HesaEngine.SDK.Enums.Champion.Vladimir;

        public string Name => "Vladimir by BadCommand";

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
			//Obj_AI_Base.OnBuffGained += OnBuffGain;
			//Obj_AI_Base.OnBuffLost += OnBuffLose;
			Chat.Print(Name + " Loaded Successfully");
        }

		public static bool _isE;
		public static int _isEcount;
		public static int _isEabort;

		//public static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainedEventArgs args)// gotta check this

		//{
		//	if (args.Buff.Name.ToLower() == "vladimiresound" || args.Buff.Name.ToLower() == "vladimire" || ObjectManager.Me.HasBuff("vladimire"))
		//	{
		//		_isE = true;
		//		_isEcount = ObjectManager.Player.CountEnemiesInRange(600f);
		//	}
		//}

		//public static void OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLostEventArgs args)
		//{
		//	if (args.Buff.Name.ToLower() == "vladimiresound" || args.Buff.Name.ToLower() == "vladimire" || ObjectManager.Me.HasBuff("vladimire"))
		//	{
		//		_isE = false;
		//	}
		//}

		private static int _currentLevel = 1;

		private static void Game_OnTick()
        {

			//if (_isE)// Gotta check this
			//{
			//	_isEabort = ObjectManager.Player.CountEnemiesInRange(200f);
			//	if (_isEabort < _isEcount)
			//	{
			//		E.Cast();
			//	}
			//	else
			//	{
			//		_isEcount = Player.CountEnemiesInRange(600f);
			//	}
			//}

			if (MiscMenu.Get<MenuKeybind>("SemiR").Active)
			{
				
				if (R.IsReady() && TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Magical) != null)
				{
					R.Cast(TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Magical), true);
				}
			}

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
