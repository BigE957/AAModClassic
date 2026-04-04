using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.FishingItem.Crate
{
    public class IceCrate : BaseAAItem
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
            Item.createTile = ModContent.TileType<IceCrate>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Crate");
            // Tooltip.SetDefault("Right click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if(Main.rand.Next(3) == 0)
            {
                int item = Main.rand.Next(8);

                switch (item)
                {
                    case 0:
                        item = ItemID.BlizzardinaBottle;
                        break;
                    case 1:
                        item = ItemID.IceBoomerang;
                        break;
                    case 2:
                        item = ItemID.IceBlade;
                        break;
                    case 3:
                        item = ItemID.IceSkates;
                        break;
                    case 4:
                        item = ItemID.SnowballCannon;
                        break;
                    case 5:
                        item = ItemID.FlurryBoots;
                        break;
                    case 6:
                        item = ItemID.IceMirror;
                        break;
                    default:
                        item = ItemID.Fish;
                        break;
                }

                int index = Item.NewItem(Item.GetSource_Loot(), (int)player.position.X, (int)player.position.Y, player.width, player.height, item, 1, false, -1, false, false);

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index, 1f, 0f, 0f, 0, 0, 0);
                }
            }
            
            //bypass all checks and spawn defaults
            player.OpenFishingCrate(4000);
        }
    }
}