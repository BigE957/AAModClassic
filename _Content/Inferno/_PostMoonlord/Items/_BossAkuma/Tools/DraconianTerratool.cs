using AAModClassic.Globals;
using AAModClassic.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Tools
{
    public class DraconianTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 54;
            Item.height = 60;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 4;
            Item.useAnimation = 20;
            Item.tileBoost += 20;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 100;
            Item.pick = 300;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Draconian Terratool");
            /* Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active
+20 tile reach"); */
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && Main.mouseRight && Main.mouseRightRelease)
            {
                AAMod.instance.TerratoolAState.ToggleUI(AAMod.instance.TerratoolInterface);
                Item.pick = 0;
                Item.axe = 0;
                Item.hammer = 0;
                Item.damage = 0;
                return false;
            }
            else if(player.altFunctionUse != 2)
            {
                Item.pick = TerratoolAUI.Pick;
                Item.axe = TerratoolAUI.Axe;
                Item.hammer = TerratoolAUI.Hammer;
                Item.damage = 100;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
