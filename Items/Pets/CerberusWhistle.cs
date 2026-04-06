using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class CerberusWhistle : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hades' Whistle");
			// Tooltip.SetDefault("Summons the guard dog of the king of the underworld himself");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = ModContent.ProjectileType<Projectiles.Cerberus>();
            Item.buffType = ModContent.BuffType<Buffs.Cerberus_Buff>();
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