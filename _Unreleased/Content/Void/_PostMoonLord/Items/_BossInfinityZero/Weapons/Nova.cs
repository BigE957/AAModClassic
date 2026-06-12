using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.Weapons
{
    public class Nova : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Nova");
            // Tooltip.SetDefault("Fires an explosive energy blast that causes an expanding explosion");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.shootSpeed = 10f;
            Item.knockBack = 0f;
            Item.width = 48;
            Item.height = 54;
            Item.damage = 390;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<Nova_NovaBurst>();
            Item.mana = 20;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.noUseGraphic = false;
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.IZ;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VoidStar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Infinitium>(), 12);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
