using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Items.Magic;
using AAModClassic.Items.Melee;
using AAModClassic.Items.Ranged;
using AAModClassic.Items.Summoning;
using AAModClassic.Tiles.Crates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.FishingItem.Crate
{
    public class VoidCrate : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 99;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = ModContent.TileType<VoidCrate_Tile>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Crate");
            // Tooltip.SetDefault("Right click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if(Main.rand.NextBool(3))
            {
                int item = Main.rand.Next(4);

                switch (item)
                {
                    case 0:
                        item = ModContent.ItemType<Voidsaber>();
                        break;
                    case 1:
                        item = ModContent.ItemType<DoomGun>();
                        break;
                    case 2:
                        item = ModContent.ItemType<DoomStaff>();
                        break;
                    default:
                        item = ModContent.ItemType<ProbeControlUnit>();
                        break;
                }

                int index = Item.NewItem(Item.GetSource_Loot(), (int)player.position.X, (int)player.position.Y, player.width, player.height, item, 1, false, -1, false, false);
                int index1 = Item.NewItem(Item.GetSource_Loot(), (int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<DoomiteScrap>(), Main.rand.Next(0, 5));

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index, 1f, 0f, 0f, 0, 0, 0);
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index1, 1f, 0f, 0f, 0, 0, 0);
                }
            }
            player.OpenFishingCrate(4000);
        }
    }
}