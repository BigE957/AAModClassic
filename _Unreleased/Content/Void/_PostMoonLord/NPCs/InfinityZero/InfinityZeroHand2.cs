using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
    [AutoloadBossHead]
    public class InfinityZeroHand2 : InfinityZeroHand1
    {
        public override string Texture => ModContent.GetInstance<InfinityZeroHand1>().Texture;		
		
        public override void SetDefaults()
        {
			base.SetDefaults();
			leftHand = false;
        }
	}
}
