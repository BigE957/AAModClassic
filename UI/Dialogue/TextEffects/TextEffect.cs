using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAModClassic.UI.Dialogue.TextEffects
{
    public abstract class TextEffect
    {
        public virtual Vector2 ModifyPos(Vector2 pos, DialogueCharacterData data, float[] args) => pos;
        public virtual float ModifyRot(float rot, DialogueCharacterData data, float[] args) => rot;
        public virtual Color ModifyColor(Color current, DialogueCharacterData data, float[] args) => current;
        public virtual Vector2 ModifyScale(Vector2 scale, DialogueCharacterData data, float[] args) => scale;

        public virtual void PreDraw(SpriteBatch spritebatch, Texture2D texture, DialogueCharacterData data) { }
        public virtual void PostDraw(SpriteBatch spritebatch, Texture2D texture, DialogueCharacterData data) { }

    }
}
