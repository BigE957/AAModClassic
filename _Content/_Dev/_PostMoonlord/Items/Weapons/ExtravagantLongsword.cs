using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class ExtravagantLongsword : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Extravagant Longsword");
            /* Tooltip.SetDefault(@"An Excellent choice.
-Big E); */
        }
        public override void SetDefaults()
		{
			Item.damage = 290;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 7;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<ExtravagantLongsword_BigE>();
            Item.shootSpeed = 12f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(39, 115, 189);
                }
            }
        }
        
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //target.AddBuff(BuffID.Wet, 1000);
        }
	}
}
