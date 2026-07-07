using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class SunPartisan_Holdout : ModProjectile
    {
        public override string GlowTexture => "AAModClassic/Glowmasks/SunSpear_Glow";
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sun Partisan");
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.scale = 1.1f;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 90;
            Projectile.hide = true;
        }


        public float MovementFactor // Change this value to alter how fast the spear moves
        {
            get { return Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 600);
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }

        public override void AI()
        {
            //dust!
            int dustId = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width / 2, Projectile.height + 5, ModContent.DustType<Dusts.AkumaADust>(), Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width / 2, Projectile.height + 5, ModContent.DustType<Dusts.AkumaADust>(), Projectile.velocity.X * 0.2f,
                Projectile.velocity.Y * 0.2f, 100, default, 2f);
            Main.dust[dustId3].noGravity = true;

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
                    //Projectile.NewProjectile(projectile.Center.X + (projectile.velocity.X * projectile.ai[0]), projectile.Center.Y + (projectile.velocity.Y * projectile.ai[0]), projectile.velocity.X * 1.4f, projectile.velocity.Y * 1.4f, mod.ProjectileType("SunSpearShot"), (int)((double)projectile.damage * 0.85f), projectile.knockBack * 0.85f, projectile.owner, 0f, 0f);
                }
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position = Projectile.position;
                dust1 = Main.dust[Dust.NewDust(position, 0, 0, ModContent.DustType<Dusts.AkumaDust>(), 4.736842f, 0f, 46, default, 1f)];
                dust2 = Main.dust[Dust.NewDust(position, 0, 0, ModContent.DustType<Dusts.AkumaADust>(), 4.736842f, 0f, 46, default, 1f)];
                dust1.noGravity = true;
                dust2.noGravity = true;
            }
			if (Projectile.timeLeft == 80)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X*0.75f, Projectile.velocity.Y*0.75f, ModContent.ProjectileType<SunPartisan_Proj>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
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
