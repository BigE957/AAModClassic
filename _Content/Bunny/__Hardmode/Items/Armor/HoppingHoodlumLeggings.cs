using AAModClassic._Content.Bunny.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class HoppingHoodlumLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.HoppingHoodlum";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hopping Hoodlum Paws");
            /* Tooltip.SetDefault(@"'Hopping Mad'"); */
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.defense = 17;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetCritChance(DamageClass.Melee) += 9;
            AddEffect(new MovementSpeedEffect(0.10f));
            AddEffect(new MaxMinionSlotEffect(1));
            AddEffect(new AggroEffect(2));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RajahPelt>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}