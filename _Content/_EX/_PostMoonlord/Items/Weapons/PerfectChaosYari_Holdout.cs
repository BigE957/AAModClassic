using AAModClassic._Content.Chaos.__Hardmode.Items.Weapons;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PerfectChaosYari_Holdout : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Yari");
		}
    	
        public override void SetDefaults()
        {
			Projectile.width = 40;  //The width of the .png file in pixels divided by 2.
			//Projectile.aiStyle = ProjAIStyleID.Spear;
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

        bool shot = false;

        public override void AI()
        {
        	if (Main.rand.NextBool(5))
            {
                int DustType = ModContent.DustType<Dusts.AkumaADust>();
                if (Main.rand.NextBool(3))
                {
                    DustType = ModContent.DustType<Dusts.YamataADust>();
                }
                if (Main.rand.NextBool(3))
                {
                    DustType = ModContent.DustType<Dusts.Discord_Dust>();
                }
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustType, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
                    
            // ai[0] = Speed value of the spear. Changes as time goes by.
            // localAI[0] = Special effect 0-1 flag value. Actived right before the spear goes backward.

            Player player = Main.player[Projectile.owner];
            float itemAnimationMax = Math.Max(1f, player.itemAnimationMax);
            float syncedItemAnimation = AAGlobalProjectile.GetSyncedItemAnimation(Projectile, player);

            // Adjust owner stats based on this projectile
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;

            // Stick to the player
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter);

            // And move outward/inward based on the speed variable.
            Projectile.position += Projectile.velocity * Projectile.ai[0];

            // If we're not movement, start.
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 3f;
                Projectile.netUpdate = true;
            }

            if (syncedItemAnimation < itemAnimationMax / 3f) // Reel back
            {
                if (!shot)
                {
                    if (Main.myPlayer == Projectile.owner && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<PerfectChaosYari_Proj>()))
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X * 1.4f, Projectile.velocity.Y * 1.4f, ModContent.ProjectileType<PerfectChaosYari_Proj>(), (int)((double)Projectile.damage * 0.85f), Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].position.X, Main.player[Projectile.owner].position.Y, Projectile.velocity.X * 1.3f, Projectile.velocity.Y * 1.3f, ModContent.ProjectileType<PerfectChaosYari_Proj>(), (int)((double)Projectile.damage * 0.85f), Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
                    }
                    shot = true;
                }
                Projectile.ai[0] -= 2.4f;
            }
            else // Move forward
                Projectile.ai[0] += 2.1f;

            // If at the end of the animation, kill the projectile.
            //Checking if == 0 is too late, lets the projectile linger into chained item uses.
            if (syncedItemAnimation <= 1f)
                Projectile.Kill();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
            if (Projectile.spriteDirection == -1)
                Projectile.rotation -= MathHelper.PiOver2;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.immune[Projectile.owner] = 5;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            Vector2 origin = Vector2.Zero;
            Main.EntitySpriteDraw(texture, drawPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, 0, 0);
            return false;
        }
    }
}