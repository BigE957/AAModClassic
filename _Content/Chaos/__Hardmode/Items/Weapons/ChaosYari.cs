using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using AAModClassic.Items.Materials;
using AAModClassic._Content.Snow.__Hardmode.Items.Weapons;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Weapons
{
    public class ChaosYari : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Yari");		
		}

        public override void SetDefaults()
        {
            Item.damage = 130;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.shootSpeed = 11f;
            Item.shoot = ModContent.ProjectileType<ChaosYari_Holdout>();  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AsgardianLance>(), 1);
            recipe.AddIngredient(ItemID.Gungnir, 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
    }
}