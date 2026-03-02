using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class AbyssalTwilight : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Twilight");
			// Tooltip.SetDefault("The Eternal Dusk Beckons");
		}

		public override void SetDefaults()
		{
			Item.damage = 19;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 14;
			Item.useAnimation = 14;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod, "ExilesKatana", 1);
			recipe.AddIngredient(Mod, "OceanRazor", 1);
			recipe.AddIngredient(Mod, "DoomiteSaber", 1);
			recipe.AddIngredient(Mod, "IceLongsword", 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 46, default, 1.381579f)];
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 400);
        }
	}
}
