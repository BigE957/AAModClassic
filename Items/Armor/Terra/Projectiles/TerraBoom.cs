using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Terra.Projectiles
{
    public class TerraBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Apocalypse");
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.Kill();
                }
            }
            Projectile.velocity *= 0;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Terrablaze_Buff>(), 120);
        }

    }
}
