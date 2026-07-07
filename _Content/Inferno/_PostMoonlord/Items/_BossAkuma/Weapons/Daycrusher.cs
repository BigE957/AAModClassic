using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class Daycrusher : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Daycrusher");
            /* Tooltip.SetDefault(@"Slams into foes with the force of a solar mass
Inflicts Daybroken"); */
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 44;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.reuseDelay = 10;
            Item.knockBack = 7.5F;
            Item.damage = 200;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Daycrusher_Holdout>();
            Item.shootSpeed = 20F;
            Item.UseSound = SoundID.Item20;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.channel = true;
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ItemID.Flairon, 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}