using AAModClassic;
using AAModClassic.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev.Tools
{
    public class GroviteTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 56;
            Item.height = 56;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 4;
            Item.useAnimation = 16;
            Item.tileBoost += 25;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 120;
            Item.pick = 320;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grovite Terratool");
            /* Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active"); */
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && Main.mouseRight && Main.mouseRightRelease)
            {
                AAMod.instance.TerratoolGroxState.ToggleUI(AAMod.instance.TerratoolInterface);
                Item.pick = 0;
                Item.axe = 0;
                Item.hammer = 0;
                Item.damage = 0;
                return false;
            }
            else if(player.altFunctionUse != 2)
            {
                Item.pick = TerratoolGroxUI.Pick;
                Item.axe = TerratoolGroxUI.Axe;
                Item.hammer = TerratoolGroxUI.Hammer;
                Item.damage = 120;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
