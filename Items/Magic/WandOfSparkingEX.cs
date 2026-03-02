using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
    public class WandOfSparkingEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starburst Wand");
            /* Tooltip.SetDefault(@"Hold to charge the wand
Wand of Sparking EX"); */
        }

        public override void SetDefaults()
        {
            Item.mana = 8;
            Item.width = 74;
            Item.height = 34;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 300;
            Item.shoot = Mod.Find<ModProjectile>("SparkWand").Type;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.sellPrice(3, 0, 0, 0);
            Item.noMelee = true;
			Item.rare = ItemRarityID.Purple;
			Item.shootSpeed = 12f;
			Item.noUseGraphic = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.WandofSparking);
                recipe.AddIngredient(null, "EXSoul");
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.Register();
            }
        }
    }
}
