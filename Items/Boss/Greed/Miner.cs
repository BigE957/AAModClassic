using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class Miner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("MINE-er");
            // Tooltip.SetDefault("Mines ores faster");
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 13;
            Item.useAnimation = 20;
            Item.pick = 205;
            Item.useStyle = 1;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 8;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override bool CanUseItem(Player player)
		{
            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
            if (Main.tileOreFinderPriority[tile.TileType] > 0 && PickCheck(tile, Item.pick))
            {
                player.PickTile(Player.tileTargetX, Player.tileTargetY, 5000);
            }
			return true;
		}

        public bool PickCheck(Tile tile, int pickPower)
        {
			ModTile tile2 = TileLoader.GetTile(tile.TileType);
            if (tile.TileType == 211 && pickPower < 200)
			{
				return false;
			}
			else if ((tile.TileType == 25 || tile.TileType == 203) && pickPower < 65)
			{
				return false;
			}
			else if (tile.TileType == 117 && pickPower < 65)
			{
				return false;
			}
			else if (tile.TileType == 37 && pickPower < 50)
			{
				return false;
			}
			else if (tile.TileType == 404 && pickPower < 65)
			{
				return false;
			}
			else if ((tile.TileType == 22 || tile.TileType == 204) && pickPower < 55)
			{
				return false;
			}
			else if (tile.TileType == 56 && pickPower < 65)
			{
				return false;
			}
			else if (tile.TileType == 58 && pickPower < 65)
			{
				return false;
			}
			else if ((tile.TileType == 226 || tile.TileType == 237) && pickPower < 210)
			{
				return false;
			}
			else if (tile.TileType == 107 && pickPower < 100)
			{
				return false;
			}
			else if (tile.TileType == 108 && pickPower < 110)
			{
				return false;
			}
			else if (tile.TileType == 111 && pickPower < 150)
			{
				return false;
			}
			else if (tile.TileType == 221 && pickPower < 100)
			{
				return false;
			}
			else if (tile.TileType == 222 && pickPower < 110)
			{
				return false;
			}
			else if (tile.TileType == 223 && pickPower < 150)
			{
				return false;
			}
			else if (tile2 != null && pickPower < tile2.MinPick)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ItemID.GoldPickaxe, 1);
			recipe.AddIngredient(null, "StoneShell", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			recipe = CreateRecipe(1);
			recipe.AddIngredient(ItemID.PlatinumPickaxe, 1);
			recipe.AddIngredient(null, "StoneShell", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
