using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Boss.Anubis
{
    public class ArtifactOfJudgement : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Artifact of Judgement");
            /* Tooltip.SetDefault(@"Taking damage builds a charge in the Artifact
Reaching a charge of 250 will summon an ''Eye of Judgement'' and reset the charge value
Your defense is lowered and speed is raised while the Eye is active"); */
            
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 34;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
            Item.expert = true;
            Item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().artifactJudgement = true;
        }
		
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Player player = Main.player[Main.myPlayer];
			string text1 = Language.GetTextValue("Mods.AAMod.Common.ArtifactOfJudgementInfo") + " " + player.GetModPlayer<AAPlayer>().artifactJudgementCharge;
            TooltipLine line = new TooltipLine(Mod, "text1", text1)
            {
                OverrideColor = Color.Yellow
            };
            tooltips.Insert(2,line);
		}
    }
}