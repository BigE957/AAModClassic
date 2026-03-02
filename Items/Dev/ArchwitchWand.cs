using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;

namespace AAMod.Items.Dev
{
    public class ArchwitchWand : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Celestial Wand");
            // Tooltip.SetDefault(@"An old wand. It seems to have not been used recently.");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 120;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 20;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = new LegacySoundStyle(2, 105, Terraria.Audio.SoundType.Sound);
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("ArchwitchStorm").Type;
            Item.shootSpeed = 7f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(121, 21, 214);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CatsEyeRifle");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}