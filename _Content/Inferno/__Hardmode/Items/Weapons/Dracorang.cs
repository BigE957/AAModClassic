using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Weapons
{
	public class Dracorang : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.LightDisc);
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.shootSpeed = 16f;
			Item.useTime = 20;
			Item.damage = 50;                            
			Item.value = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.knockBack = 4;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 20;
			Item.shoot = ModContent.ProjectileType<Dracorang_Proj>();
			Item.width = 22;
			Item.height = 32;
            Item.noMelee = true;
        }
		
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Dracorang_Proj>()] < 5)
                return true;

            return false;
        }
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dracorang");
			/* Tooltip.SetDefault(@"Leaves short living flame trail"); */
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<RadiantIncineriteBar>(), 3);
			recipe.AddIngredient(ItemID.LivingFireBlock, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
