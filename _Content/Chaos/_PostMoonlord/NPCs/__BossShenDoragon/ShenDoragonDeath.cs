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
    public class ShenDoragonDeath : ModNPC
    {
        public override string Texture => ModContent.GetInstance<ShenDoragonSpawn>().Texture;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discord's Death");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
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
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                NPC.active = false;
                NPC.netUpdate = true;
            }
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            NPC.Center = player.Center;

            NPC.ai[1]++;
            if (NPC.ai[0] == 0)
            {
                if (NPC.ai[1] == 180)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.First.1"), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] == 360)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.First.2"), AAColor.YamataDialogue, false);
                }

                if (NPC.ai[1] == 540)
                {
                    if(Main.netMode != NetmodeID.SinglePlayer)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.First.3.Multiplayer"), new Color(180, 41, 32), false);
                    else
                        BaseUtility.Chat(Language.GetOrRegister("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.First.3.Singleplayer").Format(Main.LocalPlayer.name), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] == 720)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.First.4"), AAColor.YamataDialogue, false);
                }

                if (NPC.ai[1] == 899)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.First.5"), AAColor.YamataDialogue, false);
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.First.5"), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] >= 900)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
                return;
            }
            else
            {
                if (NPC.ai[1] == 180)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.Repeat.1"), AAColor.YamataDialogue, false);
                }

                if (NPC.ai[1] == 360)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.Repeat.2"), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] == 540)
                {
                    if (Main.netMode != NetmodeID.SinglePlayer)
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.Repeat.3.Multiplayer"), new Color(180, 41, 32), false);
                    else
                    {
                        if(Main.LocalPlayer.Male)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.Repeat.3.Singleplayer.Male"), AAColor.YamataDialogue, false);
                        else
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.Repeat.3.Singleplayer.Female"), AAColor.YamataDialogue, false);
                    }
                }

                if (NPC.ai[1] == 720)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.Repeat.4"), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] == 899)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.Repeat.5"), AAColor.YamataDialogue, false);
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Defeat.Repeat.5"), new Color(180, 41, 32), false);
                    }
                }
                if (NPC.ai[1] >= 900)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
        }
    }
}