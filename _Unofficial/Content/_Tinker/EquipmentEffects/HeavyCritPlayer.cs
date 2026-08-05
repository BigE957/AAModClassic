using AAModClassic._Content.Terra.__Hardmode.Items.Armor;
using AAModClassic._Unreleased.Content.Void.Dusts;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.EquipmentEffects
{
    public class HeavyCritPlayer : ModPlayer
    {
        public int HeavyCritChance;
        public bool ForceHeavyCritOnNextAttack = false;
        private bool _doHeavyCrit;

        public List<Func<NPC, NPC.HitModifiers, Player, int>> OddChanceModifiers;
        /// <summary>
        /// this code runs AFTER the 1.5x damage multiplier is given but before damage is dealt
        /// </summary>
        public List<Action<NPC, NPC.HitModifiers, Player>> EffectsOnCrit_BeforeDamage;
        public List<Action<NPC, NPC.HitInfo, int, Player>> EffectsOnCrit_AfterDamage;

        public override void ResetEffects()
        {
            HeavyCritChance = 0;
            _doHeavyCrit = false;
            OddChanceModifiers = new();
            EffectsOnCrit_BeforeDamage = new();
            EffectsOnCrit_AfterDamage = new();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            //TODO: this probably shouldnt proc forf stuff that cant crit (ex. summons)
            if (HeavyCritChance > 0 || ForceHeavyCritOnNextAttack)
            {
                foreach (var effect in OddChanceModifiers)
                    HeavyCritChance += effect(target, modifiers, Player);

                if (HeavyCritChance > Main.rand.Next(100) + 1 || ForceHeavyCritOnNextAttack)
                {
                    _doHeavyCrit = true;
                    ForceHeavyCritOnNextAttack = false;

                    modifiers.HideCombatText();
                    modifiers.SetCrit();
                    modifiers.CritDamage += 1f;

                    foreach (var effect in EffectsOnCrit_BeforeDamage)
                        effect.Invoke(target, modifiers, Player);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (_doHeavyCrit)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 speedNShit = Player.Center.DirectionTo(target.Center) * 5;
                    speedNShit *= new Vector2(Main.rand.NextFloat(0.6f, 1.2f), Main.rand.NextFloat(0.8f, 1.2f));
                    Dust.NewDust(target.Center, 1, 1, DustID.Blood, speedNShit.X, speedNShit.Y, Scale: Main.rand.NextFloat(1, 1.75f));
                }

                SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Custom/SpecialCrit") with { PitchVariance = 0.5f, MaxInstances = 0, Volume = 0.5f }, target.Center);

                int combatTextID = CombatText.NewText(target.Hitbox, AAColor.HeavyCritCombatText, hit.Damage, true);
                if (combatTextID != 100)
                    Main.combatText[combatTextID].position.Y -= 30f;

                foreach (var effect in EffectsOnCrit_AfterDamage)
                    effect.Invoke(target, hit, damageDone, Player);

                _doHeavyCrit = false;
            }
        }
    }
}