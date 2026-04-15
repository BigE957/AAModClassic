
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.NPCs.Bosses.Zero.Protocol;
using AAModClassic.NPCs.Bosses.Zero;
using AAModClassic.Tiles.Crafters;
using AAModClassic.___Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.___Content.Stars._PostMoonlord.Items;

namespace AAModClassic.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class ZeroTesseract : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomsday Tesseract");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"DESCRIPTI0NHERE
UNSTABLE. C0NTAINS C0DE T0 ACTIVATE THE BRINGER 0F DEATH
N0N-C0NSUMABLE"); */
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }


        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ZoneVoid)
            {
                if (NPC.AnyNPCs(ModContent.NPCType<Zero>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<ZeroProtocol>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitVoidZoneFalse"), new Color(255, 0, 0), false);
            return false;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (!AAWorld.downedZero && !Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroTesseractTrue"), Color.Red.R, Color.Red.G, Color.Red.B);
            }

            if (!AAWorld.downedZero && Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroTesseractTrue"), Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (!Main.expertMode && AAWorld.downedZero)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroTesseractDownedTrue"), Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (Main.expertMode && AAWorld.downedZero)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroTesseractDownedTrue"), Color.Red.R, Color.Red.G, Color.Red.B);
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                AAWorld.zeroUS = true;
                if (!NPC.AnyNPCs(ModContent.NPCType<ZeroDeactivated>()))
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X, (int)player.position.Y - 300, ModContent.NPCType<Zero>());
            }

            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Glitch"));
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 20);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}