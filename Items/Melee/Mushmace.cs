using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.Melee
{
    public class MushMace : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Mushmace");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.Blue;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.knockBack = 4f;
            Item.damage = 19;
            Item.noUseGraphic = true;
            Item.shoot = Mod.Find<ModProjectile>("MushMace").Type;
            Item.shootSpeed = 9;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddIngredient(null, "MushiumBar", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}