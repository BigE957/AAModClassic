using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic;
using AAModClassic.CrossMod;

namespace AAModClassic.Items.Dev
{
    public class TimeTeller : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.useTime = 25;
            Item.CloneDefaults(ItemID.Terrarian);

            Item.damage = 200;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Purple;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.shoot = Mod.Find<ModProjectile>("TimeTeller").Type;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.Flat *= player.GetModPlayer<ModSupportPlayer>().Thorium_radiantBoost;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Chilled, 1000);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Time Teller");
            /* Tooltip.SetDefault("Damage changes based on time of day\n" +
				               "Damage is greatest at Midday and Midnight\n" +
                               "'Time to Die!'\n" +
                               "-Dallin"); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(181, 38, 38);
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            if (player.accWatch < 3)
                player.accWatch = 3;
        }
		
		public static float CalcDamageMultiplierFromTimeOfDay(int baseDamage)
		{
			int minDamage = baseDamage; //this is the damage you set in SetDefaults.
			int maxDamage = 350; //this is the damage you get at midday/midnight.

			float maxMultiplier = maxDamage / (float)minDamage;		
			float time = (int)Main.time;
			float calcTimeMax = 0f;
			if(Main.dayTime)
				calcTimeMax = 54000f; //max time in a day
			else
				calcTimeMax = 32400f; //max time in a night

			return BaseUtility.MultiLerp(time / calcTimeMax, 1f, maxMultiplier, 1f);
		}
    }
}