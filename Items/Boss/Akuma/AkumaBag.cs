using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Boss.Akuma
{
    public class AkumaBag : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
            Item.rare = ItemRarityID.Red;
        }


        public override int BossBagNPC => Mod.Find<ModNPC>("AkumaA").Type;

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

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("AkumaMask").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            player.QuickSpawnItem(Mod.Find<ModItem>("CrucibleScale").Type, Main.rand.Next(30, 40));
            player.QuickSpawnItem(Mod.Find<ModItem>("TaiyangBaolei").Type);
            string[] lootTable = { "AkumaTerratool", "DayStorm", "LungStaff", "MorningGlory", "RadiantDawn", "Solar", "SunSpear", "ReignOfFire", "DaybreakArrow", "Daycrusher", "Dawnstrike", "SunStorm", "SunStaff", "DragonSlasher" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Mod.Find<ModItem>(lootTable[loot]).Type);
        }
    }
}