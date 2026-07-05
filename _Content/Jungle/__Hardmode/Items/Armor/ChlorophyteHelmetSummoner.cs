using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Jungle.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class ChlorophyteHelmetSummoner : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Chlorophyte";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chlorophyte Face Paint");
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;

        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 60000;
			Item.rare = ItemRarityID.Lime;
			Item.defense = 5;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.ChlorophytePlateMail && legs.type == ItemID.ChlorophyteGreaves;
		}

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Summon) += .38f;
			AddEffect(new MaxManaEffect(80));

			AddSetEffect(new MaxMinionSlotEffect(6));
			AddSetEffect<ChlorophyteHelmetSetEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 6);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.BewitchingTable);
			recipe.Register();
		}
	}
}