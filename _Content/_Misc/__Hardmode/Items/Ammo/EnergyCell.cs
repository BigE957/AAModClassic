using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Ammo
{
    public class EnergyCell : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Ammo";
		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.width = 8;
			Item.height = 16;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.rare = ItemRarityID.Pink;
			Item.consumable = true;
			Item.shoot = ModContent.ProjectileType<EnergyCell_Proj>();
			Item.ammo = Item.type;
			
		}
		
		
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Energy Cell");
            Item.ResearchUnlockCount = 99;
        }
    }
}
