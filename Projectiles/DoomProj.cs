using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DoomProj : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doom"); 
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			AIType = -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D t = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D Glow = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");

            BaseDrawing.DrawTexture(sb, t, 0, Projectile, lightColor, true);
            BaseDrawing.DrawTexture(sb, Glow, 0, Projectile, Color.White, true);
            return false;
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188));
                Main.dust[num469].velocity *= 2f;
            }
            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Glitch"), (int)Projectile.Center.X, (int)Projectile.Center.Y);
            Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("DoomBoom").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}
