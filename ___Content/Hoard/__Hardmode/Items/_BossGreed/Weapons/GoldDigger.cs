using AAModClassic.___Content.Hoard.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Hoard.__Hardmode.Items._BossGreed.Weapons
{
    public class GoldDigger : BaseAAItem
    {
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
            recipe.AddIngredient(ItemID.GoldOre, 30);
            recipe.AddIngredient(ModContent.ItemType<StoneShell>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.PlatinumOre, 30);
            recipe.AddIngredient(ModContent.ItemType<StoneShell>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
