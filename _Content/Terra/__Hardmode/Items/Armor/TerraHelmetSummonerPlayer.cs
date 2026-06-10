using AAModClassic._Content.Hallow.__Hardmode.Items.Weapons;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    public class TerraHelmetSummonerPlayer : EquipEffectAbstract
    {
        public int CrystalMode = 0;

        public override void PostUpdate()
        {
            base.PostUpdate();

            if (effect)
            {
                if (AAMod.ArmorAbilityKey.JustPressed)
                {
                    CrystalMode++;
                    if (CrystalMode > 2)
                    {
                        CrystalMode = 0;
                    }
                }
                if (CrystalMode == 2)
                {
                    Player.lifeRegen += 12;
                    Player.statDefense.FinalMultiplier *= 1.2f;
                    Player.GetDamage(DamageClass.Generic) /= 2;
                }
            }
        }
    }
}