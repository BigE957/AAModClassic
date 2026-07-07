using System.Text.Json.Nodes;

namespace AAModClassic.DiscordSupport;

public class DiscordActivity
{
    public string Details { get; set; }

    public string State { get; set; }

    public ActivityTimestamps Timestamps { get; set; }
    public ActivityAssets Assets { get; set; }
    public ActivityParty Party { get; set; }

    public class ActivityAssets
    {
        public string LargeImage { get; set; }
        public string LargeText { get; set; }
        public string SmallImage { get; set; }
        public string SmallText { get; set; }
    }

    public class ActivityTimestamps
    {
        public long? Start { get; set; }
        public long? End { get; set; }
    }

    public class ActivityParty
    {
        public string Id { get; set; }
        public int CurrentSize { get; set; }
        public int MaxSize { get; set; }
    }

    internal object ToPayload()
    {
        var activity = new JsonObject
        {
            ["details"] = Details,
            ["state"] = State
        };

        if (Timestamps != null)
        {
            var ts = new JsonObject { ["start"] = Timestamps.Start };
            if (Timestamps.End != null)
                ts["end"] = Timestamps.End;
            activity["timestamps"] = ts;
        }

        if (Assets != null)
        {
            activity["assets"] = new JsonObject
            {
                ["large_image"] = Assets.LargeImage,
                ["large_text"] = Assets.LargeText,
                ["small_image"] = Assets.SmallImage,
                ["small_text"] = Assets.SmallText
            };
        }

        if (Party != null)
        {
            activity["party"] = new JsonObject
            {
                ["id"] = Party.Id,
                ["size"] = new JsonArray((JsonNode)Party.CurrentSize, (JsonNode)Party.MaxSize)
            };
        }

        return activity;
    }
}
