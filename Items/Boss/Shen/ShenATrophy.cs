using Terraria.ModLoader;
namespace AAMod.Items.Boss.Shen
{
    public class ShenATrophy : BaseAAItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shen Awakened Trophy");
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
			Item.rare = 2;
            Item.expert = true; Item.expertOnly = true;
			Item.createTile = Mod.Find<ModTile>("ShenATrophy").Type;
		}
	}
}