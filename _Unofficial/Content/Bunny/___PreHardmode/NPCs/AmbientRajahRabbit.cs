using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Desert.___PreHardmode.NPCs.__Friendly;
using AAModClassic._Unofficial.Content.Desert;
using AAModClassic.Assets;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Bunny.___PreHardmode.NPCs
{
    public class AmbientRajahRabbit : ModNPC
    {
        public override string Texture => "AAModClassic/_Content/Bunny/__Hardmode/NPCs/__BossRajahRabbit/RajahRabbit";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[NPC.type] = 8;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 130;
            NPC.height = 220;
            NPC.aiStyle = -1;
            NPC.damage = 130;
            NPC.defense = 90;
            NPC.lifeMax = 65000;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("AAModClassic/Sounds/Rajah");
            NPC.value = Item.buyPrice(0, 1, 10, 0);
            NPC.dontTakeDamage = true;
            NPC.netAlways = true;
        }

        public override bool CanChat() => true;

        public override string GetChat()
        {
            AnubisDialoguePlayer p = Main.LocalPlayer.GetModPlayer<AnubisDialoguePlayer>();

            Main.BestiaryTracker.Chats.RegisterChatStartWith(ContentSamples.NpcsByNetId[ModContent.NPCType<RajahRabbit>()]);

            DialogueToUse = pool.GetTree(Main.LocalPlayer, this);

            chattingLastFrame = true;

            Main.LocalPlayer.GetModPlayer<AnubisDialoguePlayer>().ChattingAnubis = NPC.whoAmI;

            if (Main.clothesWindow)
                Main.CancelClothesWindow(true);

            if (Main.LocalPlayer.sign >= 0)
            {
                Main.LocalPlayer.sign = -1;
                Main.editSign = false;
                Main.npcChatText = "";
            }

            return "";
        }
    }
}
