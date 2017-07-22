using System;
using HesaEngine.SDK;
using SharpDX;
using static Syndra.MenuManager;
using static Syndra.SpellManager;
namespace Syndra
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

			if (DrawingMenu.GetCheckbox("drawQ") && Q.IsReady())
			{
				Drawing.DrawCircle(ObjectManager.Me.Position, Q.Range, Color.Red);
			}

			if (DrawingMenu.GetCheckbox("drawW") && W.IsReady())
			{
				Drawing.DrawCircle(ObjectManager.Me.Position, W.Range, Color.Red);
			}

			if (DrawingMenu.GetCheckbox("drawE") && E.IsReady())
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, E.Range, Color.Red);
            }

			if (DrawingMenu.GetCheckbox("drawEQ") && E.IsReady())
			{
				Drawing.DrawCircle(ObjectManager.Me.Position, EQ.Range, Color.Red);
			}

			if (DrawingMenu.GetCheckbox("drawR") && R.IsReady())
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, R.Range, Color.Green);
            }
        }
    }
}
