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

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class VoidStar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Star");
            // Tooltip.SetDefault("Fires a dark, spinning vortex that homes in on enemies");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 60;
            Item.useTime = 60;
            Item.shootSpeed = 10f;
            Item.knockBack = 0f;
            Item.width = 30;
            Item.height = 26;
            Item.damage = 700;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<VoidStar_StarVortex>();
            Item.mana = 18;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddIngredient(ItemID.NebulaArcanum);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
