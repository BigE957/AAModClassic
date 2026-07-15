using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class FulguriteHelmetMage : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Fulgurite";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulgurite Helm");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 50000;
			Item.rare = ItemRarityID.Pink;
			Item.defense = 4;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == ModContent.ItemType<FulguriteChestplate>() && legs.type == ModContent.ItemType<FulguriteLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Magic) += 0.14f;
            damageMap.GetCritChance(DamageClass.Magic) += 14;
			AddEffect(new MaxManaEffect(120));

            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddSetEffect(new ManaCostMultiplierEffect(0.80f));
            else
                AddSetEffect(new ManaCostEffect(0.30f));
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