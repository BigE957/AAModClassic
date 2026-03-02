using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.FishingItem.Crate
{
    public class DesertCrate : BaseAAItem
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
            Item.createTile = Mod.Find<ModTile>("DesertCrate").Type;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desert Crate");
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
                int item = Main.rand.Next(4);

                switch (item)
                {
                    case 0:
                        item = ItemID.FlyingCarpet;
                        break;
                    case 1:
                        item = ItemID.SandstorminaBottle;
                        break;
                    default:
                        item = ItemID.PharaohsMask;
                        break;
                }

                int index = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item, 1, false, -1, false, false);
                if (item == ItemID.PharaohsMask)
                {
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.PharaohsRobe, 1, false, -1, false, false);
                }

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index, 1f, 0f, 0f, 0, 0, 0);
                }
            }
            
            //bypass all checks and spawn defaults
            player.openCrate(4000);
        }
    }
}