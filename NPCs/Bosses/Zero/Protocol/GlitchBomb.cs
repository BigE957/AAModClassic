using AAModClassic.Dusts;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Zero.Protocol
{
    public class GlitchBomb : ModProjectile
    {
        public float maxDistToAttack = 350f;
        public int target = -1;
        public float maxSpeed = 12f;
        public int targetTick = 8;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
            Projectile.scale *= 1.5f;
            Projectile.damage = 100;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override void AI()
        {
            int num103 = Player.FindClosest(Projectile.Center, 1, 1);
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] < 110f && Projectile.ai[1] > 30f)
            {
                float scaleFactor2 = Projectile.velocity.Length();
                Vector2 vector11 = Main.player[num103].Center - Projectile.Center;
                vector11.Normalize();
                vector11 *= scaleFactor2;
                Projectile.velocity = ((Projectile.velocity * 24f) + vector11) / 25f;
                Projectile.velocity.Normalize();
                Projectile.velocity *= scaleFactor2;
            }
            if (Projectile.ai[0] < 0f)
            {
                if (Projectile.velocity.Length() < 18f)
                {
                    Projectile.velocity *= 1.02f;
                }
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.5f / 255f, (255 - Projectile.alpha) * 0f / 255f, (255 - Projectile.alpha) * 0.15f / 255f);

            int dustId = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height + 10, ModContent.DustType<VoidDust>(), Projectile.velocity.X * 0.2f,
					Projectile.velocity.Y * 0.2f, 100);
				Main.dust[dustId].noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                for (int m = 0; m < 6; m++)
                {
                    int dustID = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<VoidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100);
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                }
                SoundEngine.PlaySound(SoundID.NPCDeath3, Projectile.Center);
            }

            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Sounds/Glitch"), (int)Projectile.Center.X, (int)Projectile.Center.Y);
            Projectile.NewProjectile(Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GlitchBoom>(), Projectile.damage, 1, Projectile.owner);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2) 
                    Projectile.frame = 0; 
            }
            return true;
        }
    }
}