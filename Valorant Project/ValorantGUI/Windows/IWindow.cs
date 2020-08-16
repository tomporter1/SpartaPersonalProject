namespace ValorantGUI
{
    public interface IWindow
    {
        public void WaitMouse();
        public void EndWaitMouse();
        public void SetHomePage();
        void SetContent(object newPage);
    }
}