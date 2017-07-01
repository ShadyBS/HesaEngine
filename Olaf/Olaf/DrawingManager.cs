using System;
using HesaEngine.SDK;
using SharpDX;
using static Olaf.MenuManager;
using static Olaf.SpellManager;
namespace Olaf
{
    public static class DrawingManager
    {
        public static void LoadDrawings()
        {
            Drawing.OnDraw += Drawing_OnDraw;
        }
        
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (!DrawingMenu.GetCheckbox("enable")) return;

			if (DrawingMenu.GetCheckbox("drawQ"))
			{
				Drawing.DrawCircle(ObjectManager.Me.Position, Q.Range, Color.Red);
			}
			if (DrawingMenu.GetCheckbox("drawE"))
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, E.Range, Color.Red);
            }
        }
    }
}
