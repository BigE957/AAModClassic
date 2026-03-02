using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class DjinnLamp : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desert Lamp");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            // Tooltip.SetDefault(@"Summons the Desert Djinn");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 26;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
		}

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("Djinn").Type, true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.DesertDjinn"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneDesert)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DjinnLampDesertFalse1"), Color.Goldenrod.R, Color.Goldenrod.G, Color.Goldenrod.B, false);
                return false;
            }
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Djinn").Type))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DjinnLampDesertFalse2"), Color.Goldenrod.R, Color.Goldenrod.G, Color.Goldenrod.B, false);
                return false;
            }
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = Mod.Find<ModNPC>(name).Type;
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
                string npcName = !string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName;
                if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
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

        public override void UseStyle(Player player, Rectangle heldItemFrame) { BaseUseStyle.SetStyleBoss(p, Item, true, true); }
        public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, Item); return true; }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe(1);
                recipe.AddIngredient(null, "DesertMana", 3);
                recipe.AddIngredient(ItemID.Sandstone, 30);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
        }
	}
}