using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Terraria;
using Terraria.ModLoader;


namespace AAModClassic._Content.Void._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    [AutoloadEquipGlow(EquipType.Legs)]
    public class DoomsdayLeggings : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Doomsday";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomsday Assault Greaves");
			/* Tooltip.SetDefault(@"18% increased movement speed
120 increased mana
The power to destroy entire planets rests in this armor"); */

		}

        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 18;
			Item.value = 3000000;
			Item.defense = 28;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.18f;
            player.statManaMax2 += 120;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .18f;
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 18);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}