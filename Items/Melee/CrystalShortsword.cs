using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class CrystalShortsword : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 24;
            Item.useAnimation = 28;     
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.knockBack = 1;
            Item.value = 1000;        
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Harmony Shortsword");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "TerraShard", 10);
            recipe.AddTile(TileID.Anvils);  
            recipe.Register();
        }
    }
}
