using System;
using HesaEngine.SDK;
using SharpDX;
using static Malzahar.MenuManager;
using static Malzahar.SpellManager;
namespace Malzahar
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
            
            if (DrawingMenu.GetCheckbox("drawE"))
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, E.Range, Color.Red);
            }
            if (DrawingMenu.GetCheckbox("drawR"))
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, R.Range, Color.Green);
            }
        }
    }
}
