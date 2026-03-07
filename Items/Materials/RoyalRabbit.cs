using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Materials
{
    public class RoyalRabbit : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Royal Rabbit");
            // Tooltip.SetDefault("Under direct protection by the Pouncing Punisher");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 30;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int num = NPC.NewNPC(Item.GetSource_ReleaseEntity(), (int)(player.position.X + Main.rand.Next(-20, 20)), (int)(player.position.Y - 0f), Mod.Find<ModNPC>("RoyalRabbit").Type);
            if (Main.netMode == NetmodeID.Server && num < 200)
            {
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
            }
            return true;
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
                            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Sounds/Rajah"), player.Center);
                            AAModGlobalNPC.SpawnRajah(player, true, new Vector2(player.Center.X, player.Center.Y - 2000), Language.GetTextValue("Mods.AAModClassic.Common.RajahRabbit"));

                        }
                        if (bunnyKills % 100 == 0 && bunnyKills >= 1000)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.RoyalRabbitSummoned2") + player.name.ToUpper() + "!", 107, 137, 179);
                            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Sounds/Rajah"), player.Center);
                            AAModGlobalNPC.SpawnRajah(player, true, new Vector2(player.Center.X, player.Center.Y - 2000), Language.GetTextValue("Mods.AAModClassic.Common.RajahRabbit"));
                        };
                    }
                }
                Item.active = false;
            }
        }
    }
}
