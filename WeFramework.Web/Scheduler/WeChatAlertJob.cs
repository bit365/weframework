using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.MP.AdvancedAPIs;
using WeFramework.Web.Extensions.WeChat;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Containers;
using WeFramework.Service.Users;
using WeFramework.Service.Security;
using WeFramework.Core.Paging;
using WeFramework.Core.Domain.Users;

namespace WeFramework.Web.Scheduler
{
    public class WeChatAlertJob : IJob
    {
        private static readonly string appId = WeChatConfigManager.Settings["appId"];

        private static readonly string appSecret = WeChatConfigManager.Settings["appSecret"];

        private static readonly string templateId = "U5xiKIrIxk2ZnKcNd4XyVi3xBvpV0zIgN4COX9kHydc";

        private readonly IUserService userService;

        private readonly IEntityPermissionService entityPermissionService;

        private readonly IEncryptionService encryptionService;

        public WeChatAlertJob(IUserService userService, IEntityPermissionService entityPermissionService, IEncryptionService encryptionService)
        {
            this.userService = userService;
            this.entityPermissionService = entityPermissionService;
            this.encryptionService = encryptionService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            IPagedList<User> users = userService.GetUsers(string.Empty, 1, int.MaxValue);

            List<Task> templateMessageTasks = new List<Task>();

            foreach (var user in users.Where(u => !string.IsNullOrWhiteSpace(u.WeChatOpenID)))
            {
                var templateDatas = new
                {
                    first = new TemplateDataItem("你好，检测到异常报警设备"),
                    keyword1 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                    keyword2 = new TemplateDataItem("零度云平台"),
                    keyword3 = new TemplateDataItem("传感器参数异常"),
                    remark = new TemplateDataItem("请尽快查看相关设备状态。")
                };

                var url = string.Format("http://www.xcode.me/{0}", encryptionService.Encrypt(user.WeChatOpenID, appSecret));

                AccessTokenContainer.Register(appId, appSecret);
                var task = TemplateApi.SendTemplateMessageAsync(appId, user.WeChatOpenID, templateId, url, templateDatas);
                templateMessageTasks.Add(task);
            }

            return Task.WhenAll(templateMessageTasks);
        }
    }
}