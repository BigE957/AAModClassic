using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee.Gem   //where is located
{
    public class SapphireGreatsword : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 29;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 58;              
            Item.height = 60;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 5;      
            Item.value = 3000;        
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = false;   
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.GemShot.SapphireShot>();
            Item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Sapphire Greatsword");
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
            recipe.AddIngredient(ModContent.ItemType<SapphireSaber>(), 1);
            recipe.AddIngredient(ItemID.LargeSapphire, 1);			
            recipe.AddTile(TileID.Anvils);   
            recipe.Register();

        }
    }
}
