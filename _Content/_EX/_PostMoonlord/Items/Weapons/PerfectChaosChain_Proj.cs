using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Projectiles;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PerfectChaosChain_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Saw");
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 5;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
        }

        private float RingRotation = 0f;

        public bool runOnce = true;
        float maxSpeed;
        public override void AI()
        {
            if (Projectile.velocity.X < 0)
            {
                Projectile.direction = -1;
            }
            RingRotation += 0.03f * Projectile.direction;
            if (runOnce)
            {
                maxSpeed = Projectile.velocity.Length();
                runOnce = false;
            }
        }
        public bool firstHit = true;

        NPC ConfirmedTarget;
        NPC possibleTarget;
        float distance;
        float maxDistance = 1200;
        bool foundTarget;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.localNPCImmunity[target.whoAmI] = -1;
            target.immune[Projectile.owner] = 0;

            for (int k = 0; k < 200; k++)
            {
                possibleTarget = Main.npc[k];
                distance = (possibleTarget.Center - Projectile.Center).Length();
                if (distance < maxDistance && possibleTarget.active && !possibleTarget.dontTakeDamage && Projectile.localNPCImmunity[k] >= 0 && !possibleTarget.friendly && possibleTarget.lifeMax > 5 && !possibleTarget.immortal && Collision.CanHit(Projectile.Center, 0, 0, possibleTarget.Center, 0, 0))
                {
                    ConfirmedTarget = Main.npc[k];
                    foundTarget = true;


                    maxDistance = (ConfirmedTarget.Center - Projectile.Center).Length();
                }

            }
            if (foundTarget)
            {
                Projectile.velocity = PolarVector(maxSpeed, (ConfirmedTarget.Center - Projectile.Center).ToRotation());

            }
            else
            {
                Projectile.Kill();
            }
            foundTarget = false;
        }

        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }

        public override void OnKill(int timeleft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ChaosBoomEX>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0);
            int pieCut = 20;
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.Discord_Dust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(Projectile.Center.X - 1, Projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.Discord_Dust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
            BaseDrawing.DrawTexture(Main.spriteBatch, Tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, RingRotation, Projectile.direction, 1, frame, lightColor, true);
            return false;
        }
    }
}
