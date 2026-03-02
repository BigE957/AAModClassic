using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAMod.Items;
using System;
using System.Reflection;

namespace AAMod
{
    public class ModSupport
    {

        public static FieldInfo CRevengence = null, CDeath = null, CDefiled = null;

        /*
        public static bool Revengence
        {
            get
            {
                if (calamity != null)
                {
                    if (calamity.Version >= new Version(1, 4, 2, 201))
                    {
                        return CalamityMod.world.CalamityWorld.revenge;
                    }
                    else
                    {
                        return CalamityMod.CalamityWorld.revenge;
                    }
                    
                }
                return false;
            }
        }

        public static bool Death
        {
            get
            {
                if (calamity != null)
                {
                    if (calamity.Version >= new Version(1, 4, 2, 201))
                    {
                        return CalamityMod.world.CalamityWorld.death;
                    }
                    else
                    {
                        return CalamityMod.CalamityWorld.death;
                    }
                    
                }
                return false;
            }
        }

        public static bool Defiled
        {
            get
            {
                if (calamity != null)
                {
                    if (calamity.Version >= new Version(1, 4, 2, 201))
                    {
                        return CalamityMod.world.CalamityWorld.defiled;
                    }
                    else
                    {
                        return CalamityMod.CalamityWorld.defiled;
                    }
                }
                return false;
            }
        }*/

        public static Mod GetMod(string modname)
		{
            if(ModLoader.GetMod(modname) != null)
			{
                return ModLoader.GetMod(modname);
            }
            return null;
        }

        public static ModItem GetModItem(string modname, string itemname)
		{
			ModItem item = new ModItem();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					item = mod.GetItem(itemname);
				}
				catch(Exception)
				{
					item = null;
					throw new Exception("Can't find this item" + itemname);
				}
			}

			return item;
		}

        public static ModNPC GetModNPC(string modname, string npcname)
		{
			ModNPC npc = new ModNPC();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					npc = mod.GetNPC(npcname);
				}
				catch(Exception)
				{
					npc = null;
					throw new Exception("Can't find this npc" + npcname);
				}
			}

			return npc;
		}

        public static ModProjectile GetModProjectile(string modname, string projname)
		{
			ModProjectile projectile = new ModProjectile();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					projectile = mod.GetProjectile(projname);
				}
				catch(Exception)
				{
					projectile = null;
					throw new Exception("Can't find this projectile" + projname);
				}
			}

			return projectile;
		}

        public static ModDust GetModDust(string modname, string dustname)
		{
            ModDust dust = new ModDust();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					dust = mod.GetDust(dustname);
				}
				catch(Exception)
				{
					dust = null;
					throw new Exception("Can't find this dust" + dustname);
				}
			}

			return dust;
        }

        public static ModBuff GetModBuff(string modname, string buffname)
		{
            ModBuff buff = new ModBuff();
			if(ModLoader.GetMod(modname) != null)
			{
				Mod mod = ModLoader.GetMod(modname);
				try
				{
					buff = mod.GetBuff(buffname);
				}
				catch(Exception)
				{
					buff = null;
					throw new Exception("Can't find this buff" + buffname);
				}
			}

			return buff;
        }

        public static object GetModWorldConditions(string modname, string worldname, string ConditionName, bool nopub = false, bool sta = false)
		{
			object condition = null;
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					ModSystem world = mod.GetModWorld(worldname);
					if(world != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						return world.GetType().GetField(ConditionName, binding).GetValue(world);
					}
				}
				catch
				{
					return null;
					throw new Exception("Error in reading world data.");
				}
            }
			return condition;
        }

        public static void SetModWorldConditions(string modname, string worldname, string ConditionName, object Set_value, bool nopub = false, bool sta = false)
		{
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					ModSystem world = mod.GetModWorld(worldname);
					if(world != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						FieldInfo field = world.GetType().GetField(ConditionName, binding);
                        if(field.FieldType == Set_value.GetType())
                        {
                            field.SetValue(world, Set_value);
                        }
					}
				}
				catch
				{
					throw new Exception("Error in setting world data.");
				}
            }
        }

        public static object GetModPlayerConditions(string modname, Player player, string playername, string ConditionName, bool nopub = false, bool sta = false)
		{
            object condition = null;
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					ModPlayer modplayer = player.GetModPlayer(mod, playername);
					if(player != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						return modplayer.GetType().GetField(ConditionName, binding).GetValue(modplayer);
					}
				}
				catch
				{
					return null;
					throw new Exception("Error in reading modplayer data.");
				}
            }
			return condition;
        }

        public static void SetModPlayerConditions(string modname, Player player, string playername, string ConditionName, object Set_value, bool nopub = false, bool sta = false)
		{
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					ModPlayer modplayer = player.GetModPlayer(mod, playername);
					if(player != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						FieldInfo field = modplayer.GetType().GetField(ConditionName, binding);
                        if(field.FieldType == Set_value.GetType())
                        {
                            field.SetValue(modplayer, Set_value);
                        }
					}
				}
				catch
				{
					throw new Exception("Error in setting modplayer data.");
				}
            }
        }

        public static object GetModGlobalItemConditions(string modname, Item item, string globalitemname, string ConditionName, bool nopub = false, bool sta = false)
		{
            object condition = null;
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					GlobalItem global = item.GetGlobalItem(mod, globalitemname);
					if(global != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						return global.GetType().GetField(ConditionName, binding).GetValue(global);
					}
				}
				catch
				{
					return null;
					throw new Exception("Error in reading globalitem data.");
				}
            }
			return condition;
        }

        public static void SetModGlobalItemConditions(string modname, Item item, string globalitemname, string ConditionName, object Set_value, bool nopub = false, bool sta = false)
		{
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					GlobalItem global = item.GetGlobalItem(mod, globalitemname);
					if(global != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						FieldInfo field = global.GetType().GetField(ConditionName, binding);
                        if(field.FieldType == Set_value.GetType())
                        {
                            field.SetValue(global, Set_value);
                        }
					}
				}
				catch
				{
					throw new Exception("Error in setting globalitem data.");
				}
            }
        }

        public static object GetModGlobalProjConditions(string modname, Projectile proj, string globalprojname, string ConditionName, bool nopub = false, bool sta = false)
		{
            object condition = null;
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					GlobalProjectile global = proj.GetGlobalProjectile(mod, globalprojname);
					if(global != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						return global.GetType().GetField(ConditionName, binding).GetValue(global);
					}
				}
				catch
				{
					return null;
					throw new Exception("Error in reading globalproj data.");
				}
            }
			return condition;
        }

        public static void SetModGlobalProjConditions(string modname, Projectile proj, string globalprojname, string ConditionName, object Set_value, bool nopub = false, bool sta = false)
		{
            if(ModLoader.GetMod(modname) != null)
			{
                Mod mod = ModLoader.GetMod(modname);
				try
				{
					GlobalProjectile global = proj.GetGlobalProjectile(mod, globalprojname);
					if(global != null)
					{
						BindingFlags binding = (sta? BindingFlags.Static : BindingFlags.Instance) | (nopub? BindingFlags.NonPublic : BindingFlags.Public);
						FieldInfo field = global.GetType().GetField(ConditionName, binding);
                        if(field.FieldType == Set_value.GetType())
                        {
                            field.SetValue(global, Set_value);
                        }
					}
				}
				catch
				{
					throw new Exception("Error in setting globalitem data.");
				}
            }
        }
    }

    public abstract class CrossoverItem : BaseAAItem
    {
        public string crossoverModName = "(N/A)";

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (ModSupport.GetMod(crossoverModName) != null)
            {
                TooltipLine error = new TooltipLine(Mod, "Error", "WARNING: ITEM WILL NOT FUNCTION WITHOUT " + crossoverModName.ToUpper() + " ENABLED!")
                {
                    OverrideColor = new Color(255, 50, 50)
                };
                list.Add(error);
            }
        }
    }

    public class ModSupportPlayer : ModPlayer
    {
        #region Thorium
        public float Thorium_radiantBoost
        {
            get
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    float? boost = (float?)ModSupport.GetMod("ThoriumMod").Call("GetRadiantBoost", Player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    ModSupport.GetMod("ThoriumMod").Call("SetRadiantBoost", Player.whoAmI, value);
                }
            }
        }
        public int Thorium_radiantCrit
        {
            get
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    int? boost = (int?)ModSupport.GetMod("ThoriumMod").Call("GetRadiantCrit", Player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    ModSupport.GetMod("ThoriumMod").Call("SetRadiantCrit", Player.whoAmI, value);
                }
            }
        }
        public int Thorium_healBonus
        {
            get
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    int? boost = (int?)ModSupport.GetMod("ThoriumMod").Call("GetHealBonus", Player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.GetMod("ThoriumMod") != null)
                {
                    ModSupport.GetMod("ThoriumMod").Call("SetHealBonus", Player.whoAmI, value);
                }
            }
        }
        #endregion

        #region Redemption

        public float Redemption_druidicBoost
        {
            get
            {
                if (ModSupport.GetMod("Redemption") != null)
                {
                    float? boost = (float?)ModSupport.GetMod("Redemption").Call("GetDruidicBoost", Player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (ModSupport.GetMod("Redemption") != null)
                {
                    ModSupport.GetMod("Redemption").Call("SetDruidicBoost", Player.whoAmI, value);
                }
            }
        }
        public int Redemption_druidicCrit
        {
            get
            {
                if (ModSupport.GetMod("Redemption") != null)
                {
                    int? boost = (int?)ModSupport.GetMod("Redemption").Call("GetDruidicCrit", Player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.GetMod("Redemption") != null)
                {
                    ModSupport.GetMod("Redemption").Call("SetDruidicCrit", Player.whoAmI, value);
                }
            }
        }

        #endregion
    }
}