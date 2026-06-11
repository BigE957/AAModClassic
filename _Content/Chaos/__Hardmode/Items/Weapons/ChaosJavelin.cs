using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic._Content.Desert.__Hardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Weapons
{
    public class ChaosJavelin : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Javelin");
            // Tooltip.SetDefault("Explodes on contact");
        }

        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<ChaosJavelin_Proj>();
            Item.shootSpeed = 15f;
            Item.damage = 105;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.width = 30;
            Item.height = 30;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<PrimevalJavelin>());
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
