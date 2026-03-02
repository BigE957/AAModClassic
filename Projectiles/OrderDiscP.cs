
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class OrderDiscP : ModProjectile
    {
		public static int defense = 0;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(106);
			Projectile.melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;  
            Projectile.width = 22;
            Projectile.height = 32;
			Projectile.aiStyle = 3;
			AIType = 106;
        }

		public override void SetStaticDefaults()
		{
		  // DisplayName.SetDefault("Order Disc");
		}
		
		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 211,
				Projectile.velocity.X * .5f, Projectile.velocity.Y * .5f, 200, Scale: 1.1f);
				dust.velocity += Projectile.velocity * 0.4f;
				dust.velocity *= 0.3f;
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref NPC.HitModifiers modifiers)
		{
			defense = target.defense;
			target.defense = 0;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 5;
			target.defense = defense;
		}

        public override bool OnTileCollide(Vector2 velocityChange)
        {
            if (Main.netMode != 2)
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            BaseAI.TileCollideBoomerang(Projectile, ref velocityChange, true);
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, Color.White, true);
            return false;
        }
    }
}
