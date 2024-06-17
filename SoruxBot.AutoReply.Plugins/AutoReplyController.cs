using SoruxBot.SDK.Model.Message;
using SoruxBot.SDK.Plugins.Basic;
using SoruxBot.SDK.Plugins.Service;
using SoruxBot.SDK.Attribute;
using SoruxBot.SDK.Model.Attribute;
using SoruxBot.SDK.Plugins.Model;
using SoruxBot.PluginLib.Permission;

namespace SoruxBot.Plugins.AutoReply
{
	public class AutoReplyController : PluginController
	{
		private ILoggerService _loggerService;
		private IPluginsDataStorage _pluginsDataStorage;
		private ICommonApi _bot;
		private IPluginLibPermission _permission;
		private string _permissionNode = "SoruxBot.Plugins.AutoReply";
		public AutoReplyController(ILoggerService loggerService, IPluginsDataStorage pluginsDataStroage, ICommonApi bot, IPluginLibPermission permission) 
		{
			_bot = bot;
			_loggerService = loggerService;
			_pluginsDataStorage = pluginsDataStroage;
			_permission = permission;
		}
		public override void OnPluginInitialization()
		{
			_loggerService.Info("AutoReplyController", "Plugin initialization success.");
		}
		[CommandAttribute(CommandPrefixType.None)]
		[MessageEventAttribute(MessageType.PrivateMessage)]
		public PluginFlag AutoReplyPersonal(MessageContext context)
		{
			_loggerService.Info("AutoReplyPersonal", "Get personal message.");
			if (context.MessageChain == null || context.TriggerId == "") return PluginFlag.MsgIgnored;
			if(context.TriggerId == context.BotAccount) return PluginFlag.MsgIgnored;
			if(!_permission.CheckIfValid(context, _permissionNode + "AutoReplyPersonal")) return PluginFlag.MsgIgnored;

			string text = context.MessageChain.Messages[0].ToPreviewText();
			var msg = MessageBuilder.PrivateMessage(context.TriggerId, "QQ")
			.Text("[自动回复] " + text)
			.Build();
			var newContext = new MessageContext(
				context.BotAccount,
				context.TargetPlatformAction,
				context.TargetPlatform,
				context.MessageEventType,
				context.TriggerId,
				context.TriggerPlatformId,
				context.TiedId,
				msg,
				context.MessageTime
				);
			_bot.SendMessage(newContext);
			return PluginFlag.MsgPassed;

		}
		[CommandAttribute(CommandPrefixType.None)]
		[MessageEventAttribute(MessageType.GroupMessage)]
		public PluginFlag AutoReplyGroup(MessageContext context)
		{
			return PluginFlag.MsgUnprocessed; // 禁用该方法
			_loggerService.Info("AutoReplyGroup", "Get group message.");
			if (context.MessageChain == null) { return PluginFlag.MsgUnprocessed; }
			string text = context.MessageChain.Messages[0].ToPreviewText();

			var msg = MessageBuilder.GroupMessage(context.TriggerPlatformId, "QQ")
			.Text("[自动回复] " + text)
			.Build();
			var newContext = new MessageContext(
				context.BotAccount,
				context.TargetPlatformAction,
				context.TargetPlatform,
				context.MessageEventType,
				context.TriggerId,
				context.TriggerPlatformId,
				context.TiedId,
				msg,
				context.MessageTime
				);
			_bot.SendMessage(newContext);
			return PluginFlag.MsgPassed;

		}

		private void SetPermission()
		{
			_permission.TryAddPermission(_permissionNode + "AutoReplyPersonal",
				new PermissionBuilder("QQ")
				.Insert(
					new PermissionBuilder("956884168", true)
				)
				.Insert(
					new PermissionBuilder("2796828933")
					.Insert(["2796828933"])
				)
				.Build()
			);
		}
	}
}
