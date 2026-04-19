using AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class DragonSpine : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon's Spine");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {

            Item.shoot = ModContent.ProjectileType<Projectiles.DragonSpine_Proj>();
            Item.shootSpeed = 9f;
            Item.damage = 18;
            Item.knockBack = 4f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = 40;
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>());
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
