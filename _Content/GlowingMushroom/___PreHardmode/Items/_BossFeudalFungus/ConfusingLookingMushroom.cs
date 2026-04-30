using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus
{
    public class ConfusingLookingMushroom : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Confusing Looking Mushroom");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Summons the Feudal Fungus
Can only be used in a glowing mushroom biome"); */
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<FeudalFungus>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.FeudalFungus"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneGlowshroom)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) 
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ConfusingMushroomFalse1"), Color.SkyBlue, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<FeudalFungus>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ConfusingMushroomFalse2"), Color.SkyBlue, false);
                return false;
            }
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = Mod.Find<ModNPC>(name).Type;
                if (NPC.AnyNPCs(bossType)) return; //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) { BaseUseStyle.SetStyleBoss(player, Item, true, true); }
        public override void UseItemFrame(Player p) => BaseUseStyle.SetFrameBoss(p, Item);

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.GlowingMushroom, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}