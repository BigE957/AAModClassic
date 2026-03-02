using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class Axis : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Axis");
			// Tooltip.SetDefault("Enemies struck by this spear will be surrounded by snowflakes\nNorth Pole EX");
		}
		
        public override void SetDefaults()
        {
            Item.damage = 250;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 64;
            Item.height = 64;
            Item.shoot = Mod.Find<ModProjectile>("Axis").Type;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 30;
			Item.useTime = 30;
			Item.shootSpeed = 4.75f;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
			Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Lime;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.NorthPole);
            recipe.AddIngredient(Mod.Find<ModItem>("EXSoul").Type);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
		
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        
    }
}
