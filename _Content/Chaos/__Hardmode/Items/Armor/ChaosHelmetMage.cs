using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosHelmetMage : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Chaos";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Chaos Mask");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 18;
        }

        public override void RegisterEquipStats()
        {
			damageMap.GetDamage(DamageClass.Magic) += 0.20f;
			damageMap.GetCritChance(DamageClass.Magic) += 20;
            AddEffect(new ManaCostEffect(-0.3f));
			AddEffect(new MaxManaEffect(80));
			AddEffect<GillsEffect>();

            AddSetEffect<ChaosHelmetMageSetEffect>();
            AddSetEffect<FlipperEffect>();
            AddSetEffect<IgnoreWaterEffect>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ChaosChestplate>() && legs.type == ModContent.ItemType<ChaosLeggings>();
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<AtlanteanHelmet>());
			recipe.AddIngredient(ModContent.ItemType<ChaosPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}