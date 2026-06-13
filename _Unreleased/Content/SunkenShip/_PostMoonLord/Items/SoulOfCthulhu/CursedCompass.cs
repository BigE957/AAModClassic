using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Localization;
using System;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu
{
    public class CursedCompass : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Compass");
            // Tooltip.SetDefault(@"An old Compass. Who knows what it's for?");
        }

        private static bool CthulhuActive => AAWorld.downedAllAncients && !AAWorld_Unreleased.DownedSoC;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            bool canFightSoC = AAWorld.downedAllAncients;
            foreach (TooltipLine line in list)
            {
                if (line.Mod == "Terraria" && line.Name == "ItemName")
                    line.OverrideColor = canFightSoC ? AAColor.Cthulhu : new Color(100, 100, 100);

                if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                {
                    if(CthulhuActive)
                        line.Text = Language.GetTextValue("Mods.AAModClassic.Items.CursedCompass.AltText0.Ready");
                    else if(AAWorld_Unreleased.DownedSoC)
                        line.Text = Language.GetTextValue("Mods.AAModClassic.Items.CursedCompass.AltText0.Downed");
                }

                if (!canFightSoC)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip1")
                        line.Hide();
                    if (line.Mod == "Terraria" && line.Name == "Tooltip2")
                        line.Hide();
                }
                else if(AAWorld_Unreleased.DownedSoC && line.Mod == "Terraria" && line.Name == "Tooltip1")
                    line.Text = Language.GetTextValue("Mods.AAModClassic.Items.CursedCompass.AltText1");
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.SoulOfCthulhu.SoulOfCthulhu>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The wheel doesn't do anything", Color.DarkCyan, false);
                return false;
            }
            return AAWorld.downedAllAncients;
        }

        public override bool? UseItem(Player player)
        {
            SpawnBoss(player, ModContent.NPCType<CthulhuSpawn>());
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public void SpawnBoss(Player player, int type)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.AnyNPCs(type))
                    return;
                int npcID = NPC.NewNPC(Item.GetSource_FromThis(), (int)player.Center.X, (int)player.Center.Y, type, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-300f, 300f, (float)Main.rand.NextDouble()), 300f);
                Main.npc[npcID].netUpdate2 = true;
                Main.npc[npcID].target = player.whoAmI;
            }
        }

        public float ArrowSpin = 0;

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            ArrowSpin += MathF.Sin(Main.GlobalTimeWrappedHourly) * 0.25f;
            Texture2D Arrow = ModContent.Request<Texture2D>(Texture + "_Arrow").Value;
            Vector2 offsetPos = position - Vector2.UnitY * 3;
            spriteBatch.Draw(Arrow, offsetPos, null, drawColor, CthulhuActive? ArrowSpin : 0, Arrow.Size() * 0.5f, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            ArrowSpin += MathF.Sin(Main.GlobalTimeWrappedHourly) * 0.25f;
            Texture2D Arrow = ModContent.Request<Texture2D>(Texture + "_Arrow").Value;
            Item item = Main.item[whoAmI];
            Vector2 position = item.position + new Vector2(item.width / 2, item.height * 0.5f);
            position.Y -= 8;
            spriteBatch.Draw(Arrow, position - Main.screenPosition, null, lightColor, CthulhuActive ? ArrowSpin : rotation, Arrow.Size() * 0.5f, scale, SpriteEffects.None, 0f);

        }

        public override void UseStyle(Player player, Rectangle heldItemFrame) { BaseUseStyle.SetStyleBoss(player, Item, true, true); }
        public override void UseItemFrame(Player player) { BaseUseStyle.SetFrameBoss(player, Item); }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Compass);
        }
    }
}