using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic._Content.Snow.___PreHardmode.NPCs._Night._SnowSerpent
{
    public class SnowSerpentBody : SnowSerpentHead, IBannerNPC
    {
		public int OverrideBannerNPCType => ModContent.NPCType<SnowSerpentHead>();

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Snow Serpent");
            this.HideFromBestiary();
        }

		public override void SetDefaults()
		{
            base.SetDefaults();
            NPC.dontCountMe = true;
		}

		public override bool PreKill()
		{
			return false;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}	
    }
}