using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using SimpleTemplate.Modes;
using static HesaEngine.SDK.ObjectManager;
using static SimpleTemplate.SpellManager;
using static SimpleTemplate.MenuManager;
using static SimpleTemplate.DrawingManager;

namespace SimpleTemplate
{
    public class Main : IScript
    {
        //Don't forget to change the champion first!
        private const Champion Champion = HesaEngine.SDK.Enums.Champion.Blitzcrank;

        public string Name => "Blitzcrank by BadCommand";

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
			HesaEngine.SDK.Interrupter.OnInterruptableTarget += Modes.Interrupt.DoInterrupt;
			LoadDrawings();
            Game.OnTick += Game_OnTick;
            Chat.Print(Name + " Loaded Successfullyxxasd");
        }

        private static int _currentLevel = 1;

        private static void Game_OnTick()
        {
            var mana = Me.ManaPercent;

			if (MiscMenu.GetKeybind("manualQ"))
			{
				if (Q.IsReady())
				{
					AIHeroClient target;
					target = TargetSelector.GetTarget(QMenu.GetSlider("rangeQ"), TargetSelector.DamageType.Magical);
					if (target != null && !Functions.HasSpellShield(target))
					{
						Q.CastIfHitchanceEquals(target, HitChance.VeryHigh);
					}
				}
			}
			//if (KillstealMenu.GetCheckbox("enable"))
            //    Killsteal.DoKs();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Combo && mana >= ComboMenu.GetSlider("mana"))
                Combo.DoCombo();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Harass && mana >= HarassMenu.GetSlider("mana"))
                Harass.DoHarass();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.LaneClear && mana >= LaneclearMenu.GetSlider("mana"))
                LaneClear.DoLaneClear();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.LastHit && mana >= LasthitMenu.GetSlider("mana"))
                LaneClear.DoLaneClear();

            if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Flee && mana >= FleeMenu.GetSlider("mana"))
                Flee.DoFlee();

            //This is a basic fix, due to OnLevelUp being disabled
            if (_currentLevel == Player.Level) return;
            Leveler();
            _currentLevel = Player.Level;
        }
    }
}
