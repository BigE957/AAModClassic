using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Base.BaseMod
{
    public class MProjectile : GlobalProjectile
	{
		public override bool PreDrawExtras(Projectile projectile)
		{
			BaseArmorData.lastShaderDrawObject = projectile;
			return base.PreDrawExtras(projectile);
		}		
	}
}