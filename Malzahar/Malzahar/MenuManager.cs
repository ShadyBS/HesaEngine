using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using SharpDX.DirectInput;

namespace Malzahar
{
    public static class MenuManager
    {
        public static Menu Home, ComboMenu, HarassMenu, AutoharassMenu, LaneclearMenu, LasthitMenu, FleeMenu, DrawingMenu, MiscMenu, KillstealMenu;

        private const string Prefix = " - ";

        public static void LoadMenu()
        {
            Home = Menu.AddMenu("Malzahar by BadCommand");
            
            //Main.Orb = new Orbwalker.OrbwalkerInstance(Home.AddSubMenu("Orbwalker"));

            ComboMenu = Home.AddSubMenu(Prefix + "Combo");
            ComboMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            ComboMenu.Add(new MenuCheckbox("useW", "Use W", true));
            ComboMenu.Add(new MenuCheckbox("useE", "Use E", true));
            ComboMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 0));


            HarassMenu = Home.AddSubMenu(Prefix + "Harass");
            HarassMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            HarassMenu.Add(new MenuCheckbox("useW", "Use W", true));
            HarassMenu.Add(new MenuCheckbox("useE", "Use E", true));
			HarassMenu.Add(new MenuCheckbox("autoE", "Use Auto E", true));
			HarassMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 10, 100, 50));


            LaneclearMenu = Home.AddSubMenu(Prefix + "Lane Clear");
            LaneclearMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            LaneclearMenu.Add(new MenuCheckbox("useW", "Use W", true));
            LaneclearMenu.Add(new MenuCheckbox("useE", "Use E", true));
            LaneclearMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 50));

            

            DrawingMenu = Home.AddSubMenu(Prefix + "Drawings");
            DrawingMenu.Add(new MenuCheckbox("enable", "Enable", true));
            DrawingMenu.Add(new MenuCheckbox("drawQ", "Draw Q", true));
            DrawingMenu.Add(new MenuCheckbox("drawE", "Draw E", true));
            DrawingMenu.Add(new MenuCheckbox("drawR", "Draw R", true));


            KillstealMenu = Home.AddSubMenu(Prefix + "KillSteal");
            KillstealMenu.Add(new MenuCheckbox("enable", "Enable", true));
            KillstealMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            KillstealMenu.Add(new MenuCheckbox("useW", "Use W", true));
            KillstealMenu.Add(new MenuCheckbox("useE", "Use E", true));
			KillstealMenu.Add(new MenuCheckbox("useR", "Use R", true));
            KillstealMenu.Add(new MenuCheckbox("useIgnite", "Use Ignite", true));
            KillstealMenu.Add(new MenuSlider("mana", "Mana % must be >= ", 0, 100, 0));


            MiscMenu = Home.AddSubMenu(Prefix + "Misc");
			MiscMenu.Add(new MenuCheckbox("inQ", "Interrupt Q", true));
			MiscMenu.Add(new MenuCheckbox("inR", "Interrupt R", true));
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
