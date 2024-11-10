namespace IESv1API.Util
{
    public class Util: IUtil
    {
        public object TextMessage(string message, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "text",
                text = new
                {
                    body = message
                }
            };
        }
        public object ImageMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                type = "image",
                text = new
                {
                    body = url
                }
            };
        }
    }
}
