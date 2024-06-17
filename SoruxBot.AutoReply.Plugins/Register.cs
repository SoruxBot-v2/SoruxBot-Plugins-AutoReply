using SoruxBot.SDK.Plugins.Basic;
using SoruxBot.SDK.Plugins.Ability;

namespace SoruxBot.Plugins.AutoReply
{
	public class Register : SoruxBotPlugin, ICommandPrefix
	{
		public string GetPluginPrefix() => "";

		public override string GetPluginName() => "AutoReply Plugin";

		public override string GetPluginVersion() => "1.0.0";

		public override string GetPluginAuthorName() => "mhwwhu";

		public override string GetPluginDescription() => "自动回复任何文本消息";

		public override bool IsUpperWhenPrivilege() => false;
	}
}
