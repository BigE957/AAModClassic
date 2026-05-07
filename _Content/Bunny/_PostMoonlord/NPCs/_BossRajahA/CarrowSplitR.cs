using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.NPCs._BossRajahA
{
    public class CarrowSplitR : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Carrow");
		}

		public override void SetDefaults()
		{
            Projectile.DamageType = DamageClass.Melee;
			Projectile.width = 10; 
			Projectile.height = 10; 
			Projectile.aiStyle = ProjAIStyleID.Arrow;   
			Projectile.friendly = false; 
			Projectile.hostile = true;  
			Projectile.penetrate = -1;  
			Projectile.timeLeft = 600;  
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.noDropItem = true;
        }

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CarrotDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}
