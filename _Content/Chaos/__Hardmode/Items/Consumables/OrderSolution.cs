using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Consumables
{
    public class OrderSolution : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Consumables";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Order Solution");
            /* Tooltip.SetDefault(@"Used by the Clentaminator
Cleanses the Chaos"); */
            Item.ResearchUnlockCount = 99;
        }

		public override void SetDefaults()
		{
			Item.shoot = ModContent.ProjectileType<OrderSolution_Proj>() - ProjectileID.PureSpray;
			Item.ammo = AmmoID.Solution;
			Item.width = 10;
			Item.height = 12;
			Item.value = Item.sellPrice(0, 0, 25, 0);
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
		}
	}
}
