using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class SunSpear : ModProjectile
    {

        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Texture2D[] glowMasks = new Texture2D[TextureAssets.GlowMask.Value.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Value.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i].Value;
                }
                glowMasks[glowMasks.Length - 1] = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask.Value = glowMasks;
            }
            Projectile.glowMask = customGlowMask;

            // DisplayName.SetDefault("Sun Partisan");
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.scale = 1.1f;
            Projectile.aiStyle = ProjAIStyleID.Spear;
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

            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile directio and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            // As long as the player isn't frozen, the spear can move
            if (!projOwner.frozen)
            {
                if (MovementFactor == 0f) // When intially thrown out, the ai0 will be 0f
                {
                    MovementFactor = 3f; // Make sure the spear moves forward when initially thrown out
                    Projectile.netUpdate = true; // Make sure to netUpdate this spear
                }

                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
                    MovementFactor -= 2.4f;
                else // Otherwise, increase the movement factor
                    MovementFactor += 2.1f;
            }

            // Change the spear position based off of the velocity and the movementFactor
            Projectile.position += Projectile.velocity * MovementFactor;
            // When we reach the end of the animation, we can kill the spear projectile
            if (projOwner.itemAnimation == 0) Projectile.Kill();
            // Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
            // MathHelper.ToRadians(xx degrees here)
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + MathHelper.ToRadians(135f);
            // Offset by 90 degrees here
            if (Projectile.spriteDirection == -1) Projectile.rotation -= MathHelper.ToRadians(90f);
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
				Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X*0.75f, Projectile.velocity.Y*0.75f, Mod.Find<ModProjectile>("SunSpearProj").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
			}
        }
    }
}
