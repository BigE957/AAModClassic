using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs
{
    public class DiverShot : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eyeshot");
		}
    	
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(452);
        }
    }
}