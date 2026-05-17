using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AutoloadEquipGlow(params EquipType[] equipTypes) : Attribute
    {
        public readonly EquipType[] equipTypes = equipTypes;
    }


    public class EquipGlowSystem : ModSystem
    {
        public static Dictionary<EquipType, Dictionary<int, Asset<Texture2D>>> GlowEquipTextures = [];

        public override void Load()
        {
            GlowEquipTextures = [];
            for (EquipType e = EquipType.Head; e <= EquipType.Beard; e++)
                GlowEquipTextures.Add(e, []);
        }

        public override void PostSetupContent()
        {
            AAMod.instance.Logger.Info("Loading Equip Glow Textures: ");
            foreach (Item item in ContentSamples.ItemsByType.Values)
            {
                if (item.ModItem is null || item.ModItem.Mod is not AAMod)
                    continue;

                var autoloadEquip = item.ModItem.GetType().GetInheritantAttribute<AutoloadEquipGlow>();
                if (autoloadEquip is null)
                    continue;

                AAMod.instance.Logger.Info("- Autoload Equip Glow found on item: " + item.ModItem.Name);
                foreach (var equip in autoloadEquip.equipTypes)
                {
                    if (ModContent.RequestIfExists<Texture2D>($"{item.ModItem.Texture}_{equip}_Glow", out var asset))
                    {
                        AAMod.instance.Logger.Info(" - Glow Texture found: " + asset.Name);
                        int slot = EquipLoader.GetEquipSlot(Mod, item.ModItem.Name, equip);
                        if (slot != -1)
                        {
                            AAMod.instance.Logger.Info(" - Equip Slot found: " + slot);
                            GlowEquipTextures[equip].Add(slot, asset);
                        }
                        else
                            AAMod.instance.Logger.Info(" - Could not add due to " + item.ModItem.Name + " not having a regular Equip Texture");
                    }
                    else
                        AAMod.instance.Logger.Info(" - Could not Add due to " + $"{item.ModItem.Texture}_{equip}_Glow" + " not being found.");
                }
            }
        }
    }

    public static class InheritUtil
    {
        public static T GetInheritantAttribute<T>(this Type type) where T : Attribute => type.GetCustomAttributes(inherit: true).OfType<T>().FirstOrDefault();
    }
}
