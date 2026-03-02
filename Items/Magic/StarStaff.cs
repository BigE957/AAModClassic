using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class StarStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Star Staff");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 175;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 25;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 5;
            Item.useAnimation = 16;
            Item.reuseDelay = 16;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 100000;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Star1").Type;
            Item.shootSpeed = 9f;
            Item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Stardust", 5);
            recipe.AddIngredient(null, "RadiumBar", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}