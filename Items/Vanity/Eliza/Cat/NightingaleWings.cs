using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Eliza.Cat
{

    [AutoloadEquip(EquipType.Wings)]
    public class NightingaleWings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Nightingale Wings");
            /* Tooltip.SetDefault(@"Allows flight and slow fall
'Great for impersonating Ancients Awakened Devs!'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 42;
			Item.value = 500000;
			Item.rare = ItemRarityID.Purple;
			Item.accessory = true;
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
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 300;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 10f;
			acceleration *= 2.5f;
		}

        public override bool WingUpdate(Player player, bool inUse)
        {
            if (player.controlDown && player.controlJump && player.wingTime > 0f && !player.merman)
            {
                player.velocity.Y *= 0.01f;
                if (player.velocity.Y > -2f && player.velocity.Y < 1f)
                {
                    player.velocity.Y = 1E-05f;
                }
            }

            if (inUse)
            {
                player.wingFrameCounter++;
                if (player.wingFrameCounter > 8)
                {
                    player.wingFrame++;
                    player.wingFrameCounter = 0;
                    if (player.wingFrame > 3)
                    {
                        player.wingFrame = 0;
                    }
                }
                if (player.controlJump && player.wingTime <= 0)
                {
                    player.wingFrame = 1;
                }
            }
            else
            {
                player.wingFrame = 0;
                if (player.velocity.Y != 0)
                {
                    player.wingFrame = 2;
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DragonWings");
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}
