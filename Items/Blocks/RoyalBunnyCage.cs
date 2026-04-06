using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Blocks
{
    class RoyalBunnyCage : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 24;
            Item.height = 22;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<RoyalBunnyCage_Tile>(); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Royal Bunny Cage");
            // Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "RoyalRabbit", 1);
            recipe.AddIngredient(ItemID.Terrarium, 1);
            recipe.AddRecipeGroup("AAModClassic:Gold", 20);
            recipe.Register();
        }

        public override void PostUpdate()
        {
            if (Item.lavaWet)
            {
                Player player = Main.player[Player.FindClosest(Item.Center, Item.width, Item.height)];
                for (int i = 0; i < Main.maxPlayers; ++i)
                {
                    if (player.active && !player.dead)
                    {
                        int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
                        if (bunnyKills % 100 == 0 && bunnyKills < 1000)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.RoyalRabbitSummoned1"), 107, 137, 179);
                            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Sounds/Rajah"), player.Center);
                            AAModGlobalNPC.SpawnRajah(player, true, new Vector2(player.Center.X, player.Center.Y - 2000), Language.GetTextValue("Mods.AAModClassic.Common.RajahRabbit"));

                        }
                        if (bunnyKills % 100 == 0 && bunnyKills >= 1000)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.RoyalRabbitSummoned2") + player.name.ToUpper() + "!", 107, 137, 179);
                            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Sounds/Rajah"), player.Center);
                            AAModGlobalNPC.SpawnRajah(player, true, new Vector2(player.Center.X, player.Center.Y - 2000), Language.GetTextValue("Mods.AAModClassic.Common.RajahRabbit"));
                        };
                    }
                }
                Item.active = false;
            }
        }
    }
}
