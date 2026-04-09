using AAModClassic.Globals;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAModClassic.Items.Materials
{
    public class PrimevalSkull : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Primeval Skull");
            // Tooltip.SetDefault("Energy from an age since passed radiates from this ancient fossil");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = 1000;
            Item.rare = ItemRarityID.Lime;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.Desert.ToVector3() * 0.55f * Main.essScale);
        }
    }
}