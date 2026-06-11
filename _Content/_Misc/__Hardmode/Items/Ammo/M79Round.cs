using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Ammo
{
    public class M79Round : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 25;
			Item.width = 8;
			Item.height = 16;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.Orange;
			Item.consumable = true;
			Item.shoot = ModContent.ProjectileType<M79Round_Proj>();
			Item.ammo = Item.type;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("M79 Round");
		}
    }
}
