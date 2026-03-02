using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class M79 : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("M79");
			// Tooltip.SetDefault("Uses M79 Rounds as ammo\n33% chance not to consume ammo");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.useAnimation = 65;
			Item.useTime = 65;
			Item.width = 68;
			Item.height = 24;
			Item.shoot = Mod.Find<ModProjectile>("M79P").Type;
			Item.UseSound = SoundID.Item61;
			Item.damage = 180;
			Item.shootSpeed = 11f;
			Item.noMelee = true;
			Item.value = 50000;
			Item.knockBack = 6f;
			Item.rare = ItemRarityID.Yellow;
			Item.DamageType = DamageClass.Ranged;
			Item.useAmmo = Mod.Find<ModItem>("M79Round").Type;
		}
		
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
		return Main.rand.NextFloat() >= .33;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-18, 0);
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("M79Parts").Type);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
