using AAModClassic._Content.Hoard.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class StoneSoldierHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.StoneSoldier";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Stone Soldier Helmet");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 16;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<StoneSoldierChestplate>() && legs.type == ModContent.ItemType<StoneSoldierLeggings>();
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new MiningSpeedEffect(0.10f));
			AddEffect<SpelunkerEffect>();
            AddEffect(new EmitLightFromPlayerEffect(1f, 0.95f, .8f));

            AddSetEffect<GoldRingEffect>();
            AddSetEffect<LuckyCoinEffect>();
            AddSetEffect<DiscountCardEffect>();
			AddSetEffect<StoneSoldierHelmetSetEffect>();
			AddSetEffect(new AttacksInflictBuffEffect(null, (BuffID.Midas, 600)));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MiningHelmet);
            recipe.AddIngredient(ModContent.ItemType<StoneShell>(), 6);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}