using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
	public class AtlanteanTrident : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Atlantean Trident");
			// Tooltip.SetDefault("Fires off a tri-shot of water bolts");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.width = 68;
			Item.height = 68;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 500000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item21;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.WaterBolt;
			Item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Main.myPlayer);
            }
            return false;
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("OceanTrident").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("BlazePike").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("SandLamp").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("NeutronStaff").Type);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
			
			recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("OceanTrident").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("SludgeShot").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("Sickle").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("NeutronStaff").Type);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}