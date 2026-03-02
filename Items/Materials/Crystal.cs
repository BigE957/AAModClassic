using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Crystal : BaseAAItem
    {
        public override string Texture => "AAMod/Items/Materials/Crystal";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Biome Prism");
            // Tooltip.SetDefault("A magical prism that can be enhanced with the power of a biome.");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            
            
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.value = 10000;
            Item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.COLOR_WHITEFADE1;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.COLOR_WHITEFADE1.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Prism", 5);
            recipe.AddTile(null, "TerraPrism");
            recipe.Register();
        }
    }

    public class TerraCrystal : BaseAAItem
    {
        public override string Texture => "AAMod/Items/Materials/Crystal";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Prism");
            // Tooltip.SetDefault("Imbued with the unified harmony of the land of Terraria");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.value = 10000;
            Item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.TerraGlow;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.TerraGlow.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "TerraShard", 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "DragonSpirit", 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.Register();
            }
        }
    }

    public class ChaosCrystal : BaseAAItem
    {
        public override string Texture => "AAMod/Items/Materials/Crystal";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Prism");
            // Tooltip.SetDefault("Imbued with the discordian flames of chaos");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.value = 10000;
            Item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Shen3;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.Shen3.ToVector3() * 0.55f * Main.essScale);
        }
    }
}