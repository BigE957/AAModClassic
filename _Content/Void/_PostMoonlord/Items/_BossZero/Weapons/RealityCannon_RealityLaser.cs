using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Ammo;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class RealityCannon_RealityLaser : UnstablePowerCell_Proj
    {
        public override string Texture => ModContent.GetInstance<UnstablePowerCell_Proj>().Texture;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reality Laser");
        }
    }
}
