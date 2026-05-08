using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic.Utilities
{
    public static class ChatUtils
    {
        public static void Chat(string s, Color color, bool sync = true)
        {
            Chat(s, color.R, color.G, color.B, sync);
        }

        public static void Chat(string s, byte colorR = 255, byte colorG = 255, byte colorB = 255, bool sync = true)
        {
            if (!AAConfigClient.Instance.NoBossDialogue)
            {
                if (Main.netMode == NetmodeID.SinglePlayer) { Main.NewText(s, colorR, colorG, colorB); }
                else
                    if (Main.netMode == NetmodeID.MultiplayerClient) { Main.NewText(s, colorR, colorG, colorB); }
                    else //if(sync){ NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB), Main.myPlayer); } }else
                        if (sync && Main.netMode == NetmodeID.Server) { ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB), -1); }
            }
        }
    }
}
