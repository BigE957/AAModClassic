using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Tools
{
    public class DarkmatterPitchet : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Darkmatter Pitchet");
        }


        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 54;
		    Item.pick = 235;
            Item.axe = 50;
            Item.tileBoost += 4;
            Item.damage = 60;
            Item.knockBack = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 5;
            Item.useAnimation = 19;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 5);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}