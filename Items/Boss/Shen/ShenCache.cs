using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Shen
{
    public class ShenCache : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Cache");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
			Item.expert = true; Item.expertOnly = true;
		}

        //public override int BossBagNPC => Mod.Find<ModNPC>("ShenA").Type;

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
            if (Main.rand.NextFloat() < 0.01f)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("ChaosScale").Type, Main.rand.Next(30, 40));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("ChaosSoul").Type);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("EXSoul").Type);
            string[] lootTable = 
            {
                "ChaosSlayer", "MeteorStrike", "Skyfall", "Astroid", "DraconicRipper", "FlamingTwilight", "ShenTerratool", "Timesplitter"
            };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>(lootTable[loot]).Type);
        }
	}
}