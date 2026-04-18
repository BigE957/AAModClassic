using AAModClassic.___Content.Inferno.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Akuma
{
    public class Flare : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dayfire");     
            Main.projFrames[Projectile.type] = 4;     
        }

        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame > 3)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;

        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 200);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            Projectile.timeLeft = 0;
        }

    }
}
