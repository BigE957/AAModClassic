using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CursedSickleEX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tartarus Reaper");
        }

        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 114;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 26;
            AIType = ProjectileID.Bullet;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            projHitbox.Width += 16;
            projHitbox.Height += 16;

            return projHitbox.Intersects(targetHitbox);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            npc.immune[Projectile.owner] = 8;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];
            if (target.Center.X < player.Center.X)
            {
                hitDirection = -1;
            }
            else
            {
                hitDirection = 1;
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player.dead)
            {
                Projectile.Kill();
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

            player.heldProj = Projectile.whoAmI;
            Projectile.position.X = player.Center.X - (Projectile.width / 2f);
            Projectile.position.Y = player.Center.Y - (Projectile.height / 2f);

            Projectile.NewProjectile(Projectile.Center.X + 20, Projectile.Center.Y, -15f, 0f, Mod.Find<ModProjectile>("CursedSickleEXDamage").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            Projectile.NewProjectile(Projectile.Center.X - 20, Projectile.Center.Y, 15f, 0f, Mod.Find<ModProjectile>("CursedSickleEXDamage").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);

            if (Projectile.timeLeft == 13)
            {
                Projectile.NewProjectile(Projectile.Center.X + 20, Projectile.Center.Y, -15f, 0f, Mod.Find<ModProjectile>("CursedSickleEXDamage2").Type, (int)(Projectile.damage * .35), Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.NewProjectile(Projectile.Center.X - 20, Projectile.Center.Y, 15f, 0f, Mod.Find<ModProjectile>("CursedSickleEXDamage2").Type, (int)(Projectile.damage * .35), Projectile.knockBack, Projectile.owner, 0f, 0f);
            }

            if (Projectile.timeLeft < 8)
            {
                Projectile.alpha -= 28;
            }
        }
    }
    public class CursedSickleEXEffect : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
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

            Projectile.Center = player.Center + new Vector2(-8f, -8f) + RotateVector(default, rotVec, rot + (Projectile.ai[0] * (6.28f / 2)));

            for (int m = 0; m < 5; m++)
            {
                float velX = Projectile.velocity.X / 3f * m;
                float velY = Projectile.velocity.Y / 3f * m;
                int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 75, 0, 0, 0);
                Main.dust[dustID].position.X = Projectile.Center.X - velX;
                Main.dust[dustID].position.Y = Projectile.Center.Y - velY;
                Main.dust[dustID].velocity *= 0f;
                Main.dust[dustID].alpha = 180;
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].scale = 0.8f;
            }
        }
    }

    public class CursedSickleEXDamage : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 96;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 8;
            AIType = ProjectileID.Bullet;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.AddBuff(ModContent.BuffType<Buffs.CursedHellfire>(), 210);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.position.X = player.Center.X - (Projectile.width / 2f);
            Projectile.position.Y = player.Center.Y - (Projectile.height / 2f);
        }
    }

    public class CursedSickleEXDamage2 : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 96;
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
            target.AddBuff(ModContent.BuffType<Buffs.CursedHellfire>(), 210);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.position.X = player.Center.X - (Projectile.width / 2f);
            Projectile.position.Y = player.Center.Y - (Projectile.height / 2f);
        }
    }
}