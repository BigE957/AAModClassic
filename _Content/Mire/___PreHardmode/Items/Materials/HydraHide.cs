using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Materials
{
    public class HydraHide : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra Hide");
            // Tooltip.SetDefault("The skin of a formidable foe");

            ItemTrader.ChlorophyteExtractinator.AddOption_Interchangable(ModContent.ItemType<HydraHide>(), ModContent.ItemType<ScorchedScale>());
        }

        public override void SetDefaults()
        {

            Item.width = 22;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100;
        }
    }
}
