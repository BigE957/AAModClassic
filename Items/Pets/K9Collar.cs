using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class K9Collar : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("K9 Collar");
			// Tooltip.SetDefault("Summons a robotic buddy");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = Mod.Find<ModProjectile>("K9").Type;
            Item.buffType = Mod.Find<ModBuff>("K9").Type;
            Item.noUseGraphic = true;
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
    }
}