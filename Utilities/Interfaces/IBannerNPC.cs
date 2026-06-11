using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.Interfaces
{
    public interface IBannerNPC
    {
        public int OverrideBannerNPCType => -1;

        public bool TryAddBanner(ModNPC me)
        {
            ModNPC lead;
            if (OverrideBannerNPCType != -1)
            {
                lead = ModContent.GetModNPC(OverrideBannerNPCType);

                me.Banner = lead.Type;
                if (lead.Banner == 0)
                {
                    BannerNPCLoader.NPCsToDoubleCheck.Enqueue(me.Type);
                    ModContent.GetInstance<AAMod>().Logger.Info($"  - Override Type {lead.Name}'s banner hasn't been setup yet. Adding {me.Name} to queue.");
                    return false;
                }
                else
                {
                    me.BannerItem = lead.Banner;
                    return true;
                }
            }
            else
                lead = me;

            ModContent.GetInstance<AAMod>().Logger.Info($"  - Creating new Banner");

            string path = lead.Texture;
            if(path.EndsWith("Head"))
                path = path.Substring(0, path.Length - 4);
            else if (path.EndsWith("_NPC"))
                path = path.Substring(0, path.Length - 4);

            if (ModContent.RequestIfExists<Texture2D>(path + "_Banner", out _))
            {
                string name = lead.Name;
                if (name.EndsWith("Head"))
                    name = name.Substring(0, name.Length - 4);
                else if (name.EndsWith("_NPC"))
                    name = name.Substring(0, name.Length - 4);

                BannerTile tile = new(path + "_Banner", name);

                ModContent.GetInstance<AAMod>().AddContent(tile);

                BannerTile.TileTypeToNPC.Add(tile.Type, lead.Type);

                BannerItem item = new(path + "_Banner", name, tile.Type);
                
                ModContent.GetInstance<AAMod>().AddContent(item);

                tile.RegisterItemDrop(item.Type);

                me.Banner = lead.Type;
                me.BannerItem = item.Type;

                return true;
            }

            ModContent.GetInstance<AAMod>().Logger.Info($"  - Failed to add a banner");
            ModContent.GetInstance<AAMod>().Logger.Info($"    - Texture not found: " + path + "_Banner");
            return false;
        }
    }

    public class BannerNPCLoader : ModSystem
    {
        public delegate void orig_ResizeArrays(bool optional);
        private static readonly MethodInfo resizeMethod = typeof(ModContent).GetMethod("ResizeArrays", BindingFlags.Static | BindingFlags.NonPublic);
        private static Hook loadBannersHook;

        internal static Queue<int> NPCsToDoubleCheck = [];

        public override void Load()
        {
            loadBannersHook = new Hook(resizeMethod, ResizeArraysWithRocks);
        }

        public override void Unload()
        {
            loadBannersHook = null;
        }

        public static void ResizeArraysWithRocks(orig_ResizeArrays orig, bool unloading)
        {
            FieldInfo modLoading = typeof(Mod).GetField("loading", BindingFlags.Instance | BindingFlags.NonPublic);
            if (modLoading != null)
            {
                Mod mod = ModContent.GetInstance<AAMod>();
                modLoading.SetValue(mod, true);

                mod.Logger.Info("Dynamically Adding Banners:");
                foreach (ModNPC modNPC in mod.GetContent<ModNPC>())
                {
                    if (modNPC is IBannerNPC inter)
                    {
                        mod.Logger.Info("- Attempting to add a banner to " + modNPC.Name);

                        bool success = inter.TryAddBanner(modNPC);
                        if (success)
                            mod.Logger.Info("  - Successfully added a banner");
                    }
                }

                mod.Logger.Info("- Double checking skipped NPCs");

                while (NPCsToDoubleCheck.Count > 0)
                {
                    int type = NPCsToDoubleCheck.Dequeue();
                    ModNPC me = ModContent.GetModNPC(type);
                    me.BannerItem = ModContent.GetModNPC((me as IBannerNPC).OverrideBannerNPCType).Banner;

                    mod.Logger.Info($"  - {me.Name}'s BannerItem updated from 0 to {me.BannerItem}");
                }
                
                modLoading.SetValue(mod, false);
            }
            orig(unloading);
        }
    }

    [Autoload(false)]
    public class BannerItem(string texture, string name, int bannerTile) : ModItem, ILocalizedModType
    {
        public override string LocalizationCategory => "Banners";

        public override string Name => name + "Banner";
        public override string Texture => texture;

        protected override bool CloneNewInstances => true;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(bannerTile, 0);
            Item.width = 10;
            Item.height = 24;
            Item.rare = ItemRarityID.Blue;
            Item.value = 1000;
        }
    }
    
    [Autoload(false)]
    public class BannerTile(string texture, string name) : ModBannerTile, ILocalizedModType
    {
        internal static Dictionary<int, int> TileTypeToNPC = [];
    
        public override string LocalizationCategory => "Banners";

        public override string Name => name + "Banner";
        public override string Texture => texture;
    }
}
