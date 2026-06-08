using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class StarHelmetRangedPlayer_DarkmatterSphere : StarHelmetRangedPlayer_ArmorBonusSphereAbstract
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
            useDust = ModContent.DustType<Dusts.DarkmatterDust>();

        }
        public override void InflictBuffs(NPC target)
        {
            target.AddBuff(ModContent.BuffType<DarkCurse_Buff>(), 600);
            if(!target.boss)
            {
                target.AddBuff(ModContent.BuffType<DarkLock_Buff>(), 120);
            }
        }
    }
}
