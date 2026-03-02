using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAModClassic.Items.Materials
{
    public class DragonSpirit : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Spirit");
            // Tooltip.SetDefault("It looks apple-flavored");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = 999;
            Item.value = 1000;
            Item.rare = ItemRarityID.Lime;
        }

        //>:(

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LimeGreen.ToVector3() * 0.55f * Main.essScale);
        }
    }
}