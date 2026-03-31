using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic;
using AAModClassic.CrossMod;
using AAModClassic.Items.Accessories;
using AAModClassic.Items.Blocks;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.Items.Pets;
using AAModClassic.Items.Ranged;
using AAModClassic.Items.Vanity.Mask;

namespace AAModClassic.Items.Boss.Broodmother
{
    public class BroodBag : BaseAAItem
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
        //public override int BossBagNPC => Mod.Find<ModNPC>("Broodmother").Type;

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
                //player.QuickSpawnItem(mod.ItemType("ZeroMask"));
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("BroodEgg").Type);
            }
            if (Main.rand.Next(10) == 0)
            {

                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("Incinerite").Type, Main.rand.Next(75, 125));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("BroodScale").Type, Main.rand.Next(50, 100));
            player.QuickSpawnItem(Item.GetSource_GiftOrReward(), Mod.Find<ModItem>("DragonCape").Type);

            if(ContentReplacementSystem.NeedToReplaceContent)
            {
                int[] eggDrops = [ModContent.ItemType<AAModClassic.Items.Melee.Pyrosphere>(), ModContent.ItemType<Firebuster>(), ModContent.ItemType<AAModClassic.Items.Magic.Volley>(), ModContent.ItemType<DragonsSoul>(), ModContent.ItemType<DragonsGuard>()];
                int itemID = eggDrops[Main.rand.Next(eggDrops.Length)];
                player.QuickSpawnItem(Item.GetSource_GiftOrReward(), itemID);
            }
        }
	}
}