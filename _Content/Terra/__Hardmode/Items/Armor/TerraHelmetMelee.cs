using AAModClassic._Content.Corruption.___PreHardmode.Items.Armor;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Armor;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class TerraHelmetMelee : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Terra";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Helm");
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 26;
			Item.value = 90000;
			Item.rare = ItemRarityID.Lime;
			Item.defense = 30;
		}
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<TerraChestplate>() && legs.type == ModContent.ItemType<TerraLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Melee) += .22f;
            damageMap.GetAttackSpeed(DamageClass.Melee) += 0.09f;

            setDamageMap.GetAttackSpeed(DamageClass.Melee) += 0.28f;
			AddSetEffect<TerraHelmetMeleeSetEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe;
			recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<NightsHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<FleshrendHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}