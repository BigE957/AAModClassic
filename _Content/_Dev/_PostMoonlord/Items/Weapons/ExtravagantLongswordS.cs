using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic._Content._Dev.___PreHardmode.Items.Materials;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class ExtravagantLongswordS : ExtravagantLongsword, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Extravagant Longsword");
            /* Tooltip.SetDefault(@"An Excellent choice.
-Big E); */
        }
        public override void SetDefaults()
		{
            base.SetDefaults();
			Item.shoot = ModContent.ProjectileType<ExtravagantLongswordS_BigE>();
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ExtravagantLongsword>());
            recipe.AddIngredient(ModContent.ItemType<ShinyCharm>());
            recipe.Register();
        }
    }
}
