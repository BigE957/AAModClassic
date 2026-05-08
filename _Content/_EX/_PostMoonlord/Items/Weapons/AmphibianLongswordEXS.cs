using AAModClassic._Content._Dev.___PreHardmode.Items.Materials;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class AmphibianLongswordEXS : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Amphibious Greatblade");
            // Tooltip.SetDefault(@"Amphibious Longsword EX");
        }
		public override void SetDefaults()
		{
			Item.damage = 350;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 64;
			Item.height = 64;
            Item.useTime = 30;
			Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 7;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.AmphibiousProjectileEXS>();
            Item.shootSpeed = 18f;
            Item.expert = true; Item.expertOnly = true;
		}
        
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //target.AddBuff(BuffID.Wet, 1000);
        }
        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<AmphibianLongswordEX>());
                recipe.AddIngredient(ModContent.ItemType<ShinyCharm>());
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<AmphibianLongswordS>());
                recipe.AddIngredient(ModContent.ItemType<EXSoul>());
                recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
                recipe.Register();
            }
        }
    }
}
