using AAModClassic.UI.Dialogue.DisplayEffects;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.UI.Dialogue
{
    public class DialogueDisplayDebugItem : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Debug";
        public override string Texture => "AAModClassic/_Content/_Dev/__Hardmode/Items/Weapons/AleisterStaff";
        public override void SetDefaults()
        {
            Item.width = 25;
            Item.height = 29;
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }
        public override bool? UseItem(Player player)
        {
            int slot = DialogueDisplaySystem.GetSlot("Mods.AAModClassic.Athena.Intro.Singleplayer");

            if (slot != -1)
                DialogueDisplaySystem.ProgressDialogue(slot);
            else
                DialogueDisplaySystem.StartDialogue("Mods.AAModClassic.Athena.Intro.Singleplayer", player.Center, 0, -1, effects: new BossText());

            return true;
        }
    }
}
