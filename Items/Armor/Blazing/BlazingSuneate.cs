using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Blazing
{
    [AutoloadEquip(EquipType.Legs)]
	public class BlazingSuneate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blazing Suneate");
            /* Tooltip.SetDefault(@"1% increased Damage Resistance
2% increased Melee Damage
Forged in the flames of the blazing sun"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 8;
		}
		
		public override void UpdateEquip(Player player)
        {
            player.endurance += 0.01f;
            player.GetDamage(DamageClass.Melee) += 0.02f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KindledSuneate>());
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ItemID.FossilOre, 6);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.Doomite>(), 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
	}
}