using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Effects;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    public class ShenDoragonDefeat : ModNPC
    {
        public override string Texture => ModContent.GetInstance<ShenDoragonSpawn>().Texture;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discord's Defeat");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.height = 100;
            NPC.width = 444;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.alpha = 255;
            Music = MusicManagementSystem.MusicSlots["Shen_Outro"];
            NPC.boss = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (NPC.ai[1] > 240)
            {
                int i = NPCExtensions.BeenKilled<ShenDoragonA>(true) ? 1 : 0;
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ShenDoragonDeath>(), 0, i);
                NPC.active = false;
                NPC.netUpdate = true;
            }
            else
            {
                NPC.ai[1]++;
                NPC.ai[0]++;
                if (NPC.ai[0] > 4)
                {
                    NPC.ai[0] = 0;
                    SoundEngine.PlaySound(SoundID.Item124);
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 Pos = new Vector2(NPC.position.X + Main.rand.Next(0, 444), NPC.position.Y - Main.rand.Next(0, 100));
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Pos, Vector2.Zero, ModContent.ProjectileType<ShenDoragonA_DeathBoom>(), 0, 0, Main.myPlayer, Main.rand.Next(3));
                    }
                }
            }
        }
    }
}