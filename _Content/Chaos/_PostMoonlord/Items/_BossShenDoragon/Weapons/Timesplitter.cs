using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class Timesplitter : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Timesplitter");
            /* Tooltip.SetDefault(@"It has been said that this spear was used to divide time into day and night
Inflicts Daybroken and Moonraze"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 265;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 96;
            Item.height = 96;
            Item.scale = 1.1f;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.knockBack = 4.7f;
            Item.UseSound = SoundID.Item20;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
			Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.shoot = ModContent.ProjectileType<Timesplitter_Holdout>();  //put your Spear projectile name
            Item.shootSpeed = 9f;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        

        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<AbyssalYari>());
			recipe.AddIngredient(ModContent.ItemType<SunPartisan>());
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
