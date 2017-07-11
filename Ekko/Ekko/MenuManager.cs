using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX.DirectInput;

namespace Ekko
{
    public static class MenuManager
    {
        public static Menu Home, ComboMenu, HarassMenu, AutoharassMenu, LaneclearMenu, JungleclearMenu, LasthitMenu, FleeMenu, DrawingMenu, MiscMenu, KillstealMenu;

        private const string Prefix = " - ";

        public static void LoadMenu()
        {
            Home = Menu.AddMenu("Ekko by BadCommand");
            
            //Main.Orb = new Orbwalker.OrbwalkerInstance(Home.AddSubMenu("Orbwalker"));

            ComboMenu = Home.AddSubMenu(Prefix + "Combo");
            ComboMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            ComboMenu.Add(new MenuCheckbox("useW", "Use W", true));
			ComboMenu.Add(new MenuCheckbox("useW2", "Use W without prediction", true));
			ComboMenu.Add(new MenuCheckbox("useE", "Use E", true));
			ComboMenu.Add(new MenuCheckbox("nodiveE", "Dont use E on dives", true));
			ComboMenu.Add(new MenuCheckbox("useR", "Use R", true));
			ComboMenu.Add(new MenuSlider("minR", "Minimum enemies to R", 1, 5, 3));
			ComboMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 0));


            HarassMenu = Home.AddSubMenu(Prefix + "Harass");
            HarassMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            HarassMenu.Add(new MenuCheckbox("useW", "Use W", true));
			HarassMenu.Add(new MenuCheckbox("useW2", "Use W without prediction", true));
			HarassMenu.Add(new MenuCheckbox("useE", "Use E", true));
			HarassMenu.Add(new MenuCheckbox("nodiveE", "Dont use E on dives", true)); ;
			HarassMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 50));


            LaneclearMenu = Home.AddSubMenu(Prefix + "Lane Clear");
            LaneclearMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
			LaneclearMenu.Add(new MenuSlider("minQ", "Minions to use Q", 0, 6, 4));
            LaneclearMenu.Add(new MenuCheckbox("useE", "Use E", true));
            LaneclearMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 50));

			JungleclearMenu = Home.AddSubMenu(Prefix + "Jungle Clear");
			JungleclearMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
			JungleclearMenu.Add(new MenuSlider("minQ", "Minions to use Q", 0, 6, 4));
			JungleclearMenu.Add(new MenuCheckbox("useE", "Use E", true));
			JungleclearMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 50));


			DrawingMenu = Home.AddSubMenu(Prefix + "Drawings");
            DrawingMenu.Add(new MenuCheckbox("enable", "Enable", true));
            DrawingMenu.Add(new MenuCheckbox("drawQ", "Draw Q", true));
			DrawingMenu.Add(new MenuCheckbox("drawW", "Draw W", true));
			DrawingMenu.Add(new MenuCheckbox("drawE", "Draw E", true));
            DrawingMenu.Add(new MenuCheckbox("drawR", "Draw R", true));


            KillstealMenu = Home.AddSubMenu(Prefix + "KillSteal");
            KillstealMenu.Add(new MenuCheckbox("enable", "Enable", true));
            KillstealMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            KillstealMenu.Add(new MenuCheckbox("useE", "Use E", true));
			KillstealMenu.Add(new MenuCheckbox("useR", "Use R", true));
            KillstealMenu.Add(new MenuCheckbox("useIgnite", "Use Ignite", true));
            KillstealMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 0));


            MiscMenu = Home.AddSubMenu(Prefix + "Misc");
			MiscMenu.Add(new MenuCheckbox("saveMe", "Save me with R", true));
			MiscMenu.Add(new MenuSlider("hpR", "Health % must be >= ", 0, 100, 20));
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
