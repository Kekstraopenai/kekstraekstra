using System;
using Common.Game.Console;

namespace World_Of_Sea_Battle.Components.Console
{
	// Token: 0x0200054E RID: 1358
	public class ClientConsoleCommands : ConsoleCommandsBase<ClientCommandContext>
	{
		// Token: 0x06001EE8 RID: 7912 RVA: 0x001158BA File Offset: 0x00113ABA
		public ClientConsoleCommands(ConsoleCommandsRegistry registry) : base(registry)
		{
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x001158C3 File Offset: 0x00113AC3
		public void Initialize()
		{
			this.registry.Initialize();
		}
	}
}
