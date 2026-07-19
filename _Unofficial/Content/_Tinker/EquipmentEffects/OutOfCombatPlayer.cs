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
    public class OutOfCombatPlayer : ModPlayer
    {
        public List<Action> OutOfCombatEffectsToPerform;

        public bool IsOutOfCombat;

        public int OutOfCombatThresholdModifier;
        private int _outOfCombatTimer = 0;
        public int OutOfCombatTimer
        {
            get { return _outOfCombatTimer; }
        }
        private int _outOfCombatThreshold;
        private const int _OUTOFCOMBATHRESHOLDBASE = 8 * 60;

        public override void ResetEffects()
        {
            OutOfCombatEffectsToPerform = new();
            IsOutOfCombat = false;
            OutOfCombatThresholdModifier = 0;

            _outOfCombatThreshold = _OUTOFCOMBATHRESHOLDBASE + OutOfCombatThresholdModifier;
        }

        public override void UpdateEquips()
        {
            if (_outOfCombatTimer > _outOfCombatThreshold)
            {
                IsOutOfCombat = true;
            }

            if (IsOutOfCombat)
            {
                foreach (var effect in OutOfCombatEffectsToPerform)
                {
                    effect.Invoke();
                }
            }

            _outOfCombatTimer++;
        }

        public override void OnHitAnything(float x, float y, Entity victim)
        {
            _outOfCombatTimer = 0;
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (hurtInfo.Damage > Player.statLifeMax2 * 0.05f)
                _outOfCombatTimer = 0;
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (hurtInfo.Damage > Player.statLifeMax2 * 0.05f)
                _outOfCombatTimer = 0;
        }
    }
}