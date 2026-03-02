using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using AAMod.Items;
using Microsoft.Xna.Framework;

namespace AAMod
{
    public abstract class RogueWeapon : BaseAAItem
	{
        public virtual void SafeSetDefaults()
		{
		}
		public override void SetDefaults()
		{
			SafeSetDefaults();
			Item.melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
			Item.ranged = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
			Item.magic = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
			Item.DamageType = DamageClass.Throwing;
			Item.summon = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
		}
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				float throwingDamage = (float) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingDamage", false, false);
				add += throwingDamage - 1f;
			}
		}
		public override void ModifyWeaponCrit(Player player, ref float crit)
		{
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				int throwingCrit = (int) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingCrit", false, false);
				crit = Item.crit + throwingCrit;
			}
		}
		public override float UseTimeMultiplier(Player player)
		{
			float num = 1f;
			if (ModSupport.GetMod("CalamityMod") != null)
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
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				TooltipLine tooltipLine = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "Damage" && x.Mod == "Terraria");
				if (tooltipLine != null)
				{
					string[] source = tooltipLine.Text.Split(new char[]
					{
						' '
					});
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
		public override bool ConsumeItem(Player player)
		{
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				bool throwingAmmoCost50 = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingAmmoCost50", false, false);
				bool throwingAmmoCost66 = (bool) ModSupport.GetModPlayerConditions("CalamityMod", player, "CalamityPlayer", "throwingAmmoCost66", false, false);
				return (!throwingAmmoCost50 || Main.rand.Next(1, 101) <= 50) && (!throwingAmmoCost66 || Main.rand.Next(1, 101) <= 66);
			}
			return base.ConsumeItem(player);
		}
    }

	public class RoguePlayer : ModPlayer
	{
		public float throwingDamage
        {
			get
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "throwingDamage", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 1f;
			}
			set
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
					ModSupport.SetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "throwingDamage", value, false, false);
				}
			}
		}

		public float throwingVelocity
        {
			get
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "throwingVelocity", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 1f;
			}
			set
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
					ModSupport.SetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "rogueStealthMax", value, false, false);
				}
			}
		}

		public int throwingCrit
        {
			get
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
                    int? stealth = (int?) ModSupport.GetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "throwingCrit", false, false);
                    if (stealth != null) return (int)stealth;
                }
                return 0;
			}
			set
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
					ModSupport.SetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "throwingCrit", value, false, false);
				}
			}
		}

		public float rogueStealth
        {
			get
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "rogueStealth", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 0f;
			}
			set
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
					ModSupport.SetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "rogueStealth", value, false, false);
				}
			}
		}

		public float rogueStealthMax
        {
			get
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
                    float? stealth = (float?) ModSupport.GetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "rogueStealthMax", false, false);
                    if (stealth != null) return (float)stealth;
                }
                return 0f;
			}
			set
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
					ModSupport.SetModPlayerConditions("CalamityMod", Player, "CalamityPlayer", "rogueStealthMax", value, false, false);
				}
			}
		}

		public bool StealthStrikeAvailable
        {
			get
			{
				if (ModSupport.GetMod("CalamityMod") != null)
                {
					Mod mod = ModSupport.GetMod("CalamityMod");
					ModPlayer modplayer = Player.GetModPlayer(mod, "CalamityPlayer");
					MethodInfo StealthStrike = modplayer.GetType().GetMethod("StealthStrikeAvailable", BindingFlags.Instance | BindingFlags.Public);
                    bool? stealth = (bool?)StealthStrike.Invoke(modplayer, new object[]{});
                    if (stealth != null) return (bool)stealth;
                }
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
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				rogue = (bool) ModSupport.GetModGlobalItemConditions("CalamityMod", item, "CalamityGlobalItem", "rogue", false, false);
				ModSupport.SetModGlobalItemConditions("CalamityMod", item, "CalamityGlobalItem", "rogue", true, false, false);
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
			if (ModSupport.GetMod("CalamityMod") != null)
			{
				rogue = (bool) ModSupport.GetModGlobalProjConditions("CalamityMod", projectile, "CalamityGlobalProjectile", "rogue", false, false);
				ModSupport.SetModGlobalProjConditions("CalamityMod", projectile, "CalamityGlobalProjectile", "rogue", true, false, false);
			}
		}
	}
}