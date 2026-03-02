using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class PortaProbe : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Porta-Probe");

			// Tooltip.SetDefault("Take a little life-seeking robot with you!");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ShadowOrb);
			Item.shoot = Mod.Find<ModProjectile>("MiniProbe").Type;
            Item.buffType = Mod.Find<ModBuff>("MiniProbe").Type;
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 90000, true);
            }
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(BuffID.Spelunker, 2);
            player.AddBuff(BuffID.Spelunker, 2);
        }
    }
}