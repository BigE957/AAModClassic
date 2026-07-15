using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items.Materials
{
    public class StoneShell : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stone Shell");
            // Tooltip.SetDefault(@"Harder than bedrock but lighter than pumice");
            Item.ResearchUnlockCount = 25;
        }
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 24;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Yellow;
        }
    }
}
