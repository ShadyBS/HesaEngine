using System;
using System.Data.Common;
using System.Media;
using HesaEngine.SDK;
using HesaEngine.SDK.Enums;
using HesaEngine.SDK.GameObjects;
using static SimpleTemplate.MenuManager;

namespace SimpleTemplate
{
    public static class SpellManager
    {
        public static Spell Q, W, E, R;

        public static void LoadSpells()
        {
            Q = new Spell(SpellSlot.Q, 925, TargetSelector.DamageType.Magical);
            W = new Spell(SpellSlot.W, 300);
            E = new Spell(SpellSlot.E, 250);
            R = new Spell(SpellSlot.R, 550, TargetSelector.DamageType.Magical);

			Q.SetSkillshot(0.25f, 80f, 1800f, true, SkillshotType.SkillshotLine);

           
            Main.QPred = new PredictionInput
            {
                Delay = Q.Delay,
                Radius = Q.Width,
                Speed = Q.Speed,
                Type = Q.Type,
                CollisionObjects = new[]
                {
                    CollisionableObjects.Heroes,
                    CollisionableObjects.Minions,
                    CollisionableObjects.YasuoWall
                }
            };
            //Disabled since 7.10
            //AIHeroClient.OnLevelUp += AIHeroClient_OnLevelUp;
        }

        private static void AIHeroClient_OnLevelUp(AIHeroClient sender, int level)
        {
            if (!MiscMenu.GetCheckbox("level") || !sender.IsMe) return;
            var delay = MiscMenu.GetSlider("levelDelay");

            Core.DelayAction(AutoLeveler, delay);
        }

        public static void Leveler()
        {
            if (!MiscMenu.GetCheckbox("level")) return;
            var delay = MiscMenu.GetSlider("levelDelay");

            Core.DelayAction(AutoLeveler, delay);
        }

        private static void AutoLeveler()
        {
            if (R.CanLevel())
                ObjectManager.Player.Spellbook.LevelUpSpell(SpellSlot.R);

            var first = GetSlot(MiscMenu.GetCombobox("levelFirst"));
            var second = GetSlot(MiscMenu.GetCombobox("levelSecond"));
            var third = GetSlot(MiscMenu.GetCombobox("levelThird"));

            if(ObjectManager.Player.Level == 4 && ObjectManager.Player.Spellbook.GetSpell(third).Slot.CanLevel() && ObjectManager.Player.Spellbook.GetSpell(third).Level <= 0)
                ObjectManager.Player.Spellbook.LevelUpSpell(third);

            if (ObjectManager.Player.Spellbook.GetSpell(first).Slot.CanLevel())
                ObjectManager.Player.Spellbook.LevelUpSpell(first);
                
            if (ObjectManager.Player.Spellbook.GetSpell(second).Slot.CanLevel())
                ObjectManager.Player.Spellbook.LevelUpSpell(second);

            if (ObjectManager.Player.Spellbook.GetSpell(third).Slot.CanLevel())
                ObjectManager.Player.Spellbook.LevelUpSpell(third);

        }

        private static SpellSlot GetSlot(this int value)
        {
            switch (value)
            {
                case 0:
                    return SpellSlot.Q;
                case 1:
                    return SpellSlot.W;
                case 2:
                    return SpellSlot.E;
            }
            Chat.Print("Failed to get SpellSlot");
            return SpellSlot.Unknown;
        }

        public static double GetSpellDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            var slotLevel = ObjectManager.Player.GetSpell(slot).Level - 1;
            var ap = ObjectManager.Player.FlatMagicDamageMod;
            var ad = ObjectManager.Player.FlatBaseAttackDamageMod;
            double damage = 0;

            switch (slot)
            {
                case SpellSlot.Q:
                    //Damage caused each upgrade                             AD/AP Scaling
                    damage += new[] { 10, 20, 30, 40, 50 }[slotLevel] + (0.10 * ad);
                    break;
                case SpellSlot.W:
                    //Damage caused each upgrade                             AD/AP Scaling
                    damage += new[] { 10, 20, 30, 40, 50 }[slotLevel] + (0.10 * ap);
                    break;
                case SpellSlot.E:
                    //Damage caused each upgrade                             AD/AP Scaling
                    damage += new[] { 10, 20, 30, 40, 50 }[slotLevel] + (0.10 * ad);
                    break;
                case SpellSlot.R:
                    //Damage caused each upgrade                   AD/AP Scaling
                    damage += new[] { 10, 20, 30 }[slotLevel] + (0.10 * ap);
                    break;
            } //                                                                      -10 Cause we need to add also the health regen. So we give an approximate of 10
            return ObjectManager.Player.CalcDamage(target, Damage.DamageType.Magical, damage - 10);
        }

        public static double GetTotalDamage(this Obj_AI_Base target)
        {
            var ap = ObjectManager.Player.FlatMagicDamageMod;
            var ad = ObjectManager.Player.FlatBaseAttackDamageMod;
            double damage = 0;
            
            //Damage caused each upgrade                        AD/AP Scaling
            damage += new[] { 10, 20, 30, 40, 50 }[Q.Level - 1] + (0.10 * ad);
            //Damage caused each upgrade                        AD/AP Scaling
            damage += new[] { 10, 20, 30, 40, 50 }[W.Level - 1] + (0.10 * ap);
            //Damage caused each upgrade                        AD/AP Scaling
            damage += new[] { 10, 20, 30, 40, 50 }[E.Level - 1] + (0.10 * ad);
            //Damage caused each upgrade                AD/AP Scaling
            damage += new[] { 10, 20, 30 }[R.Level - 1] + (0.10 * ap);
            //                                                                      -10 Cause we need to add also the health regen. So we give an approximate of 10
            return ObjectManager.Player.CalcDamage(target, Damage.DamageType.Magical, damage);
        }
    }
}