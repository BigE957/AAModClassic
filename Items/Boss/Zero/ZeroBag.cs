using AAModClassic.Items.Boss.Sagittarius;
using AAModClassic.Items.Pets;
using AAModClassic.Items.Vanity.Mask;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Zero
{
    public class ZeroBag : BaseAAItem
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Glowmask = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 36;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
            Item.rare = ItemRarityID.Red;
        }

        //public override int BossBagNPC => ModContent.NPCType<ZeroProtocol>();

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
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ZeroCore>(), 10));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ZeroMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenCode>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<UnstableSingularity>(), 1, 30, 40));

            int[] lootTable =
            {
                ModContent.ItemType<Battery>(),
                ModContent.ItemType<ZeroArrow>(),
                ModContent.ItemType<Vortex>(),
                ModContent.ItemType<EventHorizon>(),
                ModContent.ItemType<Items.Boss.Zero.RealityCannon>(),
                ModContent.ItemType<Items.Boss.Zero.RiftShredder>(),
                ModContent.ItemType<Items.Boss.Zero.VoidStar>(),
                ModContent.ItemType<Items.Boss.Zero.TeslaHand>(),
                ModContent.ItemType<ZeroStar>(),
                ModContent.ItemType<ZeroTerratool>(),
                ModContent.ItemType<DoomPortal>(),
                ModContent.ItemType<Gigataser>(),
                ModContent.ItemType<Items.Boss.Zero.OmegaVolley>(),
                ModContent.ItemType<Items.Boss.Zero.GenocideCannon>() };
            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
	}
}