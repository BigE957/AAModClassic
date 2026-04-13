using AAModClassic.___Content.Sky.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
    public class DragonStaff : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 50;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 60;
            Item.height = 60;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;     
            Item.noMelee = true;
            Item.knockBack = 6;        
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.mana = 5;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.DragonP>();  
            Item.shootSpeed = 13f;     
        }   

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Dragon Staff");
      // Tooltip.SetDefault("Shoots dragon scales.");
            Item.staff[Item.type] = true;
    }

		public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DragonSpirit>(), 20);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();
        }
    }
}
