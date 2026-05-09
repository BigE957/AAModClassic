using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class FreedomStar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Freedom Star");
            /* Tooltip.SetDefault(@"Tails' trusty blaster.
Hold the use button to charge, and then release a powerful Charged Shot!
Kept you waiting, huh?
Tails
Mobian Buster EX"); */
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 34;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 250;  
            Item.shoot = ModContent.ProjectileType<FreedomStar_Holdout>();
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.sellPrice(3, 0, 0, 0);
            Item.noMelee = true;
			Item.rare = ItemRarityID.Purple;
			Item.shootSpeed = 12f;
			Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<MobianBuster>());
                recipe.AddIngredient(ModContent.ItemType<EXSoul>());
                recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
                recipe.Register();
            }
        }
    }
}

// pls nerf
