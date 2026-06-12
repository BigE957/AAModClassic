using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AutoloadEquipGlow(params EquipType[] equipTypes) : Attribute
    {
        public readonly EquipType[] equipTypes = equipTypes;
    }

    public interface ICustomEquipGlow
    {
        Color Color { get; }
        bool Condition(Player p) => true;
    }

    public class EquipGlowSystem : ModSystem
    {
        public override void OnModLoad()
        {
            ModContent.GetInstance<AAMod>().Logger.Info("Loading Equip Glow Textures: ");
            foreach (ModItem item in Mod.GetContent<ModItem>())
            {

                var autoloadEquip = item.GetType().GetInheritantAttribute<AutoloadEquipGlow>();
                if (autoloadEquip is null)
                    continue;

                ModContent.GetInstance<AAMod>().Logger.Info("- Autoload Equip Glow found on item: " + item.Name);
                foreach (var equip in autoloadEquip.equipTypes)
                {
                    if (ModContent.RequestIfExists<Texture2D>($"{item.Texture}_{equip}_Glow", out var asset))
                    {
                        ModContent.GetInstance<AAMod>().Logger.Info(" - Glow Texture found: " + asset.Name);
                        int slot = EquipLoader.GetEquipSlot(Mod, item.Name, equip);
                        if (slot != -1)
                        {
                            ModContent.GetInstance<AAMod>().Logger.Info(" - Equip Slot found: " + slot);
                            EquipLoader.AddEquipTexture(Mod, $"{item.Texture}_{equip}_Glow", equip, name: item.Name + "_Glow");
                        }
                        else
                            ModContent.GetInstance<AAMod>().Logger.Info(" - Could not add due to " + item.Name + " not having a regular Equip Texture");
                    }
                    else
                        ModContent.GetInstance<AAMod>().Logger.Info(" - Could not Add due to " + $"{item.Texture}_{equip}_Glow" + " not being found.");
                }
            }
        }
    }

    public static class InheritUtil
    {
        public static T GetInheritantAttribute<T>(this Type type) where T : Attribute => type.GetCustomAttributes(inherit: true).OfType<T>().FirstOrDefault();
    }
}
