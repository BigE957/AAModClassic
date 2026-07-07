using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class MushiumHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Mushium";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushium Hat");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = 90;
			Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.defense = 3;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MushiumChestplate>() && legs.type == ModContent.ItemType<MushiumLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            AddEffect(new LifeRegenEffect(1));

			AddSetEffect<PhilosophersStoneEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MushiumBar>(), 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}