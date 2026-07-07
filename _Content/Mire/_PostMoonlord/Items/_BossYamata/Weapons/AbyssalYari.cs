using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Corruption.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class AbyssalYari : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Yari");
            // Tooltip.SetDefault(@"One of two legendary spears used to divide time into day and night");
        }

        public override void SetDefaults()
        {
            Item.damage = 350;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 132;
            Item.height = 132;
            Item.scale = 1.1f;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.shootSpeed = 5f;
            Item.shoot = ModContent.ProjectileType<AbyssalYari_Holdout>();  
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DuskBringer>());
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}
