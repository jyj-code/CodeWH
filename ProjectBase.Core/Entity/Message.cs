namespace ProjectBase.Core
{

    public class Message
    {
        public virtual MessageEnum Severity { get; set; }
        public virtual string Text { get; set; }
        public virtual string Reporter { get; set; }
        public virtual string Layer { get; set; }
        public virtual string Template { get; set; }
        public virtual string DebugDetails { get; set; }
    }
}
