using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Blazen
{

    [AutoloadEquip(EquipType.Wings)]
    public class BlazenBooster : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Tactical Assault Booster");
            /* Tooltip.SetDefault(@"Allows flight and slow fall
Hold up to rocket faster
'Great for impersonating Ancients Awakened Developers!'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 42;
			Item.value = 500000;
			Item.rare = ItemRarityID.Red;
			Item.accessory = true;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 0, 255);
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

        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse || player.jump > 0)
            {
                player.rocketDelay2--;
                if (player.rocketDelay2 <= 0)
                {
                    SoundEngine.PlaySound(SoundID.Item13, player.position);
                    player.rocketDelay2 = 60;
                }
                int num69 = 2;
                if (player.controlUp)
                {
                    num69 = 4;
                }
                for (int num70 = 0; num70 < num69; num70++)
                {
                    int type = DustID.Electric;
                    float scale = 1.75f;
                    int alpha = 100;
                    float x3 = player.position.X + player.width / 2 + 16f;
                    if (player.direction > 0)
                    {
                        x3 = player.position.X + player.width / 2 - 26f;
                    }
                    float num71 = player.position.Y + player.height - 18f;
                    if (num70 == 1 || num70 == 3)
                    {
                        x3 = player.position.X + player.width / 2 + 8f;
                        if (player.direction > 0)
                        {
                            x3 = player.position.X + player.width / 2 - 20f;
                        }
                        num71 += 6f;
                    }
                    if (num70 > 1)
                    {
                        num71 += player.velocity.Y;
                    }
                    int num72 = Dust.NewDust(new Vector2(x3, num71), 8, 8, type, 0f, 0f, alpha, default, scale);
                    Dust expr_5AAB_cp_0 = Main.dust[num72];
                    expr_5AAB_cp_0.velocity.X *= 0.1f;
                    Main.dust[num72].velocity.Y = Main.dust[num72].velocity.Y * 1f + 2f * player.gravDir - player.velocity.Y * 0.3f;
                    Main.dust[num72].noGravity = true;
                    Main.dust[num72].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    if (num69 == 4)
                    {
                        Dust expr_5B43_cp_0 = Main.dust[num72];
                        expr_5B43_cp_0.velocity.Y += 6f;
                    }
                }
                player.wingFrameCounter++;
                if (player.wingFrameCounter > 4)
                {
                    player.wingFrame++;
                    player.wingFrameCounter = 0;
                    if (player.wingFrame >= 3)
                    {
                        player.wingFrame = 0;
                    }
                }
            }
            else if (!player.controlJump || player.velocity.Y == 0f)
            {
                player.wingFrame = 3;
            }
            return true;
        }


        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
            if (player.controlDown && player.controlJump && player.wingTime > 0f)
            {
                speed = 15f;
                acceleration *= 10f;
                player.velocity.Y *= 0f;
            }
            else
            {
                speed = 10f;
                acceleration *= 6.25f;
            }
        }
	}
}
