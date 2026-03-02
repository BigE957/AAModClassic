using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.FishingItem.Crate
{
    public class VoidCrate : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = 2;
            Item.maxStack = 99;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("VoidCrate").Type;
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
            if(Main.rand.Next(3) == 0)
            {
                int item = Main.rand.Next(4);

                switch (item)
                {
                    case 0:
                        item = Mod.Find<ModItem>("VoidSaber").Type;
                        break;
                    case 1:
                        item = Mod.Find<ModItem>("DoomGun").Type;
                        break;
                    case 2:
                        item = Mod.Find<ModItem>("DoomStaff").Type;
                        break;
                    default:
                        item = Mod.Find<ModItem>("ProbeControlUnit").Type;
                        break;
                }

                int index = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item, 1, false, -1, false, false);
                int index1 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, Mod.Find<ModItem>("DeactivatedDoomite").Type, Main.rand.Next(0, 5));

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(21, -1, -1, null, index, 1f, 0f, 0f, 0, 0, 0);
                    NetMessage.SendData(21, -1, -1, null, index1, 1f, 0f, 0f, 0, 0, 0);
                }
            }
            player.openCrate(4000);
        }
    }
}