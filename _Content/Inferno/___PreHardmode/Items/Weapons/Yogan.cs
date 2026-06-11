using AAModClassic._Content.Snow.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons
{
    public class Yogan : BaseAAItem
    {
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.Sunfury);

            Item.damage = 24; 
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */; 
            Item.width = 46; 
            Item.height = 66;    
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.shoot = ModContent.ProjectileType<Yogan_Holdout>();
			Item.UseSound = SoundID.Item18;
            Item.channel = true;
        }

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Yogan");
            // Tooltip.SetDefault(@"Ignites enemies on hit");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
            base.SetStaticDefaults();
        }
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyrosphere>());
            recipe.AddIngredient(ModContent.ItemType<GlacierBreaker>());
            recipe.AddIngredient(ItemID.BlueMoon);
			recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
			recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyrosphere>());
            recipe.AddIngredient(ModContent.ItemType<GlacierBreaker>());
            recipe.AddIngredient(ItemID.BlueMoon);
			recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
