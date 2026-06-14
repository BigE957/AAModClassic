using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Weapons
{
    public class SaltwaterSpear : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Saltwater Spear");		
		}

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.knockBack = 2.3f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 0, 40, 0);
            Item.rare = ItemRarityID.Green;
            Item.shootSpeed = 5f;
            Item.shoot = ModContent.ProjectileType<SaltwaterSpear_Holdout>();  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coral, 15);
            recipe.AddIngredient(ItemID.Starfish, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}