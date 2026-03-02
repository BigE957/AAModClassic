using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class SoulSiphon : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Soul Siphon");
			/* Tooltip.SetDefault(@"I swear if you ask me for a song one more time...
-Charlie"); */
		}
		public override void SetDefaults()
		{
			Item.damage = 220;
            Item.useStyle = 1;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.knockBack = 7f;
            Item.width = 60;
            Item.height = 56;
            Item.scale = 1.15f;
            Item.UseSound = SoundID.Item71;
            Item.rare = 11;
            Item.shootSpeed = 9f;
            Item.value = 500000;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("SoulSiphon").Type;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(60, 12, 98);
                }
            }
        }
        
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //target.AddBuff(BuffID.SoulDrain, 1000);
        }
	}
}
