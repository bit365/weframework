using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System.IO;

namespace WeFramework.Web.Extensions.WeChat
{
    public class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        public CustomMessageHandler(Stream stream, PostModel model, int maxRecordCount = 10) : base(stream, model, maxRecordCount)
        {
            GlobalMessageContext.ExpireMinutes = 3;
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            return new SuccessResponseMessage();
        }

        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "欢迎关注零度云平台微信公众号。";
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            return null;
        }
    }
}