using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons
{
    public class Annihilator : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Fires a quantum laser that creates an immensely powerful singularity");
            
        }

        public override void SetDefaults()
		{
			Item.damage = 420;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 34;
			Item.height = 58;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 0;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.UseSound = SoundID.Item75;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Annihilator_Annihilation>();
			Item.shootSpeed = 8f;
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

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Neutralizer>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Infinitium>(), 12);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
