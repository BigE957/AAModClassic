using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class BlazingLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Blazing";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blazing Suneate");
            /* Tooltip.SetDefault(@"'Forged in the flames of the blazing sun'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 8;
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Melee) += 0.02f;
            AddEffect(new EnduranceEffect(0.01f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KindledLeggings>());
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 12);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
	}
}