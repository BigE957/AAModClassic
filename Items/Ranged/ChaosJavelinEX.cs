using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class ChaosJavelinEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Perfect Chaos Javelin");
            /* Tooltip.SetDefault(@"Explodes on contact
Chaos Javelin EX"); */
        }

        public override void SetDefaults()
        {
            Item.shoot = Mod.Find<ModProjectile>("ChaosJavelinEX").Type;
            Item.shootSpeed = 17f;
            Item.damage = 400;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.width = 30;
            Item.height = 30;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(5, 0, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "ChaosJavelin");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
