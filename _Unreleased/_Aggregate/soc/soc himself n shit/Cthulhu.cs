namespace AAModClassic._Unreleased.NPCs.Bosses.SoC
{
    /*public class Cthulhu : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cthulhu, Cosmic Calamity");

        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.alpha = 255;
            npc.damage = 0;
            music = MusicManagementSystem.MusicSlots["Cthulhu"];
            npc.lifeMax = 1500000;
            npc.dontTakeDamage = false;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.boss = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }


        public float[] shootAI = new float[4];

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();
            }
        }

        public override void AI()
        {
            BaseAI.AISpaceOctopus(npc, ref customAI, 1f, 5f, 90f, 70f, FireMagic);
            
        }

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, ModContent.ProjectileType<MagicBlast>(), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 24f, true, new Vector2(20f, 15f));
        }
    }*/
}