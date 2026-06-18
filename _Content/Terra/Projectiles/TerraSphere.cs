using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.Projectiles
{
    public class TerraSphere : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Bomb");
            Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true; 
			Projectile.hostile = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 20;
			Projectile.ignoreWater = true;
            Projectile.tileCollide = true;          
		}

        public override void AI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }

            if (Projectile.CountsAsClass(DamageClass.Melee))
            {
                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = Projectile.DirectionTo(n.Center) * 30;
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / 20);
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 1000;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(Projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = Projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            Projectile.Distance(Main.npc[selectedTarget].Center) > distance)
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeleft)
        {
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), (int)Projectile.Center.X, (int)Projectile.Center.Y, 0, 0, ModContent.ProjectileType<TerraBoom>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            Main.projectile[p].Center = Projectile.Center;
            Main.projectile[p].DamageType = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) == true ? Projectile.DamageType : DamageClass.Melee;
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Terra, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
            
        }
    }
}
