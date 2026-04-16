using AAModClassic.___Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class Void : BaseAAItem
    {

        public override void SetDefaults()
        {
			Item.useTime = 25;
            Item.CloneDefaults(ItemID.Terrarian);
            Item.damage = 190;                            
            Item.value = 1000000;
            Item.rare = ItemRarityID.Cyan;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.rare = ItemRarityID.Purple;
            Item.shoot = ModContent.ProjectileType<Projectiles.Void>();  
		}

        public override void SetStaticDefaults()
        {
             // DisplayName.SetDefault("Void");
            // Tooltip.SetDefault("Made out of pure Dark Matter");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }

    }
}
