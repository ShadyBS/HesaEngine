using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX.DirectInput;

namespace Olaf
{
    public static class MenuManager
    {
        public static Menu Home, ComboMenu, HarassMenu, AutoharassMenu, LaneclearMenu, LasthitMenu, FleeMenu, DrawingMenu, MiscMenu, KillstealMenu, JungleclearMenu;

        private const string Prefix = " - ";

        public static void LoadMenu()
        {
            Home = Menu.AddMenu("Olaf by BadCommand");
            
            //Main.Orb = new Orbwalker.OrbwalkerInstance(Home.AddSubMenu("Orbwalker"));

            ComboMenu = Home.AddSubMenu(Prefix + "Combo");
            ComboMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
			ComboMenu.Add(new MenuSlider("maxQ", "Max Q range", 400, 1000, 900));
			ComboMenu.Add(new MenuCheckbox("useW", "Use W", true));
            ComboMenu.Add(new MenuCheckbox("useE", "Use E", true));
			ComboMenu.Add(new MenuSlider("minE", "Stop using E if my health % <", 0, 100, 0));
			ComboMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 0));


            HarassMenu = Home.AddSubMenu(Prefix + "Harass");
            HarassMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
			HarassMenu.Add(new MenuSlider("maxQ", "Max Q range", 400, 1000, 900));
			HarassMenu.Add(new MenuCheckbox("useW", "Use W", true));
            HarassMenu.Add(new MenuCheckbox("useE", "Use E", true));
			HarassMenu.Add(new MenuCheckbox("autoE", "Use Auto E", true));
			HarassMenu.Add(new MenuSlider("minE", "Stop using E if my health % <", 0, 100, 0));
			HarassMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 50));


            LaneclearMenu = Home.AddSubMenu(Prefix + "Lane Clear");
            LaneclearMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            LaneclearMenu.Add(new MenuCheckbox("useW", "Use W", true));
            LaneclearMenu.Add(new MenuCheckbox("useE", "Use E", true));
            LaneclearMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 50));

			JungleclearMenu = Home.AddSubMenu(Prefix + "Jungle Clear");
			JungleclearMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
			JungleclearMenu.Add(new MenuCheckbox("useW", "Use W", true));
			JungleclearMenu.Add(new MenuCheckbox("useE", "Use E", true));
			JungleclearMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 50));

			LasthitMenu = Home.AddSubMenu(Prefix + "Last Hit");
			LasthitMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
			LasthitMenu.Add(new MenuCheckbox("useE", "Use E", true));
			LasthitMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 50));


			DrawingMenu = Home.AddSubMenu(Prefix + "Drawings");
            DrawingMenu.Add(new MenuCheckbox("enable", "Enable", true));
            DrawingMenu.Add(new MenuCheckbox("drawQ", "Draw Q", true));
            DrawingMenu.Add(new MenuCheckbox("drawE", "Draw E", true));


            KillstealMenu = Home.AddSubMenu(Prefix + "KillSteal");
            KillstealMenu.Add(new MenuCheckbox("enable", "Enable", true));
            KillstealMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            KillstealMenu.Add(new MenuCheckbox("useE", "Use E", true));
            KillstealMenu.Add(new MenuCheckbox("useIgnite", "Use Ignite", true));
            KillstealMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 0));


            MiscMenu = Home.AddSubMenu(Prefix + "Misc");
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
