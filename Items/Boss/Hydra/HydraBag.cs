using AAModClassic;
using AAModClassic.CrossMod;
using AAModClassic.Items.Accessories;
using AAModClassic.Items.Magic;
using AAModClassic.Items.Melee;
using AAModClassic.Items.Pets;
using AAModClassic.Items.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Hydra
{
    public class HydraBag : BaseAAItem
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
			Item.width = 36;
			Item.height = 32;
			Item.expert = true; Item.expertOnly = true;
		}

        //public override int BossBagNPC => ModContent.NPCType<Hydra>();

        public override bool CanRightClick()
		{
			return true;
        }
        
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

        public override void RightClick(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<HydraMask1>());
            }
            else if (Main.rand.Next(7) == 1)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<HydraMask2>());
            }
            else if(Main.rand.Next(7) == 2)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<HydraMask3>());
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<Abyssium>(), Main.rand.Next(75, 125));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<HydraHide>(), Main.rand.Next(50, 100));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), ModContent.ItemType<HydraPendant>());

            if (ContentReplacementSystem.NeedToReplaceContent)
            {
                int[] podDrops = [ModContent.ItemType<HydrasSpear>(), ModContent.ItemType<Mossket>(), ModContent.ItemType<GunkWand>(), ModContent.ItemType<GlowmossBall>(), ModContent.ItemType<ShadowBand>()];
                int itemID = podDrops[Main.rand.Next(podDrops.Length)];
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), itemID);
            }
        }
	}
}