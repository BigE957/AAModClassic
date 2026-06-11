using AAModClassic._Content.RedMushroom.World.Tiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Consumables
{
    public class MyceliumSeeds : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mycelium Seeds");
            // Tooltip.SetDefault("Plants Mycelium"); ;	
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
            //Item.createTile = ModContent.TileType<Mycelium_Tile>();
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
                tile.TileType = (ushort)ModContent.TileType<Mycelium_Tile>();
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