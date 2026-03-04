using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Shen
{
    public class ChaosSlayerEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ikari");
            /* Tooltip.SetDefault(@"Unleashes explosive blades of chaos to smite your foes
blades go through tiles
Chaos Slayer EX"); */
        }

        public override void SetDefaults()
        {
            Item.width = 85;
            Item.height = 85;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.UseSound = SoundID.Item103;
            Item.damage = 666;
            Item.knockBack = 12;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.expert = true; Item.expertOnly = true;
            Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("ChaosSlayerSwordEX").Type;
			Item.shootSpeed = 7;
            Item.useTurn = true;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, type, damage, knockback, player.whoAmI);
			for (int m = 0; m < 2; m++)
			{
				Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, m == 0 ? Mod.Find<ModProjectile>("ChaosSlayerSwordRedEX").Type : Mod.Find<ModProjectile>("ChaosSlayerSwordBlueEX").Type, damage, knockback, player.whoAmI);
			}
			return false;
		}

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ChaosSlayer");
            recipe.AddIngredient(null, "PerfectChaos");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}