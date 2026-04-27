using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic.Items.Vanity.Mask;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Shen
{
    public class ShenCache : BaseAAItem
	{
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Cache");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Glowmask = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow");
        }

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
			Item.expert = true; Item.expertOnly = true;
		}

        //public override int BossBagNPC => ModContent.NPCType<ShenA>();

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Glowmask.Value;
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
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShenAMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosSoul>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosScale>(), 1, 30, 40));

            int[] lootTable = { ModContent.ItemType<ChaosSlayer>(), ModContent.ItemType<MeteorStrike>(), ModContent.ItemType<Skyfall>(), ModContent.ItemType<Astroid>(), ModContent.ItemType<DraconicRipper>(), ModContent.ItemType<FlamingTwilight>(), ModContent.ItemType<ShenTerratool>(), ModContent.ItemType<Timesplitter>() };
            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
	}
}