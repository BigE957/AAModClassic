
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Usable
{
    public class MireSeeds : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dank Seeds");
            // Tooltip.SetDefault("Plants Mire grass"); ;	
		}		
		
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Yellow;
            Item.value = BaseUtility.CalcValue(0, 0, 0, 5);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.createTile = Mod.Find<ModTile>("MireGrass").Type;
            Item.consumable = true;		
        }

		public override bool CanUseItem(Player p)
		{
			Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
			if(tile != null && tile.HasTile && tile.TileType == TileID.Mud)
			{
				WorldGen.destroyObject = true;
				TileID.Sets.BreakableWhenPlacing[TileID.Mud] = true;
				return base.CanUseItem(p);				
			}
			return false;
		}

		public override bool? UseItem(Player p)/* tModPorter Suggestion: Return null instead of false */
		{
			WorldGen.destroyObject = false;
			TileID.Sets.BreakableWhenPlacing[TileID.Mud] = false;		
			return base.UseItem(p);
		}
	}
}