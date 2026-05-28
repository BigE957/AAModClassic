using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic._Content.Snow.Projectiles;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class Axis_Holdout : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            //Projectile.scale = 1.1f;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 90;
            Projectile.hide = true;
            //Projectile.aiStyle = ProjAIStyleID.Spear;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Axis");
        }

        public override void AI()
        {
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
                Projectile.ai[0] -= 2.4f;
            else // Move forward
                Projectile.ai[0] += 2.1f;

            // If at the end of the animation, kill the projectile.
            //Checking if == 0 is too late, lets the projectile linger into chained item uses.
            if (syncedItemAnimation <= 1f)
                Projectile.Kill();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
            if (Projectile.spriteDirection == -1)
                Projectile.rotation -= MathHelper.PiOver2;

            if (Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
            {
                Projectile.ai[0] -= 2.4f;
                if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner)
                {
                    Projectile.localAI[0] = 1f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + Projectile.velocity, Projectile.velocity * 2.6f, ModContent.ProjectileType<Axis_Proj>(), (int)(Projectile.damage * 0.8), Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
                }
            }
        }

        public bool stop = false;
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (!stop)
			{
				Vector2 vel1 = new Vector2(-1, -1);
				vel1 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X+130, target.position.Y+130, vel1.X, vel1.Y, ModContent.ProjectileType<Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel2 = new Vector2(1, 1);
				vel2 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X-130, target.position.Y-130, vel2.X, vel2.Y, ModContent.ProjectileType<Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel3 = new Vector2(1, -1);
				vel3 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X-130, target.position.Y+130, vel3.X, vel3.Y, ModContent.ProjectileType<Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel4 = new Vector2(-1, 1);
				vel4 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X+130, target.position.Y-130, vel4.X, vel4.Y, ModContent.ProjectileType<Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel5 = new Vector2(0, -1);
				vel5 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X, target.position.Y+130, vel5.X, vel5.Y, ModContent.ProjectileType<Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel6 = new Vector2(0, 1);
				vel6 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X, target.position.Y-130, vel6.X, vel6.Y, ModContent.ProjectileType<Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel7 = new Vector2(1, 0);
				vel7 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X-130, target.position.Y, vel7.X, vel7.Y, ModContent.ProjectileType<Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel8 = new Vector2(-1, 0);
				vel8 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X+130, target.position.Y, vel8.X, vel8.Y, ModContent.ProjectileType<Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				stop = true;
			}
		}

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            Vector2 origin = Vector2.Zero;
            Main.EntitySpriteDraw(texture, drawPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, 0, 0);
            //Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.GlowMask[Projectile.glowMask].Value, drawPosition, null, Color.White, Projectile.rotation, origin, Projectile.scale, 0, 0);
            return false;
        }
    }
}
