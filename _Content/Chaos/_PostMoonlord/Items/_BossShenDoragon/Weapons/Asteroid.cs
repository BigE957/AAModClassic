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
    public class Asteroid : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Asteroid");
            /* Tooltip.SetDefault(@"Crashes into enemies with the force of an astroid crashing into earth
Inflicts Discordian Inferno"); */
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.knockBack = 7.5f;
            Item.damage = 150;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Asteroid_Holdout>();
            Item.shootSpeed = 32f;
            Item.UseSound = SoundID.Item20;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.channel = true;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Daycrusher>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Flairdra>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 5);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}