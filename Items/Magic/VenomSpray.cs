using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class VenomSpray : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 12;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 24;
            Item.height = 28;
            Item.useStyle = 5;        
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = 1;
            Item.mana = 5;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.shoot = Mod.Find<ModProjectile>("Venom").Type;
            Item.shootSpeed = 9f;    
        }   

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Venom Spray");
          // Tooltip.SetDefault("");
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), Item.shoot, Item.damage, Item.knockBack, Main.myPlayer);
            return false;
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(null, "AbyssiumBar", 10);
            recipe.AddTile(TileID.Bookcases);   
            recipe.Register();
        }
    }
}
