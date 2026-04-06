using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons
{
    public class Annihilator_Annihilation : ModProjectile
	{
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Anhialation");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.friendly = true;  
			Projectile.hostile = false;       
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
			Projectile.alpha = 100;           
			Projectile.light = 0.5f;         
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;        
			Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
            Projectile.alpha = 30;           
		}


        public override void OnKill(int timeleft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<Annihilator_AnnihilationBurst>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }



        public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(Color.White) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
