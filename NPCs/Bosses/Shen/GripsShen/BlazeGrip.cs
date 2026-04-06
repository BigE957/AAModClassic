
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using AAModClassic.Globals;
using AAModClassic.Dusts;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Buffs;

namespace AAModClassic.NPCs.Bosses.Shen.GripsShen
{
    [AutoloadBossHead]
    public class BlazeGrip : BaseShenGrips
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grip of Blazing Fury");
            Main.npcFrameCount[NPC.type] = 14;
        }

	    public override void SetDefaults()
        {
			base.SetDefaults();
			NPC.lifeMax = 70000;
            NPC.damage = 60;
            NPC.defense = 60;
            NPC.boss = true;
            NPC.buffImmune[BuffID.OnFire] = true;

            offsetBasePoint = new Vector2(-280f, 0f);		
        }	

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0) //this make so when the npc has 0 life(dead) he will spawn this
            {
                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<AkumaDust>();
                int dust2 = ModContent.DustType<AkumaDust>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (NPC.alpha > 0)
            {
                return AAColor.Akuma;
            }
            return lightColor;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/BlazeGrip_Glow");

            int shader = 0;
            if (NPC.ai[0] == 0)
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            }
            if (NPC.ai[0] != 0 || NPC.ai[0] != 1 || NPC.ai[0] != 5)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 2, NPC.scale, 7, true, 0, 0, Color.Orange, NPC.frame);
            }
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, shader, NPC, drawColor);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, shader, NPC, Color.White);
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonFire_Buff>(), 180);
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = 0;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
