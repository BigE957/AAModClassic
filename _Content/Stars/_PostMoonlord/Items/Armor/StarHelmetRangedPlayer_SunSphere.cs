using AAModClassic._Content.Stars.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class StarHelmetRangedPlayer_SunSphere : StarHelmetRangedPlayer_ArmorBonusSphereAbstract
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 22;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            useDust = ModContent.DustType<Dusts.RadiumDust>();

        }
        public override void InflictBuffs(NPC target)
        {
            target.AddBuff(ModContent.BuffType<RadiumInferno_Buff>(), 600);
        }
    }
}
