using AAModClassic.Base.BaseMod.Base;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Banners
{
    public class Banners : BaseAAItem
	{
		int pStyle = -1;
		string dName = null;

        protected override bool CloneNewInstances => true;

        public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
		{
			return false;
		}

		public override void AutoStaticDefaults()
		{
			// DisplayName.SetDefault(Regex.Replace(GetType().Name, "([A-Z])", " $1").Trim());		
		}

		public Banners SetupBanner(string dname, int pstyle)
		{
            pStyle = pstyle;
			dName = dname;
			return this;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broken Banner");
			if(dName != null)
			{
				// DisplayName.SetDefault(dName + " Banner");
				BaseUtility.AddTooltips(Item, new string[] { "Nearby players get a bonus against: " + dName });	
			}
		}

        public override void SetDefaults()
        {
			if(dName != null)
			{
				Item.createTile = ModContent.TileType<Tiles.Banners.Banners_Tile>();
				Item.placeStyle = pStyle;			
			}
			Item.scale = 0.7f;
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Blue;
            Item.value = BaseUtility.CalcValue(0, 0, 10, 0);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.consumable = true;
		}
	}
}