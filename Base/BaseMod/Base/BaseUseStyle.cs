using Microsoft.Xna.Framework;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic.Base.BaseMod.Base
{
    public class BaseUseStyle
    {
        //------------------------------------------------------//
        //----------------BASE USE STYLE CLASS------------------//
        //------------------------------------------------------//
        // Contains methods relating to custom use styles.      //
        //------------------------------------------------------//
        //  Author(s): Grox the Great                           //
        //------------------------------------------------------//

		/*
		 * Simulates useStyle 4, ie most boss summoning items.
		 * 
		 * useItemHitbox: if true, uses the item's hitbox for offsetting instead of the texture's width and height.
		 * center: if true, centers the item.
		 */
		public static void SetStyleBoss(Player player, Item item, bool useItemHitbox = false, bool center = false)
		{
			Rectangle hitbox = (useItemHitbox || Main.netMode == NetmodeID.Server || Main.dedServ ? item.Hitbox : new Rectangle(0, 0, TextureAssets.Item[item.type].Width(), TextureAssets.Item[item.type].Height()));
			player.itemRotation = 0f;
			player.itemLocation.X = player.position.X + (float)player.width * 0.5f + ((center ? 0f : (float)hitbox.Width * 0.5f) - 9f - player.itemRotation * 14f * (float)player.direction - 4f) * (float)player.direction;
			player.itemLocation.Y = player.position.Y + (float)hitbox.Height * 0.5f + 4f;
			if (player.gravDir == -1f)
			{
				player.itemRotation = -player.itemRotation;
				player.itemLocation.Y = player.position.Y + (float)player.height + (player.position.Y - player.itemLocation.Y);
			}
			if (Main.myPlayer == player.whoAmI && Main.netMode != NetmodeID.SinglePlayer)
			{
				NetMessage.SendData(MessageID.PlayerControls, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0);
				NetMessage.SendData(MessageID.ShotAnimationAndSound, -1, -1, NetworkText.FromLiteral(""), player.whoAmI, 0f, 0f, 0f, 0);
			}
		}

		/*
		 * Change the arm frame in the same way that useStyle 4 would.
		 */
		public static void SetFrameBoss(Player player, Item item)
		{
			player.bodyFrame.Y = player.bodyFrame.Height * 2;
		}
    }
}