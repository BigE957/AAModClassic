using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace AAModClassic.Projectiles.Akuma
{
    public class MorningGlory : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Morning Glory");
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 180;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;
        }
		
        private const int alphaReduction = 25;
		
        public override void AI()
        {	
			if (Projectile.ai[1] != -1f) Projectile.rotation =
				Projectile.velocity.ToRotation() + (float)Math.PI / 2 + (float)Math.PI / 4;
			if (Projectile.ai[1] == -1f) Projectile.rotation =
				Projectile.velocity.ToRotation() + (float)Math.PI / 2;
			
			if (Projectile.ai[1] != -1f) Projectile.ai[0]++;
			
			if (Projectile.ai[0] == 1f || Projectile.ai[0] == 3f)
			{
				int numberProjectiles = 2;
				float rotation = MathHelper.ToRadians(1);
				if (Projectile.ai[0] == 3f) rotation = MathHelper.ToRadians(2);
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
					int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<MorningGlory>(),  Projectile.damage, Projectile.knockBack, Projectile.owner, 0, -1f);
					Main.projectile[proj].usesLocalNPCImmunity = true;
					Main.projectile[proj].localNPCHitCooldown = 10;
					Main.projectile[proj].penetrate = -1;
					Main.projectile[proj].rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2 + (float)Math.PI / 4;
				}
			}

			if (Projectile.ai[1] != -1f)
			{
				if (Projectile.alpha > 0)
				{
					Projectile.alpha -= alphaReduction;
				}
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
			}
			if (Projectile.ai[1] == -1f)
			{
				Projectile.alpha += 2;
				if (Projectile.alpha >= 255)
				{
					Projectile.Kill();
				}
			}
			
			for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0);
                
                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }
		
        public override bool PreDraw(ref Color lightColor)
        {
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			if (Projectile.ai[1] == -1f) texture = Mod.GetTexture("Projectiles/Akuma/MorningGloryPhantom");
            Main.spriteBatch.Draw(texture, new Vector2(Projectile.Center.X - Main.screenPosition.X, Projectile.Center.Y - Main.screenPosition.Y + 2),
                        new Rectangle(0, 0, texture.Width, texture.Height), Color.White, Projectile.rotation,
                        new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f), 1f, SpriteEffects.None, 0f);
            return false;
        }

        public override void OnKill(int i)
        {
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<AkumaExp>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
        }
	}
}
 