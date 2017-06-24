using HesaEngine.SDK;
using SharpDX.DirectInput;

namespace Simple
{
    public static class MenuManager
    {
        public static Menu Home, ComboMenu, HarassMenu, AutoharassMenu, LaneclearMenu, JungleclearMenu, LasthitMenu, FleeMenu, DrawingMenu, MiscMenu, KillstealMenu;

        private const string Prefix = " - ";

        public static void LoadMenu()
        {
            Home = Menu.AddMenu("Shyvana by BadCommand");
            
            //Main.Orb = new Orbwalker.OrbwalkerInstance(Home.AddSubMenu("Orbwalker"));

            ComboMenu = Home.AddSubMenu(Prefix + "Combo");
            ComboMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            ComboMenu.Add(new MenuCheckbox("useW", "Use W", true));
            ComboMenu.Add(new MenuCheckbox("useE", "Use E", true));


            HarassMenu = Home.AddSubMenu(Prefix + "Harass");
            HarassMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            HarassMenu.Add(new MenuCheckbox("useW", "Use W", true));
            HarassMenu.Add(new MenuCheckbox("useE", "Use E", true));


            LaneclearMenu = Home.AddSubMenu(Prefix + "Lane Clear");
            LaneclearMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            LaneclearMenu.Add(new MenuCheckbox("useW", "Use W", true));
            LaneclearMenu.Add(new MenuCheckbox("useE", "Use E", true));

			JungleclearMenu = Home.AddSubMenu(Prefix + "Jungle Clear");
			JungleclearMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
			JungleclearMenu.Add(new MenuCheckbox("useW", "Use W", true));
			JungleclearMenu.Add(new MenuCheckbox("useE", "Use E", true));

			LasthitMenu = Home.AddSubMenu(Prefix + "LastHit");
            LasthitMenu.Add(new MenuCheckbox("useQ", "Use Q", true));
            LasthitMenu.Add(new MenuCheckbox("useW", "Use W", true));
            LasthitMenu.Add(new MenuCheckbox("useE", "Use E", true));


            FleeMenu = Home.AddSubMenu(Prefix + "Flee");
            FleeMenu.Add(new MenuCheckbox("useW", "Use W", true));
            

            DrawingMenu = Home.AddSubMenu(Prefix + "Drawings");
            DrawingMenu.Add(new MenuCheckbox("enable", "Enable", true));
            DrawingMenu.Add(new MenuCheckbox("drawW", "Draw W", true));
            DrawingMenu.Add(new MenuCheckbox("drawE", "Draw E", true));


            KillstealMenu = Home.AddSubMenu(Prefix + "KillSteal");
            KillstealMenu.Add(new MenuCheckbox("enable", "Enable", true));
            KillstealMenu.Add(new MenuCheckbox("useE", "Use E", true));


            MiscMenu = Home.AddSubMenu(Prefix + "Misc");
            MiscMenu.Add(new MenuCheckbox("level", "Enable Spell Leveler", true));
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
