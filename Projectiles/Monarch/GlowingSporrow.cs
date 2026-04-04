using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Monarch
{
    public class GlowingSporrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Sporrow");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.GetGlobalProjectile<ImplaingProjectile>().CanImpale = true;
            Projectile.GetGlobalProjectile<ImplaingProjectile>().damagePerImpaler = 0;
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 0);

            BaseDrawing.AddLight(Projectile.Center, new Color(9, 60, 128));

            BaseDrawing.DrawTexture(Main.spriteBatch, texture, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 1, frame, lightColor, true);
            BaseDrawing.DrawTexture(Main.spriteBatch, Mod.GetTexture("Glowmasks/GlowingSporrow_Glow"), 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 1, frame, Color.White, true);
        }
        protected float maxTicks = 45f;

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = height = 10;
            return true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public bool IsStickingToTarget
        {
            get { return Projectile.ai[0] == 1f; }
            set { Projectile.ai[0] = value ? 1f : 0f; }
        }

        public float TargetWhoAmI
        {
            get { return Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.timeLeft = 30 * 60;

            IsStickingToTarget = true;
            TargetWhoAmI = target.whoAmI;
            Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
            Projectile.netUpdate = true;
            target.AddBuff(ModContent.BuffType<Impaled>(), 900);
            Projectile.penetrate = -1;
            Projectile.damage = 0;


            Point[] stickingJavelins = new Point[1]; // The point array holding for sticking javelins
            int javelinIndex = 0; // The javelin index
            for (int i = 0; i < Main.maxProjectiles; i++) // Loop all projectiles
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != Projectile.whoAmI // Make sure the looped projectile is not the current javelin
                    && currentProjectile.active // Make sure the projectile is active
                    && currentProjectile.owner == Main.myPlayer // Make sure the projectile's owner is the client's player
                    && currentProjectile.type == Projectile.type // Make sure the projectile is of the same type as projectile javelin
                    && currentProjectile.ai[0] == 1f // Make sure ai0 state is set to 1f (set earlier in ModifyHitNPC)
                    && currentProjectile.ai[1] == target.whoAmI
                ) // Make sure ai1 is set to the target whoAmI (set earlier in ModifyHitNPC)
                {
                    stickingJavelins[javelinIndex++] =
                        new Point(i, currentProjectile.timeLeft); // Add the current projectile's index and timeleft to the point array
                    if (javelinIndex >= stickingJavelins.Length
                    ) // If the javelin's index is bigger than or equal to the point array's length, break
                    {
                        break;
                    }
                }
            }
            // Here we loop the other javelins if new javelin needs to take an older javelin's place.
            if (javelinIndex >= stickingJavelins.Length)
            {
                int oldJavelinIndex = 0;
                // Loop our point array
                for (int i = 1; i < stickingJavelins.Length; i++)
                {
                    // Remove the already existing javelin if it's timeLeft value (which is the Y value in our point array) is smaller than the new javelin's timeLeft
                    if (stickingJavelins[i].Y < stickingJavelins[oldJavelinIndex].Y)
                    {
                        oldJavelinIndex = i; // Remember the index of the removed javelin
                    }
                }
                // Remember that the X value in our point array was equal to the index of that javelin, so it's used here to kill it.
                Main.projectile[stickingJavelins[oldJavelinIndex].X].Kill();
            }
        }


        public virtual void NonStickingBehavior()
        {
            Projectile.ai[0] = 15f;
            Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;


            if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
		}

		// Change projectile number if you want to alter how the alpha changes
		private const int alphaReduction = 25;

        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= alphaReduction;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            // If ai0 is 0f, run projectile code. projectile is the 'movement' code for the javelin as long as it isn't sticking to a target
            if (!IsStickingToTarget)
            {
                NonStickingBehavior();

                // Make sure to set the rotation accordingly to the velocity, and add some to work around the sprite's rotation
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            }
            // projectile code is ran when the javelin is sticking to a target
            if (IsStickingToTarget)
            {
                // These 2 could probably be moved to the ModifyNPCHit hook, but in vanilla they are present in the AI
                Projectile.ignoreWater = true; // Make sure the projectile ignores water
                Projectile.tileCollide = false; // Make sure the projectile doesn't collide with tiles anymore
                int aiFactor = 15; // Change projectile factor to change the 'lifetime' of projectile sticking javelin
                bool killProj = false; // if true, kill projectile at the end
                Projectile.localAI[0] += 1f;
                int projTargetIndex = (int)TargetWhoAmI;
                if (Projectile.localAI[0] >= 60 * aiFactor || projTargetIndex < 0 || projTargetIndex >= 200) // If the index is past its limits, kill it
                {
                    killProj = true;
                }
                else if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage) // If the target is active and can take damage
                {
                    Projectile.Center = Main.npc[projTargetIndex].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
                }
                else
                {
                    killProj = true;
                }

                if (killProj)
                {
                    Projectile.Kill();
                }

            }
        }
    }
}
