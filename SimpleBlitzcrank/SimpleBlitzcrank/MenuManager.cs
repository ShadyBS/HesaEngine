using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX.DirectInput;

namespace SimpleTemplate
{
    public static class MenuManager
    {
        public static Menu Home, QMenu, ComboMenu, HarassMenu, AutoharassMenu, LaneclearMenu, LasthitMenu, FleeMenu, DrawingMenu, MiscMenu, KillstealMenu;

        private const string Prefix = " - ";

        public static void LoadMenu()
        {
            Home = Menu.AddMenu(ObjectManager.Me.Hero + "by BadCommand");
            
            //Main.Orb = new Orbwalker.OrbwalkerInstance(Home.AddSubMenu("Orbwalker"));

            ComboMenu = Home.AddSubMenu(Prefix + "Combo");
            ComboMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            ComboMenu.Add(new MenuCheckbox("useE", "Use E", true));
            ComboMenu.Add(new MenuCheckbox("useR", "Use R", true));
			ComboMenu.Add(new MenuSlider("minR", "Ult if enemies around", 1, 5, 3));
			ComboMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 0));

			QMenu = Home.AddSubMenu(Prefix + "Q Options");
			QMenu.Add(new MenuSlider("rangeQ", "Maximum Range for Q", 200, 925, 925));
			QMenu.Add(new MenuSlider("minQ", "Minimum Range for Q", 200, 925, 300));
			foreach (var hero in HesaEngine.SDK.ObjectManager.Heroes.Enemies)
			{
				QMenu.Add(new MenuCheckbox("blq" + hero.ChampionName, hero.ChampionName, false));
			}

			HarassMenu = Home.AddSubMenu(Prefix + "Harass");
            HarassMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            HarassMenu.Add(new MenuCheckbox("useE", "Use E", true));
            HarassMenu.Add(new MenuCheckbox("useR", "Use R", true));
			HarassMenu.Add(new MenuSlider("minR", "Ult if enemies around", 1, 5, 3));
			HarassMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 30));


            FleeMenu = Home.AddSubMenu(Prefix + "Flee");
			FleeMenu.Add(new MenuCheckbox("useW", "Use W", true));
            FleeMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 0));
            

            DrawingMenu = Home.AddSubMenu(Prefix + "Drawings");
            DrawingMenu.Add(new MenuCheckbox("enable", "Enable", true));
            DrawingMenu.Add(new MenuCheckbox("drawQ", "Draw Q", false));
            DrawingMenu.Add(new MenuCheckbox("drawE", "Draw E", false));
            DrawingMenu.Add(new MenuCheckbox("drawR", "Draw R", true));
			DrawingMenu.Add(new MenuCheckbox("drawminR", "Draw min Q", true));
			DrawingMenu.Add(new MenuCheckbox("drawmaxR", "Draw max Q", true));

			KillstealMenu = Home.AddSubMenu(Prefix + "KillSteal");
            KillstealMenu.Add(new MenuCheckbox("enable", "Enable", true));
			KillstealMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
			KillstealMenu.Add(new MenuCheckbox("useR", "Use R", true));
			KillstealMenu.Add(new MenuCheckbox("useIgnite", "Use Ignite", true));
			KillstealMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 10));


			MiscMenu = Home.AddSubMenu(Prefix + "Misc");
			MiscMenu.Add(new MenuCheckbox("useDP", "Use DarkPrediction", true));
			MiscMenu.Add(new MenuCheckbox("useRafterQ", "Follow up Q with R", true));
			MiscMenu.Add(new MenuKeybind("manualQ", "Manual grab no blacklist", SharpDX.DirectInput.Key.A, MenuKeybindType.Hold));
			MiscMenu.Add(new MenuCheckbox("agQ", "AntiGapclose Q", true));
			MiscMenu.Add(new MenuCheckbox("inQ", "Interrupt Q", true));
			MiscMenu.Add(new MenuCheckbox("inE", "Interrupt E", true));
			MiscMenu.Add(new MenuCheckbox("inR", "Interrupt R", true));
			MiscMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 30));
            MiscMenu.Add(new MenuCheckbox("level", "Enable Spell Leveler", false));
            MiscMenu.Add(new MenuSlider("levelDelay", "Level UP Delay", 10, 1000, 200));
            MiscMenu.Add(new MenuCombo("levelFirst", "Level UP First", new[] { "Q", "W", "E" }));
            MiscMenu.Add(new MenuCombo("levelSecond", "Level UP Second", new[] { "Q", "W", "E" }, 1));
            MiscMenu.Add(new MenuCombo("levelThird", "Level UP Third", new[] { "Q", "W", "E" }, 2));


        }

        public static bool GetCheckbox(this Menu menu, string value)
        {
            return menu.Get<MenuCheckbox>(value).Checked;
        }

        public static bool GetKeybind(this Menu menu, string value)
        {
            return menu.Get<MenuKeybind>(value).Active;
        }

        public static int GetSlider(this Menu menu, string value)
        {
            return menu.Get<MenuSlider>(value).CurrentValue;
        }

        public static int GetCombobox(this Menu menu, string value)
        {
            return menu.Get<MenuCombo>(value).CurrentValue;
        }
    }
}
