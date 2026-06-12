using System;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Input;
using TheraEngine.PlatformTools;
using UWEngine.Core;
using World_Of_Sea_Battle.Components.Client;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle
{
	// Token: 0x02000014 RID: 20
	public class EntryPoint
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003A9C File Offset: 0x00001C9C
		public static GameCore RunTheGame(IPlatformTools tools, GameParams gameParams)
		{
			Engine.PlatformTools = tools;
			Environment.SetEnvironmentVariable("FNA3D_FORCE_DRIVER", "D3D11");
			Environment.SetEnvironmentVariable("FNA_KEYBOARD_USE_SCANCODES", "1");
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			return new Main(gameParams);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003AD4 File Offset: 0x00001CD4
		private static void CheckFileNames(string[] data, DirectoryInfo dir)
		{
			foreach (FileInfo fileInfo in dir.EnumerateFiles())
			{
				if (fileInfo.Name.Contains(".xnb") && Array.IndexOf<string>(data, fileInfo.Name) == -1)
				{
					fileInfo.Delete();
				}
			}
			foreach (DirectoryInfo dir2 in dir.EnumerateDirectories())
			{
				EntryPoint.CheckFileNames(data, dir2);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B80 File Offset: 0x00001D80
		internal static string substructPackets(Tlist<Type> t)
		{
			if (t.Size < 4)
			{
				return "not-enough";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" 1: ");
			stringBuilder.Append(t.Array[3]);
			stringBuilder.Append(" 2: ");
			stringBuilder.Append(t.Array[2]);
			stringBuilder.Append(" 3: ");
			stringBuilder.Append(t.Array[1]);
			stringBuilder.Append(" 4: ");
			stringBuilder.Append(t.Array[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003C14 File Offset: 0x00001E14
		public static string checkStt_problem()
		{
			string text = "";
			if (Session.Account != null && Session.Account != null)
			{
				text = ", Player: " + Session.Account.PlayerName;
			}
			if (Global.Game.GetCurrentSceneName == GameSceneName.Loading)
			{
				return "Error in loading";
			}
			if (Global.Game.GetCurrentSceneName == GameSceneName.Entry)
			{
				return string.Concat(new string[]
				{
					"Error in entry scene, network state: ",
					(Global.Network == null) ? "-" : (Global.Network.IsStarted ? "started" : "disabled"),
					", last packets: Send: ",
					EntryPoint.substructPackets(Networking.lastSendMessages),
					", Receive: ",
					EntryPoint.substructPackets(Networking.lastReceiveMessages),
					text
				});
			}
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (InputHelper.NowInputState != null)
				{
					foreach (Keys keys in InputHelper.NowInputState.DownKeys)
					{
						stringBuilder.Append('{');
						stringBuilder.Append(keys.ToString());
						stringBuilder.Append('}');
					}
				}
				return string.Concat(new string[]
				{
					"Error in game scene, network state: ",
					(Global.Network == null) ? "-" : (Global.Network.IsStarted ? "started" : "disabled"),
					", last packets: Send: ",
					EntryPoint.substructPackets(Networking.lastSendMessages),
					", Receive: ",
					EntryPoint.substructPackets(Networking.lastReceiveMessages),
					" Down keys: ",
					stringBuilder.ToString(),
					text
				});
			}
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				return string.Concat(new string[]
				{
					"Error in port, last packets: Send: ",
					EntryPoint.substructPackets(Networking.lastSendMessages),
					", Receive: ",
					EntryPoint.substructPackets(Networking.lastReceiveMessages),
					text
				});
			}
			return "error";
		}

		// Token: 0x0400002B RID: 43
		internal const bool EngineerMode = false;
	}
}
