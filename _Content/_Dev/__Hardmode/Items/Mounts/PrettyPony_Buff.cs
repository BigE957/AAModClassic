using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Mounts
{
	public class PrettyPony_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pretty Pony");
			// Description.SetDefault("Its a horse.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<BegPony>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
