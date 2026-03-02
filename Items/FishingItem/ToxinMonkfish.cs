using Terraria.ModLoader;
namespace AAMod.Items.FishingItem
{
    public class ToxinMonkfish : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toxin Monkfish");
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
            Item.rare = 4;
            AARarity = 6;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 128000;
            Item.createTile = Mod.Find<ModTile>("ToxinMonkfishTile").Type;
        }
    }
}
