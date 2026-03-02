
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class DoomSeeds : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doom Seeds");
            // Tooltip.SetDefault("Plants Doomgrass"); ;	
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
            Item.createTile = Mod.Find<ModTile>("DoomGrass").Type;
            Item.consumable = true;		
        }

		public override bool CanUseItem(Player p)
		{
			Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
			if(tile != null && tile.HasTile && tile.TileType == TileID.Dirt)
			{
				WorldGen.destroyObject = true;
				TileID.Sets.BreakableWhenPlacing[TileID.Dirt] = true;
				return base.CanUseItem(p);				
			}
			return false;
		}

		public override bool? UseItem(Player p)/* tModPorter Suggestion: Return null instead of false */
		{
			WorldGen.destroyObject = false;
			TileID.Sets.BreakableWhenPlacing[TileID.Dirt] = false;		
			return base.UseItem(p);
		}
	}
}