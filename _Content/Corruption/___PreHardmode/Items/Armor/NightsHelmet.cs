using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Corruption.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class NightsHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Nights";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Night's Helm");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 28;
			Item.value = 90000;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 6;
		}
		

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<NightsChestplate>() && legs.type == ModContent.ItemType<NightsLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetAttackSpeed(DamageClass.Melee) += 0.09f;

			AddSetEffect(new MovementSpeedEffect(0.22f));
			AddSetEffect<PanicNecklaceEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ShadowHelmet, 1);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.AddIngredient(ItemID.Bone, 5);
			recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 5);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}