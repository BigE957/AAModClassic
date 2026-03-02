using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    public class YamataShot : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Blast");
            Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 20;   
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.HydraToxin>(), 600);
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 89, Terraria.Audio.SoundType.Sound));
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y - 2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y - 4f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y - 51 + 8, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("Shockwave").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}
