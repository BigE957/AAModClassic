using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Dusts
{
    public class MireDust : ModDust
    {
        public override void SetStaticDefaults()
        {
            UpdateType = DustID.GrassBlades;
        }
    }
}