using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items.Materials
{
    public class SeraphFeather : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Seraph Feather");
            // Tooltip.SetDefault("A silvery feather from a harpy seraph");
            Item.ResearchUnlockCount = 25;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 36;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Lime;
        }
    }
}