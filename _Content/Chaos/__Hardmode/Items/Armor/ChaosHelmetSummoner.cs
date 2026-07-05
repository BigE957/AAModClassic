using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosHelmetSummoner : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Chaos";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Chaos Visor");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 15;
        }
		
        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.3f;

            AddSetEffect(new MaxMinionSlotEffect(4));
            AddSetEffect<ChaosHelmetSummonerSetEffect>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ChaosChestplate>() && legs.type == ModContent.ItemType<ChaosLeggings>();
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DoomiteHelmet>());
			recipe.AddIngredient(ModContent.ItemType<ChaosPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}