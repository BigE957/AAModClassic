using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons
{
	public class BlazePike : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Magic";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blaze Pike");
			// Tooltip.SetDefault("Very hot to touch");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 3;
			Item.width = 56;
			Item.height = 56;
			Item.useTime = 27;
			Item.useAnimation = 27;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.DD2FlameBurstTowerT1Shot;
			Item.shootSpeed = 6f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile p = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            p.DamageType = DamageClass.Magic;
            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 10);
			recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}