using Terraria.ModLoader;
namespace AAMod.Items.Boss.Yamata
{
    public class YamataATrophy : BaseAAItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yamata No Orochi Trophy");
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
            Item.rare = 1;
            Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 2000;
            Item.rare = 10;
            Item.expert = true; Item.expertOnly = true;
			Item.createTile = Mod.Find<ModTile>("YamataATrophy").Type;
		}
	}
}