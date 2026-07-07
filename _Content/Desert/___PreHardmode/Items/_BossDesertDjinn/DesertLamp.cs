using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

using Terraria.Localization;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn
{
    //imported from my tAPI mod because I'm lazy
    public class DesertLamp : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.BossSummon";
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
			Item.maxStack = Item.CommonMaxStack;
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
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<DesertDjinn>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.DesertDjinn"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            /*if (WorldTypeSystem.WorldType == AAWorldType.Beta)
                return false;
            */
            if (!player.ZoneDesert)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DjinnLampDesertFalse1"), Color.Goldenrod.R, Color.Goldenrod.G, Color.Goldenrod.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<DesertDjinn>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.DjinnLampDesertFalse2"), Color.Goldenrod.R, Color.Goldenrod.G, Color.Goldenrod.B, false);
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
                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.Center.X, (int)player.Center.Y, bossType, 0);
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

        public override void UseStyle(Player player, Rectangle heldItemFrame) { BaseUseStyle.SetStyleBoss(player, Item, true, true); }
        public override void UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, Item); }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe(1);
                recipe.AddIngredient(ModContent.ItemType<DesertMana>(), 3);
                recipe.AddIngredient(ItemID.Sandstone, 30);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
        }
	}
}