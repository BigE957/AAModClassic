using ReLogic.Utilities;
using Terraria.Audio;

namespace AAModClassic.UI.Dialogue.DialogueEvents
{
    public class SoundEvent : DialogueEvent
    {
        SoundStyle style;
        SlotId soundSlot;

        public override void UpdateEvent()
        {
            if (EventCounter == 0)
                soundSlot = SoundEngine.PlaySound(style = new SoundStyle(Args[0]));
            else if (!SoundEngine.TryGetActiveSound(soundSlot, out var result) || result.Style != style)
                EventOver = true;
        }
    }
}
