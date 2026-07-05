using AAModClassic._Content.Hallow.__Hardmode.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Jungle.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class ChlorophyteHelmetSummoner2 : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Chlorophyte";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Face Paint");
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 60000;
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 6;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.ChlorophytePlateMail && legs.type == ItemID.ChlorophyteGreaves;
		}

        public override void RegisterEquipStats()
        {
			damageMap.GetDamage(DamageClass.Summon) += .42f;
			AddEffect(new MaxManaEffect(120));

			AddSetEffect(new MaxMinionSlotEffect(7));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HallowedHelmetSummoner>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChlorophyteHelmetSummoner>(), 1);
            recipe.AddTile(TileID.BewitchingTable);
			recipe.Register();
		}
	}
}