using Terraria.ID;
using Terraria.ModLoader;
namespace AAMod.Items.FishingItem
{
    public class SharpeningLavaFish : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sharpening Lava Fish");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.LightRed;
            AARarity = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 128000;
            Item.createTile = Mod.Find<ModTile>("SharpeningLavaFishTile").Type;
        }
    }
}
