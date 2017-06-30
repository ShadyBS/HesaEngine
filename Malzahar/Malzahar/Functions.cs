using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HesaEngine;
using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;

namespace Malzahar
{
    public static class Functions
    {
        public static bool HasSpellShield(this AIHeroClient target)
        {
            return target.HasBuff("BlackShield") //Morgana E
                   || target.HasBuff("SivirE") //Sivir E
                   || target.HasBuff("NocturneShroudofDarkness") //Nocturne E
                   || target.HasBuff("bansheesveil") //Banshee
                   || target.HasBuff("itemmagekillerveil"); //
        }

        public static bool IsNotKillable(this AIHeroClient target)
        {
            return target.HasBuff("UndyingRage") //Tryndamere R
                   || target.HasBuff("JudicatorIntervention") //Kayle R
                   || target.HasBuff("VladimirSanguinePool") //Vladimir W
                   || target.HasBuff("KindredRNoDeathBuff") //Kindred R
                   || target.HasSpellShield(); //SpellShields
        }

        #region Summoners
        
        public static SpellSlot GetSummoner(string summoner)
        {
            return ObjectManager.Player.GetSpellSlot(summoner);
        }
        public static SpellSlot GetIgniteSlot()
        {
            return GetSummoner("SummonerDot");
        }
        public static SpellSlot GetHealSlot()
        {
            return GetSummoner("SummonerHeal");
        }
        public static SpellSlot GetFlashSlot()
        {
            return GetSummoner("SummonerFlash");
        }
        public static SpellSlot GetBarrierSlot()
        {
            return GetSummoner("SummonerBarrier");
        }
        public static SpellSlot GetExhaustSlot()
        {
            return GetSummoner("SummonerExhaust");
        }
        public static SpellSlot GetSmiteSlot()
        {
            return GetSummoner("SummonerSmite");
        }

        #endregion Summoners

        public static void UseIgnite()
        {
            var summoner = GetIgniteSlot();
            if (!summoner.IsReady()) return;

            var enemy = ObjectManager.Heroes.Enemies.FirstOrDefault(
                x => x.IsValidTarget(600) && !x.IsDead && GetIgniteDamage(GetIgniteSlot()) > MinionHealthPrediction.GetHealthPrediction(x, 250));
            ObjectManager.Player.Spellbook.CastSpell(summoner, enemy);

        }

        public static int GetIgniteDamage(SpellSlot summoner)
        {
            return ObjectManager.Player.Level * 20 + 50;
        }
    }
}
