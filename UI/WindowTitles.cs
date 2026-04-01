using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using ReLogic.OS;

namespace AAModClassic.UI
{
    // Thanks Jasper
    public class WindowTitles : ModSystem
    {
        private static LocalizedText _AAWindowTitle;
        private static bool loaded = false;
        public override void PostSetupContent()
        {
            if (Main.dedServ)
                return;

            Main.QueueMainThreadAction(() =>
            {
                //Modified so ONLY Ancients Awakened titles appear because I'm evil >:)

                // the other method involving some terraria intrinsic function didn't work, so i'm just ignoring it
                //var vanillaTitles = Language.FindAll(new Regex("^GameTitle\\.")).ToList();
                var customTitles = Language.FindAll(new Regex("^Mods\\.AAModClassic\\.UI\\.WindowTitle\\.")).ToList();

                var allTitles = new List<LocalizedText>();
                //allTitles.AddRange(vanillaTitles);
                allTitles.AddRange(customTitles);

                _AAWindowTitle ??= allTitles[Main.rand.Next(allTitles.Count)];

                // this is what vanilla terraria does to set it's title, so i'm replicating that here
                Platform.Get<IWindowService>().SetUnicodeTitle(Main.instance.Window, _AAWindowTitle.Value);
                Platform.Get<IWindowService>().SetIcon(Main.instance.Window);

                loaded = true;
            });
        }

        public override void Unload()
        {
            if (Main.dedServ)
                return;
            Main.QueueMainThreadAction(() =>
            {
                Platform.Get<IWindowService>().SetUnicodeTitle(Main.instance.Window, Terraria.Lang.GetRandomGameTitle());
                Platform.Get<IWindowService>().SetIcon(Main.instance.Window);

                _AAWindowTitle = null;
                loaded = false;
            });
            base.Unload();
        }
    }
}
