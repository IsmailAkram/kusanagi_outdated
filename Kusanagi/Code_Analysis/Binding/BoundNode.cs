namespace Kusanagi.Code_Analysis.Binding
{
    internal abstract class BoundNode
    {
        public abstract BoundNodeKind Kind { get; }        
    }
}