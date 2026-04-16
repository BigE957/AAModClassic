using AAModClassic.___Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.CrossMod;
using AAModClassic.Items.Pets;
using AAModClassic.Items.Ranged;
using AAModClassic.Items.Vanity.Mask;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Broodmother
{
    public class BroodBag : BaseAAItem
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
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<Broodmother>();

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
                modPlayer.PHMDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BroodmotherMask>(), 7));

            if (ContentReplacementSystem.NeedToReplaceContent)
                itemLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<AAModClassic.Items.Melee.Pyrosphere>(), ModContent.ItemType<Firebuster>(), ModContent.ItemType<AAModClassic.Items.Magic.Volley>(), ModContent.ItemType<DragonsSoul>(), ModContent.ItemType<DragonsGuard>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AAModClassic.Items.Pets.BroodEgg>(), 7));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonCape>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BroodScale>(), 1, 50, 100));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<IncineriteOre>(), 1, 75, 125));
        }
	}
}