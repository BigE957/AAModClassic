using Humanizer;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Linq;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace AAModClassic.UI.World
{
    public class AAWorldSelectChanges : ModSystem
    {
        public override void Load()
        {
            On_AWorldListItem.GetIcon += GetAAWorldIcon;
            On_UIWorldListItem.AddTmlElements += ModifyIconBorder;
        }

        public override void SaveWorldHeader(TagCompound tag)
        {
            if(AAWorld.downedSAncient)
                tag["DefeatedAnySuperancient"] = true;
        }

        private Asset<Texture2D> GetAAWorldIcon(On_AWorldListItem.orig_GetIcon orig, AWorldListItem self)
        {
            if (self.Data.ZenithWorld || self.Data.DrunkWorld || self.Data.ForTheWorthy || self.Data.NotTheBees || self.Data.Anniversary || self.Data.DontStarve || self.Data.RemixWorld || self.Data.NoTrapsWorld)
                return orig(self);
            else if (self.Data.WorldGenModsRecorded && self.Data.TryGetModVersionGeneratedWith("AAModClassic", out _))
                return AAMod.instance.Assets.Request<Texture2D>("UI/World/AAWorldIcon_" + (self.Data.HasCorruption ? "Corruption_" : "Crimson_") + (self.Data.IsHardMode ? "Hardmode" : "PreHardmode"), AssetRequestMode.ImmediateLoad);
            else
                return orig(self);
        }

        private void ModifyIconBorder(On_UIWorldListItem.orig_AddTmlElements orig, UIWorldListItem self, WorldFileData data, ref float offset)
        {
            if (data.DefeatedMoonlord && data.TryGetHeaderData<AAWorldSelectChanges>(out var tag) && tag.ContainsKey("DefeatedAnySuperancient"))
            {
                UIImage border = (UIImage)self.Children.First().Children.Last();
                border.SetImage(AAMod.instance.Assets.Request<Texture2D>("UI/World/AAWorldIcon_Border_PostSuperancient", AssetRequestMode.ImmediateLoad));
                border.HAlign = 0.5f;
                border.VAlign = 0.5f;
                border.Top = new StyleDimension(-10f, 0f);
                border.Left = new StyleDimension(-3f, 0f);
                border.IgnoresMouseInteraction = true;
            }

            orig(self, data, ref offset);
        }
    }
}
