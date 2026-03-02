using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Dev
{
    public class CatsEyeRifleEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Silencer");
            /* Tooltip.SetDefault(@"Fires Shadow bolts
Doesn't require ammo
Cat's Eye Rifle EX"); */
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
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
            Item.shoot = Mod.Find<ModProjectile>("CatsEye").Type;
            Item.knockBack = 12; 
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.autoReuse = true; 
            Item.shootSpeed = 25f; 
            Item.crit = 5;
            Item.expert = true; Item.expertOnly = true;
            Item.rare = ItemRarityID.Red;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

		
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CatsEyeRifle");
            recipe.AddIngredient(null, "EXSoul");
            recipe.Register(); 
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "ArchwitchStaff");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}