using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Projectiles;
using AAModClassic;

namespace AAModClassic.Items.Ranged
{
    public class TheVulcano : BaseAAItem
    {
        
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 58;
            Item.height = 24;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Vulcan>();
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = false;
            Item.shootSpeed = 10f;
            Item.value = 10000;
            Item.useAmmo = AmmoID.Gel;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Vulcan");
            // Tooltip.SetDefault("Turns Gel into an explosive lob of magma");
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

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 40);
			recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
    }
}
