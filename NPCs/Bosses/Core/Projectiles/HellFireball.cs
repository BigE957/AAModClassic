using System;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Core.Projectiles;

public class HellFireball : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = 32;
		Projectile.height = 32;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		Projectile.velocity.X = Projectile.velocity.X * 0.98f;
		Projectile.velocity.Y = Projectile.velocity.Y + 0.35f;
		if (Main.rand.NextBool(2))
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 200, default(Color), 0.5f);
			Dust obj = Main.dust[num];
			obj.velocity *= 0.3f;
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 5; i++)
		{
			int num = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, DustID.Torch, (0f - Projectile.velocity.X) * 0.2f, (0f - Projectile.velocity.Y) * 0.2f, 100, default(Color), 2f);
			Main.dust[num].noGravity = true;
		}
		SoundEngine.PlaySound(SoundID.Item124);
		Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, new Vector2(0f, 0f), ModContent.ProjectileType<HellBoom>(), Projectile.damage, 2f, 255, 0f, 0f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Type].Value, 0, Projectile, Color.White);
		return false;
	}
}
