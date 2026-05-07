using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using AAModClassic.Items.Materials;
using AAModClassic._Content.Snow.___PreHardmode.Items.Weapons;

namespace AAModClassic._Content.Snow.__Hardmode.Items.Weapons
{
    public class AsgardianLance : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Asgardian Lance");		
		}

        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<AsgardianLance_Holdout>();  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<AsgardianLance_Proj>(), damage, knockback, Main.myPlayer);
            return true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RaiderLance>(), 1);
            recipe.AddIngredient(ModContent.ItemType<HeroShards>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
    }
}