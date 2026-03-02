using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
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
            Item.shoot = Mod.Find<ModProjectile>("MorningStar").Type;
            Item.shootSpeed = 10f;
            Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.knockBack = 3;
			Item.value = 500000;
			Item.UseSound = new LegacySoundStyle(2, 15, Terraria.Audio.SoundType.Sound);
			Item.autoReuse = true;
            Item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Stardust", 5);
            recipe.AddIngredient(null, "RadiumBar", 15);
            recipe.AddTile(null, "QuantumFusionAccelerator");
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
