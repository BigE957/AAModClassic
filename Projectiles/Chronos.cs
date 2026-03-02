using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class Chronos : ModProjectile 
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chronos");
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 14;
            Projectile.height = 14;          
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;    
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 400f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 18f;
        }

        public override void PostAI()
        {
            Projectile.localAI[1] += 1f;
            if (Projectile.localAI[1] >= 20f)
            {
                float num3 = 400f;
                Vector2 vector = Projectile.velocity;
                Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector2.Normalize();
                vector2 *= Main.rand.Next(10, 41) * 0.1f;
                if (Main.rand.Next(3) == 0)
                {
                    vector2 *= 2f;
                }
                vector *= 0.25f;
                vector += vector2;
                for (int j = 0; j < 200; j++)
                {
                    if (Main.npc[j].CanBeChasedBy(this, false))
                    {
                        float num4 = Main.npc[j].position.X + Main.npc[j].width / 2;
                        float num5 = Main.npc[j].position.Y + Main.npc[j].height / 2;
                        float num6 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num4) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num5);
                        if (num6 < num3 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
                        {
                            num3 = num6;
                            vector.X = num4;
                            vector.Y = num5;
                            vector -= Projectile.Center;
                            vector.Normalize();
                            vector *= 8f;
                        }
                    }
                }
                vector *= 0.8f;
                Projectile.NewProjectile(Projectile.Center.X - vector.X, Projectile.Center.Y - vector.Y, vector.X, vector.Y, ModContent.ProjectileType<Time>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1, 0f);
                Projectile.localAI[1] = 0f;
            }
        }
    }
}
