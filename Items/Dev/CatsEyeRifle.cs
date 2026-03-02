using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Dev
{
    public class CatsEyeRifle : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cat's Eye Rifle");
            /* Tooltip.SetDefault(@"Fires Shadow bolts
Doesn't require ammo
'QUICK HIDE THE LOLI STASH'
-Liz"); */
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
            Item.damage = 430;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged; 
            Item.width = 72; 
            Item.height = 22;
            Item.useTime = 30; 
            Item.useAnimation = 30; 
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = Mod.Find<ModProjectile>("CatsEye").Type;
            Item.knockBack = 12;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Cyan; 
            Item.UseSound = new LegacySoundStyle(2, 40, Terraria.Audio.SoundType.Sound);
            Item.autoReuse = false; 
            Item.shootSpeed = 20f;
            Item.crit = 0;

			glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
			glowmaskDrawType = GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun
			glowmaskDrawColor = Color.White; //glowmask draw color			
			customNameColor = new Color(121, 21, 214); //custom name color				
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ArchwitchWand");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}