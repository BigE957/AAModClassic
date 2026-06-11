using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Pets;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic.CrossMod;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.BossStandard
{
    public class HydraTreasureBag : BaseAAItem
	{
        //TODO: Doesnt seem to exist
        //public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            //Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
			Item.expert = true; Item.expertOnly = true;
		}

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }

        //public override int BossBagNPC => ModContent.NPCType<Hydra>();

        public override bool CanRightClick()
		{
			return true;
        }
        
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            /*
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
            */
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
            if (ContentReplacementSystem.NeedToReplaceContent)
                itemLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<HydrasSpear>(), ModContent.ItemType<Mossket>(), ModContent.ItemType<GunkWand>(), ModContent.ItemType<GlowingMossBall>(), ModContent.ItemType<ShadowBand>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HydraMask1>(), 7));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HydraMask2>(), 7));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HydraMask3>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HydraPendant>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HydraHide>(), 1, 50, 100));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AbyssiumOre>(), 1, 75, 125));
        }
	}
}