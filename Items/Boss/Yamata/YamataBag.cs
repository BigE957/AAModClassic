using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Boss.Yamata
{
    public class YamataBag : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Red;
            Item.expert = true; Item.expertOnly = true;
        }

        //public override int BossBagNPC => ModContent.NPCType<YamataA>();

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow").Value;
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

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<YamataMask>());
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<DreadScale>(), Main.rand.Next(30, 40));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<Naitokurosu>());
            string[] lootTable = { "Flairdra", "Crescent", "Hydraslayer", "AbyssArrow", "HydraStabber", "MidnightWrath", "YamataTerratool", "Sevenshot"};
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
        }
    }
}