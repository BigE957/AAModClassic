using AAModClassic.Items.Materials;
using AAModClassic.Items.Vanity.Mask;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Equinox
{
    public class EquinoxBag : BaseAAItem
	{
        public static Asset<Texture2D> DaybringerTreasureBagTex;
        public static Asset<Texture2D> DaybringerTreasureBagGlowmask;
        public static Asset<Texture2D> NightcrawlerTreasureBagTex;
        public static Asset<Texture2D> NightcrawlerTreasureBagGlowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            /* Tooltip.SetDefault(@"{$CommonItemTooltip.RightClickToOpen}
Contained loot depends on the time of day"); */

            DaybringerTreasureBagTex = ModContent.Request<Texture2D>("AAModClassic/Items/Boss/Equinox/DBBag");
            DaybringerTreasureBagGlowmask = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DBBag_Glow");
            NightcrawlerTreasureBagTex = ModContent.Request<Texture2D>("AAModClassic/Items/Boss/Equinox/NCBag");
            NightcrawlerTreasureBagGlowmask = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/NCBag_Glow");
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
        //public override int BossBagNPC => ModContent.NPCType<DaybringerHead>();

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = DaybringerTreasureBagTex.Value;
            Texture2D texture2 = NightcrawlerTreasureBagTex.Value;
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
            Texture2D texture = DaybringerTreasureBagTex.Value;
            Texture2D textureGlow = DaybringerTreasureBagGlowmask.Value;
            Texture2D texture2 = NightcrawlerTreasureBagTex.Value;
            Texture2D texture2Glow = NightcrawlerTreasureBagGlowmask.Value;
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
                    player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<NightcrawlerMask>());
                }
                if (Main.rand.Next(20) == 0)
                {
                    AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                    modPlayer.PMLDevArmor();
                }
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<DarkEnergy>(), Main.rand.Next(40, 90));
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<DarkVoid>());
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<DaybringerMask>());
                }
                if (Main.rand.Next(20) == 0)
                {
                    AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                    modPlayer.PMLDevArmor();
                }
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<Stardust>(), Main.rand.Next(40, 90));
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<RadiantStar>());
            }
            if (AAWorld.RadiumOre)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<StarIdol>());
            }
        }
	}
}