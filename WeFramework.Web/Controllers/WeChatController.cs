using AutoMapper;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFramework.Core.Domain.Users;
using WeFramework.Core.Paging;
using WeFramework.Service.Security;
using WeFramework.Service.Users;
using WeFramework.Web.Core.Mvc;
using WeFramework.Web.Extensions.WeChat;
using WeFramework.Web.Models.Users;
using WeFramework.Web.Models.WeChat;
using WeFramework.Web.Properties;

namespace WeFramework.Web.Controllers
{
    public class WeChatController : BaseController
    {
        private readonly IUserService userService;

        private readonly IEntityPermissionService entityPermissionService;

        private readonly IEncryptionService encryptionService;

        private static readonly string token = WeChatConfigManager.Settings["token"];

        private static readonly string encodingAesKey = WeChatConfigManager.Settings["encodingAesKey"];

        private static readonly string appId = WeChatConfigManager.Settings["appId"];

        private static readonly string appSecret = WeChatConfigManager.Settings["appSecret"];

        public WeChatController(IUserService userService,  IEntityPermissionService entityPermissionService, IEncryptionService encryptionService)
        {
            this.userService = userService;
            this.entityPermissionService = entityPermissionService;
            this.encryptionService = encryptionService;
        }

        [HttpGet]
        public ActionResult Handler(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, token))
            {
                return Content(echostr);
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Handler(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, token))
            {
                throw new ArgumentException("postModel");
            }
            postModel.Token = token;
            postModel.EncodingAESKey = encodingAesKey;
            postModel.AppId = appId;

            var messageHandler = new CustomMessageHandler(Request.InputStream, postModel);

            messageHandler.Execute();

            return new FixWeixinBugWeixinResult(messageHandler);
        }

        public ActionResult BindingUserOauthCallback(string code)
        {
            var result = OAuthApi.GetAccessToken(appId, appSecret, code);

            if (result.errcode == 0)
            {
                User user = userService.GetUserByWeChatOpenID(result.openid);
                if (user != null)
                {
                    return View("MessagePage", new MessagePageModel { Title = "绑定成功", Message = "您已成功绑定零度云账号，请勿重复操作。" });
                }
                else
                {
                    ViewBag.WeChatSessionID = encryptionService.Encrypt(result.openid, appSecret);
                    return View("BindingUserLogin");
                }
            }

            return View("MessagePage", new MessagePageModel { HasError = true, Title = "操作失败", Message = "绑定过程出错，请稍后再试。" });
        }

        [HttpPost]
        public ActionResult BindingUser(LoginModel model, string weChatSessionID)
        {
            if (ModelState.IsValid)
            {
                if (userService.ValidateUser(model.UserName, model.Password))
                {
                    string openId = encryptionService.Decrypt(weChatSessionID, appSecret);

                    AccessTokenContainer.Register(appId, appSecret);
                    var memberToGroupResult = GroupsApi.MemberUpdate(appId, openId, 100);

                    if (memberToGroupResult.errcode == 0)
                    {
                        User user = userService.GetUser(model.UserName);
                        user.WeChatOpenID = openId;
                        userService.UpdateUser(user);
                        return View("MessagePage", new MessagePageModel
                        {
                            Title = "绑定成功",
                            Message = "您已成功绑定零度云账号。"
                        });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "指定用户分组失败");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Resources.InvalidUserNameOrPassword);
                }
            }

            ViewBag.WeChatSessionID = weChatSessionID;

            return View("BindingUserLogin", model);
        }
    }
}