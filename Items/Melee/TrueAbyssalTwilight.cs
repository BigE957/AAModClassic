using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueAbyssalTwilight : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dread Twilight");
			// Tooltip.SetDefault("The rising moon incarnate");
		}
		public override void SetDefaults()
		{
			Item.damage = 75;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 76;
			Item.height = 76;
			Item.useTime = 17;
			Item.useAnimation = 17;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item19;
			Item.autoReuse = false;
			Item.shoot = Mod.Find<ModProjectile>("TrueAbyssalTwilightShot").Type;
            Item.shootSpeed = 10f;
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

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(Mod, "AbyssalTwilight", 1);
                recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 500);
        }
	}
}
