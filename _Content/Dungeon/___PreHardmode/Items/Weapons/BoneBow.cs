using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Dungeon.___PreHardmode.Items.Weapons
{
    public class BoneBow : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bone Bow");
            // Tooltip.SetDefault("Replaces Wooden Arrows with Bone Arrows");
        }

        public override void SetDefaults()
        {
            Item.damage = 17; 
            Item.noMelee = true; //This makes sure the bow doesn't do melee damage
            Item.DamageType = DamageClass.Ranged; //This causes your bow to do ranged damage
            Item.width = 20; //Hitbox width
            Item.height = 64; //Hitbox height
            Item.useTime = 25; //How long it takes to use the weapon. If this is shorter than the useAnimation it will fire twice in one click.
            Item.useAnimation = 25;  //The animations time length
            Item.useStyle = ItemUseStyleID.Shoot; //The style in which the item gets used. 5 for bows.
            Item.shoot = ProjectileID.WoodenArrowFriendly; //Makes the bow shoot arrows
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 0; //The amount of knockback the item has
            Item.value = Item.sellPrice(0, 0, 8, 0);
            Item.rare = ItemRarityID.Orange; //The item's name color
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = false; //if the Bow autoreuses or not
            Item.shootSpeed = 5f; //The arrows speed when shot
            Item.crit = 0; //Crit chance
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(ItemID.Cobweb, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        /*public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }*/
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ProjectileID.BoneArrow;
            }
            return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
        }
    }
}