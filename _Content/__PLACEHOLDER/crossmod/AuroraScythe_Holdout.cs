using AAModClassic.CrossMod;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.__PLACEHOLDER.crossmod
{
    public class AuroraScythe_Holdout : ModProjectile
    { 
		public override void SetDefaults()
		{
			Projectile.width = 130;
			Projectile.height = 128;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.light = 0.2f;
			Projectile.tileCollide = false;
			Projectile.ownerHitCheck = true;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 26;
			AIType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];	
			
			Projectile.ai[0]++;
			
			if (player.dead)
			{
				Projectile.Kill();
				return;
			}
			
			if (player.direction > 0)
			{
				Projectile.rotation += 0.35f;
				Projectile.spriteDirection = 1;
			}
			else
			{
				Projectile.rotation -= 0.35f;
				Projectile.spriteDirection = -1;
			}
			
			Projectile.position.X = player.Center.X - Projectile.width / 2f;
			Projectile.position.Y = player.Center.Y - Projectile.height / 2f;
			
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + 20, Projectile.Center.Y, -15f, 0f, ModContent.ProjectileType<AuroraScytheDamage>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X - 20, Projectile.Center.Y, 15f, 0f, ModContent.ProjectileType<AuroraScytheDamage>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			
			if (Projectile.timeLeft == 13)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + 20, Projectile.Center.Y, -15f, 0f, ModContent.ProjectileType<AuroraScytheDamage2>(), (int)(Projectile.damage * .35), Projectile.knockBack, Projectile.owner, 0f, 0f);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X - 20, Projectile.Center.Y, 15f, 0f, ModContent.ProjectileType<AuroraScytheDamage2>(), (int)(Projectile.damage * .35), Projectile.knockBack, Projectile.owner, 0f, 0f);
			}
			
			if (Projectile.timeLeft < 8)
			{
				Projectile.alpha = 100;
			}
			if (Projectile.timeLeft < 6)
			{
				Projectile.alpha = 140;
			}
			if (Projectile.timeLeft < 4)
			{
				Projectile.alpha = 180;
			}
			if (Projectile.timeLeft < 2)
			{
				Projectile.alpha = 220;
			}
		}
	}
    public class AuroraScytheDamage : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 130;
            Projectile.height = 128;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 8;
            AIType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.Frostburn, 200, false);
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];
            if (Main.rand.Next(100) <= player.GetModPlayer<ModSupportPlayer>().Thorium_radiantCrit)
            {
                modifiers.SetCrit();
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.position.X = player.Center.X - Projectile.width / 2f;
            Projectile.position.Y = player.Center.Y - Projectile.height / 2f;
        }
    }
    public class AuroraScytheDamage2 : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 130;
            Projectile.height = 128;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 4;
            AIType = ProjectileID.Bullet;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];
            if (Main.rand.Next(100) <= player.GetModPlayer<ModSupportPlayer>().Thorium_radiantCrit)
            {
                modifiers.SetCrit();
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.position.X = player.Center.X - Projectile.width / 2f;
            Projectile.position.Y = player.Center.Y - Projectile.height / 2f;
        }
    }
    public class AuroraScytheEffect : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public static Color lightColor = new Color(41, 60, 103);


        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false; Projectile.ownerHitCheck = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 24;
        }

        public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
        {
            float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
            float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
            return new Vector2(newPosX, newPosY);
        }

        public Vector2 rotVec = new Vector2(0, 65);
        public float rot = 0f;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.direction > 0)
            {
                rot += 0.20f;
            }
            else
            {
                rot -= 0.20f;
            }

            Projectile.Center = player.Center + new Vector2(-8f, -8f) + RotateVector(default, rotVec, rot + Projectile.ai[0] * (6.28f / 2));

            for (int m = 0; m < 5; m++)
            {
                float velX = Projectile.velocity.X / 3f * m;
                float velY = Projectile.velocity.Y / 3f * m;
                int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.IceDust>(), 0, 0, 0);
                //int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 0, default, 1.2f);
                Main.dust[dustID].position.X = Projectile.Center.X - velX;
                Main.dust[dustID].position.Y = Projectile.Center.Y - velY;
                Main.dust[dustID].velocity *= 0f;
                Main.dust[dustID].alpha = 180;
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].scale = 0.8f;
            }
        }
    }
}