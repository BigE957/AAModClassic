using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
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
            //Tooltip.SetDefault(@"Hurls a mysterious anchor that deals an impact with the forces of several dimensions");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
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
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.knockBack = 7.5F;
            Item.damage = 150;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<RealityAnchor_Holdout>();
            Item.shootSpeed = 14f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.channel = true;
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
