using AAModClassic._Content.Mire.Buffs;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class AbyssalBomb_SoulBombSmall : AbyssalBomb_SoulBomb
	{
        public override void SetDefaults()
		{
            base.SetDefaults();
            isSmall = true;
        }
    }
}
