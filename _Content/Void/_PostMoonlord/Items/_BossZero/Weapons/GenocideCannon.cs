using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{

    public class GenocideCannon : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Genocide Cannon");
            /* Tooltip.SetDefault(@"Fires highly explosive Rockets
25% chance to fire a glitched rocket that explodes into frag rockets"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 400;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 66;
            Item.height = 28;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 10f;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 24f;
            Item.shoot = ModContent.ProjectileType<GenocideCannon_VoidRocket>();
            Item.useAmmo = AmmoID.Rocket;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, 0);
		}
		
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            type = Main.rand.NextBool(4) ? ModContent.ProjectileType<GenocideCannon_R0CKET>() : ModContent.ProjectileType<GenocideCannon_VoidRocket>();
            Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddIngredient(ItemID.RocketLauncher, 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}