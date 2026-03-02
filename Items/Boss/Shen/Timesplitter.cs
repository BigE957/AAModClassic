using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
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
            Item.useStyle = 5;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.rare = 9;
            Item.shoot = Mod.Find<ModProjectile>("TimesplitterP").Type;  //put your Spear projectile name
            Item.shootSpeed = 9f;
            AARarity = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "AbyssalYari");
			recipe.AddIngredient(null, "SunSpear");
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
