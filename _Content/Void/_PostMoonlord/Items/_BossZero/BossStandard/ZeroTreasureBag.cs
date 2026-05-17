using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Ammo;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Pets;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Tools;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Unofficial.Content.Void._PostMoonlord.Items._BossZero.BossStandard;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.BossStandard
{
    public class ZeroTreasureBag : BaseAAItem
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 36;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
            Item.rare = ItemRarityID.Red;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
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

            LeadingConditionRule notUnofficialRule = new(new AAConditions.NotUnofficial());

            notUnofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ZeroMask>(), 7));

            itemLoot.Add(notUnofficialRule);

            LeadingConditionRule unofficialRule = new(new AAConditions.Unofficial());

            unofficialRule.OnSuccess(ItemDropRule.OneFromOptions(7, ModContent.ItemType<ZeroMask>(), ModContent.ItemType<ZeroAMask>()));

            itemLoot.Add(unofficialRule);

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ZeroMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenCode>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<UnstableSingularity>(), 1, 30, 40));

            int[] lootTable =
            {
                ModContent.ItemType<UnstablePowerCell>(),
                ModContent.ItemType<SingularityArrow>(),
                ModContent.ItemType<TheVortex>(),
                ModContent.ItemType<EventHorizon>(),
                ModContent.ItemType<RealityCannon>(),
                ModContent.ItemType<RiftShredder>(),
                ModContent.ItemType<VoidStar>(),
                ModContent.ItemType<BrokenZeroWeapon>(),
                ModContent.ItemType<StallionsStar>(),
                ModContent.ItemType<DoomsdayTerratool>(),
                ModContent.ItemType<DoomPortal>(),
                ModContent.ItemType<Gigataser>(),
                ModContent.ItemType<OmegaVolley>(),
                ModContent.ItemType<GenocideCannon>() };
            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
	}
}