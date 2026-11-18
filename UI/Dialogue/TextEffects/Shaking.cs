using Microsoft.Xna.Framework;
using Terraria;

namespace AAModClassic.UI.Dialogue.TextEffects
{
    internal class Shaking : TextEffect
    {
        private const float StandardAmp = 2f;

        public override Vector2 ModifyPos(Vector2 pos, DialogueCharacterData data, float[] args)
        {
            var amp = StandardAmp;
            if (args.Length > 0)
                amp = args[0];

            return pos + Main.rand.NextVector2Circular(amp, amp);
        }
    }
}
