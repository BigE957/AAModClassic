using AAModClassic.Utilities;
using Microsoft.Xna.Framework;

namespace AAModClassic.UI.Dialogue.DisplayEffects
{
    public class UIFadeIn : DisplayEffect
    {
        public override Vector2 TextOffsetFromStart(Vector2 startPos, Vector2 textSize) => startPos;

        public override Vector2 AppearPositioning(Vector2 startPos, Vector2 goalPos, float time, DialogueCharacterData charData) => Vector2.Lerp(goalPos + Vector2.One * 24f, goalPos, MathUtils.CircOutEasing(time / TimeToAppear));

        public override bool FadeWhenTooFar => false;
    }
}
