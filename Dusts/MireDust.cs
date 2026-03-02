using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class MireDust : ModDust
    {
        public override void SetStaticDefaults()
        {
            UpdateType = DustID.GrassBlades;
        }
    }
}