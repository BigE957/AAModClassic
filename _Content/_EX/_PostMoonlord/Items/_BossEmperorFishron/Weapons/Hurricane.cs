using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Projectiles;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;

namespace AAModClassic._Content._EX._PostMoonlord.Items._BossEmperorFishron.Weapons
{
    public class Hurricane : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hurricane");
			/* Tooltip.SetDefault(@"Shoots 2 waves of 6 arrows
You have a chance to shoot Oceanic Arrow
66% chance not to consume arrow
Tsunami EX"); */
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 10;
            Item.width = 30;
            Item.height = 62;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.UseSound = SoundID.Item5;
            Item.damage = 100;
            Item.shootSpeed = 13f;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Yellow;
            Item.noMelee = true;
            Item.value = 200000;
            Item.DamageType = DamageClass.Ranged;
			Item.autoReuse = true;
        }
		
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .66;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float num121 = 0.314159274f;
			int num122 = 5;
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
			float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
			Vector2 vector14 = velocity;
			vector14.Normalize();
			vector14 *= 40f;
			bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
			int arrowtype = type;
			for (int num123 = 0; num123 < num122; num123++)
			{
				float num124 = num123 - (num122 - 1f) / 2f;
				Vector2 vector15 = vector14.RotatedBy(num121 * num124, default);
				if (!flag11)
				{
					vector15 -= vector14;
				}
				if (Main.rand.NextBool(8))
				{
					type = ModContent.ProjectileType<OceanicArrow>();
				}
				else
				{
					type = arrowtype;
				}
				int num125 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X + vector15.X, vector2.Y + vector15.Y, num82, num83, type, damage, knockback, player.whoAmI);
				Main.projectile[num125].noDropItem = true;
			}
			return false;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Tsunami);
			recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}