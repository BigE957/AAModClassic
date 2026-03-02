using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class DynaskullJavelin : BaseAAItem
    {

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dynaskull Javelin");
            // Tooltip.SetDefault("If stuck in an enemy and that enemy dies, releases a homing bolt of Dyna-Energy");
        }

        public override void SetDefaults()
        {
            Item.shoot = Mod.Find<ModProjectile>("DynaskullJavelin").Type;
            Item.shootSpeed = 12f;
            Item.damage = 40;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.width = 30;
            Item.height = 30;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.BoneJavelin, 500);
            recipe.AddIngredient(null, "DragonSpine", 500);
            recipe.AddIngredient(null, "Winterbreak", 500);
            recipe.AddIngredient(null, "Incapacitator", 500);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
