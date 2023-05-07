namespace _Main.Scripts.Update
{
    public interface IUpdateObject
    {
        void MyUpdate();
        void SubscribeUpdateManager();
        void UnSubscribeUpdateManager();

    }
}