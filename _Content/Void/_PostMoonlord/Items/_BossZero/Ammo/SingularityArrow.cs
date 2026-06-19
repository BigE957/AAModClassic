using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Ammo
{
    public class SingularityArrow : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Ammo";
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Singularity Arrow");
            /* Tooltip.SetDefault(@"The only thing faster than light is the void that devours it
Non-consumable"); */
        }

        public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 40;
            Item.consumable = false;
			Item.knockBack = 7f;
			Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
			Item.shoot = ModContent.ProjectileType<SingularityArrow_Proj>();
			Item.ammo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ItemID.MoonlordArrow, 999);
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}
	}
}
