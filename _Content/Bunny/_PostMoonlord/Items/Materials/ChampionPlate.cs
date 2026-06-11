using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Materials
{
    public class ChampionPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Champion Plate");
            // Tooltip.SetDefault("Forged from Champium");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Purple;
        }

        

    }
}
