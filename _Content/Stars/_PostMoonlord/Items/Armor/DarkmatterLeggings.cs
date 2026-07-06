using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    [AutoloadEquipGlow(EquipType.Legs)]
    public class DarkmatterLeggings : EquipAbstract, ILocalizedModType, ICustomEquipGlow
	{
        public new string LocalizationCategory => "Items.Armor.Darkmatter";
        public Color Color => AAColor.Nightcrawler;

        public bool Condition(Player p) => !Main.dayTime && p.GetModPlayer<AAPlayer>().DarkmatterSet;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Greaves");
			/* Tooltip.SetDefault(@"'Dark, yet still barely visible'"); */

		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 300000;
			Item.defense = 24;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
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

        public override void RegisterEquipEffects()
        {
            AddEffect(new MovementSpeedEffect(0.24f));
            AddEffect(new MaxRunSpeedEffect(0.24f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 27);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 15);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
}