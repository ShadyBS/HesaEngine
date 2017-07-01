using HesaEngine.SDK;
using HesaEngine.SDK.GameObjects;

namespace Olaf
{
    public static class Pred
    {
        public static void PredictionCast(this Spell spell, Obj_AI_Base target, HitChance hit = HitChance.High)
        {
            var pred = spell.GetPrediction(target);
            if (pred.Hitchance >= hit)
            {
                spell.Cast(pred.CastPosition, true);
            }
        }
    }
}
