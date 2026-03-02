using Terraria;

namespace AAMod.NPCs.Bosses.Equinox
{
    public class Sunbeam : Moonray
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sunbeam");
		}

		public override void Effects()
		{
        	Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.5f / 255f, (255 - Projectile.alpha) * 0.5f / 255f, (255 - Projectile.alpha) * 0.05f / 255f);	
		}
    }
}