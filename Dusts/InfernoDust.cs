using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class InfernoDust : ModDust
    {
        public override void SetStaticDefaults()
        {
            UpdateType = DustID.GrassBlades;
        }
    }
}