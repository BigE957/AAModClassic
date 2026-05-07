using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Weapons
{
    public class MoltenLance : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 30;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 112;
            Item.height = 112;
            Item.scale = 1.1f;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.knockBack = 4.4f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
			Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 2, 40, 0);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<MoltenLance_Holdout>();  //put your Spear projectile name
            Item.shootSpeed = 5f;
        }
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Molten Lance");
            // Tooltip.SetDefault("Makes instant barbeque shish kebabs!");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 12);   
            recipe.AddTile(TileID.Anvils);   
            recipe.Register();
        }
    }
}
