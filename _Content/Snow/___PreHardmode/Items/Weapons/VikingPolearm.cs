using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;

namespace AAModClassic._Content.Snow.___PreHardmode.Items.Weapons
{
    public class VikingPolearm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Viking Polearm");		
		}

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
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
            Item.shootSpeed = 2.5f;
            Item.shoot = ModContent.ProjectileType<VikingPolearm_Holdout>();  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SnowMana>(), 8);
            recipe.AddIngredient(ItemID.IceBlock, 40);
            recipe.AddIngredient(ItemID.BorealWood, 12);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}