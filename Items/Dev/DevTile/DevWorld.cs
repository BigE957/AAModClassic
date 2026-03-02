using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace AAMod.Items.Dev.DevTile
{
    public class DevWorld : ModSystem
	{
        public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
		{
            InvokerBookSetOK = true;
            CCBoxSetOK = true;
        }
        public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
		{
			List<string> list = new List<string>();
			if (InvokerBookSetOK) list.Add("InvokerBookSetOK");
            if (CCBoxSetOK) list.Add("CCBoxSetOK");
            TagCompound tagCompound = new TagCompound();
			tagCompound.Add("DevTileSet", list);
			return tagCompound;
        }

        public override void LoadWorldData(TagCompound tag)
		{
            IList<string> list = tag.GetList<string>("DevTileSet");
            InvokerBookSetOK = list.Contains("InvokerBookSetOK");
            CCBoxSetOK = list.Contains("CCBoxSetOK");
        }
        public static bool InvokerBookSetOK;
        public static bool CCBoxSetOK;
    }
}