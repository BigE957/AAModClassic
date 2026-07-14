
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void.___PreHardmode.NPCs;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero
{
    //imported from my tAPI mod because I'm lazy
    public class DoomsdayTesseract : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.BossSummon";
        public static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomsday Tesseract");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"DESCRIPTI0NHERE
UNSTABLE. C0NTAINS C0DE T0 ACTIVATE THE BRINGER 0F DEATH
N0N-C0NSUMABLE"); */
            if (Main.netMode != NetmodeID.Server)
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
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

        

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<ZAAPlayer>().ZoneVoid)
            {
                if (NPC.AnyNPCs(ModContent.NPCType<Zero>()))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ZeroUnitFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<ZeroA>()))
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

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            var position = Item.Center - Main.screenPosition;
            var origin = glowmask.Size() / 2f;
            spriteBatch.Draw(glowmask.Value, position, null, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}