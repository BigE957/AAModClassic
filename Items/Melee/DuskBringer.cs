using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class DuskBringer : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 30;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 124;
            Item.height = 124;
            Item.scale = 1.1f;
            Item.maxStack = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 4.5f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
			Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 6, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = Mod.Find<ModProjectile>("DBP").Type;  //put your Spear projectile name
            Item.shootSpeed = 5f;
        }
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Dusk Bringer");
      // Tooltip.SetDefault("");
    }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MoltenLance", 1); 
			recipe.AddIngredient(null, "AncientPoker", 1);
			recipe.AddIngredient(null, "GrassSpear", 1);
			recipe.AddIngredient(ItemID.DarkLance , 1);
            recipe.AddTile(TileID.Anvils);   
            recipe.Register();
        }
    }
}
