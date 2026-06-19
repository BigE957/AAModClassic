using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Materials
{
    public class DragonFire : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon's Fire");
            // Tooltip.SetDefault("It's really really hot.");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
        }
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.Ichor);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = refItem.maxStack;
            Item.value = refItem.value;
            Item.rare = refItem.rare;
            Item.alpha = 40;
        }
    }
}