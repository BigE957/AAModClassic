using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

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
        internal static Queue<int> NPCsToDoubleCheck = [];

        public override void OnModLoad()
        {
            Mod mod = ModContent.GetInstance<AAMod>();

            mod.Logger.Info("Dynamically Adding Banners:");

            foreach (ModNPC modNPC in mod.GetContent<ModNPC>().ToList())
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
        }
    }

    [Autoload(false)]
    public class BannerItem(string texture, string name, int bannerTile) : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Banners";
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

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.MultiTileSway[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);

            int height = ModContent.Request<Texture2D>(Texture).Height();
            int count = (int)MathF.Ceiling(height / 16f);
            TileObjectData.newTile.Height = count;

            List<int> heights = [];
            for (int i = 0; i < count; i++)
            {
                if (height >= 16)
                {
                    heights.Add(i == count - 1 ? 12 : 16);
                    height -= 16;
                }
                else
                    heights.Add(height - 2);
            }
            TileObjectData.newTile.CoordinateHeights = heights.ToArray();

            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.DrawYOffset = -2; // Draw this tile 2 pixels up, allowing the banner pole to align visually with the bottom of the tile it is anchored to.

            // This alternate placement supports placing on un-hammered platform tiles. Note how the DrawYOffset accounts for the height adjustment needed for the tile to look correctly attached.
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
            TileObjectData.newAlternate.DrawYOffset = -10;
            TileObjectData.addAlternate(0);

            TileObjectData.addTile(Type);

            DustType = -1; // No dust when mined
            AddMapEntry(new Color(13, 88, 130), Language.GetText("MapObject.Banner"));
        }
    }
}
