using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Crimson.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class FleshrendHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Fleshrend";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fleshrend Helm");
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 26;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 7;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<FleshrendChestplate>() && legs.type == ModContent.ItemType<FleshrendLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Melee) += .07f;

            AddSetEffect<CrimsonHelmetSetEffect>();
			AddSetEffect<FleshrendHelmetSetEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CrimsonHelmet, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 5);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}