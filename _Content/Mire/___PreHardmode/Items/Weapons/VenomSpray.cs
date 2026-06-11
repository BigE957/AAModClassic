using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Weapons
{
    public class VenomSpray : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 12;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 24;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Shoot;        
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.Blue;
            Item.mana = 5;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.shoot = ModContent.ProjectileType<VenomSpray_Venom>();
            Item.shootSpeed = 9f;    
        }   

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Venom Spray");
          // Tooltip.SetDefault("");
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, velocity, Item.shoot, Item.damage, Item.knockBack, Main.myPlayer);
            return false;
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 10);
            recipe.AddTile(TileID.Bookcases);   
            recipe.Register();
        }
    }
}
