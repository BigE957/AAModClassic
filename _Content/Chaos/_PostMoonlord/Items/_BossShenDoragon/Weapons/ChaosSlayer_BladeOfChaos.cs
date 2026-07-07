using AAModClassic._Content.Chaos.Buffs;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class ChaosSlayer_BladeOfChaos : ModProjectile
    {
		public int swordType = 0;
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blade of Chaos");
		}

        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 0;
            Projectile.tileCollide = false;
			Projectile.extraUpdates = 1;
        }

		public float vectorOffset = 0f;
		public bool offsetLeft = false;
		public Vector2 originalVelocity = Vector2.Zero;

        public override void AI()
        {
			int dustType = swordType == 0 ? ModContent.DustType<Dusts.DiscordLight>() : swordType == 1 ? ModContent.DustType<Dusts.AkumaDustLight>() : ModContent.DustType<Dusts.YamataDustLight>();

			int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
			Main.dust[dustID].velocity *= 0f;
			Main.dust[dustID].noLight = false;
			Main.dust[dustID].noGravity = true;
			if(swordType != 0)
			{
				dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1) - Projectile.velocity, 2, 2, dustType, 0f, 0f, 100, Color.White, 1.2f);
				Main.dust[dustID].velocity *= 0f;
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}

			if(originalVelocity == Vector2.Zero)
			{
				originalVelocity = Projectile.velocity;
			}
			if(swordType != 0)
			{
				if(offsetLeft)
				{
					vectorOffset -= 0.04f;
					if(vectorOffset <= -1f)
					{
						vectorOffset = -1f;
						offsetLeft = false;
					}
				}else
				{
					vectorOffset += 0.04f;
					if(vectorOffset >= 1f)
					{
						vectorOffset = 1f;
						offsetLeft = true;
					}
				}
				float velRot = BaseUtility.RotationTo(Projectile.Center, Projectile.Center + originalVelocity);
				Projectile.velocity = BaseUtility.RotateVector(default, new Vector2(Projectile.velocity.Length(), 0f), velRot + vectorOffset * 0.5f);
			}
			Projectile.rotation = BaseUtility.RotationTo(Projectile.Center, Projectile.Center + Projectile.velocity) + 1.57f - MathHelper.PiOver4;
			Projectile.spriteDirection = 1;
        }

        public override void OnKill(int timeLeft)
        {
			int dustType = swordType == 0 ? ModContent.DustType<Dusts.Discord_Dust>() : swordType == 1 ? ModContent.DustType<Dusts.AkumaDustLight>() : ModContent.DustType<Dusts.YamataDustLight>();
			int pieCut = 20;
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<DiscordianInferno_Buff>(), 600);
        }

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 150);
		}		
    }
}