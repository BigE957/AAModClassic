using AAModClassic._Content._Dev.TEMPliz.projs;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.TEMPliz
{
    public class CatsEyeRifleEX : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Silencer");
            /* Tooltip.SetDefault(@"Fires Shadow bolts
Doesn't require ammo
Cat's Eye Rifle EX"); */

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<ArchwitchStaff>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override void SetDefaults()
        {
            Item.damage = 1750; 
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 86; 
            Item.height = 22; 
            Item.useTime = 30; 
            Item.useAnimation = 30;  
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<CatsEye>();
            Item.knockBack = 12; 
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.autoReuse = true; 
            Item.shootSpeed = 25f; 
            Item.crit = 5;
            Item.expert = true; Item.expertOnly = true;
            Item.rare = ItemRarityID.Red;
        }

		
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CatsEyeRifle>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.Register(); 
        }
    }
}