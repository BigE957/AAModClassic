using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu
{
    public class CursedCompass : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Compass");
            // Tooltip.SetDefault(@"An old Compass. Who knows what it's for?");
        }

        private static bool CthulhuFightable => AAWorld.downedAllAncients && !AAWorld_Unreleased.downedSoC;

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
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = CthulhuFightable ? new Color(100, 100, 100) : AAColor.Cthulhu;
                }
            }
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {

            // Tooltip.SetDefault(CthulhuFightable ? "An old, broken compass. Who knows what it's for." : "The compass' arrow spins rapidly, giving off an eerie vibe.");
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.SoulOfCthulhu.SoulOfCthulhu>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The wheel doesn't do anything", Color.DarkCyan, false);
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            SpawnBoss(player, "CthulhuSpawn", "The Soul of Cthulhu");
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = Mod.Find<ModNPC>(name).Type;
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC(Item.GetSource_FromThis(), (int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-300f, 300f, (float)Main.rand.NextDouble()), 300f);
                Main.npc[npcID].netUpdate2 = true;
                Main.npc[npcID].target = player.whoAmI;
            }
        }

        //TODOSOC bring it back?
        /*public float ArrowSpin = 0;

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            ArrowSpin += .008f;
            Texture2D Arrow = ModContent.Request<Texture2D>("AAModClassic/Items/BossSummons/CursedCompass_Arrow").Value;
            Vector2 offsetPositon = new Vector2(item.position.X, item.position.Y - 2);
            spriteBatch.Draw(Arrow, position, null, drawColor, CthulhuFightable? ArrowSpin : 0, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            ArrowSpin += .008f;
            Texture2D texture2D13 = Main.itemTexture[item.type];
            Texture2D Arrow = ModContent.Request<Texture2D>("AAModClassic/Items/BossSummons/CursedCompass_Arrow").Value;
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture2D13.Height * 0.5f + 2f);
            Vector2 offsetPositon = new Vector2(item.position.X, item.position.Y - 2);
            spriteBatch.Draw(Arrow, position, null, Main.DiscoColor, CthulhuFightable ? ArrowSpin : rotation, texture2D13.Size() * 0.5f, scale, SpriteEffects.None, 0f);

        }*/

        public override void UseStyle(Player player, Rectangle heldItemFrame) { BaseUseStyle.SetStyleBoss(player, Item, true, true); }
        public override void UseItemFrame(Player player) { BaseUseStyle.SetFrameBoss(player, Item); }
    }
}