using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Weapons
{
    public class Dracorang_Proj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(106);
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;  
            Projectile.width = 22;
            Projectile.height = 32;
			Projectile.aiStyle = ProjAIStyleID.Boomerang;
			AIType = ProjectileID.LightDisc;
        }

		public override void SetStaticDefaults()
		{
		  // DisplayName.SetDefault("Dracorang");
		}
		
		public override void AI()
		{
			int type = Main.rand.Next(326,328);
			int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, type, Projectile.damage/3, 0, Main.myPlayer);
			Main.projectile[proj].hostile = false;
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].penetrate = 1;
			Main.projectile[proj].timeLeft = 15;
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 5;
		}
    }
}
