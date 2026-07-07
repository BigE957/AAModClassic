using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tools
{
    public class FulguritePitchet : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fulgurite Pitchet");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 48;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.LightRed;
		    Item.pick = 200;
            Item.axe = 40;
            Item.tileBoost += 1;

            Item.damage = 60;
            Item.knockBack = 4;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 6;
            Item.useAnimation = 22;

            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useTurn = true;
            Item.autoReuse = true;
            
            Item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 18);
            recipe.AddIngredient(ItemID.SoulofFright, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}