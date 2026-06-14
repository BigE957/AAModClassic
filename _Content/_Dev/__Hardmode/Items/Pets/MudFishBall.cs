using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Pets
{
    public class MudFishBall : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Pets";

        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Mud Fish Ball");

			// Tooltip.SetDefault("It seems to have something in it already");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.UnluckyYarn);
			Item.shoot = ModContent.ProjectileType<MudFishBall_Mudkip>();
            
            Item.buffType = ModContent.BuffType<MudFishBall_Buff>();
		}

        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
	}
}