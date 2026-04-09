using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class GrassSpear : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 132;
            Item.height = 132;
            Item.scale = 1.1f;
            Item.maxStack = 1;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.knockBack = 4.7f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
			Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 2, 40, 0);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<Projectiles.GSP>();  //put your Spear projectile name
            Item.shootSpeed = 5f;
        }
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Grass Spear");
          // Tooltip.SetDefault("");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();  
            recipe.AddIngredient(ItemID.Stinger, 4);
            recipe.AddIngredient(ItemID.JungleSpores, 4);
            recipe.AddTile(TileID.Anvils);   
            recipe.Register();
        }
    }
}
