using AAModClassic._Content.Hoard.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Weapons
{
    public class GoldDigger : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetDefaults()
        {
            Item.damage = 40;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;     
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.mana = 5;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GoldDigger_Bolt>();
            Item.shootSpeed = 13f;     
        }   

    public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gold Digger");
            /* Tooltip.SetDefault(@"Fires a projectile that, upon collision with a tile, creates a fountain of coins
Only 1 fountain may be active at once"); */
            Item.staff[Item.type] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("AAModClassic:GoldOre", 30);
            recipe.AddIngredient(ModContent.ItemType<StoneShell>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
