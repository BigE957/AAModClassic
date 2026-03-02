using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class AkumaTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 54;
            Item.height = 60;
            Item.useStyle = 1;
            Item.useTime = 4;
            Item.useAnimation = 20;
            Item.tileBoost += 20;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 9;
            AARarity = 13;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 100;
            Item.pick = 300;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
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
                Item.autoReuse = false;
                Item.noUseGraphic = true;
                AAMod.instance.TerratoolAState.ToggleUI(AAMod.instance.TerratoolInterface);
                Item.pick = 0;
                Item.axe = 0;
                Item.hammer = 0;
                Item.damage = 0;
                return false;
            }
            else if(player.altFunctionUse != 2)
            {
                Item.autoReuse = true;
                Item.noUseGraphic = false;
                Item.pick = UI.TerratoolAUI.Pick;
                Item.axe = UI.TerratoolAUI.Axe;
                Item.hammer = UI.TerratoolAUI.Hammer;
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
