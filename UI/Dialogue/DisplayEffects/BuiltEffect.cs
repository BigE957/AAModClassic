using Microsoft.Xna.Framework;
using Terraria;

namespace AAModClassic.UI.Dialogue.DisplayEffects
{
    public class BuiltEffect : DisplayEffect
    {
        public override Vector2 AppearPositioning(Vector2 startPos, Vector2 goalPos, float time, DialogueCharacterData charData) => Vector2.Lerp(goalPos + Vector2.UnitX.RotatedBy(charData.Index) * 400, goalPos, time / TimeToAppear);

        public override float AppearRotation(float goalRotation, float time, DialogueCharacterData charData) => MathHelper.Lerp(goalRotation + MathHelper.TwoPi * 2, goalRotation, time / TimeToAppear);
    }
}
