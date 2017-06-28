using System;
using HesaEngine.SDK;
using SharpDX;
using static SimpleTemplate.MenuManager;
using static SimpleTemplate.SpellManager;
namespace SimpleTemplate
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
				Drawing.DrawCircle(ObjectManager.Me.Position, Q.Range, Color.White);
			}
            if (DrawingMenu.GetCheckbox("drawE"))
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, E.Range, Color.Red);
            }
            if (DrawingMenu.GetCheckbox("drawR"))
            {
                Drawing.DrawCircle(ObjectManager.Me.Position, R.Range, Color.Green);
            }
			if (DrawingMenu.GetCheckbox("drawmaxR"))
			{
				Drawing.DrawCircle(ObjectManager.Me.Position, QMenu.GetSlider("rangeQ"), Color.Green);
			}
			if (DrawingMenu.GetCheckbox("drawminR"))
			{
				Drawing.DrawCircle(ObjectManager.Me.Position, QMenu.GetSlider("minQ"), Color.Green);
			}
		}
    }
}
