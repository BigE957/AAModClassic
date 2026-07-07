using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.___PreHardmode.Items.Weapons
{
    public class FlamingGelWand : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetDefaults()
        {

            Item.damage = 9;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 26;
            Item.height = 38;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.noMelee = true;
            Item.knockBack = 2;        
            Item.value = 1000;
            Item.rare = ItemRarityID.Green;
            Item.mana = 5;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<FlamingGelWand_FlamingGel>();  
            Item.shootSpeed = 7f;     
        }   

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Flaming Gel Wand");
      // Tooltip.SetDefault("It shoots flaming gel");
            Item.staff[Item.type] = true;
        }

		public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WandofSparking, 1);   //you need 10 Wood
			recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.Register();
        }
    }
}
