using IESv1API.Models.WhatsappCloud;
using IESv1API.Services.WhatsappCloud.SendMessage;
using IESv1API.Util;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IESv1API.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsappController : Controller
    {

        private readonly IWhatsappCloudSendMessage _whatsappCloudSendMessage;
        private readonly IUtil _util;
        public WhatsappController(IWhatsappCloudSendMessage whatsappCloudSendMessage, IUtil util)
        {
            _whatsappCloudSendMessage = whatsappCloudSendMessage;
            _util = util;
        }

        [HttpGet("Sample")]
        public async Task<IActionResult> Sample()
        {
            var data = new
            {
                messaging_product = "whatsapp",
                to = "51944043423",
                type = "text",
                text = new
                {
                    body = "Test API SAMPLE"
                }
            };

            var result = await _whatsappCloudSendMessage.Execute(data);
            return Ok("Ok sample");
        }

        [HttpGet("VerifyToken")]
        public IActionResult VerifyToken()
        {
            string AccesToken = "AARONTMP";

            var token = Request.Query["hub.verify_token"].ToString();

            var challenge = Request.Query["hub.challenge"].ToString();

            if(challenge != null && token != null && token == AccesToken)
            {
                return Ok(challenge);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> ReceivedMessage([FromBody] WhatsappCloudModel body)
        {
            try
            {
                var Message = body.Entry[0]?.Changes[0]?.Value?.Messages[0];

                if(Message != null)
                {
                    var userNumber = Message.From;
                    var userText = GetUserTexT(Message);

                    object objectMessage;

                    switch (userText.ToUpper())
                    {
                        case "TEXT":
                            objectMessage = _util.TextMessage("QQQ", userNumber);
                            break;
                        case "IMAGE":
                            objectMessage = _util.ImageMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/image_whatsapp.png", userNumber);
                            break;
                        default:
                            objectMessage = _util.TextMessage("No entiendo", userNumber);
                            break;
                    }

                    await _whatsappCloudSendMessage.Execute(objectMessage);

                }

                return Ok("EVENT_RECEIVED");
            }
            catch (Exception ex)
            {
                return Ok("EVENT_RECEIVED");
            }
        }

        private string GetUserTexT(Message message)
        {
            string TypeMessage = message.Type;

            if(TypeMessage.ToUpper() == "TEXT")
            {
                return message.Text.Body;
            }
            else if (TypeMessage.ToUpper() == "INTERACTIVE")
            {
                string interactiveType = message.Interactive.Type;

                if(interactiveType.ToUpper() == "LIST_REPLY")
                {
                    return message.Interactive.List_Reply.Tittle;
                }
                else if(interactiveType.ToUpper() == "BUTTON_REPLY")
                {
                    return message.Interactive.Button_Reply.Tittle;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }

        }
    }
}
