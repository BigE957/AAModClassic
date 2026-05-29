using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Dungeon.___PreHardmode.Items.Weapons
{
    public class DeathlyLongbow : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deathly Longbow");
            // Tooltip.SetDefault("Replaces Bone Arrows with Flaming Skulls");
        }

        public override void SetDefaults()
        {
            Item.damage = 33; 
            Item.noMelee = true; //This makes sure the bow doesn't do melee damage
            Item.DamageType = DamageClass.Ranged; //This causes your bow to do ranged damage
            Item.width = 22; //Hitbox width
            Item.height = 64; //Hitbox height
            Item.useTime = 20; //How long it takes to use the weapon. If this is shorter than the useAnimation it will fire twice in one click.
            Item.useAnimation = 20;  //The animations time length
            Item.useStyle = ItemUseStyleID.Shoot; //The style in which the item gets used. 5 for bows.
            Item.shoot = ProjectileID.WoodenArrowFriendly; //Makes the bow shoot arrows
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 0; //The amount of knockback the item has
            Item.value = Item.sellPrice(0, 1, 8, 0);
            Item.rare = ItemRarityID.Orange; //The item's name color
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true; //if the Bow autoreuses or not
            Item.shootSpeed = 10f; //The arrows speed when shot
            Item.crit = 0; //Crit chance
        }
        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<BoneBow>(), 1);
                recipe.AddIngredient(ItemID.BeesKnees, 1);
                recipe.AddIngredient(ItemID.DemonBow, 1);
                recipe.AddIngredient(ItemID.MoltenFury, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<BoneBow>(), 1);
                recipe.AddIngredient(ItemID.BeesKnees, 1);
                recipe.AddIngredient(ItemID.TendonBow, 1);
                recipe.AddIngredient(ItemID.MoltenFury, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.BoneArrowFromMerchant)
            {
                Projectile p = Projectile.NewProjectileDirect(source, position, velocity, ProjectileID.Skull, damage, knockback, player.whoAmI);
                p.DamageType = DamageClass.Ranged;
            }
            else
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

            return false;
        }
    }
}