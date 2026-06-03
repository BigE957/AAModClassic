using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Dusts;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class AbyssalBomb_SoulBomb : ModProjectile
	{
        public bool isSmall = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Bomb");     
            Main.projFrames[Projectile.type] = 4;     
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
		{
			Projectile.width = 14;               
			Projectile.height = 14;              
			Projectile.aiStyle = ProjAIStyleID.Arrow;             
			Projectile.friendly = true;         
			Projectile.hostile = false;         
			Projectile.DamageType = DamageClass.Magic;           
			Projectile.penetrate = 1;           
			Projectile.timeLeft = 600;          
			Projectile.alpha = 20;              
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
            Projectile.aiStyle = 0;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                Projectile.width = 40;
                Projectile.height = 42;
            }
            else if (!isSmall) // removed this bcuz it was causing weird issues with no benefit since scale is replaced elsewhere. fixed (or, properly added) elsewhere
                Projectile.scale *= 1.2f;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;

                }
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.rotation += Projectile.velocity.X * 0.1f;
                float num689 = 500f;
                int num690 = -1;
                for (int num691 = 0; num691 < 200; num691++)
                {
                    NPC nPC5 = Main.npc[num691];
                    if (nPC5.CanBeChasedBy(this, false) && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC5.position, nPC5.width, nPC5.height))
                    {
                        float num692 = (nPC5.Center - Projectile.Center).Length();
                        if (num692 < num689)
                        {
                            num690 = num691;
                            num689 = num692;
                        }
                    }
                }
                Projectile.ai[0] = num690 + 1;
                if (Projectile.ai[0] == 0f)
                {
                    Projectile.ai[0] = -15f;
                }
                if (Projectile.ai[0] > 0f)
                {
                    float scaleFactor5 = Main.rand.Next(35, 75) / 30f;
                    Projectile.velocity = (Projectile.velocity * 20f + Vector2.Normalize(Main.npc[(int)Projectile.ai[0] - 1].Center - Projectile.Center + new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101))) * scaleFactor5) / 21f;
                    Projectile.netUpdate = true;
                }
            }
            else if (Projectile.ai[0] > 0f)
            {
                Vector2 value23 = Vector2.Normalize(Main.npc[(int)Projectile.ai[0] - 1].Center - Projectile.Center);
                Projectile.velocity = (Projectile.velocity * 40f + value23 * 12f) / 41f;
            }
            else
            {
                Projectile.ai[0] += 1f;
                Projectile.alpha -= 25;
                if (Projectile.alpha < 50)
                {
                    Projectile.alpha = 50;
                }
                Projectile.velocity *= 0.95f;
            }
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = Main.rand.Next(80, 121) / 100f;
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && !isSmall)
                    Projectile.ai[1] = Projectile.ai[1] * 1.2f;
                Projectile.netUpdate = true;
            }
            Projectile.scale = Projectile.ai[1];
            return;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 600);
        }
        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int explosionType = isSmall == true ? ModContent.ProjectileType<AbyssalBomb_SoulsplosionSmall>() : ModContent.ProjectileType<AbyssalBomb_Soulsplosion>();
            int dustType = isSmall == true ? ModContent.DustType<YamataDust>() : ModContent.DustType<YamataADust>();
            if (!isSmall)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
                double deltaAngle = spread / 4;
                for (int i = 0; i < 2; i++)
                {
                    double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 3f) * 5, (float)(Math.Cos(offsetAngle) * 3f) * 5, ModContent.ProjectileType<Projectiles.HydraSoulProj>(), Projectile.damage / 6, Projectile.knockBack, Projectile.owner, 0f, 0f);
                    Main.projectile[proj].DamageType = DamageClass.Magic;
                    proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 3f) * 5, (float)(-Math.Cos(offsetAngle) * 3f) * 5, ModContent.ProjectileType<Projectiles.HydraSoulProj>(), Projectile.damage / 6, Projectile.knockBack, Projectile.owner, 0f, 0f);
                    Main.projectile[proj].DamageType = DamageClass.Magic;
                }
            }
            for (int num468 = 0; num468 < 20; num468++)
            {
                Vector2 dustPos = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) == true ? Projectile.position : new Vector2(Projectile.width, Projectile.height);
                int num469 = Dust.NewDust(dustPos, Projectile.width, Projectile.height, dustType, -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                dustPos = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) == true ? Projectile.position : Projectile.Center;
                num469 = Dust.NewDust(dustPos, Projectile.width, Projectile.height, dustType, -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            Vector2 explosionPos = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) == true ? Projectile.Center : Projectile.position;
            Projectile.NewProjectile(Projectile.GetSource_Death(), explosionPos.X, explosionPos.Y, Projectile.velocity.X, Projectile.velocity.Y, explosionType, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}
