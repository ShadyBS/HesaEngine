using HesaEngine.SDK;
using HesaEngine.SDK.Args;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using Syndra.Modes;
using static HesaEngine.SDK.ObjectManager;
using static Syndra.SpellManager;
using static Syndra.MenuManager;
using static Syndra.DrawingManager;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Syndra
{
    public class Main : IScript
    {
        //Don't forget to change the champion first!
        private const Champion Champion = HesaEngine.SDK.Enums.Champion.Syndra;

        public string Name => "Syndra by BadCommand" ;

		public string Version => "1.0.0";

        public string Author => "BadCommand";

        public static PredictionInput QPred, WPred, EPred, RPred;

        public static Orbwalker.OrbwalkerInstance Orb => Core.Orbwalker;

		public static bool DoEQ = false;

		public static bool waitW = false;

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
			GameObject.OnCreate += On_Create;
			GameObject.OnDelete += On_Delete;
			Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
			HesaEngine.SDK.Interrupter.OnInterruptableTarget += Modes.Interrupt.DoInterrupt;
			Chat.Print(Name + " Loaded Successfully");
		}

		public static List<Obj_AI_Minion> MyBalls = new List<Obj_AI_Minion>();

		public static void On_Create(GameObject sender, EventArgs args)
		{
			if (sender.Name.ToLower() == "seed")
			{
				MyBalls.Add(sender as Obj_AI_Minion);
			}
		}

		public static void On_Delete(GameObject sender, EventArgs args)
		{
			if (sender.Name.ToLower() == "seed")
			{
				MyBalls.Remove(sender as Obj_AI_Minion);
			}
		}

		public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
		{
			if (sender.IsMe && DoEQ && args.SData.Name == "SyndraQ" && E.IsReady())
			{
				var customeDelay = Q.Delay - (E.Delay + ((Player.Distance(args.End)) / E.Speed));
				Core.DelayAction(() => E.Cast(args.End), (int)(customeDelay * 1000));
				Main.DoEQ = false;
			}
			if (sender.IsMe & args.SData.Name == "SyndraE" && W.IsReady())
			{
				waitW = true;
				Core.DelayAction(() => waitW = false, 500);
				
			}
		}


		private static int _currentLevel = 1;

		private static void Game_OnTick()
        {
            var mana = Me.ManaPercent;

			if (MiscMenu.GetKeybind("manualEQ") && E.IsReady() && Q.IsReady())
			{
				Q.Cast(ObjectManager.Player.Position.Extend(Game.CursorPosition, Q.Range));
				DoEQ = true;
			}

			if (MiscMenu.GetKeybind("semimanualEQ") && E.IsReady() && Q.IsReady())
			{
				var target = Heroes.Enemies.Where(enemy => enemy.IsValidTarget(EQ.Range)).OrderBy(enemy => enemy.Distance(Game.CursorPosition)).FirstOrDefault();
				var qpos = DarkPrediction.CirclerPrediction(Q, (AIHeroClient)target, 1);
				if (qpos != DarkPrediction.empt && qpos.Distance(ObjectManager.Player) <= Q.Range)
					Q.Cast(qpos);
				DoEQ = true;
			}

			if (DoEQ && !E.IsReady())
				DoEQ = false;

			if (KillstealMenu.GetCheckbox("enable"))
               Killsteal.DoKs();

			if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.JungleClear && mana >= JungleclearMenu.GetSlider("mana"))
				JungleClear.DoJungleClear();

			if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.LastHit && mana >= LasthitMenu.GetSlider("mana"))
				LastHit.DoLastHit();

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
