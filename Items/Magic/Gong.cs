using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
    public class Gong : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 54;
            Item.maxStack = 1;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
			Item.damage = 20;                        
            Item.DamageType = DamageClass.Magic;
			Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;        
            Item.noMelee = true;
            Item.knockBack = 4;
			Item.mana = 8;             
            Item.UseSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/GONG");
            Item.autoReuse = true;
            Item.shoot = ProjectileID.TopazBolt;
			Item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gong");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:Gold", 15);
            recipe.AddIngredient(ItemID.WhiteString);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddTile(TileID.Anvils);   
            recipe.Register();
        }

    }
}
