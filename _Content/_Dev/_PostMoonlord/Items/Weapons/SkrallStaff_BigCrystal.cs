using AAModClassic._Vanilla.Facsimiles._1._3._5._3;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class SkrallStaff_BigCrystal : BoulderStaffOfEarthFacsimile
    {
        public override void SetDefaults()
        {
            //Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            base.SetDefaults();
            Projectile.penetrate = -1;  
            Projectile.width = 44;
            Projectile.height = 44;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 900;
            Projectile.DamageType = DamageClass.Magic;
        }

		public override void SetStaticDefaults()
		{
		// DisplayName.SetDefault("Big Crystal");
		}


    }
}
