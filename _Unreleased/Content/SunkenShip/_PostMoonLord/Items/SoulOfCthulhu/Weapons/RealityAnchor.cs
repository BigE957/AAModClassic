using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
    public class RealityAnchor : BaseAAItem
    {

        public override void SetStaticDefaults()
        {

            //DisplayName.SetDefault("Reality Anchor");
            //Tooltip.SetDefault(@"The further the anchor falls, the larger the explosion when it hits a tile");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Cthulhu;
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 48;
            Item.value = Item.buyPrice(1, 0, 0, 0); ;
            Item.rare = ItemRarityID.Blue;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.knockBack = 7.5F;
            Item.damage = 300;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<RealityAnchor_Proj>();
            Item.shootSpeed = 14f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            AARarity = 14;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RealityBar>(), 5);
            recipe.AddIngredient(ItemID.Anchor, 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
