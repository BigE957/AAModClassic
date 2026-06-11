using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Weapons
{
    public class BreakingDawn : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Breaking Dawn");
        }

		public override void SetDefaults()
		{
            
			Item.damage = 90;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 15;
            Item.shoot = ModContent.ProjectileType<BreakingDawn_MorningStar>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = 500000;
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 5);
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 15);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust;
            dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.StarDust>(), 0f, 0f, 46, default, 1.25f);
			dust.noGravity = true;
        }
	}
}
