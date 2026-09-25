using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace AAModClassic.UI.Dialogue.TextEffects
{
    public class Wavy : TextEffect
    {
        private const float StandardAmp = 6f;

        private const float StandardFreq = 1.5f;

        private const float StandardOffsetFactor = MathHelper.TwoPi / 320f; //since we use position, this means a full cycle will occur every 320 coordiantes

        public override Vector2 ModifyPos(Vector2 pos, DialogueCharacterData data, float[] args)
        {
            var amp = StandardAmp;
            if(args.Length > 0)
                amp = args[0];

            var freq = StandardFreq;
            if (args.Length > 1)
                freq = args[1];

            var indexFactor = StandardOffsetFactor;
            if (args.Length > 2)
                indexFactor = args[2];

            var sineWave = (float)Math.Sin(Main.GlobalTimeWrappedHourly * freq + data.TextPosition.X * indexFactor) * amp;

            return pos + Vector2.UnitY * sineWave;
        }
    }
}
