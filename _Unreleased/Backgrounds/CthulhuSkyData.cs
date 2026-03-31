using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Unreleased.NPCs.Bosses.SoC;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Backgrounds
{
    public class CthulhuSkyData : ScreenShaderData
    {
        private int SoCIndex;

        public CthulhuSkyData(string passName) : base(passName)
        {
        }

        private void UpdateCthulhuSky()
        {

            int SoCType = ModContent.NPCType<SoC>();
            if (SoCIndex >= 0 && Main.npc[SoCIndex].active && Main.npc[SoCIndex].type == SoCType)
            {
                return;
            }
            SoCIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == SoCType)
                {
                    SoCIndex = i;
                    break;
                }
            }
            if (Main.player[Main.myPlayer].InZone("Ocean") && !AAWorld_Unreleased.downedSoC && AAWorld.downedAllAncients)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateCthulhuSky();
            if (SoCIndex != -1)
            {
                UseTargetPosition(Main.npc[SoCIndex].Center);
            }
            base.Apply();
        }
    }
}