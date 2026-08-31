using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Pets
{
    public class PortaProbe : ModItem
	{
        public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Porta-Probe");

			//Tooltip.SetDefault("Take a little life-seeking robot with you!");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ShadowOrb);
            Item.shoot = ModContent.ProjectileType<PortaProbe_MiniProbe>();
            Item.buffType = ModContent.BuffType<PortaProbe_Buff>();
		}

        public override bool? UseItem(Player player)
        {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 90000, true);
            }
            return true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(BuffID.Spelunker, 2);
            player.AddBuff(BuffID.Spelunker, 2);
        }
    }
}