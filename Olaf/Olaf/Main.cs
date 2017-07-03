using HesaEngine.SDK;
using HesaEngine.SDK.Args;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using static HesaEngine.SDK.ObjectManager;
using Olaf.Modes;
using static Olaf.SpellManager;
using static Olaf.MenuManager;
using static Olaf.DrawingManager;

namespace Olaf
{
    public class Main : IScript
    {
        //Don't forget to change the champion first!
        private const Champion Champion = HesaEngine.SDK.Enums.Champion.Olaf;

        public string Name => "Olaf by BadCommand" ;

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
			Orbwalker.BeforeAttack += Before_Attack;
			Orbwalker.AfterAttack += Orbwalker_AfterAttack;
			Chat.Print(Name + " Loaded Successfully");
        }

		public static void Before_Attack(BeforeAttackEventArgs ArgsTarget)
		{
			if (Orb.ActiveMode.Equals(Orbwalker.OrbwalkingMode.Combo) && ComboMenu.GetCheckbox("useW"))
			{
				W.Cast();
			}
			if (Orb.ActiveMode.Equals(Orbwalker.OrbwalkingMode.Harass) && HarassMenu.GetCheckbox("useW"))
			{
				W.Cast();
			}
			if (Orb.ActiveMode.Equals(Orbwalker.OrbwalkingMode.LaneClear) && LaneclearMenu.GetCheckbox("useW"))
			{
				W.Cast();
			}
			if (Orb.ActiveMode.Equals(Orbwalker.OrbwalkingMode.JungleClear) && JungleclearMenu.GetCheckbox("useW"))
			{
				W.Cast();
			}
		}

		public static Item THydra;

		private void Orbwalker_AfterAttack(HesaEngine.SDK.GameObjects.AttackableUnit unit, HesaEngine.SDK.GameObjects.AttackableUnit target)
		{
			THydra = new Item(3748, 385);
			if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.Combo && ComboMenu.GetCheckbox("useH"))
			{
				if (Item.CanUseItem(ItemId.Ravenous_Hydra_Melee_Only))
				{
					Item.UseItem(ItemId.Ravenous_Hydra_Melee_Only);
				}
				else if (Item.CanUseItem(ItemId.Tiamat_Melee_Only))
				{
					Item.UseItem(ItemId.Tiamat_Melee_Only);
				}
				else if (Item.HasItem(3748) && THydra.IsReady())
				{
					Item.UseItem(3748);
					Orbwalker.ResetAutoAttackTimer();
				}
			}
		}

		private static int _currentLevel = 1;

		private static void Game_OnTick()
        {
            var mana = Me.ManaPercent;

			if (HarassMenu.GetCheckbox("autoE") && ObjectManager.Me.HealthPercent > HarassMenu.GetSlider("minE") && mana >= HarassMenu.GetSlider("mana"))
			{
				if (E.IsReady())
				{
					var target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
					if (target != null)
					{
						E.CastOnUnit(target);
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

			if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.JungleClear && mana >= JungleclearMenu.GetSlider("mana"))
				JungleClear.DoJungleClear();

			if (Orb.ActiveMode == Orbwalker.OrbwalkingMode.LastHit && mana >= LasthitMenu.GetSlider("mana"))
				LastHit.DoLastHit();


			//This is a basic fix, due to OnLevelUp being disabled
			if (_currentLevel == Player.Level) return;
            Leveler();
            _currentLevel = Player.Level;
        }
    }
}
