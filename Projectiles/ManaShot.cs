using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ManaShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LightBeam);
            Projectile.penetrate = 1;  
            Projectile.width = 18;
            Projectile.height = 18;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 900;
        }
		
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.9210526f)
			{
				Dust dust;
				Vector2 position = Projectile.position;
                dust = Main.dust[Dust.NewDust(position, 0, 0, DustID.Shadowflame, 4.736842f, 0f, 46, new Color(0, 255, 217), 1.184211f)];
                dust.fadeIn = 0.9868421f;
                dust.noGravity = true;
			}
		}

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Shadowflame, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 46, new Color(0, 255, 217), 1.184211f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Shadowflame, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 46, new Color(0, 255, 217), 1.184211f);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Texture2D[] glowMasks = new Texture2D[TextureAssets.GlowMask.Value.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Value.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i].Value;
                }
                glowMasks[glowMasks.Length - 1] = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask.Value = glowMasks;
            }
            Projectile.glowMask = customGlowMask;

            // DisplayName.SetDefault("Mana Petal");
		}


    }
}
