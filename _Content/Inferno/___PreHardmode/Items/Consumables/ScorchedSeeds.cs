using AAModClassic._Content.Inferno.World.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables
{
    public class ScorchedSeeds : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scorched Seeds");
            // Tooltip.SetDefault("Plants Inferno grass"); ;	
		}		
		
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 0, 0, 5);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.useTurn = true;
            //Item.createTile = ModContent.TileType<InfernoGrass_Tile>();
            Item.consumable = true;		
        }

        public override bool? UseItem(Player player) => true;

        public override bool ConsumeItem(Player player)
        {
            var tileX = Player.tileTargetX;
            var tileY = Player.tileTargetY;
            var tile = Framing.GetTileSafely(tileX, tileY);

            if (tile.HasTile && tile.TileType == TileID.Dirt && player.IsInTileInteractionRange(tileX, tileY, TileReachCheckSettings.Simple))
            {
                tile.TileType = (ushort)ModContent.TileType<InfernoGrass_Tile>();
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendTileSquare(player.whoAmI, tileX, tileY);
                }
                SoundEngine.PlaySound(SoundID.Dig, player.Center);
                return true;
            }

            return false;
        }
    }
}