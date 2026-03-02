using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss.Zero
{
    public class RiftShredder : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rift Shredder");
			// Tooltip.SetDefault("Shoots void stars that shred through reality itself");
        }

		public override void SetDefaults()
		{
            
			Item.damage = 190;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 94;
			Item.height = 70;
			Item.useTime = 22;
            Item.shoot = Mod.Find<ModProjectile>("Rift").Type;
            Item.shootSpeed = 10f;
            Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = new LegacySoundStyle(2, 15, Terraria.Audio.SoundType.Sound);
			Item.autoReuse = true;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(null, "BreakingDawn");
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust;
            dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 46, default, 1.25f);
			dust.noGravity = true;
        }
	}
}
