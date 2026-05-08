using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items.Weapons
{
    public class Winterbreak : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Winterbreak");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {

            Item.shoot = ModContent.ProjectileType<Winterbreak_Proj>();
            Item.shootSpeed = 10f;
            Item.damage = 32;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = 60;
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ModContent.ItemType<SnowMana>());
            recipe.AddIngredient(ItemID.BorealWood, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
