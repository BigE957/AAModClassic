using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.___Content.Inferno.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class BlazingChestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Blazing Dao");
			/* Tooltip.SetDefault(@"2% increased Damage Resistance
2% increased Melee Damage
Forged in the flames of the blazing sun"); */
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 8;
		}

        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.02f;
            player.GetDamage(DamageClass.Melee) += 0.02f;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KindledChestplate>());
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ItemID.FossilOre, 8);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
	}
}
