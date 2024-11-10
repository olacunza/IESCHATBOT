namespace IESv1API.Util
{
    public interface IUtil
    {
        object TextMessage(string message, string number);
        object ImageMessage(string url, string number);
    }
}
