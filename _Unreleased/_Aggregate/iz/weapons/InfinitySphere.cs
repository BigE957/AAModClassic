using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased._Aggregate.iz.weapons
{
    public class InfinitySphere : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Bomb");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 5;     //The recording mode
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
		{
			Projectile.width = 14;               //The width of projectile hitbox
			Projectile.height = 14;              //The height of projectile hitbox
			Projectile.aiStyle = ProjAIStyleID.Arrow;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Ranged;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 20;              //How much light emit around the projectile
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
            
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
                Projectile.netUpdate = true;
            }
            Projectile.scale = Projectile.ai[1];
            return;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Moonraze>(), 600);
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(Projectile.width, Projectile.height), Projectile.width, Projectile.height, ModContent.DustType<YamataDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, ModContent.DustType<YamataDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<Toxiboom>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}
