using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Materials
{
    public class CrucibleScale : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crucible Scale");
            // Tooltip.SetDefault("The fury of the draconian sun eminates from this scale");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 4));
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.OrangeRed.ToVector3() * 0.55f * Main.essScale);
        }
    }
}