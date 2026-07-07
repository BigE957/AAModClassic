using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._CrossMod.CalamityMod
{
    public abstract class RogueWeapon : BaseAAItem
	{
        public virtual void SafeSetDefaults()
		{
		}
		public override void SetDefaults()
		{
			SafeSetDefaults();
			Item.DamageType = CalamityMod.IsEnabled ? CalamityMod.RogueClass : DamageClass.Throwing;
		}

		//TODO: These are terrifying and I would hope are not actually needed... I really hope.
		/*
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			if(CalamityMod.IsEnabled)
			{
				float throwingDamage = (float) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingDamage", false, false);
                damage.Flat += throwingDamage - 1f;
			}
		}
		public override void ModifyWeaponCrit(Player player, ref float crit)
		{
			if(CalamityMod.IsEnabled)
			{
				int throwingCrit = (int) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingCrit", false, false);
				crit = Item.crit + throwingCrit;
			}
		}
		public override float UseTimeMultiplier(Player player)
		{
			float num = 1f;
			if(CalamityMod.IsEnabled)
			{
				bool gloveOfPrecision = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "gloveOfPrecision", false, false);
				bool gloveOfRecklessness = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "gloveOfRecklessness", false, false);
				if (gloveOfPrecision)
				{
					num -= 0.2f;
				}
				if (gloveOfRecklessness)
				{
					num += 0.2f;
				}
			}
			return num;
		}
		*/
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if(CalamityMod.IsEnabled)
			{
				TooltipLine tooltipLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "Damage" && x.Mod == "Terraria");
				if (tooltipLine != null)
				{
					string[] source = tooltipLine.Text.Split(' ');
					string str = source.First();
					string str2 = source.Last();
					tooltipLine.Text = str + " rogue " + str2;
				}
			}
			else
			{
				TooltipLine error = new TooltipLine(Mod, "Error", "WARNING: ITEM WILL NOT FUNCTION WITHOUT CALAMITY ENABLED!")
                {
                    OverrideColor = new Color(255, 50, 50)
                };
                tooltips.Add(error);
			}
		}

		//TODO: See above, though this one might be needed. However calamity doesnt do consumable rogue weapons anymore sooooooooooooooo
		/*
		public override bool ConsumeItem(Player player)
		{
			if(CalamityMod.IsEnabled)
			{
				bool throwingAmmoCost50 = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingAmmoCost50", false, false);
				bool throwingAmmoCost66 = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingAmmoCost66", false, false);
				return (!throwingAmmoCost50 || Main.rand.Next(1, 101) <= 50) && (!throwingAmmoCost66 || Main.rand.Next(1, 101) <= 66);
			}
			return base.ConsumeItem(player);
		}
		*/
    }

	public class RoguePlayer : ModPlayer
	{
        //TODO: See above, like why would you need these.
        /*
		public float ThrowingDamage
        {
			get
			{
				if(CalamityMod.IsEnabled)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "throwingDamage", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 1f;
			}
			set
			{
				if(CalamityMod.IsEnabled)
                {
					ModSupport.SetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "throwingDamage", value, false, false);
				}
			}
		}

		public int ThrowingCrit
        {
			get
			{
				if(CalamityMod.IsEnabled)
                {
                    int? stealth = (int?) ModSupport.GetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "throwingCrit", false, false);
                    if (stealth != null) return (int)stealth;
                }
                return 0;
			}
			set
			{
				if(CalamityMod.IsEnabled)
                {
					ModSupport.SetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "throwingCrit", value, false, false);
				}
			}
		}
		*/

        public float ThrowingVelocity
        {
			get
			{
                if (CalamityMod.IsEnabled)
                    return (float)CalamityMod.Call("GetRogueVelocity", Player);
                return 0;
            }
			set
			{
				if(CalamityMod.IsEnabled)
                    CalamityMod.Call("AddRogueVelocity", Player, value - (float)CalamityMod.Call("GetRogueVelocity", Player));
            }
		}

		public float RogueStealth
        {
            get
            {
                if (CalamityMod.IsEnabled)
                    return (float)CalamityMod.Call("GetCurrentStealth", Player);
                return 0;
            }
        }

		public float RogueStealthMax
        {
            get
            {
                if (CalamityMod.IsEnabled)
                    return (float)CalamityMod.Call("GetMaxStealth", Player);
                return 0;
            }
        }

		public bool StealthStrikeAvailable
        {
			get
			{
				if(CalamityMod.IsEnabled)
                    return (bool)CalamityMod.Call("CanStealthStrike", Player);
                return false;
			}
		}
	}

	public class RogueItem : GlobalItem
	{
		public override bool InstancePerEntity => true;
		protected override bool CloneNewInstances => true;
		public bool rogue;

        public override void SetDefaults(Item item)
		{
			if(CalamityMod.IsEnabled)
			{
				rogue = item.CountsAsClass(CalamityMod.RogueClass);
				//ModSupport.SetModGlobalItemConditions("CalamityMod", item, "CalamityGlobalItem", "rogue", true, false, false);
			}
		}
	}

	public class RogueProj : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		protected override bool CloneNewInstances => true;
		public bool rogue;
		public bool stealthStrike = false;
        public override void SetDefaults(Projectile projectile)
		{
            if(CalamityMod.IsEnabled)
            {
                rogue = projectile.CountsAsClass(CalamityMod.RogueClass);
                //ModSupport.SetModGlobalProjConditions("CalamityMod", projectile, "CalamityGlobalProjectile", "rogue", true, false, false);
            }
		}
	}
}