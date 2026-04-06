using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class IchorSpear : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ichor Spear");
            // Tooltip.SetDefault("Inflicts Ichor to enemy on hit");
        }

        public override void SetDefaults()
        {
            Item.damage = 48;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 80;
            Item.height = 80;
            Item.scale = 1.1f;
            Item.maxStack = 1;
            Item.useTime = 24;
            Item.useAnimation = 18;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.autoReuse = true;
            Item.rare = ItemRarityID.Orange;
            Item.shootSpeed = 5f;
            Item.shoot = ModContent.ProjectileType<Projectiles.IchorSpear>();  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CrimtaneBar, 12);
			recipe.AddIngredient(ItemID.Ichor, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
    }
}
