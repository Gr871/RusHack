namespace Scripts.UI.Resize
{
    public interface IResizer
    {
        float Height { get; }
        void SetWidth(float value);
    }
}