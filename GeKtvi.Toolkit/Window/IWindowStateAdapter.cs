namespace GeKtvi.Toolkit.Window
{
    public interface IWindowStateAdapter
    {
        bool IsMaximized { get; }
        bool IsMinimized { get; }
        bool IsNormal { get; }

        void SetMaximizedState();
        void SetMinimizedState();
        void SetNormalState();
    }
}