using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class FulguriteHelmetMelee : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Fulgurite";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulgurite Helmet");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 50000;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 22;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<FulguriteChestplate>() && legs.type == ModContent.ItemType<FulguriteLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Melee) += 0.12f;
            damageMap.GetCritChance(DamageClass.Melee) += 12;
            damageMap.GetAttackSpeed(DamageClass.Melee) += 0.10f;

            setDamageMap.GetAttackSpeed(DamageClass.Melee) += 0.20f;
			AddEffect(new MovementSpeedEffect(0.20f));
            AddSetEffect<FulguriteHelmetSetEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}