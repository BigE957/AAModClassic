using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Weapons;
using AAModClassic._Content.Hoard._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Weapons
{
    public class OreStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ore Staff");
        }

        public override void SetDefaults()
        {
            Item.damage = 160;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.width = 38;
            Item.height = 44;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<OreStaff_OreCluster>();
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.shootSpeed = 12;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<GoldDigger>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CovetiteBar>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
