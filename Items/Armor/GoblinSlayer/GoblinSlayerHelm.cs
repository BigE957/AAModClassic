using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using Terraria.ID;

namespace AAMod.Items.Armor.GoblinSlayer
{
    [AutoloadEquip(EquipType.Head)]
	public class GoblinSlayerHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Goblin Slayer's Helm");
			// Tooltip.SetDefault(@"An immense hatred of Goblinkind haunts this helm");

		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 22;
			Item.value = Item.sellPrice (0, 0, 5, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 6;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("GoblinSlayerChest").Type && legs.type == Mod.Find<ModItem>("GoblinSlayerGreaves").Type;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.GoblinSlayerHelmBonus");
            player.GetModPlayer<AAPlayer>().goblinSlayer = true;
            int num = 0;
            num += player.bodyFrame.Y / 56;
            if (num >= Main.OffsetsPlayerHeadgear.Length)
            {
                num = 0;
            }
            Vector2 vector = new Vector2(3 * player.direction - ((player.direction == 1) ? 1 : 0), -11.5f * player.gravDir) + Vector2.UnitY * player.gfxOffY + player.Size / 2f + Main.OffsetsPlayerHeadgear[num];
            Vector2 vector2 = new Vector2(3 * player.shadowDirection[1] - ((player.direction == 1) ? 1 : 0), -11.5f * player.gravDir) + player.Size / 2f + Main.OffsetsPlayerHeadgear[num];
            Vector2 vector3 = Vector2.Zero;
            if (player.mount.Active && player.mount.Cart)
            {
                int num2 = Math.Sign(player.velocity.X);
                if (num2 == 0)
                {
                    num2 = player.direction;
                }
                vector3 = new Vector2(MathHelper.Lerp(0f, -8f, player.fullRotation / 0.7853982f), MathHelper.Lerp(0f, 2f, Math.Abs(player.fullRotation / 0.7853982f))).RotatedBy(player.fullRotation, default);
                if (num2 == Math.Sign(player.fullRotation))
                {
                    vector3 *= MathHelper.Lerp(1f, 0.6f, Math.Abs(player.fullRotation / 0.7853982f));
                }
            }
            if (player.fullRotation != 0f)
            {
                vector = vector.RotatedBy(player.fullRotation, player.fullRotationOrigin);
                vector2 = vector2.RotatedBy(player.fullRotation, player.fullRotationOrigin);
            }
            float num3 = 0f;
            if (player.mount.Active)
            {
                num3 = player.mount.PlayerOffset;
            }
            Vector2 vector4 = player.position + vector + vector3;
            Vector2 vector5 = player.oldPosition + vector2 + vector3;
            vector5.Y -= num3 / 2f;
            vector4.Y -= num3 / 2f;
            float num4 = 1f;
            switch (player.yoraiz0rEye % 10)
            {
                case 1:
                    return;
            }
            if (player.yoraiz0rEye < 7)
            {
                DelegateMethods.v3_1 = Main.hslToRgb(Main.rgbToHsl(player.eyeColor).X, 1f, 0.5f).ToVector3() * 0.5f * num4;
                if (player.velocity != Vector2.Zero)
                {
                    Utils.PlotTileLine(player.Center, player.Center + player.velocity * 2f, 4f, new Utils.PerLinePoint(DelegateMethods.CastLightOpen));
                }
                else
                {
                    Utils.PlotTileLine(player.Left, player.Right, 4f, new Utils.PerLinePoint(DelegateMethods.CastLightOpen));
                }
            }
            int num5 = (int)Vector2.Distance(vector4, vector5) / 3 + 1;
            if (Vector2.Distance(vector4, vector5) % 3f != 0f)
            {
                num5++;
            }
            for (float num6 = 1f; num6 <= num5; num6 += 1f)
            {
                Dust dust = Main.dust[Dust.NewDust(player.Center, 0, 0, DustID.TheDestroyer, 0f, 0f, 0, default, 1f)];
                dust.position = Vector2.Lerp(vector5, vector4, num6 / num5);
                dust.noGravity = true;
                dust.velocity = Vector2.Zero;
                dust.customData = this;
                dust.scale = num4;
                dust.shader = GameShaders.Armor.GetSecondaryShader(player.cYorai, player);
            }
        }
	}
}