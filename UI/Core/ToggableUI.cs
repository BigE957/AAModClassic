using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace AAMod.UI.Core
{
    // © Even More Modifiers by Jofairden
    internal abstract class ToggableUI : UIState
    {
        public bool Visible { get; set; }

        public virtual void ToggleUI(UserInterface userInterface, UIState state = null)
        {
            state = state ?? this;

            if (userInterface.CurrentState is ToggableUI && userInterface.CurrentState != state)
            {
                ((ToggableUI)userInterface.CurrentState).ToggleUI(userInterface, userInterface.CurrentState);
            }

            Visible = !Visible;
            userInterface.ResetLasts();
            userInterface.SetState(Visible ? state : null);

            SoundEngine.PlaySound(SoundID.MenuOpen);
        }
    }
}
