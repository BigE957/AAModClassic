using AAModClassic.Dusts;
using AAModClassic.Globals;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ChaosYariEX : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Yari");
		}
    	
        public override void SetDefaults()
        {
			Projectile.width = 40;  //The width of the .png file in pixels divided by 2.
			Projectile.aiStyle = ProjAIStyleID.Spear;
			Projectile.DamageType = DamageClass.Melee;  //Dictates whether this is a melee-class weapon.
			Projectile.timeLeft = 90;
			Projectile.height = 40;  //The height of the .png file in pixels divided by 2.
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.ownerHitCheck = true;
			Projectile.hide = true;
        }

        public override void AI()
        {
        	Main.player[Projectile.owner].direction = Projectile.direction;
        	Main.player[Projectile.owner].heldProj = Projectile.whoAmI;
        	Main.player[Projectile.owner].itemTime = Main.player[Projectile.owner].itemAnimation;
        	Projectile.position.X = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - Projectile.width / 2;
        	Projectile.position.Y = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - Projectile.height / 2;
        	Projectile.position += Projectile.velocity * Projectile.ai[0];
        	if (Main.rand.Next(5) == 0)
            {
                int DustType = ModContent.DustType<AkumaADust>();
                if (Main.rand.Next(3) == 0)
                {
                    DustType = ModContent.DustType<YamataADust>();
                }
                if (Main.rand.Next(3) == 1)
                {
                    DustType = ModContent.DustType<Discord>();
                }
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustType, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        	if(Projectile.ai[0] == 0f)
        	{
        		Projectile.ai[0] = 3f;
        		Projectile.netUpdate = true;
        	}
        	if(Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
        	{
        		Projectile.ai[0] -= 2.4f;
                if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<ChaosYariEXShot>()))
                {
					Projectile.localAI[0] = 1f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X * 1.4f, Projectile.velocity.Y * 1.4f, ModContent.ProjectileType<ChaosYariEXShot>(), (int)((double)Projectile.damage * 0.85f), Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].position.X, Main.player[Projectile.owner].position.Y, Projectile.velocity.X * 1.3f, Projectile.velocity.Y * 1.3f, ModContent.ProjectileType<ChaosYariEXShot>(), (int)((double)Projectile.damage * 0.85f), Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
				}
        	}
        	else
        	{
        		Projectile.ai[0] += 0.95f;
        	}
        	
        	if(Main.player[Projectile.owner].itemAnimation == 0)
        	{
        		Projectile.Kill();
        	}
        	
        	Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
        	if(Projectile.spriteDirection == -1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.immune[Projectile.owner] = 5;
        }
    }
}