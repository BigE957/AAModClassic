using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.AH.Haruka
{
    public class HarukaArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 30;
			Projectile.friendly = false;
            Projectile.hostile = true;
			Projectile.timeLeft = 1200;
			Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
		}

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = height = 10;
			return true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Haruka Arrow");
		}

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Buffs.HydraToxin>(), 180);
            Projectile.netUpdate = true;
        }

		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

        public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
			     Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
			}
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			
		}
	}
}