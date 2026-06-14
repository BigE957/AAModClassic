using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Pets
{
    public class GlowingMossBall : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Pets";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glowing Moss Ball");
			/* Tooltip.SetDefault(@"Summons a glowmoss ball
Don't ask what makes it glow, Trust me"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<GlowingMossBall_Pet>();
			Item.width = 16;
			Item.height = 30;
			Item.UseSound = SoundID.Item2;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Yellow;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 5, 50, 0);
			Item.buffType = ModContent.BuffType<GlowingMossBall_Buff>();
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