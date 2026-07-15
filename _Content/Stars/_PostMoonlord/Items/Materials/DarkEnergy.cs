using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Materials
{
    public class DarkEnergy : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Energy");
            // Tooltip.SetDefault("It's oddly weightless");
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            Item.ResearchUnlockCount = 25;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Purple;
            Item.value = 10000;
        }
    }
}
