using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class GaleOfWings : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 50;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 24;
            Item.height = 28;
            Item.useStyle = 5;        
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 7;
            Item.mana = 8;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.shoot = Mod.Find<ModProjectile>("Tornado").Type;
            Item.shootSpeed = 9f;    
        }   

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Gale of Wings");
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
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(null, "GoddessFeather", 10);
            recipe.AddTile(TileID.Bookcases);   
            recipe.Register();
        }
    }
}
