using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TerraShortsword : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Broadsword");
        }
		public override void SetDefaults()
		{
            
			Item.damage = 36;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 30;
			Item.height = 36;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Terra, 0f, 0f, 46, default, .5f)];
                dust.noGravity = true;
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Bone, 10);
			recipe.AddIngredient(ItemID.Stinger, 8);
			recipe.AddIngredient(ItemID.JungleSpores, 6);
			recipe.AddIngredient(ItemID.CrimtaneBar, 10);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}
