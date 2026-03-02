using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ChaosChainEX : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Perfect Chaos Chain");
            /* Tooltip.SetDefault(@"Fires a spinning blade that shreds enemies
Chaos Chain EX"); */
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.useStyle = 5;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.knockBack = 1f;
            Item.width = 30;
            Item.height = 10;
            Item.damage = 275;
            Item.shoot = Mod.Find<ModProjectile>("ChaosChainEX").Type;
            Item.shootSpeed = 18f;
            Item.UseSound = SoundID.Item116;
            Item.rare = 9;
            Item.expert = true; Item.expertOnly = true;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "ChaosChain", 1);
            recipe.AddIngredient(null, "EXSoul",1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}