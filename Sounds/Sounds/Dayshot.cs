using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Sounds.Sounds
{
    public class Dayshot : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            if (soundInstance.State == SoundState.Playing)
                return null;
            soundInstance.Volume = volume * 1f;
            soundInstance.Pan = pan;
            soundInstance.Pitch = Main.rand.Next(-1, 1) * .05f;
            return soundInstance;
        }
    }
}