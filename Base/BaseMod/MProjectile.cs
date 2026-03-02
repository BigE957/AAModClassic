using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAMod
{
    public class MProjectile : GlobalProjectile
	{
		public override bool PreDrawExtras(Projectile projectile)
		{
			BaseArmorData.lastShaderDrawObject = projectile;
			return base.PreDrawExtras(projectile, spriteBatch);
		}		
	}
}