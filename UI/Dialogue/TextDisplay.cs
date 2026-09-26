using AAModClassic.UI.Dialogue.DialogueEvents;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.UI.Dialogue.TextEffects;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;
using Terraria.UI.Chat;
using static ReLogic.Graphics.DynamicSpriteFont;

namespace AAModClassic.UI.Dialogue
{
    public class TextDisplay : UIElement
    {
        public static readonly Dictionary<string, SoundStyle> DialogueSounds = new()
        {
            { "Amidias", SoundID.NPCHit1 },
            { "Otonilou", SoundID.NPCHit25 }
        };

        /// <summary>
        /// How long this dialogue has existed
        /// </summary>
        public int DialogueTimer = 0;
        /// <summary>
        /// The position from which the text originates
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        public bool SwitchingPage = false;
        public bool ProgressDialogue = true;
        public bool ClosingDialogue = false;
        public bool ScreenLocked;

        public Vector2 TextSize { get; private set; }
        public Vector2 SizeOffsetFromStart { get; private set; }

        public bool Switching => SwitchingPage || ClosingDialogue;
        internal int SwitchCounter = 0;

        internal DialoguePage DialoguePage;
        internal DisplayEffect DisplayEffects;
        internal string Text = "";
        private int TextTimer = 0;
        internal int textIndex = 0;
        internal int Uptime = 0;
        internal Asset<DynamicSpriteFont> Font;

        //Effects
        internal Dictionary<int, (float IndexOffset, float gradiantSpeed, string[] hexcodes)> UniqueColors = [];
        internal Dictionary<int, (float IndexOffset, float gradiantSpeed, string[] hexcodes)> UniqueBorderColors = [];

        internal Dictionary<int, float> Pauses = [];
        internal Dictionary<int, List<(TextEffect Effect, float[] args)>> TextEffects = [];
        internal Dictionary<int, Vector2> UniqueScales = [];
        internal List<int> LineBreakIndexes = [];

        private DialogueCharacterData[] CharacterData;
        private Color BaseColor = Color.White;
        private Color BaseBorderColor = Color.Black;
        internal bool Crawling = true;
        private int storedDelay = 0;
        private bool lockDelay = false;
        private float WrapWidth = -1;

        public TextDisplay(DialoguePage textData, DisplayEffect displayEffects, bool screenLocked = false, float wrapWidth = -1, Asset<DynamicSpriteFont>? font = null)
        {
            DisplayEffects = displayEffects;
            ScreenLocked = screenLocked;
            DialoguePage = textData;
            DisplayEffects = displayEffects;
            Font = font ?? FontAssets.MouseText;
            WrapWidth = wrapWidth;
        }

        public override void OnActivate()
        {
            Text = "";
            UniqueColors = [];
            UniqueBorderColors = [];
            Pauses = [];
            TextEffects = [];
            UniqueScales = [];

            if (DialoguePage.Event != null)
                return;

            if (Font is null || !Font.IsLoaded)
                return;

            int fullLength = 0;
            List<string> lines = [];
            for (int i = 0; i < DialoguePage.Lines.Length; i++)
            {
                string fullLine = DialoguePage.Lines[i];

                FindEffects(ref fullLine, fullLength);

                if (fullLine[^1] != ' ')
                    fullLine += ' ';

                lines.Add(fullLine);
                fullLength += fullLine.Length;
            }

            if (WrapWidth != -1)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i];
                    if (line[^1] == ' ')
                        line = line.Remove(line.Length - 1, 1);

                    int finalIndex = 0;
                    float width = MeasureString(line, Font.Value).X;

                    if (width > WrapWidth)
                    {
                        string yoinked = "";
                        do
                        {
                            finalIndex = line.LastIndexOf(' ');
                            if (finalIndex < line.Length - 1)
                                finalIndex++;
                            yoinked = line.Substring(finalIndex) + yoinked;
                            line = line.Remove(finalIndex);
                        } while (MeasureString(line, Font.Value).X > WrapWidth);

                        lines[i] = line;
                        if (yoinked[0] == ' ')
                            yoinked = yoinked.Remove(0, 1);

                        if (i >= lines.Count - 1)
                            lines.Add(yoinked);
                        else
                            lines[i + 1] = yoinked + lines[i + 1];
                    }
                }
            }

            fullLength = 0;
            int[] lineLengths = new int[lines.Count];
            LineBreakIndexes.Clear();
            for (int i = 0; i < lines.Count; i++)
            {
                lineLengths[i] = lines[i].Length;

                Text += lines[i];

                fullLength += lines[i].Length;
                LineBreakIndexes.Add(fullLength);
            }

            if (DialoguePage.BaseColor != null)
                BaseColor = WorldTextSystem.GetColorFromHex(DialoguePage.BaseColor);

            if (DialoguePage.BaseBorderColor != null)
                BaseBorderColor = WorldTextSystem.GetColorFromHex(DialoguePage.BaseBorderColor);
            else
            {
                BaseBorderColor = BaseColor * DialoguePage.BorderDarkening;
                BaseBorderColor.A = 255;
            }

            CharacterData = new DialogueCharacterData[Text.Length];

            for (int i = 0; i < Text.Length; i++)
            {
                int j = 0;
                int summedLength = 0;
                for (; j < lineLengths.Length; j++)
                {
                    summedLength += lineLengths[j];
                    if (i < summedLength)
                        break;
                }
                CharacterData[i] = new(i, Text.Length, j);
            }

            textIndex = 0;
            Crawling = true;
            TextTimer = 0;
            storedDelay = 0;
            lockDelay = false;
            DialogueTimer = 0;
            Uptime = 0;

            Vector2 zero = Vector2.Zero;
            bool newLine = true;

            float textWidth = 0f;

            float highestFirstLineYScale = 1f;
            for (int j = 0; j < Text.Length; j++)
            {
                if (Text[j] == '\n')
                    break;
                if (UniqueScales.TryGetValue(j, out Vector2 uniqueScale) && uniqueScale.Y > highestFirstLineYScale)
                    highestFirstLineYScale = uniqueScale.Y;
            }

            SizeOffsetFromStart = new(8, 16 * highestFirstLineYScale);

            for (int i = 0; i < Text.Length; i++)
            {
                char c = Text[i];

                #region Positioning
                Vector2 scale = Vector2.One;
                if (UniqueScales.TryGetValue(i, out Vector2 result))
                    scale = result;
                else if (DialoguePage.TextScale != -1)
                    scale *= DialoguePage.TextScale;

                //Checks for Special Characters, and handles Line Breaks
                switch (c)
                {
                    case '\n':
                        if (zero.X > textWidth)
                            textWidth = zero.X;

                        zero.X = 0;

                        float highestYscale = 1f;
                        for (int j = i + 1; j < Text.Length; j++)
                        {
                            if (Text[j] == '\n')
                                break;
                            if (UniqueScales.TryGetValue(j, out Vector2 uniqueScale) && uniqueScale.Y > highestYscale)
                                highestYscale = uniqueScale.Y;
                        }
                        zero.Y += Font.Value.LineSpacing * highestYscale;
                        newLine = true;
                        continue;
                    case '\r':
                        continue;
                }

                if (LineBreakIndexes.Contains(i))
                {
                    if (zero.X > textWidth)
                        textWidth = zero.X;

                    zero.X = 0;

                    float highestYscale = 1f;
                    for (int j = i + 1; j < Text.Length; j++)
                    {
                        if (Text[j] == '\n')
                            break;
                        if (UniqueScales.TryGetValue(j, out Vector2 uniqueScale) && uniqueScale.Y > highestYscale)
                            highestYscale = uniqueScale.Y;
                    }
                    zero.Y += Font.Value.LineSpacing * highestYscale;
                    newLine = true;
                }

                //Sets the character's position within the full text
                SpriteCharacterData spriteData = Font.Value.SpriteCharacters[c];
                Vector3 kerning = spriteData.Kerning;
                Rectangle padding = spriteData.Padding;

                if (newLine)
                    kerning.X = Math.Max(kerning.X, 0f);
                else
                    zero.X += Font.Value.CharacterSpacing * scale.X;

                zero.X += kerning.X * scale.X;
                Vector2 position = zero + spriteData.Glyph.Size() * 0.5f;
                position.X += padding.X * scale.X;
                position.Y += padding.Y * scale.Y;

                CharacterData[i].TextPosition = position - (Vector2.UnitY * scale.Y * Font.Value.LineSpacing * 0.5f);

                zero.X += (kerning.Y + kerning.Z) * scale.X;
                newLine = false;
                #endregion
            }

            if (zero.X > textWidth)
                textWidth = zero.X;
            float textHeight = zero.Y;

            if (DialoguePage.AlignType != Alignment.Left)
            {
                for (int i = 0; i < lineLengths.Length; i++)
                {
                    DialogueCharacterData furthestChar = CharacterData.Last(d => d.LineNumber == i && Text[d.Index] != '\n');
                    float xPos = furthestChar.TextPosition.X;
                    float dif = textWidth - xPos;
                    if (DialoguePage.AlignType == Alignment.Center)
                        foreach (var c in CharacterData.Where(c => c.LineNumber == i))
                            c.TextPosition.X += dif / 2f;
                    else
                        foreach (var c in CharacterData.Where(c => c.LineNumber == i))
                            c.TextPosition.X += dif;
                }
            }

            TextSize = new Vector2(textWidth + 8, textHeight + 12) + SizeOffsetFromStart;
        }

        public void ResetText(DialoguePage textData)
        {
            DialoguePage = textData;
            OnActivate();
        }

        private void FindEffects(ref string fullLine, int fullLength)
        {
            Stack<int> returnPoints = [];
            Stack<string> returnString = [];

            for (int j = 0; j < fullLine.Length; j++)
            {
                char c = fullLine[j];

                if (c == '[')
                {
                    int k = j + 1;
                    string currentData = "[";
                    bool readingData = true;
                    for (k = j + 1; k < fullLine.Length; k++)
                    {
                        if (fullLine[k] == ']')
                            break;
                        if (fullLine[k] == '[')
                        {
                            returnPoints.Push(j);
                            returnString.Push(currentData);
                            currentData = "[";
                            c = fullLine[k];
                            j = k;
                            readingData = true;
                        }
                        else if (readingData)
                        {
                            currentData += fullLine[k];
                            if (fullLine[k] == ':')
                                readingData = false;
                        }

                    }
                    if (fullLine[k] != ']')
                        throw new Exception("[ was found without a ] after it.");

                    string effect = fullLine[j..k];
                    string ID = "";
                    string Text = "";
                    List<float> Params = [];
                    List<string> ColorParams = [];
                    List<string> BorderColorParams = [];
                    string Param = "";
                    bool readingText = false;
                    bool readingParams = false;
                    for (int l = 1; l < effect.Length; l++)
                    {
                        char ch = effect[l];
                        if (ch == '(')
                        {
                            readingParams = true;
                            continue;
                        }
                        else if (ch == ':')
                        {
                            readingText = true;
                            continue;
                        }
                        else if (ch == ']')
                            break;

                        if (readingText)
                            Text += ch;
                        else if (readingParams)
                        {
                            if (ch == ',' || ch == ')')
                            {
                                if (ID == "Colors")
                                {
                                    if (float.TryParse(Param, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float result))
                                        Params.Add(result);
                                    else
                                        ColorParams.Add(Param);
                                }
                                else if (ID == "BorderColors")
                                {
                                    if (float.TryParse(Param, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float result))
                                        Params.Add(result);
                                    else
                                        BorderColorParams.Add(Param);
                                }
                                else
                                {
                                    if (float.TryParse(Param, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float result))
                                        Params.Add(result);
                                    else
                                        throw new Exception("Invalid Parameter found");
                                }
                                Param = "";
                            }
                            else if (ch == ' ')
                                continue;
                            else
                                Param += ch;
                        }
                        else
                            ID += ch;
                    }

                    fullLine = fullLine.Remove(j, k - j + 1);
                    fullLine = fullLine.Insert(j, Text);

                    if (ID == "Pause")
                    {
                        int index = j + fullLength;
                        int storedLen = 0;
                        foreach (string s in returnString)
                            storedLen += s.Length;

                        Pauses.Add(index - storedLen - 1, Params[0]);
                    }
                    else
                    {
                        for (int i = 0; i < Text.Length; i++)
                        {
                            int index = j + i + fullLength;
                            if (ID == "Colors")
                            {
                                int storedLen = 0;
                                foreach (string s in returnString)
                                    storedLen += s.Length;

                                UniqueColors.Add(index - storedLen, (Params.Count == 0 ? 0 : Params[0], Params.Count < 2 ? 1 : Params[1], [.. ColorParams]));
                            }
                            else if (ID == "BorderColors")
                            {
                                int storedLen = 0;
                                foreach (string s in returnString)
                                    storedLen += s.Length;

                                UniqueBorderColors.Add(index - storedLen, (Params.Count == 0 ? 0 : Params[0], Params.Count < 2 ? 1 : Params[1], [.. BorderColorParams]));
                            }
                            else if (ID == "Scale")
                            {
                                int storedLen = 0;
                                foreach (string s in returnString)
                                    storedLen += s.Length;

                                Vector2 scale;
                                if (Params.Count == 0)
                                    scale = Vector2.One;
                                else if (Params.Count == 1)
                                    scale = new(Params[0], Params[0]);
                                else
                                    scale = new(Params[0], Params[1]);


                                UniqueScales.Add(index - storedLen, scale);
                            }
                            else
                            {
                                int storedLen = 0;
                                foreach (string s in returnString)
                                    storedLen += s.Length;

                                string path = "AAModClassic.UI.Dialogue.TextEffects.";
                                Type t = Type.GetType(path + ID) ?? throw new Exception("Invalid text effect ID found");
                                TextEffect te = (TextEffect)Activator.CreateInstance(t);
                                if (TextEffects.TryGetValue(index - storedLen, out var value))
                                    value.Add(new(te, [.. Params]));
                                else
                                    TextEffects.Add(index - storedLen, [new(te, [.. Params])]);
                            }
                        }
                    }

                    if (returnPoints.Count > 0)
                    {
                        j = returnPoints.Pop() - 1;
                        returnString.Pop();
                    }
                }
            }
        }

        private Vector2 MeasureString(string text, DynamicSpriteFont font)
        {
            if (text.Length == 0)
                return Vector2.Zero;

            Vector2 zero = Vector2.Zero;
            zero.Y = font.LineSpacing;
            float val = 0f;
            int num = 0;
            float num2 = 0f;
            bool newLine = true;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                Vector2 scale = Vector2.One;
                if (UniqueScales.TryGetValue(i, out Vector2 result))
                    scale = result;
                else if (DialoguePage.TextScale != -1)
                    scale *= DialoguePage.TextScale;

                //Checks for Special Characters, and handles Line Breaks
                switch (c)
                {
                    case '\n':
                        zero.X = 0;

                        float highestYscale = 1f;
                        for (int j = i + 1; j < text.Length; j++)
                        {
                            if (text[j] == '\n')
                                break;
                            if (UniqueScales.TryGetValue(j, out Vector2 uniqueScale) && uniqueScale.Y > highestYscale)
                                highestYscale = uniqueScale.Y;
                        }
                        zero.Y += font.LineSpacing * highestYscale;
                        newLine = true;
                        continue;
                    case '\r':
                        continue;
                }

                //Sets the character's position within the full text
                SpriteCharacterData spriteData = font.SpriteCharacters[c];
                Vector3 kerning = spriteData.Kerning;
                Rectangle padding = spriteData.Padding;

                if (newLine)
                    kerning.X = Math.Max(kerning.X, 0f);
                else
                    zero.X += font.CharacterSpacing * scale.X;

                zero.X += kerning.X * scale.X;
                Vector2 position = zero + spriteData.Glyph.Size() * 0.5f;
                position.X += padding.X * scale.X;
                position.Y += padding.Y * scale.Y;

                zero.X += (kerning.Y + kerning.Z) * scale.X;
                newLine = false;
            }

            zero.X += Math.Max(num2, 0f);
            zero.Y += num * font.LineSpacing;
            zero.X = Math.Max(zero.X, val);
            return zero;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (DialoguePage.Event != null)
                DialoguePage.Event.UpdateEvent();
            else if (!Switching)
            {
                if (DisplayEffects.FadeWhenTooFar)
                {
                    float distFromSource = Vector2.Distance(Main.LocalPlayer.Center, Position);
                    SwitchCounter = (int)(MathHelper.Clamp((distFromSource - DisplayEffects.FadeBuffer) / DisplayEffects.FadeDistance, 0f, 1f) * DisplayEffects.TimeToDisappear);
                }

                int textDelay = DialoguePage.TextDelay;
                int inPunctuationDelay = DialoguePage.InPunctuationDelay;

                if (DialoguePage.Event != null && !DialoguePage.Event.IsOver)
                    DialoguePage.Event.UpdateEvent();

                if (textIndex < Text.Length - 1)
                {
                    int delay;
                    int loopCounter = 0;
                    bool forcedPause = false;

                    do
                    {
                        if (TextTimer == 0)
                        {
                            if (!lockDelay)
                            {
                                char currentChar = Text[textIndex];
                                PunctuationData data = new();

                                if (DialoguePage.BasePunctuationDelay != null)
                                    data = DialoguePage.BasePunctuationDelay;

                                if (DialoguePage.PunctuationDelays != null)
                                {
                                    if (DialoguePage.PunctuationDelays.TryGetValue(currentChar.ToString(), out var value))
                                        data = value;
                                }

                                bool shouldApplyDelay = IsStoppingPunctuation(currentChar, textIndex == 0 ? null : Text[textIndex - 1], textIndex == Text.Length - 1 ? null : Text[textIndex + 1]);

                                if (shouldApplyDelay)
                                {
                                    if (data.ForceSet)
                                        storedDelay = data.Delay;
                                    else
                                        storedDelay += data.Delay;
                                }

                                if (data.Locks)
                                    lockDelay = true;
                            }

                            if (Pauses.TryGetValue(textIndex, out float pause))
                            {
                                storedDelay = (int)(pause * 60);
                                forcedPause = true;
                            }
                        }
                        else if (Pauses.ContainsKey(textIndex))
                            forcedPause = true;

                        int delayToUse = (IsPunctuation(Text[textIndex]) && textIndex > 0 && IsPunctuation(Text[textIndex - 1])) ? inPunctuationDelay : textDelay;

                        bool shouldIncludeStored = Text[textIndex] == ' ' || Text[textIndex] == '\n' || forcedPause;
                        delay = (shouldIncludeStored && storedDelay > 0 ? delayToUse + storedDelay : delayToUse);

                        if (loopCounter == 0)
                            TextTimer++;

                        if ((delay == 0 || (TextTimer + loopCounter) % delay == 0) && TextTimer >= 0)
                        {
                            if (Text[textIndex] == ' ' || forcedPause)
                            {
                                storedDelay = 0;
                                lockDelay = false;
                            }
                            else
                            {
                                string speaker = null;
                                if (DialoguePage.Speaker != null)
                                    speaker = DialoguePage.Speaker;
                                if (speaker != null && DialogueSounds.TryGetValue(speaker, out var value))
                                    SoundEngine.PlaySound(value);
                            }

                            TextTimer = 0;
                            ++textIndex;
                        }

                        if (delay != 0)
                            break;

                        loopCounter++;
                    } while (delay == 0 && textIndex < Text.Length && TextTimer >= 0);
                }
                else
                    Crawling = false;
            }

            if (!Crawling)
                Uptime++;
            DialogueTimer++;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Vector2 textTop = DisplayEffects.TextOffsetFromStart(Position, TextSize);
            Vector2 pageTop = textTop - SizeOffsetFromStart;

            DisplayEffects.PreDraw(spriteBatch, pageTop, TextSize, DialogueTimer, SwitchCounter);

            #region Shadow Drawing
            for (int i = 0; i < textIndex; i++)
            {
                char c = Text[i];

                if (c == '\r' || c == '\n')
                    continue;

                if (CharacterData == null)
                    Activate();

                Vector2 drawPos;
                float rotation = 0f;
                float opacity = 1f;
                Vector2 scale = Vector2.One;
                if (UniqueScales.TryGetValue(i, out Vector2 result))
                    scale = result;

                Color color;
                if (UniqueColors.TryGetValue(i, out var textColors))
                {
                    Color[] colors = new Color[textColors.hexcodes.Length];
                    for (int j = 0; j < colors.Length; j++)
                        colors[j] = WorldTextSystem.GetColorFromHex(textColors.hexcodes[j]);

                    color = !AAConfigClient.Instance.TextEffects ? colors[0] : ColorUtils.MulticolorLerp((Main.GlobalTimeWrappedHourly * textColors.gradiantSpeed) + (i * textColors.IndexOffset), colors);
                }
                else
                    color = BaseColor;

                Color borderColor;
                if (UniqueBorderColors.TryGetValue(i, out var borderColors))
                {
                    Color[] colors = new Color[borderColors.hexcodes.Length];
                    for (int j = 0; j < colors.Length; j++)
                        colors[j] = WorldTextSystem.GetColorFromHex(borderColors.hexcodes[j]);

                    borderColor = !AAConfigClient.Instance.TextEffects ? colors[0] : ColorUtils.MulticolorLerp((Main.GlobalTimeWrappedHourly * borderColors.gradiantSpeed) + (i * borderColors.IndexOffset), colors);
                }
                else
                    borderColor = BaseBorderColor;

                if (CharacterData[i].Timer < DisplayEffects.TimeToAppear)
                {
                    drawPos = DisplayEffects.AppearPositioning(Position, textTop + CharacterData[i].TextPosition, CharacterData[i].Timer, CharacterData[i]);
                    opacity = DisplayEffects.AppearOpacity(opacity, CharacterData[i].Timer, CharacterData[i]);
                    color = DisplayEffects.AppearColoring(color, CharacterData[i].Timer, CharacterData[i]);
                    rotation = DisplayEffects.AppearRotation(rotation, CharacterData[i].Timer, CharacterData[i]);
                    scale = DisplayEffects.AppearScale(scale, CharacterData[i].Timer, CharacterData[i]);
                }
                else
                    drawPos = textTop + CharacterData[i].TextPosition;

                if (SwitchCounter > 0)
                {
                    drawPos = DisplayEffects.DisappearPositioning(drawPos, SwitchCounter, CharacterData[i]);
                    opacity = DisplayEffects.DisappearOpacity(opacity, SwitchCounter, CharacterData[i]);
                    color = DisplayEffects.DisappearColoring(color, SwitchCounter, CharacterData[i]);
                    rotation = DisplayEffects.DisappearRotation(rotation, SwitchCounter, CharacterData[i]);
                    scale = DisplayEffects.DisappearScale(scale, SwitchCounter, CharacterData[i]);
                }

                if (!ScreenLocked)
                    drawPos -= Main.screenPosition;

                if (AAConfigClient.Instance.TextEffects)
                    foreach (var l in TextEffects.Where(v => v.Key == i))
                        foreach ((TextEffect Effect, float[] args) in l.Value)
                        {
                            drawPos = Effect.ModifyPos(drawPos, CharacterData[i], args);

                            rotation = Effect.ModifyRot(rotation, CharacterData[i], args);

                            color = Effect.ModifyColor(color, CharacterData[i], args);

                            scale = Effect.ModifyScale(scale, CharacterData[i], args);
                        }

                SpriteCharacterData spriteData = Font.Value.SpriteCharacters[c];
                Vector2 origin = spriteData.Glyph.Size() * 0.5f;

                CharacterData[i].SetDrawInfo(drawPos, spriteData.Glyph, color * opacity, rotation, scale);

                foreach (var l in TextEffects.Where(v => v.Key == i))
                    foreach ((TextEffect Effect, float[] args) in l.Value)
                        Effect.PreDraw(spriteBatch, spriteData.Texture, CharacterData[i]);

                for (int j = 0; j < ChatManager.ShadowDirections.Length; j++)
                    spriteBatch.Draw(spriteData.Texture, drawPos + (ChatManager.ShadowDirections[j] * 2), spriteData.Glyph, borderColor * opacity, rotation, origin, scale, SpriteEffects.None, 0);
            }
            #endregion

            #region Character Drawing
            for (int i = 0; i < textIndex; i++)
            {
                char c = Text[i];

                if (c == '\r' || c == '\n')
                    continue;

                if (CharacterData == null)
                    Activate();

                SpriteCharacterData spriteData = Font.Value.SpriteCharacters[c];
                Vector2 origin = spriteData.Glyph.Size() * 0.5f;

                spriteBatch.Draw(spriteData.Texture, CharacterData[i].DrawPosition, spriteData.Glyph, CharacterData[i].DrawColor, CharacterData[i].Rotation, origin, CharacterData[i].Scale, SpriteEffects.None, 0);

                foreach (var l in TextEffects.Where(v => v.Key == i))
                    foreach ((TextEffect Effect, float[] args) in l.Value)
                        Effect.PostDraw(spriteBatch, spriteData.Texture, CharacterData[i]);

                CharacterData[i].Timer++;
            }
            #endregion

            DisplayEffects.PostDraw(spriteBatch, pageTop, TextSize, DialogueTimer, SwitchCounter);
        }

        private static bool IsStoppingPunctuation(char current, char? before, char? after)
        {
            if (IsPunctuation(current))
            {
                bool hasLetterBefore = before.HasValue && char.IsLetter(before.Value);
                bool hasLetterAfter = after.HasValue && char.IsLetter(after.Value);
                bool isMidWord = hasLetterBefore && hasLetterAfter;

                if (!isMidWord)
                    return true;
            }

            return false;
        }

        private static bool IsPunctuation(char c)
        {
            UnicodeCategory category = char.GetUnicodeCategory(c);
            return category >= UnicodeCategory.ConnectorPunctuation && category <= UnicodeCategory.OtherPunctuation;
        }
    }

    public enum Alignment
    {
        None = -1,
        Left,
        Center,
        Right
    }

    public class DialogueTextData
    {
        public DialoguePage[] Pages { get; init; }
        public DialoguePage this[int index] { get => Pages[index]; set => Pages[index] = value; }

        public int Page { get; set; }
        public int PageCount => Pages.Length;

        public string DefaultColor { get; init; }
        public string DefaultSpeaker { get; init; }

        public int DefaultScale { get; init; }

        public Alignment AlignType { get; init; }

        public int TextDelay { get; init; }
        public int InPunctuationDelay { get; init; }
        public PunctuationData BasePunctuationDelay { get; init; }
        public int PunctuationDelayCap { get; init; }
        public Dictionary<string, PunctuationData> PunctuationDelays { get; init; }

        /// <summary>
        /// Only used for DialogueLoader
        /// </summary>
        public int Revision { get; init; }

        [JsonConstructor]
        public DialogueTextData(DialoguePage[] Pages, int Page = 0, string DefaultColor = null, string DefaultSpeaker = null, int DefaultScale = 1, Alignment AlignType = 0, int TextDelay = 3, int InPunctuationDelay = -1, PunctuationData BasePunctuationDelay = null, int PunctuationDelayCap = 60, Dictionary<string, PunctuationData> PunctuationDelays = null)
        {
            this.Pages = Pages;
            this.Page = Page;
            this.DefaultColor = DefaultColor;
            this.DefaultSpeaker = DefaultSpeaker;
            this.DefaultScale = DefaultScale;
            this.TextDelay = TextDelay;
            this.InPunctuationDelay = InPunctuationDelay == -1 ? TextDelay : InPunctuationDelay;
            this.BasePunctuationDelay = BasePunctuationDelay ?? new();
            this.PunctuationDelayCap = PunctuationDelayCap;
            this.PunctuationDelays = PunctuationDelays ?? [];
            this.AlignType = AlignType;

            foreach (DialoguePage p in Pages)
            {
                p.BaseColor ??= this.DefaultColor;
                p.Speaker ??= this.DefaultSpeaker;
                if (p.TextScale == -1)
                    p.TextScale = this.DefaultScale;
                if (p.TextDelay == -1)
                    p.TextDelay = this.TextDelay;
                if (p.InPunctuationDelay == -1)
                    p.InPunctuationDelay = this.InPunctuationDelay;
                if (p.AlignType == Alignment.None)
                    p.AlignType = this.AlignType;
                p.BasePunctuationDelay ??= this.BasePunctuationDelay;
                if (p.PunctuationDelayCap == -1)
                    p.PunctuationDelayCap = this.PunctuationDelayCap;
                p.PunctuationDelays ??= this.PunctuationDelays;
            }
        }
    }

    public class DialoguePage
    {
        public string[] Lines { get; set; } = [];

        public string BaseColor { get; set; } = null;
        public string BaseBorderColor { get; set; } = null;
        public float BorderDarkening { get; set; } = 0.25f;
        public string Speaker { get; set; } = null;

        public int TextScale { get; set; } = -1;
        public Alignment AlignType { get; set; } = Alignment.None;

        public int TextDelay { get; set; } = -1;
        public int InPunctuationDelay { get; set; } = -1;
        public PunctuationData BasePunctuationDelay { get; set; } = null;
        public int PunctuationDelayCap { get; set; } = -1;
        public Dictionary<string, PunctuationData> PunctuationDelays { get; set; } = null;

        public DialogueEvent Event { get; set; } = null;
    }

    public class PunctuationData
    {
        public int Delay { get; set; } = 10;
        public bool ForceSet { get; set; } = false;
        public bool Locks { get; set; } = false;
    }

    public class DialogueCharacterData(int index, int textLength, int lineNumber)
    {
        public int Timer = 0;

        #region Text Info
        public int Index = index;

        public int TextLength = textLength;

        public int LineNumber = lineNumber;

        public float CompletionRatio => Index / (float)TextLength;

        public Vector2 TextPosition = Vector2.Zero;
        #endregion

        #region Draw Info
        public Vector2 DrawPosition;
        public Rectangle Frame;
        public Color DrawColor;
        public float Rotation;
        public Vector2 Scale;

        internal void SetDrawInfo(Vector2 drawPos, Rectangle frame, Color color, float rotation, Vector2 scale)
        {
            DrawPosition = drawPos;
            Frame = frame;
            DrawColor = color;
            Rotation = rotation;
            Scale = scale;
        }
        #endregion
    }

}
