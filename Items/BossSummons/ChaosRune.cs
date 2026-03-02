using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Shen;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAMod.Items.BossSummons
{
    public class ChaosRune : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Rune");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13;
            /* Tooltip.SetDefault(@"A cursed tablet bursting with chaotic energy
Summons Shen Doragon's true awakened form
Non-Consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 28;
            Item.rare = ItemRarityID.Green;
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
                    line2.OverrideColor = new Color(176, 39, 157);
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
                AAColor.Shen3,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            Texture2D texture2 = TextureAssets.Item[Item.type].Value;
            spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            for (int i = 0; i < 4; i++)
            {
                //Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position, null, AAColor.Shen3, 0, origin, scale, SpriteEffects.None, 0f);

            }

            return false;
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Yamata.Yamata>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Yamata.Awakened.YamataA>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Yamata.YamataTransition>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosRuneYamataFalse"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Akuma.Akuma>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Akuma.Awakened.AkumaA>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Akuma.AkumaTransition>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosRuneAkumaFalse"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Shen>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosRuneFalse"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<ShenA>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosRuneFalse"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<ShenSpawn>()) || NPC.AnyNPCs(ModContent.NPCType<ShenTransition>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDefeat>()) || NPC.AnyNPCs(ModContent.NPCType<ShenDeath>()))
            {
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosRuneTrue1"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ChaosRuneTrue2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("ShenA").Type, false, 0, 0);
            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/ShenRoar"), player.position);
            return true;
        }
    }
}
