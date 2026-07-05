using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class VikingChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Viking";
		public static int counter = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Viking Platemail");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice (0, 0, 5, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 9;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == ModContent.ItemType<VikingHelmet>() && legs.type == ModContent.ItemType<VikingLeggings>();
		}

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += 0.07f;

			AddSetEffect(new EnduranceEffect(0.04f));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 14);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}