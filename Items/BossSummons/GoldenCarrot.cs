using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Rajah;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace AAMod.Items.BossSummons
{
    public class GoldenCarrot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ten Karat Carrot");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            // Tooltip.SetDefault(@"Summons the Pouncing Punisher himself");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.rare = 2;
            Item.maxStack = 20;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.UseSound = Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah");
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !(NPC.AnyNPCs(ModContent.NPCType<Rajah>()) ||
                NPC.AnyNPCs(ModContent.NPCType<SupremeRajah>()));
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int overrideDirection = Main.rand.Next(2) == 0 ? -1 : 1;
            SpawnBoss(player, Mod.Find<ModNPC>("Rajah").Type, true, player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, -1200), Language.GetTextValue("Mods.AAMod.Common.RajahRabbit"));
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<Potions.Carrot>(), 5);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddIngredient(ItemID.GoldBunny, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default, string overrideDisplayName = "", bool namePlural = false)
        {
            if (npcCenter == default)
                npcCenter = player.Center;
            if (Main.netMode != 1)
            {
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType, 0);
                Main.npc[npcID].ai[3] = -1;
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate2 = true;
                if (spawnMessage)
                {
                    string npcName = !string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName;
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].ModNPC != null)
                        npcName = Main.npc[npcID].ModNPC.DisplayName.GetDefault();
                    if (namePlural)
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != 1) BaseUtility.Chat(npcName + Language.GetTextValue("Mods.AAMod.Common.BosshasAwoken"), 175, 75, 255, false); }
                        else
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(npcName + Language.GetTextValue("Mods.AAMod.Common.BosshasAwoken")), new Color(175, 75, 255), -1);
                        }
                    }
                    else
                    {
                        if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                        else
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                            {
                            NetworkText.FromLiteral(npcName)
                            }), new Color(175, 75, 255), -1);
                        }
                    }
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, overrideDisplayName, namePlural);
            }
        }
    }
}