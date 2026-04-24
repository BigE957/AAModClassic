using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent
{
    //imported from my tAPI mod because I'm lazy
    public class SubzeroCrystal : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Subzero Crystal");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            // Tooltip.SetDefault(@"Summons the Subzero Serpent");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 24;
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
            SpawnBoss(player, ModContent.NPCType<SubzeroSerpentHead>(), Language.GetTextValue("Mods.AAModClassic.Common.SubzeroSerpent"));
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            //if (WorldTypeSystem.WorldType == AAWorldType.Beta)
            //    return false;

            if (!player.ZoneSnow)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.SubzeroCrystalSnowZoneFalse"), Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<SubzeroSerpentHead>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.SubzeroCrystalFalse"), Color.Cyan.R, Color.Cyan.G, Color.Cyan.B, false);
                return false;
            }
            return true;
        }

        public void SpawnBoss(Player player, int bossType, string displayName)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!

                int type = 0;
                if (player.ZoneCorrupt)
                {
                    type = 1;
                }
                else if (player.ZoneCrimson)
                {
                    type = 2;
                }
                else if (player.GetModPlayer<AAPlayer>().ZoneInferno)
                {
                    type = 3;
                }
                else if (player.GetModPlayer<AAPlayer>().ZoneMire)
                {
                    type = 4;
                }
                else if (player.ZoneHallow)
                {
                    type = 5;
                }

                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.Center.X, (int)player.Center.Y, bossType, 0, ai2: type);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), -1000f);
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
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<SnowMana>(), 3);
            recipe.AddIngredient(ItemID.IceBlock, 30);
            recipe.AddTile(TileID.IceMachine);
            //recipe.AddCondition(Language.GetText("Mods.AAModClassic.Common.Conditions.ReleaseOrMixed"), () => WorldTypeSystem.WorldType != AAWorldType.Beta);
            recipe.Register();
        }
	}
}