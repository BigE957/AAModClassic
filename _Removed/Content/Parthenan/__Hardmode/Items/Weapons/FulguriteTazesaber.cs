using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Weapons
{
	public class FulguriteTazesaber : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fulgurite Tazesaber");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
		{
			Item.damage = 90;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 46;
			Item.height = 48;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = 54000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PurpleTorch);
		}

		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 18);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
