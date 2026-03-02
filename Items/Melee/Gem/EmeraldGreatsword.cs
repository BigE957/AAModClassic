using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class EmeraldGreatsword : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 32;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 58;              
            Item.height = 60;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;        
            Item.knockBack = 5;      
            Item.value = 3000;        
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = false;   
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.GemShot.EmeraldShot>();
            Item.shootSpeed = 9f;
        }

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Emerald Greatsword");
          // Tooltip.SetDefault("");
        }
        static int shoot = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shoot++;
            if (shoot % 3 != 0) return false;

            shoot = 0;
            return true;
        }
        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "EmeraldSaber", 1);
            recipe.AddIngredient(ItemID.LargeEmerald, 1);		
            recipe.AddTile(TileID.Anvils);   
            recipe.Register();

        }
    }
}
