using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.__Hardmode.Items.Accessories
{
    public class CuringBandEffect_CuringStealth : ModBuff
	{
        public static Asset<Texture2D> BuffOverlay;

        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Glitched");
            // Description.SetDefault("Your head is like 10 feet in front of you");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = false;

            BuffOverlay = ModContent.Request<Texture2D>(Texture + "_BuffOverlay");
        }

        public override void Update(Player player, ref int index)
        {
            base.Update(player, ref index);
            player.aggro -= 200;
            player.lifeRegen += 6;
        }
    }
}
