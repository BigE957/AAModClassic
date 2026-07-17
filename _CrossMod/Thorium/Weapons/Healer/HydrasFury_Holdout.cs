using System;
using AAModClassic.Assets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.Thorium.Weapons.Healer
{
    public class HydrasFury_Holdout : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 130;
			Projectile.height = 130;
            Projectile.aiStyle = 0;
            Projectile.light = 0.2f;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 26;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.DamageType = ThoriumMod.HealerClass;
        }

        public static Vector2 DustOffset => new Vector2(-12, 48);
        public static float SpinSpeed => 0.45f;
        public static int DustCount => 3;
        public static float DustScale => 0.8f;
        public static int DustAlpha => 180;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead)
            {
                Projectile.Kill();
                return;
            }
            Projectile projectile = Projectile;
            projectile.rotation += player.direction * player.gravDir * SpinSpeed;
            Projectile.spriteDirection = player.direction;
            player.heldProj = Projectile.whoAmI;
            Projectile.Center = player.Center;
            Projectile.gfxOffY = player.gfxOffY;
            SpawnDust();

            if (projectile.timeLeft < 10)
            {
                projectile.alpha += 30;
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                }
            }
        }

        private void SpawnDust()
        {
            int scythes = 2;
            int type = ModContent.DustType<Dusts.AcidDust>();
            Vector2 dustCenter = new Vector2(Projectile.width, -Projectile.height) / 2f + DustOffset;
            if (scythes <= 0 || DustCount <= 0 || type <= -1)
                return;

            for (int scytheIndex = 0; scytheIndex < scythes; scytheIndex++)
            {
                float offset = scytheIndex * MathHelper.TwoPi / scythes;
                float rot = Projectile.rotation;
                Vector2 rotationOffset = dustCenter;
                if (Projectile.spriteDirection < 0)
                    rotationOffset.X = 0f - rotationOffset.X;

                float myRot = rot + offset;
                rotationOffset = rotationOffset.RotatedBy(myRot);
                Vector2 rotationCenter = Projectile.Center + new Vector2(0f, Projectile.gfxOffY) + rotationOffset;
                for (int j = 0; j < DustCount; j++)
                {
                    Vector2 velocity = (myRot + MathHelper.PiOver2 + Main.rand.NextFloat(-MathHelper.Pi / 16f, MathHelper.Pi / 16f)).ToRotationVector2() * 10 * SpinSpeed;
                    Dust dust = Dust.NewDustPerfect(rotationCenter, type, velocity, DustAlpha, Scale: DustScale);
                    dust.noGravity = true;
                    dust.noLight = true;
                }
            }
        }

        /// <summary>
        /// Flag checked when the projectile has scythe charges and a suitable NPC is hit, then set to false
        /// </summary>
        public bool CanGiveScytheCharge
        {
            get { return (Projectile.localAI[0] == 0f); }
            set { Projectile.localAI[0] = (value ? 0f : 1f); }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            CanGiveScytheCharge = ThoriumMod.TryGainSoulEssence(Main.player[Projectile.owner], target, 1, CanGiveScytheCharge);
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.Poisoned, 200, false);
            }
        }
    }
}