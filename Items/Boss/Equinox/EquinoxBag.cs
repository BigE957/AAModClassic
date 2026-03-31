using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Boss.Equinox
{
    public class EquinoxBag : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
			/* Tooltip.SetDefault(@"{$CommonItemTooltip.RightClickToOpen}
Contained loot depends on the time of day"); */
		}

		public override void SetDefaults()
		{
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.width = 32;
			Item.height = 36;
			Item.rare = ItemRarityID.Purple;
			Item.expert = true; Item.expertOnly = true;
        }
        //public override int BossBagNPC => Mod.Find<ModNPC>("DaybringerHead").Type;

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Items/Boss/Equinox/DBBag").Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("AAModClassic/Items/Boss/Equinox/NCBag").Value;
            if (Main.dayTime)
            {
                spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Items/Boss/Equinox/DBBag").Value;
            Texture2D textureGlow = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DBBag_Glow").Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("AAModClassic/Items/Boss/Equinox/NCBag").Value;
            Texture2D texture2Glow = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/NCBag_Glow").Value;
            if (Main.dayTime)
            {
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
                spriteBatch.Draw
                (
                    textureGlow,
                    new Vector2
                    (
                        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );

                return false;
            }
            else
            {
                spriteBatch.Draw
                (
                    texture2,
                    new Vector2
                    (
                        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
                spriteBatch.Draw
                (
                    texture2Glow,
                    new Vector2
                    (
                        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );

                return false;
            }
        }

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
            if (!Main.dayTime)
            {
                if (Main.rand.Next(7) == 0)
                {
                    player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("NCMask").Type);
                }
                if (Main.rand.Next(20) == 0)
                {
                    AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                    modPlayer.PMLDevArmor();
                }
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("DarkEnergy").Type, Main.rand.Next(40, 90));
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("DarkVoid").Type);
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("DBMask").Type);
                }
                if (Main.rand.Next(20) == 0)
                {
                    AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                    modPlayer.PMLDevArmor();
                }
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("Stardust").Type, Main.rand.Next(40, 90));
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("RadiantStar").Type);
            }
            if (AAWorld.RadiumOre)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("StarIdol").Type);
            }
        }
	}
}