using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class Axis_Holdout : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(342);
			Projectile.aiStyle = ProjAIStyleID.Spear;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Axis");
        }
		
		public override void AI()
		{
			if (Projectile.ai[0] == 0f)
			{
				Projectile.ai[0] = 3f;
				Projectile.netUpdate = true;
			}
			if (Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
			{
				Projectile.ai[0] -= 2.4f;
				if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner)
				{
					Projectile.localAI[0] = 1f;
					if (Collision.CanHit(Main.player[Projectile.owner].position, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height, new Vector2(Projectile.Center.X + Projectile.velocity.X, Projectile.Center.Y + Projectile.velocity.Y), Projectile.width, Projectile.height))
					{
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + Projectile.velocity.X, Projectile.Center.Y + Projectile.velocity.Y, Projectile.velocity.X * 2.6f, Projectile.velocity.Y * 2.6f, ModContent.ProjectileType<Axis_Proj>(), (int)(Projectile.damage * 0.8), Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
					}
				}
			}
			else
			{
				Projectile.ai[0] += 2.1f;
			}
		}
		
		public bool stop = false;
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (!stop)
			{
				Vector2 vel1 = new Vector2(-1, -1);
				vel1 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X+130, target.position.Y+130, vel1.X, vel1.Y, ModContent.ProjectileType<Axis_Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel2 = new Vector2(1, 1);
				vel2 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X-130, target.position.Y-130, vel2.X, vel2.Y, ModContent.ProjectileType<Axis_Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel3 = new Vector2(1, -1);
				vel3 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X-130, target.position.Y+130, vel3.X, vel3.Y, ModContent.ProjectileType<Axis_Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel4 = new Vector2(-1, 1);
				vel4 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X+130, target.position.Y-130, vel4.X, vel4.Y, ModContent.ProjectileType<Axis_Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel5 = new Vector2(0, -1);
				vel5 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X, target.position.Y+130, vel5.X, vel5.Y, ModContent.ProjectileType<Axis_Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel6 = new Vector2(0, 1);
				vel6 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X, target.position.Y-130, vel6.X, vel6.Y, ModContent.ProjectileType<Axis_Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel7 = new Vector2(1, 0);
				vel7 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X-130, target.position.Y, vel7.X, vel7.Y, ModContent.ProjectileType<Axis_Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				Vector2 vel8 = new Vector2(-1, 0);
				vel8 *= 5f;
				Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.position.X+130, target.position.Y, vel8.X, vel8.Y, ModContent.ProjectileType<Axis_Snowflake>(), Projectile.damage/3, 0, Main.myPlayer);
				stop = true;
			}
		}
    }
}
