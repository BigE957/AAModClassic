using Terraria;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Bosses.Shen
{
    public class ShenAHitbox : ModNPC
    {
        public override string Texture => "AAModClassic/BlankTex";

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = NPC.lifeMax * 1;
            NPC.damage = (int)(NPC.damage * .8f);
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shen Doragon Awakened");
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.damage = 130;
            NPC.defense = 0;
            NPC.lifeMax = 1;
            NPC.value = 0f;
            NPC.knockBackResist = 0.0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.dontTakeDamage = true;
        }
        public override bool PreAI()
        {
            NPC.TargetClosest(true);
            int boss = (int)NPC.ai[0];
            if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != Mod.Find<ModNPC>("ShenA").Type)
            {
                NPC.active = false;
                return false;
            }
            NPC.netUpdate = true;
            NPC.position.X = Main.npc[boss].Center.X - 50;
            NPC.position.Y = Main.npc[boss].position.Y;
            return false;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}