using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Weapons
{
    public class Executioner : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 25;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 124;
            Item.height = 124;
            Item.scale = 1.1f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
			Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 6, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<Executioner_Holdout>();  //put your Spear projectile name
            Item.shootSpeed = 5f;
        }
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Executioner");
            // Tooltip.SetDefault("");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 10);
            recipe.AddTile(TileID.Anvils); 
            recipe.Register();
        }
    }
}
