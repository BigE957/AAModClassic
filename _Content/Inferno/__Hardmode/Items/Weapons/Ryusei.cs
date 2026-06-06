using AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Terra.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Weapons
{
    public class Ryusei : BaseAAItem
    {
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.SolarEruption);

            Item.damage = 35; 
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */; 
            Item.width = 46; 
            Item.height = 66;    
            Item.knockBack = 7;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.shoot = ModContent.ProjectileType<Ryusei_Holdout>();
			Item.UseSound = SoundID.Item18;
            Item.channel = true;
        }

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Ryusei");
            // Tooltip.SetDefault(@"Ignites enemies on hit with flames and Dragonfire");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Yogan>());
            recipe.AddIngredient(ModContent.ItemType<HeroRelics>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
