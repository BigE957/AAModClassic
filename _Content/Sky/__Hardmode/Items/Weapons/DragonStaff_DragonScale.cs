using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAModClassic._Content.Sky.__Hardmode.Items.Weapons
{
    public class DragonStaff_DragonScale : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.HornetStinger);
			Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 3;  
            Projectile.width = 16;
            Projectile.height = 16;
        }

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			List<NPC> list = new List<NPC>();
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.CanBeChasedBy(this, false) && Projectile.Distance(nPC.Center) < 800f)
				{
					list.Add(nPC);
				}
			}
			Vector2 center = Projectile.Center;
			Vector2 value = Vector2.Zero;
			if (list.Count > 0)
			{
				NPC expr_94 = list[Main.rand.Next(list.Count)];
				center = expr_94.Center;
				value = expr_94.velocity;
			}
			int num = Main.rand.Next(2) * 2 - 1;
			Vector2 vector = new Vector2(num * (4f + Main.rand.Next(3)), 0f);
			Vector2 vector2 = center + new Vector2(-(float)num * 120, 0f);
			vector += (center + value * 15f - vector2).SafeNormalize(Vector2.Zero) * 2f;
			int p = Projectile.NewProjectile(Projectile.GetSource_OnHit(target), vector2, vector, ProjectileID.MonkStaffT2Ghast, Projectile.damage/2, 0f, Projectile.owner, 0f, 0f);
			Main.projectile[p].DamageType = DamageClass.Magic;
		}

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0, new Color(50, 200, 0), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0, new Color(50, 200, 0), 1f);
                Main.dust[num469].velocity *= 2f;
            }
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Scale");
    }

    }
}
