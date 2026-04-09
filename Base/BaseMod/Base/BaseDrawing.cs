using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using AAModClassic.Base.NPCs;
using AAModClassic.Base.Projectiles;
using ReLogic.Content;

namespace AAModClassic.Base.BaseMod.Base
{
    public class InterfaceLayer
    {
        public string name;
        public Action<InterfaceLayer, SpriteBatch> drawAction;
        public GameInterfaceLayer listItem;

        public bool visible
        {
            get
            {
                return (listItem == null ? false : listItem.Active);
            }
            set
            {
                if (listItem != null) listItem.Active = value;
            }
        }

        public InterfaceLayer(string n, Action<InterfaceLayer, SpriteBatch> action)
        {
            name = n;
            drawAction = action;
        }

        public void Draw()
        {
            drawAction(this, Main.spriteBatch);
        }
    }
    public class BaseDrawing
    {
        //public static ShaderContext shaderContext = new ShaderContext();
        //------------------------------------------------------//
        //----------------BASE DRAWING CLASS--------------------//
        //------------------------------------------------------//
        // Contains methods for various drawing functions, such //
        // as guns, afterimages, drawing on the player, etc.    //
        //------------------------------------------------------//
        //  Author(s): Grox the Great, Yoraiz0r                 //
        //------------------------------------------------------//

        /*
         * Returns a rectangle representing the frame on a texture.
         * 
         * pixelSpaceX/pixelSpaceY : The 'pixel space' seperating two frames in the texture on the X/Y axis, respectively.
         */
        public static Rectangle GetFrame(int currentFrame, int frameWidth, int frameHeight, int pixelSpaceX = 0, int pixelSpaceY = 2)
        {
            pixelSpaceY *= currentFrame;
            int startY = (frameHeight * currentFrame) + pixelSpaceY;
            return new Rectangle(0, startY, frameWidth - pixelSpaceX, frameHeight);
        }


        public static int GetDye(Player drawPlayer, int accSlot, bool social = false, bool wings = false)
        {
            int dye = accSlot % 10;
            if (!wings && accSlot < 10 && drawPlayer.hideVisibleAccessory[dye]) return -1;
            return GameShaders.Armor.GetShaderIdFromItemId(drawPlayer.dye[dye].type);
        }

        /*
         * Convenience method for getting lighting color with buff effects and such applied using an npc and a position. 
         * effects : if false, do not spawn dust- and light-related effects, only color the light.
         */
        public static Color GetNPCColor(NPC npc, Vector2? position = null, bool effects = true, float shadowOverride = 0f)
        {
            return npc.GetAlpha(BuffEffects(npc, GetLightColor(position != null ? (Vector2)position : npc.Center), (shadowOverride != 0f ? shadowOverride : 0f), effects, npc.poisoned, npc.onFire, npc.onFire2, Main.player[Main.myPlayer].detectCreature, false, false, false, npc.venom, npc.midas, npc.ichor, npc.onFrostBurn, false, false, npc.dripping, npc.drippingSlime, npc.loveStruck, npc.stinky));
        }

        /*
         * Convenience method for getting lighting color with buff effects and such applied using a player and a position. 
         * effects : if false, do not spawn dust- and light-related effects, only color the light.
         */
        public static Color GetPlayerColor(Player p, Vector2? position = null, bool effects = false, float shadowOverride = 0f)
        {
            return p.GetImmuneAlpha(BuffEffects(p, GetLightColor(position != null ? (Vector2)position : p.Center), (shadowOverride != 0f ? shadowOverride : 0/*p.shadow*/), effects, p.poisoned, p.onFire, p.onFire2, false, p.noItems, p.blind, p.bleed, p.venom, false, p.ichor, p.onFrostBurn, p.burned, p.honey, p.dripping, p.drippingSlime, p.loveStruck, p.stinky), 0f/*p.shadow*/);
        }

        /*
         * Convenience method for getting lighting color using an npc or projectile position.
         */
        public static Color GetLightColor(Vector2 position)
        {
            return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
        }

        /*
         * Convenience method for adding lighting using an npc or projectile position, using a Color instance for color.
         */
        public static void AddLight(Vector2 position, Color color, float brightnessDivider = 1F)
        {
            AddLight(position, color.R / 255F, color.G / 255F, color.B / 255F, brightnessDivider);
        }
        /*
         * Convenience method for adding lighting using an npc or projectile position with 0F - 1F color values.
         */
        public static void AddLight(Vector2 position, float colorR, float colorG, float colorB, float brightnessDivider = 1F)
        {
            Lighting.AddLight((int)(position.X / 16f), (int)(position.Y / 16f), colorR / brightnessDivider, colorG / brightnessDivider, colorB / brightnessDivider);
        }

        /*
         * Applies buff coloring and spawns effects based on if the effect is true or false. 
         */
        public static Color BuffEffects(Entity codable, Color lightColor, float shadow = 0f, bool effects = true, bool poisoned = false, bool onFire = false, bool onFire2 = false, bool hunter = false, bool noItems = false, bool blind = false, bool bleed = false, bool venom = false, bool midas = false, bool ichor = false, bool onFrostBurn = false, bool burned = false, bool honey = false, bool dripping = false, bool drippingSlime = false, bool loveStruck = false, bool stinky = false)
        {
            float cr = 1f; float cg = 1f; float cb = 1f; float ca = 1f;
            if (effects && honey && Main.rand.Next(30) == 0)
            {
                int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.Honey, 0f, 0f, 150, default(Color), 1f);
                Main.dust[dustID].velocity.Y = 0.3f;
                Main.dust[dustID].velocity.X *= 0.1f;
                Main.dust[dustID].scale += (float)Main.rand.Next(3, 4) * 0.1f;
                Main.dust[dustID].alpha = 100;
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].velocity += codable.velocity * 0.1f;
                //if (codable is Player) Main.playerDrawDust.Add(dustID);
            }
            if (poisoned)
            {
                if (effects && Main.rand.Next(30) == 0)
                {
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.Poisoned, 0f, 0f, 120, default(Color), 0.2f);
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].fadeIn = 1.9f;
                    //if (codable is Player) Main.playerDrawDust.Add(dustID);
                }
                cr *= 0.65f;
                cb *= 0.75f;
            }
            if (venom)
            {
                if (effects && Main.rand.Next(10) == 0)
                {
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.Venom, 0f, 0f, 100, default(Color), 0.5f);
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].fadeIn = 1.5f;
                    //if (codable is Player) Main.playerDrawDust.Add(dustID);
                }
                cg *= 0.45f;
                cr *= 0.75f;
            }
            if (midas)
            {
                cb *= 0.3f;
                cr *= 0.85f;
            }
            if (ichor)
            {
                if (codable is NPC) { lightColor = new Color(255, 255, 0, 255); } else { cb = 0f; }
            }
            if (burned)
            {
                if (effects)
                {
                    int dustID = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, DustID.Torch, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 2f);
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].velocity *= 1.8f;
                    Main.dust[dustID].velocity.Y -= 0.75f;
                    //if (codable is Player) Main.playerDrawDust.Add(dustID);
                }
                if (codable is Player)
                {
                    cr = 1f;
                    cb *= 0.6f;
                    cg *= 0.7f;
                }
            }
            if (onFrostBurn)
            {
                if (effects)
                {
                    if (Main.rand.Next(4) < 3)
                    {
                        int dustID = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, DustID.IceTorch, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                        Main.dust[dustID].noGravity = true;
                        Main.dust[dustID].velocity *= 1.8f;
                        Main.dust[dustID].velocity.Y -= 0.5f;
                        if (Main.rand.Next(4) == 0)
                        {
                            Main.dust[dustID].noGravity = false;
                            Main.dust[dustID].scale *= 0.5f;
                        }
                        //if (codable is Player) Main.playerDrawDust.Add(dustID);
                    }
                    Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 0.1f, 0.6f, 1f);
                }
                if (codable is Player)
                {
                    cr *= 0.5f;
                    cg *= 0.7f;
                }
            }
            if (onFire)
            {
                if (effects)
                {
                    if (Main.rand.Next(4) != 0)
                    {
                        int dustID = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, DustID.Torch, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                        Main.dust[dustID].noGravity = true;
                        Main.dust[dustID].velocity *= 1.8f;
                        Main.dust[dustID].velocity.Y -= 0.5f;
                        if (Main.rand.Next(4) == 0)
                        {
                            Main.dust[dustID].noGravity = false;
                            Main.dust[dustID].scale *= 0.5f;
                        }
                        //if (codable is Player) Main.playerDrawDust.Add(dustID);
                    }
                    Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
                }
                if (codable is Player)
                {
                    cb *= 0.6f;
                    cg *= 0.7f;
                }
            }
            if (dripping && shadow == 0f && Main.rand.Next(4) != 0)
            {
                Vector2 position = codable.position;
                position.X -= 2f; position.Y -= 2f;
                if (Main.rand.Next(2) == 0)
                {
                    int dustID = Dust.NewDust(position, codable.width + 4, codable.height + 2, DustID.Wet, 0f, 0f, 50, default(Color), 0.8f);
                    if (Main.rand.Next(2) == 0) Main.dust[dustID].alpha += 25;
                    if (Main.rand.Next(2) == 0) Main.dust[dustID].alpha += 25;
                    Main.dust[dustID].noLight = true;
                    Main.dust[dustID].velocity *= 0.2f;
                    Main.dust[dustID].velocity.Y += 0.2f;
                    Main.dust[dustID].velocity += codable.velocity;
                    //if(codable is Player) Main.playerDrawDust.Add(dustID);
                }
                else
                {
                    int dustID = Dust.NewDust(position, codable.width + 8, codable.height + 8, DustID.Wet, 0f, 0f, 50, default(Color), 1.1f);
                    if (Main.rand.Next(2) == 0) Main.dust[dustID].alpha += 25;
                    if (Main.rand.Next(2) == 0) Main.dust[dustID].alpha += 25;
                    Main.dust[dustID].noLight = true;
                    Main.dust[dustID].noGravity = true;
                    Main.dust[dustID].velocity *= 0.2f;
                    Main.dust[dustID].velocity.Y += 1f;
                    Main.dust[dustID].velocity += codable.velocity;
                    //if(codable is Player) Main.playerDrawDust.Add(dustID);
                }
            }
            if (drippingSlime && shadow == 0f)
            {
                int alpha = 175;
                Color newColor = new Color(0, 80, 255, 100);
                if (Main.rand.Next(4) != 0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Vector2 position2 = codable.position;
                        position2.X -= 2f; position2.Y -= 2f;
                        int dustID = Dust.NewDust(position2, codable.width + 4, codable.height + 2, DustID.TintableDust, 0f, 0f, alpha, newColor, 1.4f);
                        if (Main.rand.Next(2) == 0) Main.dust[dustID].alpha += 25;
                        if (Main.rand.Next(2) == 0) Main.dust[dustID].alpha += 25;
                        Main.dust[dustID].noLight = true;
                        Main.dust[dustID].velocity *= 0.2f;
                        Main.dust[dustID].velocity.Y += 0.2f;
                        Main.dust[dustID].velocity += codable.velocity;
                        //if(codable is Player) Main.playerDrawDust.Add(dustID);
                    }
                }
                cr *= 0.8f;
                cg *= 0.8f;
            }
            if (onFire2)
            {
                if (effects)
                {
                    if (Main.rand.Next(4) != 0)
                    {
                        int dustID = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, DustID.CursedTorch, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                        Main.dust[dustID].noGravity = true;
                        Main.dust[dustID].velocity *= 1.8f;
                        Main.dust[dustID].velocity.Y -= 0.5f;
                        if (Main.rand.Next(4) == 0)
                        {
                            Main.dust[dustID].noGravity = false;
                            Main.dust[dustID].scale *= 0.5f;
                        }
                        //if (codable is Player) Main.playerDrawDust.Add(dustID);
                    }
                    Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
                }
                if (codable is Player)
                {
                    cb *= 0.6f;
                    cg *= 0.7f;
                }
            }
            if (noItems)
            {
                cr *= 0.65f;
                cg *= 0.8f;
            }
            if (blind)
            {
                cr *= 0.7f;
                cg *= 0.65f;
            }
            if (bleed)
            {
                bool dead = (codable is Player ? ((Player)codable).dead : codable is NPC ? ((NPC)codable).life <= 0 : false);
                if (effects && !dead && Main.rand.Next(30) == 0)
                {
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.Blood, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dustID].velocity.Y += 0.5f;
                    Main.dust[dustID].velocity *= 0.25f;
                    //if (codable is Player) Main.playerDrawDust.Add(dustID);
                }
                cg *= 0.9f;
                cb *= 0.9f;
            }
            if (loveStruck && effects && shadow == 0f && Main.instance.IsActive && !Main.gamePaused && Main.rand.Next(5) == 0)
            {
                Vector2 value = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
                value.Normalize();
                value.X *= 0.66f;
                int goreID = Gore.NewGore(codable.GetSource_FromThis(), codable.position + new Vector2((float)Main.rand.Next(codable.width + 1), (float)Main.rand.Next(codable.height + 1)), value * (float)Main.rand.Next(3, 6) * 0.33f, 331, (float)Main.rand.Next(40, 121) * 0.01f);
                Main.gore[goreID].sticky = false;
                Main.gore[goreID].velocity *= 0.4f;
                Main.gore[goreID].velocity.Y -= 0.6f;
                //if (codable is Player) Main.playerDrawGore.Add(goreID);
            }
            if (stinky && shadow == 0f)
            {
                cr *= 0.7f;
                cb *= 0.55f;
                if (effects && Main.rand.Next(5) == 0 && Main.instance.IsActive && !Main.gamePaused)
                {
                    Vector2 value2 = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
                    value2.Normalize(); value2.X *= 0.66f; value2.Y = Math.Abs(value2.Y);
                    Vector2 vector = value2 * (float)Main.rand.Next(3, 5) * 0.25f;
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.FartInAJar, vector.X, vector.Y * 0.5f, 100, default(Color), 1.5f);
                    Main.dust[dustID].velocity *= 0.1f;
                    Main.dust[dustID].velocity.Y -= 0.5f;
                    //if (codable is Player) Main.playerDrawDust.Add(dustID);
                }
            }
            lightColor.R = (byte)((float)lightColor.R * cr);
            lightColor.G = (byte)((float)lightColor.G * cg);
            lightColor.B = (byte)((float)lightColor.B * cb);
            lightColor.A = (byte)((float)lightColor.A * ca);
            if (codable is NPC) NPCLoader.DrawEffects((NPC)codable, ref lightColor);
            if (hunter && (codable is NPC ? ((NPC)codable).lifeMax > 1 : true))
            {
                if (effects && !Main.gamePaused && Main.instance.IsActive && Main.rand.Next(50) == 0)
                {
                    int dustID = Dust.NewDust(codable.position, codable.width, codable.height, DustID.MagicMirror, 0f, 0f, 150, default(Color), 0.8f);
                    Main.dust[dustID].velocity *= 0.1f;
                    Main.dust[dustID].noLight = true;
                    //if (codable is Player) Main.playerDrawDust.Add(dustID);
                }
                byte colorR = 50, colorG = 255, colorB = 50;
                if (codable is NPC && !(((NPC)codable).friendly || ((NPC)codable).catchItem > 0 || (((NPC)codable).damage == 0 && ((NPC)codable).lifeMax == 5)))
                {
                    colorR = 255; colorG = 50;
                }
                if (!(codable is NPC) && lightColor.R < 150) { lightColor.A = Main.mouseTextColor; }
                if (lightColor.R < colorR) { lightColor.R = colorR; }
                if (lightColor.G < colorG) { lightColor.G = colorG; }
                if (lightColor.B < colorB) { lightColor.B = colorB; }
            }
            return lightColor;
        }

        public static bool ShouldDrawHelmet(Player drawPlayer, int itemType = -1) { return drawPlayer.head > 0 && ShouldDrawArmor(drawPlayer, 0, itemType); }
        public static bool ShouldDrawChestplate(Player drawPlayer, int itemType = -1) { return drawPlayer.body > 0 && ShouldDrawArmor(drawPlayer, 1, itemType); }
        public static bool ShouldDrawLeggings(Player drawPlayer, int itemType = -1) { return drawPlayer.legs > 0 && ShouldDrawArmor(drawPlayer, 2, itemType); }

        public static bool ShouldDrawArmor(Player drawPlayer, int armorType, int itemType = -1)
        {
            if (drawPlayer.merman || drawPlayer.wereWolf) { return false; }
            if (itemType == -1) { return (drawPlayer.armor[10 + armorType].type > ItemID.None) || (IsBlank(drawPlayer.armor[10 + armorType]) && drawPlayer.armor[0 + armorType].type > ItemID.None); }
            return (drawPlayer.armor[10 + armorType].type == itemType) || (IsBlank(drawPlayer.armor[10 + armorType]) && drawPlayer.armor[0 + armorType].type == itemType);
        }

        public static bool ShouldDrawAccessory(Player drawPlayer, int itemType)
        {
            for (int m = 3; m < 8 + drawPlayer.extraAccessorySlots; m++)
            {
                if (drawPlayer.armor[m + 10].type == itemType) return true;
                if (IsBlank(drawPlayer.armor[m + 10]) && !drawPlayer.hideVisibleAccessory[m] && (drawPlayer.armor[m].type == itemType)) return true;
            }
            return false;
        }

        private static bool IsBlank(Item item)
        {
            if (item.type <= ItemID.None || item.stack <= 0) return true;
            return string.IsNullOrEmpty(item.Name);
        }

        /*
		 * Returns true if the requirements for drawing the player's held item are satisfied.
		 */
        public static bool ShouldDrawHeldItem(Player drawPlayer)
        {
            return ShouldDrawHeldItem(drawPlayer.inventory[drawPlayer.selectedItem], drawPlayer.itemAnimation, drawPlayer.wet, drawPlayer.dead);
        }

        /*
         * Returns true if the requirements for drawing the held item are satisfied.
         * 
         * isDead : should be false, is for players mostly.
         */
        public static bool ShouldDrawHeldItem(Item item, int itemAnimation, bool isWet, bool isDead = false)
        {
            return ((itemAnimation > 0 || item.holdStyle > 0) && item.type > ItemID.None && !isDead && !item.noUseGraphic && (!isWet || !item.noWet));
        }

        /*
         * Draw a weapon in a sword-like fashion. (ie only when used, centered and rotating)
         * 
         * Returns : the value for LetDraw.
         * wepColor : weapon's tint.
         * XOffset / YOffset : Offsets the sword's position on the X/Y axis.
         */
        public static bool DrawHeldSword(object sb, int shader, Player drawPlayer, Color lightColor = default(Color), float scale = 0f, float xOffset = 0, float yOffset = 0, Rectangle? frame = null, int frameCount = 1, Texture2D overrideTex = null)
        {
            if (ShouldDrawHeldItem(drawPlayer))
            {
                Item item = drawPlayer.inventory[drawPlayer.selectedItem];
                DrawHeldSword(sb, (overrideTex != null ? overrideTex : TextureAssets.Item[item.type].Value), shader, drawPlayer.itemLocation, item, drawPlayer.direction, drawPlayer.itemRotation, scale <= 0f ? item.scale : scale, lightColor, item.color, xOffset, yOffset, drawPlayer.gravDir, drawPlayer, frame, frameCount);
                return false;
            }
            return true;
        }

        /*
         * Draw a texture in a sword-like fashion. (ie only when used, centered and rotating)
         *
         * wepColor : weapon's tint.
         * XOffset / YOffset : Offsets the sword's position on the X/Y axis.
         */
        public static void DrawHeldSword(object sb, Texture2D tex, int shader, Vector2 position, Item item, int direction, float itemRotation, float itemScale, Color lightColor = default(Color), Color wepColor = default(Color), float xOffset = 0, float yOffset = 0, float gravDir = -1f, Entity entity = null, Rectangle? frame = null, int frameCount = 1)
        {
            if (frame == null) { frame = new Rectangle(0, 0, tex.Width, tex.Height); }
            if (lightColor == default(Color)) { lightColor = GetLightColor(position); }
            xOffset *= direction;
            SpriteEffects spriteEffect = direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (gravDir == -1f) { yOffset *= -1; spriteEffect = spriteEffect | SpriteEffects.FlipVertically; }
            if (entity is Player)
            {
                Player drawPlayer = (Player)entity; yOffset -= drawPlayer.gfxOffY;
            }
            else
            if (entity is NPC)
            {
                NPC drawNPC = (NPC)entity; yOffset -= drawNPC.gfxOffY;
            }
            int drawType = item.type;

            Vector2 drawPos = position - Main.screenPosition;
            Vector2 texOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f / frameCount);
            Vector2 rotOrigin = new Vector2((texOrigin.X - texOrigin.X * (float)direction), gravDir == -1f ? 0 : tex.Height) + new Vector2(xOffset, -yOffset);

            if (gravDir == -1f) //reverse gravity
            {
                if (sb is List<DrawData>)
                {
                    DrawData dd = new DrawData(tex, drawPos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
                    dd.shader = shader;
                    ((List<DrawData>)sb).Add(dd);
                }
                else
                if (sb is SpriteBatch) ((SpriteBatch)sb).Draw(tex, drawPos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);

                if (wepColor != default(Color))
                {
                    if (sb is List<DrawData>)
                    {
                        DrawData dd = new DrawData(tex, drawPos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
                        dd.shader = shader;
                        ((List<DrawData>)sb).Add(dd);
                    }
                    else
                    if (sb is SpriteBatch) ((SpriteBatch)sb).Draw(tex, drawPos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
                }
            }
            else //normal gravity
            {
                if (drawType == 425 || drawType == 507)
                {
                    if (direction == 1) { spriteEffect = SpriteEffects.FlipVertically; } else { spriteEffect = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically); }
                }
                if (sb is List<DrawData>)
                {
                    DrawData dd = new DrawData(tex, drawPos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
                    dd.shader = shader;
                    ((List<DrawData>)sb).Add(dd);
                }
                else
                if (sb is SpriteBatch) ((SpriteBatch)sb).Draw(tex, drawPos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);

                if (wepColor != default(Color))
                {
                    if (sb is List<DrawData>)
                    {
                        DrawData dd = new DrawData(tex, drawPos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
                        dd.shader = shader;
                        ((List<DrawData>)sb).Add(dd);
                    }
                    else
                    if (sb is SpriteBatch) ((SpriteBatch)sb).Draw(tex, drawPos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
                }
            }
        }

        public static bool DrawHeldGun(object sb, int shader, Player drawPlayer, Color lightColor = default(Color), float scale = 0f, float xOffset = 0, float yOffset = 0, bool shakeX = false, bool shakeY = false, float shakeScalarX = 1.0f, float shakeScalarY = 1.0f, Rectangle? frame = null, int frameCount = 1, Texture2D overrideTex = null)
        {
            if (ShouldDrawHeldItem(drawPlayer))
            {
                Item item = drawPlayer.inventory[drawPlayer.selectedItem];
                DrawHeldGun(sb, (overrideTex != null ? overrideTex : TextureAssets.Item[item.type].Value), shader, drawPlayer.itemLocation, item, drawPlayer.direction, drawPlayer.itemRotation, scale <= 0f ? item.scale : scale, lightColor, item.color, xOffset, yOffset, shakeX, shakeY, shakeScalarX, shakeScalarY, drawPlayer.gravDir, drawPlayer, frame, frameCount);
                return false;
            }
            return true;
        }

        /*
         * Draws a texture in a gun-like fashion. (ie only when used and in the direction of the cursor)
         * 
         * direction : the direction the sprite should point. (-1 for left, 1 for right)
         * itemRotation : Rotation of the item.
         * itemScale : Scale of the item.
         * lightColor : color of the light the weapon is at.
         * wepColor : weapon's tint.
         * XOffset / YOffset : Offsets the gun's position on the X/Y axis.
         * shakeX / shakeY : If true, shakes the sprite on the X/Y axis.
         * shakeScaleX / shakeScaleY : If shakeX/shakeY is true, this scales the amount it shakes by.
         * gravDir : the direction of gravity.
         * entity : If drawing for a player or npc, the instance of them. (can be null)
         */
        public static void DrawHeldGun(object sb, Texture2D tex, int shader, Vector2 position, Item item, int direction, float itemRotation, float itemScale, Color lightColor = default(Color), Color wepColor = default(Color), float xOffset = 0, float yOffset = 0, bool shakeX = false, bool shakeY = false, float shakeScalarX = 1.0f, float shakeScalarY = 1.0f, float gravDir = 1f, Entity entity = null, Rectangle? frame = null, int frameCount = 1)
        {
            if (frame == null) { frame = new Rectangle(0, 0, tex.Width, tex.Height); }
            if (lightColor == default(Color)) { lightColor = GetLightColor(position); }
            SpriteEffects spriteEffect = direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (gravDir == -1f) { yOffset *= -1; spriteEffect = spriteEffect | SpriteEffects.FlipVertically; }
            int type = item.type;
            int fakeType = type;
            Vector2 texOrigin = new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2) / frameCount);
            if (entity is Player)
            {
                Player drawPlayer = (Player)entity; yOffset += drawPlayer.gfxOffY;
            }
            else
            if (entity is NPC)
            {
                NPC drawNPC = (NPC)entity; yOffset += drawNPC.gfxOffY;
            }
            Vector2 rotOrigin = new Vector2(-(float)xOffset, ((float)(tex.Height / 2) / frameCount) - yOffset);
            if (direction == -1)
            {
                rotOrigin = new Vector2((float)(tex.Width + xOffset), ((float)(tex.Height / 2) / frameCount) - yOffset);
            }
            Vector2 pos = new Vector2((float)((int)(position.X - Main.screenPosition.X + texOrigin.X)), (float)((int)(position.Y - Main.screenPosition.Y + texOrigin.Y)));

            if (shakeX) { pos.X += shakeScalarX * (Main.rand.Next(-5, 6) / 9f); }
            if (shakeY) { pos.Y += shakeScalarY * (Main.rand.Next(-5, 6) / 9f); }

            if (sb is List<DrawData>)
            {
                DrawData dd = new DrawData(tex, pos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
                dd.shader = shader;
                ((List<DrawData>)sb).Add(dd);
            }
            else
            if (sb is SpriteBatch) ((SpriteBatch)sb).Draw(tex, pos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);

            if (wepColor != default(Color))
            {
                if (sb is List<DrawData>)
                {
                    DrawData dd = new DrawData(tex, pos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
                    dd.shader = shader;
                    ((List<DrawData>)sb).Add(dd);
                }
                else
                if (sb is SpriteBatch) ((SpriteBatch)sb).Draw(tex, pos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
            }
            try { if (type != fakeType) { item.type = type; } }
            catch { }
        }

        /*
         * Draws the given texture in a spear-like fashion (texture is oriented at the upper-right corner) using the projectile provided.
         */
        public static void DrawProjectileSpear(object sb, Texture2D texture, int shader, Projectile p, Color? overrideColor = null, float offsetX = 0f, float offsetY = 0f)
        {
            offsetX += (-texture.Width * 0.5f);
            Color lightColor = overrideColor != null ? (Color)overrideColor : p.GetAlpha(GetLightColor(Main.player[p.owner].Center));
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            offsetY -= Main.player[p.owner].gfxOffY;
            Vector2 offset = BaseUtility.RotateVector(p.Center, p.Center + new Vector2(p.direction == -1 ? offsetX : offsetY, p.direction == 1 ? offsetX : offsetY), p.rotation - 2.355f) - p.Center;
            if (sb is List<DrawData>)
            {
                DrawData dd = new DrawData(texture, p.Center - Main.screenPosition + offset, new Rectangle(0, 0, texture.Width, texture.Height), lightColor, p.rotation, origin, p.scale, p.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                dd.shader = shader;
                ((List<DrawData>)sb).Add(dd);
            }
            else
            if (sb is SpriteBatch) ((SpriteBatch)sb).Draw(texture, p.Center - Main.screenPosition + offset, new Rectangle(0, 0, texture.Width, texture.Height), lightColor, p.rotation, origin, p.scale, p.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        public static void DrawAura(object sb, Texture2D texture, int shader, Entity codable, float auraPercent, float distanceScalar = 1f, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
        {
            int frameCount = (codable is NPC ? Main.npcFrameCount[((NPC)codable).type] : 1);
            Rectangle frame = (codable is NPC ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Height, texture.Width));
            float scale = (codable is NPC ? ((NPC)codable).scale : ((Projectile)codable).scale);
            float rotation = (codable is NPC ? ((NPC)codable).rotation : ((Projectile)codable).rotation);
            int spriteDirection = (codable is NPC ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection);
            float offsetY2 = (codable is NPC ? ((NPC)codable).gfxOffY : 0f);
            DrawAura(sb, texture, shader, codable.position + new Vector2(0f, offsetY2), codable.width, codable.height, auraPercent, distanceScalar, scale, rotation, spriteDirection, frameCount, frame, offsetX, offsetY, overrideColor);
        }

        public static void DrawAura(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float auraPercent, float distanceScalar = 1f, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default(Rectangle), float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
        {
            Color lightColor = overrideColor != null ? (Color)overrideColor : GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
            float percentHalf = auraPercent * 5f * distanceScalar;
            float percentLight = MathHelper.Lerp(0.8f, 0.2f, auraPercent);
            lightColor.R = (byte)(lightColor.R * percentLight);
            lightColor.G = (byte)(lightColor.G * percentLight);
            lightColor.B = (byte)(lightColor.B * percentLight);
            lightColor.A = (byte)(lightColor.A * percentLight);
            Vector2 position2 = position;
            for (int m = 0; m < 4; m++)
            {
                float offX = offsetX;
                float offY = offsetY;
                switch (m)
                {
                    case 0: offX += percentHalf; break;
                    case 1: offX -= percentHalf; break;
                    case 2: offY += percentHalf; break;
                    case 3: offY -= percentHalf; break;
                }
                position2 = new Vector2(position.X + offX, position.Y + offY);
                DrawTexture(sb, texture, shader, position2, width, height, scale, rotation, direction, framecount, frame, lightColor);
            }
        }

        /*
         * Draws the given texture multiple times with each one being farther away and more faded depending on velocity.
         * Uses a Entity(NPC/Projectile) for width, height, position, rotation, sprite direction, and velocity. If an npc, also uses framecount and frame.
         */
        public static void DrawAfterimage(object sb, Texture2D texture, int shader, Entity codable, float distanceScalar = 1.0F, float sizeScalar = 1.0f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null, Rectangle? overrideFrame = null, int overrideFrameCount = 0)
        {
            int frameCount = (overrideFrameCount > 0 ? overrideFrameCount : codable is NPC ? Main.npcFrameCount[((NPC)codable).type] : 1);
            Rectangle frame = (overrideFrame != null ? (Rectangle)overrideFrame : codable is NPC ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height));
            float scale = (codable is NPC ? ((NPC)codable).scale : ((Projectile)codable).scale);
            float rotation = (codable is NPC ? ((NPC)codable).rotation : ((Projectile)codable).rotation);
            int spriteDirection = (codable is NPC ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection);
            Vector2[] velocities = new Vector2[] { codable.velocity };
            if (useOldPos)
            {
                velocities = (codable is NPC ? ((NPC)codable).oldPos : ((Projectile)codable).oldPos);
            }
            float offsetY2 = (codable is NPC ? ((NPC)codable).gfxOffY : 0f);
            DrawAfterimage(sb, texture, shader, codable.position + new Vector2(0f, offsetY2), codable.width, codable.height, velocities, scale, rotation, spriteDirection, frameCount, frame, distanceScalar, sizeScalar, imageCount, useOldPos, offsetX, offsetY, overrideColor);
        }

        /*
         * Draws the given texture multiple times with each one being farther away and more faded depending on velocity.
         * 
         * oldPoints : an array of points used to draw the afterimage.
         * distanceScalar : How far away from each other each image is.
         * sizeScalar : the amount to scale by for each image. (NOTE: this is ADDITIVE!)
         * fullbright : If the images are fullbright or not.
         * alphaAmt : The amount of alpha to subtract with each image. (0-255)
         * imageCount : How many images to draw.
         * useOldPos : If true, considers the given array as old positions instead of old oldPoints.
         */
        public static void DrawAfterimage(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, Vector2[] oldPoints, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default(Rectangle), float distanceScalar = 1.0F, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
        {
            Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / framecount / 2));
            Color lightColor = overrideColor != null ? (Color)overrideColor : GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
            Vector2 velAddon = default(Vector2);
            Vector2 originalpos = position;
            Vector2 offset = new Vector2(offsetX, offsetY);
            for (int m = 1; m <= imageCount; m++)
            {
                scale *= sizeScalar;
                Color newLightColor = lightColor;
                newLightColor.R = (byte)(newLightColor.R * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.G = (byte)(newLightColor.G * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.B = (byte)(newLightColor.B * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.A = (byte)(newLightColor.A * (imageCount + 3 - m) / (imageCount + 9));
                if (useOldPos)
                {
                    position = Vector2.Lerp(originalpos, (m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1]), distanceScalar);
                    DrawTexture(sb, texture, shader, position + offset, width, height, scale, rotation, direction, framecount, frame, newLightColor);
                }
                else
                {
                    Vector2 velocity = (m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1]);
                    velAddon += velocity * distanceScalar;
                    DrawTexture(sb, texture, shader, position + offset - velAddon, width, height, scale, rotation, direction, framecount, frame, newLightColor);
                }
            }
        }

        public static void DrawChain(object sb, Texture2D texture, int shader, Vector2 start, Vector2 end, float Jump = 0f, Color? overrideColor = null, float scale = 1f, bool drawEndsUnder = false, Func<Texture2D, Vector2, Vector2, Vector2, Rectangle, Color, float, float, int, bool> OnDrawTex = null)
        {
            DrawChain(sb, new Texture2D[] { texture, texture, texture }, shader, start, end, Jump, overrideColor, scale, drawEndsUnder, OnDrawTex);
        }

        //code written by Yoraiz0r, heavily edited by GroxTheGreat
        /*
         * Draws a chain from the start position to the end position using the texture provided.
         * 
         * textures : an array of 3 textures: the 'start' texture, the segment texture and the 'end' texture.
         * start : the starting point of the chain.
         * end : the ending point of the chain.
         * Jump : The amount to 'jump' to draw the next piece of chain. If -1, will use the texture height.
         * overrideColor : the color to draw the chain with.
         * scale : the scalar of the chain.
         * drawEndsUnder : If true, the end textures will be drawn under the segment texture. Otherwise, drawn above it.
         * OnDrawTex : If not null, called when the chain draws a texture. Return true to draw the chain piece, false to not draw it. Parameters, in order:
         *             1 - The texture.
         *             2 - The world position of the chain.
         *             3 - The draw position of the chain.
         *             4 - The center of the texture.
         *             5 - The frame of the texture being used.
         *             6 - The color the texture is being drawn.
         *             7 - The rotation of the chain.
         *             8 - The scale of the chain.
         *             9 - The count of this chain piece in the entire thing. (-1 for start tex, -2 for end tex)
         */
        public static void DrawChain(object sb, Texture2D[] textures, int shader, Vector2 start, Vector2 end, float Jump = 0f, Color? overrideColor = null, float scale = 1f, bool drawEndsUnder = false, Func<Texture2D, Vector2, Vector2, Vector2, Rectangle, Color, float, float, int, bool> OnDrawTex = null)
        {
            if (Jump <= 0f)
                Jump = (textures[1].Height - 2f) * scale;
            Vector2 dir = end - start;
            dir.Normalize();
            float length = Vector2.Distance(start, end);
            float Way = 0f;
            float rotation = BaseUtility.RotationTo(start, end) - 1.57f;
            int texID = 0;
            int maxTextures = textures.Length - 2;
            int currentChain = 0;
            int iterCap = 100;
            int iters = 0;

            while (Way < length)
            {
                if (iters++ > iterCap)
                    break;

                float texWidth;
                float texHeight;
                Vector2 texCenter;
                Vector2 v;
                Color lightColor;
                Action drawEnds = () =>
                {
                    if (textures[0] != null && Way == 0f)
                    {
                        float texWidth2 = (float)textures[0].Width;
                        float texHeight2 = (float)textures[0].Height;
                        Vector2 texCenter2 = new Vector2(texWidth2 / 2f, texHeight2 / 2f) * scale;
                        Vector2 v2 = start - Main.screenPosition + texCenter2;
                        Color lightColor2 = (overrideColor != null ? (Color)overrideColor : GetLightColor(start + texCenter2));
                        if (OnDrawTex != null && !OnDrawTex(textures[0], start + texCenter2, v2 - texCenter2, texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, scale, -1)) { }
                        else
                        {
                            if (sb is List<DrawData>)
                            {
                                DrawData dd = new DrawData(textures[0], v2 - texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0);
                                dd.shader = shader;
                                ((List<DrawData>)sb).Add(dd);
                            }
                            else
                            if (sb is SpriteBatch)
                            {
                                ((SpriteBatch)sb).Draw(textures[0], v2 - texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0);
                            }
                        }
                    }
                    if (textures[maxTextures + 1] != null && Way + Jump >= length)
                    {
                        float texWidth2 = (float)textures[maxTextures + 1].Width;
                        float texHeight2 = (float)textures[maxTextures + 1].Height;
                        Vector2 texCenter2 = new Vector2(texWidth2 / 2f, texHeight2 / 2f) * scale;
                        Vector2 v2 = end - Main.screenPosition + texCenter2;
                        Color lightColor2 = (overrideColor != null ? (Color)overrideColor : GetLightColor(end + texCenter2));
                        if (OnDrawTex != null && !OnDrawTex(textures[maxTextures + 1], end + texCenter2, v2 - texCenter2, texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, scale, -2)) { }
                        else
                        {
                            if (sb is List<DrawData>)
                            {
                                DrawData dd = new DrawData(textures[maxTextures + 1], v2 - texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0);
                                dd.shader = shader;
                                ((List<DrawData>)sb).Add(dd);
                            }
                            else
                            if (sb is SpriteBatch)
                            {
                                ((SpriteBatch)sb).Draw(textures[maxTextures + 1], v2 - texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0);
                            }
                        }
                    }
                };
                texWidth = (float)textures[1].Width;
                texHeight = (float)textures[1].Height;
                texCenter = new Vector2(texWidth / 2f, texHeight / 2f) * scale;

                v = (start + dir * Way) + texCenter;
                if (InDrawZone(v))
                {
                    v -= Main.screenPosition;
                    if ((Way == 0f || Way + Jump >= length) && drawEndsUnder) { drawEnds(); }
                    lightColor = (overrideColor != null ? (Color)overrideColor : GetLightColor((start + dir * Way) + texCenter));
                    texID++;
                    if (texID >= maxTextures) { texID = 0; }
                    if (OnDrawTex != null && !OnDrawTex(textures[texID + 1], (start + dir * Way) + texCenter, v - texCenter, texCenter, new Rectangle(0, 0, (int)texWidth, (int)texHeight), lightColor, rotation, scale, currentChain)) { }
                    else
                    {
                        if (sb is List<DrawData>)
                        {
                            DrawData dd = new DrawData(textures[texID + 1], v - texCenter, new Rectangle(0, 0, (int)texWidth, (int)texHeight), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0);
                            dd.shader = shader;
                            ((List<DrawData>)sb).Add(dd);
                        }
                        else
                        if (sb is SpriteBatch)
                        {
                            ((SpriteBatch)sb).Draw(textures[texID + 1], v - texCenter, new Rectangle(0, 0, (int)texWidth, (int)texHeight), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0);
                        }
                    }
                    currentChain++;
                    if ((Way == 0f || Way + Jump >= length) && !drawEndsUnder) { drawEnds(); }
                }
                Way += Jump;
            }
        }

        /*
         * Draws the given texture using the override color.
         * Uses a Entity for width, height, position, rotation, and sprite direction.
         */
        public static void DrawTexture(object sb, Texture2D texture, int shader, Entity codable, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
        {
            DrawTexture(sb, texture, shader, codable, 1, overrideColor, drawCentered, overrideOrigin);
        }

        /*
         * Draws the given texture using the override color.
         * Uses a Entity for width, height, position, rotation, and sprite direction.
         */
        public static void DrawTexture(object sb, Texture2D texture, int shader, Entity codable, int framecountX, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
        {
            Color lightColor = (overrideColor != null ? (Color)overrideColor : codable is Item ? ((Item)codable).GetAlpha(GetLightColor(codable.Center)) : codable is NPC ? GetNPCColor(((NPC)codable), codable.Center, false) : codable is Projectile ? ((Projectile)codable).GetAlpha(GetLightColor(codable.Center)) : GetLightColor(codable.Center));
            int frameCount = (codable is Item ? 1 : codable is NPC ? Main.npcFrameCount[((NPC)codable).type] : 1);
            Rectangle frame = (codable is NPC ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height));
            float scale = (codable is Item ? ((Item)codable).scale : codable is NPC ? ((NPC)codable).scale : ((Projectile)codable).scale);
            float rotation = (codable is Item ? 0 : codable is NPC ? ((NPC)codable).rotation : ((Projectile)codable).rotation);
            int spriteDirection = (codable is Item ? 1 : codable is NPC ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection);
            float offsetY = (codable is NPC ? ((NPC)codable).gfxOffY : 0f);
            DrawTexture(sb, texture, shader, codable.position + new Vector2(0f, offsetY), codable.width, codable.height, scale, rotation, spriteDirection, frameCount, framecountX, frame, lightColor, drawCentered, overrideOrigin);
        }

        public static void DrawTexture(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float scale, float rotation, int direction, int framecount, Rectangle frame, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
        {
            DrawTexture(sb, texture, shader, position, width, height, scale, rotation, direction, framecount, 1, frame, overrideColor, drawCentered, overrideOrigin);
        }

        /*
         * Draws the given texture using lighting nearby, or the overriden color given.
         */
        public static void DrawTexture(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float scale, float rotation, int direction, int framecount, int framecountX, Rectangle frame, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
        {
            Vector2 origin = overrideOrigin != default(Vector2) ? overrideOrigin : new Vector2((float)(frame.Width / framecountX / 2), (float)(texture.Height / framecount / 2));
            Color lightColor = overrideColor != null ? (Color)overrideColor : GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
            if (sb is List<DrawData>)
            {
                DrawData dd = new DrawData(texture, GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, frame, framecount, framecountX, scale, drawCentered), frame, lightColor, rotation, origin, scale, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                dd.shader = shader;
                ((List<DrawData>)sb).Add(dd);
            }
            else if (sb is SpriteBatch)
            {
                bool applyDye = shader > 0;
                if (applyDye)
                {
                    ((SpriteBatch)sb).End();
                    ((SpriteBatch)sb).Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);
                }
                ((SpriteBatch)sb).Draw(texture, GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, frame, framecount, framecountX, scale, drawCentered), frame, lightColor, rotation, origin, scale, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                if (applyDye)
                {
                    ((SpriteBatch)sb).End();
                    ((SpriteBatch)sb).Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                }
            }
        }

        public static void DrawTileTexture(SpriteBatch sb, Texture2D texture, int x, int y, bool slopeDraw = true, bool flipTex = false, bool ignoreHalfBricks = false, bool? overrideHalfBrick = null, Func<Color, Color> overrideColor = null, Vector2 offset = default(Vector2))
        {
            Tile tile = Main.tile[x, y];
            int frameX = (tile != null && tile.HasTile ? tile.TileFrameX : 0);
            int frameY = (tile != null && tile.HasTile ? tile.TileFrameY : 0);
            DrawTileTexture(sb, texture, x, y, 16, 16, frameX, frameY, slopeDraw, flipTex, ignoreHalfBricks, overrideHalfBrick, overrideColor, offset);
        }

        /*
		 * Draws the given texture using a tile and the lighting nearby, or the overrideColor.
		 * 
		 * x/y : the x/y cooridinates of the tile.
		 * fwidth/fheight : The width and height of one tile. (usually this is 16)
		 * slopeDraw : If true, draw slopelike textures if the tile is sloped.
		 * flipTex : If true, flips the texture on the X axis.
		 * ignoreHalfBricks : If true, ignores half bricks and will not merge with them. THIS IS NOT NORMAL BEHAVIOR!
		 * overrideHalfBrick : If not null, overrides wether or not this is a halfbrick tile by the value given.
		 * overrideColor : An override color used to override the color drawn with.
		 */
        public static void DrawTileTexture(SpriteBatch sb, Texture2D texture, int x, int y, int fwidth = 16, int fheight = 16, int frameX = 0, int frameY = 0, bool slopeDraw = true, bool flipTex = false, bool ignoreHalfBricks = false, bool? overrideHalfBrick = null, Func<Color, Color> overrideColor = null, Vector2 offset = default(Vector2))
        {
            Tile tile = Main.tile[x, y];
            //if(!tile.active()){ return; }
            bool halfBrick = (overrideHalfBrick != null ? (bool)overrideHalfBrick : tile.IsHalfBlock);
            int halfBrickOffset = halfBrick ? 8 : 0;
            Color color = Lighting.GetColor(x, y);
            Vector2 drawOffset = (Main.drawToScreen ? default(Vector2) : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange)) + offset;
            if (tile.IsActuated) { color = color.MultiplyRGB(Color.Gray); } //tile.actColor(color)
            SpriteEffects effects = (flipTex ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            Vector2 drawPos = GetTileDrawPosition(x, y, fwidth, fheight, drawOffset);
            int gfxCheck = (int)(255f * (1f - Main.gfxQuality) + 30f * Main.gfxQuality);
            int gfxCheck2 = (int)(50f * (1f - Main.gfxQuality) + 2f * Main.gfxQuality);
            if (slopeDraw && tile.Slope > 0) //slopes
            {
                bool rightSlope = tile.RightSlope;
                bool topSlope = tile.TopSlope;
                for (int m = 0; m < 8; m++)
                {
                    int xOffset = (rightSlope ? (m * 2) : (16 - m * 2 - 2));
                    int yOffset = (topSlope ? (m * 2) : 0);
                    int frameOffsetX = xOffset;
                    int height = 14 - m * 2;
                    sb.Draw(texture, drawPos + new Vector2(xOffset, yOffset), new Rectangle(frameX + frameOffsetX, frameY, 2, height), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                }
                if (topSlope) sb.Draw(texture, drawPos + new Vector2(0f, 14f), new Rectangle(frameX, frameY + 14, 16, 2), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                else sb.Draw(texture, drawPos, new Rectangle(frameX, frameY, 16, 2), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
            }
            else //sidebricks
            if (!ignoreHalfBricks && Main.tileSolid[(int)tile.TileType] && !halfBrick && (Main.tile[x - 1, y].IsHalfBlock || Main.tile[x + 1, y].IsHalfBlock))
            {
                if (Main.tile[x - 1, y].IsHalfBlock && Main.tile[x + 1, y].IsHalfBlock)
                {
                    sb.Draw(texture, drawPos + new Vector2(0f, 8f), new Rectangle(frameX, frameY + 8, fwidth, 8), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                    sb.Draw(texture, drawPos, new Rectangle(126, 0, 16, 8), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                }
                else
                if (Main.tile[x - 1, y].IsHalfBlock)
                {
                    sb.Draw(texture, drawPos + new Vector2(0f, 8f), new Rectangle(frameX, frameY + 8, fwidth, 8), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                    sb.Draw(texture, drawPos + new Vector2(4f, 0f), new Rectangle(frameX + 4, frameY, fwidth - 4, fheight), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                    sb.Draw(texture, drawPos, new Rectangle(126, 0, 4, 8), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                }
                else
                if (Main.tile[x + 1, y].IsHalfBlock)
                {
                    sb.Draw(texture, drawPos + new Vector2(0f, 8f), new Rectangle(frameX, frameY + 8, fwidth, 8), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                    sb.Draw(texture, drawPos, new Rectangle(frameX, frameY, fwidth - 4, fheight), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                    sb.Draw(texture, drawPos + new Vector2(12f, 0f), new Rectangle(138, 0, 4, 8), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                }
                else
                {
                    sb.Draw(texture, drawPos, new Rectangle(frameX, frameY, fwidth, fheight), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                }
            }
            else
            if (Lighting.LegacyEngine.Mode < 2 && Main.tileSolid[(int)tile.TileType] && !halfBrick && !tile.IsActuated)
            {
                if ((int)color.R > gfxCheck || (double)color.G > (double)gfxCheck * 1.1 || (double)color.B > (double)gfxCheck * 1.2)
                {
                    Color[] lightArray = new Color[9];
                    Lighting.GetColor9Slice(x, y, ref lightArray);
                    for (int m = 0; m < 9; m++)
                    {
                        int offsetX = 0;
                        int offsetY = 0;
                        int width = 4;
                        int height = 4;
                        Color mixedrawColor = color;
                        Color lightColor = lightArray[m];
                        if (m == 1) { width = 8; offsetX = 4; }
                        else
                        if (m == 2) { offsetX = 12; }
                        else
                        if (m == 3) { height = 8; offsetY = 4; }
                        else
                        if (m == 4) { width = 8; height = 8; offsetX = 4; offsetY = 4; }
                        else
                        if (m == 5) { offsetX = 12; offsetY = 4; height = 8; }
                        else
                        if (m == 6) { offsetY = 12; }
                        else
                        if (m == 7) { width = 8; height = 4; offsetX = 4; offsetY = 12; }
                        else
                        if (m == 8) { offsetX = 12; offsetY = 12; }
                        mixedrawColor.R = (byte)((color.R + lightColor.R) / 2);
                        mixedrawColor.G = (byte)((color.G + lightColor.G) / 2);
                        mixedrawColor.B = (byte)((color.B + lightColor.B) / 2);
                        sb.Draw(texture, drawPos + new Vector2(offsetX, offsetY), new Rectangle(frameX + offsetX, frameY + offsetY, width, height), (overrideColor != null ? overrideColor(mixedrawColor) : mixedrawColor), 0f, default(Vector2), 1f, effects, 0f);
                    }
                }
                else
                if ((int)color.R > gfxCheck2 || (double)color.G > (double)gfxCheck2 * 1.1 || (double)color.B > (double)gfxCheck2 * 1.2)
                {
                    Color[] lightArray = new Color[4];
                    Lighting.GetColor4Slice(x, y, ref lightArray);
                    for (int m = 0; m < 4; m++)
                    {
                        int offsetX = 0;
                        int offsetY = 0;
                        Color mixedrawColor = color;
                        Color lightColor = lightArray[m];
                        if (m == 1) { offsetX = 8; }
                        if (m == 2) { offsetY = 8; }
                        if (m == 3) { offsetX = 8; offsetY = 8; }
                        mixedrawColor.R = (byte)((color.R + lightColor.R) / 2);
                        mixedrawColor.G = (byte)((color.G + lightColor.G) / 2);
                        mixedrawColor.B = (byte)((color.B + lightColor.B) / 2);
                        sb.Draw(texture, drawPos + new Vector2(offsetX, offsetY), new Rectangle(frameX + offsetX, frameY + offsetY, 8, 8), (overrideColor != null ? overrideColor(mixedrawColor) : mixedrawColor), 0f, default(Vector2), 1f, effects, 0f);
                    }
                }
                else
                {
                    sb.Draw(texture, drawPos, new Rectangle(frameX, frameY, fwidth, fheight), color, 0f, default(Vector2), 1f, effects, 0f);
                }
            }
            else
            if (halfBrickOffset == 8 && (!Main.tile[x, y + 1].HasTile || !Main.tileSolid[(int)Main.tile[x, y + 1].TileType] || Main.tile[x, y + 1].IsHalfBlock))
            {
                sb.Draw(texture, drawPos + new Vector2(0, halfBrickOffset), new Rectangle(frameX, frameY, fwidth, fheight - halfBrickOffset - 4), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
                sb.Draw(texture, drawPos + new Vector2(0, 12f), new Rectangle(144, 66, fwidth, 4), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
            }
            else
            {
                sb.Draw(texture, drawPos + new Vector2(0, halfBrickOffset), new Rectangle(frameX, frameY, fwidth, fheight), (overrideColor != null ? overrideColor(color) : color), 0f, default(Vector2), 1f, effects, 0f);
            }
        }

        public static void DrawWallTexture(SpriteBatch sb, Texture2D texture, int x, int y, bool drawOutline = false, Func<Color, Color> overrideColor = null, Vector2 offset = default(Vector2))
        {
            Tile tile = Main.tile[x, y];
            bool hasWall = tile != null && tile.WallType > WallID.None;
            int wallFrameX = (hasWall ? tile.WallFrameX : 0);
            int wallFrameY = (hasWall ? tile.WallFrameY : 0);
            int frameOffsetY = (hasWall ? (int)(Main.wallFrame[tile.WallType] * 180) : 0);
            DrawWallTexture(sb, texture, x, y, wallFrameX, wallFrameY, frameOffsetY, drawOutline, overrideColor, offset);
        }

        /*
		 * Draws the given texture using a wall and the lighting nearby, or the overrideColor.
		 * 
		 * x/y : the x/y cooridinates of the wall.
		 * drawOutline : If true, draws the merging outline when two wall types meet.
		 * overrideColor : An override color used to override the color drawn with.
		 */
        public static void DrawWallTexture(SpriteBatch sb, Texture2D texture, int x, int y, int wallFrameX, int wallFrameY, int frameOffsetY, bool drawOutline = false, Func<Color, Color> overrideColor = null, Vector2 offset = default(Vector2))
        {
            int gfxCheck = (int)(255f * (1f - Main.gfxQuality) + 100f * Main.gfxQuality);
            int gfxCheck2 = (int)(120f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
            Vector2 drawOffset = (Main.drawToScreen ? default(Vector2) : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange)) + offset;
            int tileColor = (int)((Main.tileColor.R + Main.tileColor.G + Main.tileColor.B) / 3);
            float tileColorFloat = (float)((double)tileColor * 0.53) / 255f;
            if (Lighting.LegacyEngine.Mode == 2) { tileColorFloat = (float)(Main.tileColor.R - 12) / 255f; }
            else
            if (Lighting.LegacyEngine.Mode == 3) { tileColorFloat = (float)(tileColor - 12) / 255f; }
            Color color = (overrideColor != null ? overrideColor(default(Color)) : Lighting.GetColor(x, y));
            if (Lighting.LegacyEngine.Mode < 2)
            {
                if ((int)color.R > gfxCheck || (double)color.G > (double)gfxCheck * 1.1 || (double)color.B > (double)gfxCheck * 1.2)
                {
                    Color[] lightArray = new Color[9];
                    Lighting.GetColor9Slice(x, y, ref lightArray);
                    for (int m = 0; m < 9; m++)
                    {
                        int offsetX = 0;
                        int offsetY = 0;
                        int width = 12;
                        int height = 12;
                        Color color2 = color;
                        Color color3 = lightArray[m];
                        if (m == 1) { width = 8; offsetX = 12; }
                        if (m == 2) { offsetX = 20; }
                        if (m == 3) { height = 8; offsetY = 12; }
                        if (m == 4) { width = 8; height = 8; offsetX = 12; offsetY = 12; }
                        if (m == 5) { offsetX = 20; offsetY = 12; height = 8; }
                        if (m == 6) { offsetY = 20; }
                        if (m == 7) { width = 12; offsetX = 12; offsetY = 20; }
                        if (m == 8) { offsetX = 20; offsetY = 20; }
                        color2.R = (byte)((color.R + color3.R) / 2);
                        color2.G = (byte)((color.G + color3.G) / 2);
                        color2.B = (byte)((color.B + color3.B) / 2);
                        sb.Draw(texture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8 + offsetX), (float)(y * 16 - (int)Main.screenPosition.Y - 8 + offsetY)) + drawOffset, new Rectangle(wallFrameX + offsetX, wallFrameY + offsetY + frameOffsetY, width, height), (overrideColor != null ? overrideColor(color2) : color2), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    }
                }
                else
                if ((int)color.R > gfxCheck2 || (double)color.G > (double)gfxCheck2 * 1.1 || (double)color.B > (double)gfxCheck2 * 1.2)
                {
                    Color[] lightArray = new Color[4];
                    Lighting.GetColor4Slice(x, y, ref lightArray);
                    for (int n = 0; n < 4; n++)
                    {
                        int offsetX = 0;
                        int offsetY = 0;
                        Color color4 = color;
                        Color color5 = lightArray[n];
                        if (n == 1) { offsetX = 16; }
                        if (n == 2) { offsetY = 16; }
                        if (n == 3) { offsetX = 16; offsetY = 16; }
                        color4.R = (byte)((color.R + color5.R) / 2);
                        color4.G = (byte)((color.G + color5.G) / 2);
                        color4.B = (byte)((color.B + color5.B) / 2);
                        sb.Draw(texture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8 + offsetX), (float)(y * 16 - (int)Main.screenPosition.Y - 8 + offsetY)) + drawOffset, new Rectangle(wallFrameX + offsetX, wallFrameY + offsetY + frameOffsetY, 16, 16), (overrideColor != null ? overrideColor(color4) : color4), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    }
                }
                else
                {
                    Rectangle rect = new Rectangle(wallFrameX, wallFrameY + frameOffsetY, 32, 32);
                    sb.Draw(texture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8), (float)(y * 16 - (int)Main.screenPosition.Y - 8)) + drawOffset, rect, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }
            }
            if (drawOutline && ((double)color.R > (double)gfxCheck2 * 0.4 || (double)color.G > (double)gfxCheck2 * 0.35 || (double)color.B > (double)gfxCheck2 * 0.3))
            {
                bool outlineLeft = Main.tile[x - 1, y].WallType > WallID.None && Main.wallBlend[(int)Main.tile[x - 1, y].WallType] != Main.wallBlend[(int)Main.tile[x, y].WallType];
                bool outlineRight = Main.tile[x + 1, y].WallType > WallID.None && Main.wallBlend[(int)Main.tile[x + 1, y].WallType] != Main.wallBlend[(int)Main.tile[x, y].WallType];
                bool outlineUp = Main.tile[x, y - 1].WallType > WallID.None && Main.wallBlend[(int)Main.tile[x, y - 1].WallType] != Main.wallBlend[(int)Main.tile[x, y].WallType];
                bool outlineDown = Main.tile[x, y + 1].WallType > WallID.None && Main.wallBlend[(int)Main.tile[x, y + 1].WallType] != Main.wallBlend[(int)Main.tile[x, y].WallType];
                if (outlineLeft) sb.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y)) + drawOffset, new Rectangle(0, 0, 2, 16), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                if (outlineRight) sb.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(x * 16 - (int)Main.screenPosition.X + 14), (float)(y * 16 - (int)Main.screenPosition.Y)) + drawOffset, new Rectangle(14, 0, 2, 16), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                if (outlineUp) sb.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y)) + drawOffset, new Rectangle(0, 0, 16, 2), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                if (outlineDown) sb.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y + 14)) + drawOffset, new Rectangle(0, 14, 16, 2), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }

        /*
		 * Returns the draw position of a texture for tiles.
		 */
        public static Vector2 GetTileDrawPosition(int x, int y, int width, int height, Vector2 drawOffset)
        {
            return new Vector2((x * 16 - (int)Main.screenPosition.X) - (width - 16f) / 2f, (float)(y * 16 - (int)Main.screenPosition.Y)) + drawOffset;
        }

        /*
         * Returns the draw position of a texture for npcs and projectiles.
         */
        public static Vector2 GetDrawPosition(Vector2 position, Vector2 origin, int width, int height, int texWidth, int texHeight, Rectangle frame, int framecount, int framecountX, float scale, bool drawCentered = false)
        {
            Vector2 screenPos = new Vector2((int)Main.screenPosition.X, (int)Main.screenPosition.Y);
            if (drawCentered)
            {
                Vector2 texHalf = new Vector2(texWidth / framecountX / 2, texHeight / framecount / 2);
                return position + new Vector2(width / 2, height / 2) - (texHalf * scale) + (origin * scale) - screenPos;
            }
            return position - screenPos + new Vector2(width / 2, height) - (new Vector2(texWidth / framecountX / 2, texHeight / framecount) * scale) + (origin * scale) + new Vector2(0f, 5f);
        }

        /*
         * Draws the given texture using a player and lighting nearby, or the overriden color given.
         * 
         * locationType : 0 == head, 1 == body, 2 == legs.
         */
        public static void DrawPlayerTexture(object sb, Texture2D texture, int shader, Player drawPlayer, Vector2 ediPos, int locationType, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null, Rectangle? frameRect = null, float scaleOverride = 0f)
        {
            Vector2 locationPos = (locationType == 0 ? drawPlayer.headPosition : locationType == 1 ? drawPlayer.bodyPosition : drawPlayer.legPosition);
            float locationRot = (locationType == 0 ? drawPlayer.headRotation : locationType == 1 ? drawPlayer.bodyRotation : drawPlayer.legRotation);
            Rectangle locationFrame = (locationType == 0 ? drawPlayer.headFrame : locationType == 1 ? drawPlayer.bodyFrame : drawPlayer.legFrame);
            DrawPlayerTexture(sb, texture, shader, drawPlayer, ediPos, locationPos, locationRot, locationFrame, offsetX, offsetY, overrideColor, frameRect, scaleOverride);
        }

        /*
         * Draws the given texture using a player and lighting nearby, or the overriden color given.
         * It draws using the player's current frame and thus expects a texture with the same number of frames.
         * 
         * locationPos : the location position. (in Player: headPosition, bodyPosition, legPosition)
         * locationRot : the location rotation. (in Player: headRotation, bodyRotation, legRotation)
         * locationFrame : The location frame. (in Player: headFrame, bodyFrame, legFrame)
         * offsetX, offsetY: offset the drawing on the X and Y axis respectively.
         * frameRect : the bounding box of the drawing area. (usually drawPlayer.bodyFrame)
         */
        public static void DrawPlayerTexture(object sb, Texture2D texture, int shader, Player drawPlayer, Vector2 ediPos, Vector2 locationPos, float locationRot, Rectangle locationFrame, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null, Rectangle? frameRect = null, float scaleOverride = 0f)
        {
            offsetX = (drawPlayer.direction == -1 ? -offsetX : offsetX);
            offsetY += 4f; //adding y offset to put properly into place.

            Vector2 frameCenter = new Vector2((float)locationFrame.Width * 0.5f, (float)locationFrame.Height * 0.5f);
            Color color = (overrideColor != null ? (Color)overrideColor : GetPlayerColor(drawPlayer, drawPlayer.Center));
            Rectangle frame = frameRect != null ? (Rectangle)frameRect : drawPlayer.bodyFrame;

            SpriteEffects effect = drawPlayer.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (drawPlayer.gravDir == -1.0f) { effect = effect | SpriteEffects.FlipVertically; }

            float scale = scaleOverride > 0f ? scaleOverride : 1f;
            if (sb is PlayerDrawSet drawset)
            {
                DrawData dd = new DrawData(texture, new Vector2((float)((int)(ediPos.X - (int)Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(ediPos.Y - (int)Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height))) + new Vector2(offsetX * scale, offsetY * scale) + locationPos + frameCenter, frame, color, locationRot, frameCenter, scale, effect, 0);
                dd.shader = shader;
                drawset.DrawDataCache.Add(dd);
            }
            else if (sb is SpriteBatch)
            {
                bool applyDye = shader > 0;
                if (applyDye)
                {
                    ((SpriteBatch)sb).End();
                    ((SpriteBatch)sb).Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    GameShaders.Armor.ApplySecondary(shader, drawPlayer, null);
                }
                ((SpriteBatch)sb).Draw(texture, new Vector2((float)((int)(ediPos.X - (int)Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(ediPos.Y - (int)Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height))) + new Vector2(offsetX * scale, offsetY * scale) + locationPos + frameCenter, frame, color, locationRot, frameCenter, scale, effect, 0);
                if (applyDye)
                {
                    ((SpriteBatch)sb).End();
                    ((SpriteBatch)sb).Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                }
            }
        }

        //used by InDrawZone to prevent making a new rectangle every time the method is called
        private static Rectangle drawZoneRect = default(Rectangle);

        public static bool InDrawZone(Vector2 vec, bool noScreenPos = false)
        {
            if ((int)Main.screenPosition.X - 300 != drawZoneRect.X || (int)Main.screenPosition.Y - 300 != drawZoneRect.Y) drawZoneRect = new Rectangle((int)Main.screenPosition.X - 300, (int)Main.screenPosition.Y - 300, Main.screenWidth + 600, Main.screenHeight + 600);
            if (noScreenPos) vec += Main.screenPosition;
            return drawZoneRect.Contains((int)vec.X, (int)vec.Y);
        }
    }
}