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

        public TextDisplay(DialoguePage textData, DisplayEffect displayEffects, bool screenLocked = false, float wrapWidth = -1, Asset<DynamicSpriteFont> font = null)
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
            SwitchCounter = 0;
            SwitchingPage = false;
            ClosingDialogue = false;

            if (DialoguePage.Event != null)
                return;

            if (Font is null || !Font.IsLoaded)
                return;

            ParsedPage parsed = Parse(DialoguePage, Font.Value, WrapWidth);
            Text = parsed.Text;
            LineBreakIndexes = parsed.LineBreakIndexes;
            UniqueColors = parsed.UniqueColors;
            UniqueBorderColors = parsed.UniqueBorderColors;
            Pauses = parsed.Pauses;
            TextEffects = parsed.TextEffects;
            UniqueScales = parsed.UniqueScales;
            int[] lineLengths = parsed.LineLengths;

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

            Vector2[] charPositions = new Vector2[Text.Length];
            TextSize = MeasureText(Text, Font.Value, UniqueScales, LineBreakIndexes, DialoguePage.TextScale, out Vector2 offset, charPositions: charPositions);
            SizeOffsetFromStart = offset;

            for (int i = 0; i < Text.Length; i++)
                CharacterData[i].TextPosition = charPositions[i];

            if (DialoguePage.AlignType == Alignment.Center || DialoguePage.AlignType == Alignment.Right)
            {
                float textWidth = TextSize.X - 8f - offset.X;

                for (int line = 0; line < lineLengths.Length; line++)
                {
                    List<DialogueCharacterData> lineChars = CharacterData.Where(d => d.LineNumber == line).ToList();
                    DialogueCharacterData furthest = lineChars.LastOrDefault(d => Text[d.Index] != '\n');
                    if (furthest == null)
                        continue;

                    float dif = textWidth - furthest.TextPosition.X;
                    float shift = DialoguePage.AlignType == Alignment.Center ? dif / 2f : dif;

                    foreach (DialogueCharacterData d in lineChars)
                        d.TextPosition.X += shift;
                }
            }
        }

        public static Vector2 MeasureText(string text, DynamicSpriteFont font, IReadOnlyDictionary<int, Vector2> uniqueScales, ICollection<int> lineBreakIndexes, float defaultTextScale, out Vector2 sizeOffsetFromStart, float startOffsetX = 8f, float startOffsetYPerScale = 16f, Vector2? extraPadding = null, Vector2[] charPositions = null)
        {
            Vector2 padding = extraPadding ?? new Vector2(8f, 12f);

            float HighestYScale(int start)
            {
                float highest = 1f;
                if (uniqueScales == null)
                    return highest;

                for (int j = start; j < text.Length; j++)
                {
                    if (text[j] == '\n')
                        break;
                    if (uniqueScales.TryGetValue(j, out Vector2 s) && s.Y > highest)
                        highest = s.Y;
                }
                return highest;
            }

            sizeOffsetFromStart = new Vector2(startOffsetX, startOffsetYPerScale * HighestYScale(0));

            Vector2 pen = Vector2.Zero;
            bool newLine = true;
            float textWidth = 0f;

            void BreakLine(int nextLineScanStart)
            {
                if (pen.X > textWidth)
                    textWidth = pen.X;

                pen.X = 0f;
                pen.Y += font.LineSpacing * HighestYScale(nextLineScanStart);
                newLine = true;
            }

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                Vector2 scale = Vector2.One;
                if (uniqueScales != null && uniqueScales.TryGetValue(i, out Vector2 unique))
                    scale = unique;
                else if (defaultTextScale != -1f)
                    scale *= defaultTextScale;

                if (c == '\n')
                {
                    BreakLine(i + 1);
                    continue;
                }
                if (c == '\r')
                    continue;

                if (lineBreakIndexes != null && lineBreakIndexes.Contains(i))
                    BreakLine(i + 1);

                if (!font.SpriteCharacters.TryGetValue(c, out SpriteCharacterData spriteData))
                    spriteData = font.SpriteCharacters[font.DefaultCharacter];

                Vector3 kerning = spriteData.Kerning;
                Rectangle glyphPadding = spriteData.Padding;

                if (newLine)
                    kerning.X = Math.Max(kerning.X, 0f);
                else
                    pen.X += font.CharacterSpacing * scale.X;

                pen.X += kerning.X * scale.X;

                if (charPositions != null)
                {
                    Vector2 position = pen + spriteData.Glyph.Size() * 0.5f;
                    position.X += glyphPadding.X * scale.X;
                    position.Y += glyphPadding.Y * scale.Y;
                    charPositions[i] = position - Vector2.UnitY * scale.Y * font.LineSpacing * 0.5f;
                }

                pen.X += (kerning.Y + kerning.Z) * scale.X;
                newLine = false;
            }

            if (pen.X > textWidth)
                textWidth = pen.X;
            float textHeight = pen.Y;

            return new Vector2(textWidth + padding.X, textHeight + padding.Y) + sizeOffsetFromStart;
        }

        public static Vector2 MeasureText(string text, DynamicSpriteFont font, IReadOnlyDictionary<int, Vector2> uniqueScales = null, ICollection<int> lineBreakIndexes = null, float defaultTextScale = -1f) => MeasureText(text, font, uniqueScales, lineBreakIndexes, defaultTextScale, out _);

        public void ResetText(DialoguePage textData)
        {
            DialoguePage = textData;
            OnActivate();
        }

        private static void FindEffects(ref string fullLine, int fullLength, ParsedPage result)
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
                                    if (float.TryParse(Param, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float color))
                                        Params.Add(color);
                                    else
                                        ColorParams.Add(Param);
                                }
                                else if (ID == "BorderColors")
                                {
                                    if (float.TryParse(Param, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float color))
                                        Params.Add(color);
                                    else
                                        BorderColorParams.Add(Param);
                                }
                                else
                                {
                                    if (float.TryParse(Param, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float parameter))
                                        Params.Add(parameter);
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

                        result.Pauses.Add(index - storedLen - 1, Params[0]);
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

                                result.UniqueColors.Add(index - storedLen, (Params.Count == 0 ? 0 : Params[0], Params.Count < 2 ? 1 : Params[1], [.. ColorParams]));
                            }
                            else if (ID == "BorderColors")
                            {
                                int storedLen = 0;
                                foreach (string s in returnString)
                                    storedLen += s.Length;

                                result.UniqueBorderColors.Add(index - storedLen, (Params.Count == 0 ? 0 : Params[0], Params.Count < 2 ? 1 : Params[1], [.. BorderColorParams]));
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


                                result.UniqueScales.Add(index - storedLen, scale);
                            }
                            else
                            {
                                int storedLen = 0;
                                foreach (string s in returnString)
                                    storedLen += s.Length;

                                string path = "AAModClassic.UI.Dialogue.TextEffects.";
                                Type t = Type.GetType(path + ID) ?? throw new Exception("Invalid text effect ID found");
                                TextEffect te = (TextEffect)Activator.CreateInstance(t);
                                if (result.TextEffects.TryGetValue(index - storedLen, out var value))
                                    value.Add(new(te, [.. Params]));
                                else
                                    result.TextEffects.Add(index - storedLen, [new(te, [.. Params])]);
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
                                Speaker speaker = null;
                                if (DialoguePage.Speaker != null)
                                    speaker = DialoguePage.Speaker;
                                if (speaker != null && DialogueSounds.TryGetValue(speaker.Name, out var value))
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

            DisplayEffects.PreDraw(spriteBatch, pageTop, TextSize, DialogueTimer, SwitchCounter, ClosingDialogue);

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

            DisplayEffects.PostDraw(spriteBatch, pageTop, TextSize, DialogueTimer, SwitchCounter, ClosingDialogue);
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

        internal sealed class ParsedPage
        {
            public string Text = "";
            public int[] LineLengths = [];
            public List<int> LineBreakIndexes = [];
            public Dictionary<int, (float IndexOffset, float gradiantSpeed, string[] hexcodes)> UniqueColors = [];
            public Dictionary<int, (float IndexOffset, float gradiantSpeed, string[] hexcodes)> UniqueBorderColors = [];
            public Dictionary<int, float> Pauses = [];
            public Dictionary<int, List<(TextEffect Effect, float[] args)>> TextEffects = [];
            public Dictionary<int, Vector2> UniqueScales = [];
        }

        /// <summary>Effect parsing + word wrap, with no instance state. Shared by OnActivate and the size prediction.</summary>
        internal static ParsedPage Parse(DialoguePage page, DynamicSpriteFont font, float wrapWidth)
        {
            ParsedPage result = new();

            int fullLength = 0;
            List<string> lines = [];
            for (int i = 0; i < page.Lines.Length; i++)
            {
                string fullLine = page.Lines[i];
                FindEffects(ref fullLine, fullLength, result);

                if (fullLine[^1] != ' ')
                    fullLine += ' ';

                lines.Add(fullLine);
                fullLength += fullLine.Length;
            }

            if (wrapWidth != -1)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i];
                    if (line[^1] == ' ')
                        line = line.Remove(line.Length - 1, 1);

                    if (MeasureLineWidth(line, font, result.UniqueScales, page.TextScale) > wrapWidth)
                    {
                        string yoinked = "";
                        do
                        {
                            int finalIndex = line.LastIndexOf(' ');
                            if (finalIndex < line.Length - 1)
                                finalIndex++;
                            yoinked = line.Substring(finalIndex) + yoinked;
                            line = line.Remove(finalIndex);
                        } while (MeasureLineWidth(line, font, result.UniqueScales, page.TextScale) > wrapWidth);

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
            result.LineLengths = new int[lines.Count];
            for (int i = 0; i < lines.Count; i++)
            {
                result.LineLengths[i] = lines[i].Length;
                result.Text += lines[i];
                fullLength += lines[i].Length;
                result.LineBreakIndexes.Add(fullLength);
            }

            return result;
        }

        private static float MeasureLineWidth(string text, DynamicSpriteFont font, IReadOnlyDictionary<int, Vector2> uniqueScales, float defaultTextScale)
        {
            float width = 0f;
            bool newLine = true;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                Vector2 scale = Vector2.One;
                if (uniqueScales.TryGetValue(i, out Vector2 unique))
                    scale = unique;
                else if (defaultTextScale != -1)
                    scale *= defaultTextScale;

                if (c == '\n')
                {
                    width = 0f;
                    newLine = true;
                    continue;
                }
                if (c == '\r')
                    continue;

                Vector3 kerning = font.SpriteCharacters[c].Kerning;

                if (newLine)
                    kerning.X = Math.Max(kerning.X, 0f);
                else
                    width += font.CharacterSpacing * scale.X;

                width += kerning.X * scale.X;
                width += (kerning.Y + kerning.Z) * scale.X;
                newLine = false;
            }

            return width;
        }

        public static Vector2 MeasurePage(DialoguePage page, DynamicSpriteFont font, float wrapWidth, out Vector2 sizeOffsetFromStart)
        {
            ParsedPage parsed = Parse(page, font, wrapWidth);
            return MeasureText(parsed.Text, font, parsed.UniqueScales, parsed.LineBreakIndexes, page.TextScale, out sizeOffsetFromStart);
        }

        public Vector2 PredictTextSize(DialoguePage page, out Vector2 sizeOffsetFromStart)
        {
            if (page.Event != null || Font is null || !Font.IsLoaded)
            {
                sizeOffsetFromStart = SizeOffsetFromStart;
                return TextSize;
            }

            return MeasurePage(page, Font.Value, WrapWidth, out sizeOffsetFromStart);
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
        public Speaker DefaultSpeaker { get; init; }

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
        public DialogueTextData(DialoguePage[] Pages, int Page = 0, string DefaultColor = null, Speaker DefaultSpeaker = null, int DefaultScale = 1, Alignment AlignType = 0, int TextDelay = 3, int InPunctuationDelay = -1, PunctuationData BasePunctuationDelay = null, int PunctuationDelayCap = 60, Dictionary<string, PunctuationData> PunctuationDelays = null)
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
        public Speaker Speaker { get; set; } = null;

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
