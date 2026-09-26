using AAModClassic.UI.Dialogue.DisplayEffects;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.UI.Dialogue
{
    public class UIDebugItem : ModItem, ILocalizedModType
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
            if (DialogueUISystem.Visible)
                DialogueUISystem.EndDialogue();
            else
                DialogueUISystem.StartDialogue("Mods.CalamityMod.EvilSmasher.DemonAltar", 0, player.Center - Vector2.UnitY * 128);

            return true;
        }
    }
}
