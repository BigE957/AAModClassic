
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
namespace AAModClassic._Content.Void.__Hardmode.Items.Consumables
{
    public class VoidKey : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Consumables";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomstopper Chip");
			// Tooltip.SetDefault("'Unlocks Doomsday Chests'");
		}


        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.LightPurple;
            Item.maxStack = Item.CommonMaxStack;
			Item.value = 800000;
            Item.noMelee = true;
        }
    }
}
