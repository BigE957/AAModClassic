using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.__Hardmode.Items.Consumables
{
    public class BlackSolution : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Consumables";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Black Solution");
            /* Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the Void"); */
            Item.ResearchUnlockCount = 99;
        }

		public override void SetDefaults()
		{
			Item.shoot = ModContent.ProjectileType<BlackSolution_Proj>() - ProjectileID.PureSpray;
			Item.ammo = AmmoID.Solution;
			Item.width = 10;
			Item.height = 12;
			Item.value = Item.sellPrice(0, 0, 25, 0);
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 9999;
			Item.consumable = true;
		}
	}
}
