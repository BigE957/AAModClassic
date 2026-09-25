namespace AAModClassic.UI.Dialogue.DialogueEvents
{
    public abstract class DialogueEvent
    {
        public string ID { get; set; }
        public string[] Args { get; set; }

        internal bool EventOver = true;
        public bool IsOver => EventOver;

        internal int EventCounter = 0;

        public virtual void UpdateEvent()
        {
            EventCounter++;
        }
    }
}
