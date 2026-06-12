using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Interface;

namespace WorldOfSeaBattles.Interface.DebugPanel
{
	// Token: 0x02000590 RID: 1424
	public class DebugActionsForm : Form
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x00045430 File Offset: 0x00043630
		private static int ActionsPerPage
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x000E4817 File Offset: 0x000E2A17
		private static int TotalPages
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00127154 File Offset: 0x00125354
		public DebugActionsForm(Vector2 {26355}) : base(new Marker(ref {26355}, ref DebugActionsForm.actionsPanel), DebugActionsForm.actionsPanel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.{26371} = DebugActionsForm.LoadActions(this.{26371});
			base.EvRemoveFromContainer += this.{26364};
			this.AnimatedFocus = false;
			Color gray = Color.Gray;
			float num = (float)DebugActionsForm.actionButton.Height * 0.75f;
			int num2 = 2;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChildPos(stackForm, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 5f);
			Form form = new Form(base.Pos.SetHeight(num).Border(-5f, 0f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			stackForm.AddItem(new UiControl[]
			{
				form
			});
			stackForm.AddSpace((float)num2);
			this.{26372} = new Button(new Marker(0f, 0f, 50f, num), DebugActionsForm.actionButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26372}.SetText("Next", Fonts.Arial_10Bold, gray, false);
			this.{26372}.EvClick += this.{26365};
			form.AddChildPos(this.{26372}, PositionAlignment.RightDown, PositionAlignment.LeftUp, num + 5f, 0f, false);
			this.{26373} = new Button(new Marker(0f, 0f, 50f, num), DebugActionsForm.actionButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26373}.SetText("Prev", Fonts.Arial_10Bold, gray, false);
			this.{26373}.EvClick += this.{26367};
			form.AddChildPos(this.{26373}, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 0f);
			Vector2 {13342} = form.Pos.XY + new Vector2((form.Pos.WH.X - num - 5f) / 2f, num / 2f);
			this.{26374} = new Label({13342}, Fonts.Arial_10Bold, Color.White * 0.5f, "-/-", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild(this.{26374}.Center());
			Button button3 = new Button(new Marker(0f, 0f, num, num), DebugActionsForm.actionButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button3.SetText("R", Fonts.Arial_10Bold, gray, false);
			button3.EvClick += delegate(ClickUiEventArgs {26375})
			{
				new {17312}("Вы точно хотите сбросить команды на дефолтные?", delegate(int {26376})
				{
					if ({26376} == 2)
					{
						return;
					}
					DebugActionsForm.reset = true;
					DebugActionsForm.resetPage = (({26376} == 0) ? -1 : DebugActionsForm.currentPage);
					DebugPanel.Close();
					DebugPanel.Open();
				}, new string[]
				{
					"Все",
					"Текущая страница",
					"Отмена"
				});
			};
			form.AddChildPos(button3, PositionAlignment.RightDown, PositionAlignment.LeftUp, 0f);
			for (int i = 0; i < DebugActionsForm.ActionsPerPage; i++)
			{
				int index = i;
				Form form2 = new Form(new Marker(5f, 5f, (float)DebugActionsForm.actionButton.Width, num), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				Button button = new Button(form2.Pos.SetWidth((float)DebugActionsForm.actionButton.Width - num - 5f), DebugActionsForm.actionButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.SetLiveText(delegate
				{
					ValueTuple<string, string> action = this.GetAction(index);
					string item = action.Item1;
					string item2 = action.Item2;
					if (string.IsNullOrWhiteSpace(item) && string.IsNullOrWhiteSpace(item2))
					{
						button.AllowMouseInput = false;
						button.Opacity = 0.5f;
						return "Нет команды";
					}
					button.AllowMouseInput = true;
					button.Opacity = 1f;
					return item ?? "";
				}, Fonts.Arial_10Bold, gray, false);
				button.EvClick += delegate(ClickUiEventArgs {26378})
				{
					DebugPanel.Execute(this.GetAction(index).Item2, false);
				};
				form2.AddChild(button);
				Button button2 = new Button(new Marker(0f, 0f, num, num), DebugActionsForm.actionButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button2.SetText("E", Fonts.Arial_10Bold, gray, false);
				button2.EvClick += delegate(ClickUiEventArgs {26379})
				{
					ValueTuple<string, string> action = this.GetAction(index);
					string currName = action.Item1;
					string currCmd = action.Item2;
					new {17312}(delegate(string {26380})
					{
						new {17312}(delegate(string {26381})
						{
							this.{26357}(index, string.IsNullOrWhiteSpace({26381}) ? {26380} : {26381}, {26380});
						}, 20, "Введите название", (currCmd == currName) ? "" : currName, null);
					}, 100, "Введите команду", currCmd, null);
				};
				form2.AddChildPos(button2, PositionAlignment.RightDown, PositionAlignment.LeftUp, 0f);
				stackForm.AddItem(new UiControl[]
				{
					form2
				});
				stackForm.AddSpace((float)num2);
			}
			this.{26361}(DebugActionsForm.currentPage);
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00127A40 File Offset: 0x00125C40
		[return: TupleElementNames(new string[]
		{
			"name",
			"cmd"
		})]
		private ValueTuple<string, string> GetAction(int {26356})
		{
			int num = DebugActionsForm.currentPage * DebugActionsForm.ActionsPerPage + {26356};
			if (num >= 0 && num < this.{26371}.Length)
			{
				return this.{26371}[num];
			}
			return new ValueTuple<string, string>(null, null);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00127A80 File Offset: 0x00125C80
		private void {26357}(int {26358}, string {26359}, string {26360})
		{
			int num = DebugActionsForm.currentPage * DebugActionsForm.ActionsPerPage + {26358};
			if (num >= 0 && num < this.{26371}.Length)
			{
				this.{26371}[num] = new ValueTuple<string, string>({26359}, {26360});
			}
			this.{26364}();
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00127AC4 File Offset: 0x00125CC4
		private void {26361}(int {26362})
		{
			DebugActionsForm.currentPage = Math.Clamp({26362}, 0, DebugActionsForm.TotalPages - 1);
			DebugActionsForm.<OpenPage>g__SetActive|17_0(DebugActionsForm.currentPage > 0, this.{26373});
			DebugActionsForm.<OpenPage>g__SetActive|17_0(DebugActionsForm.currentPage < DebugActionsForm.TotalPages - 1, this.{26372});
			Label label = this.{26374};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<int>(DebugActionsForm.currentPage + 1);
			defaultInterpolatedStringHandler.AppendLiteral("/");
			defaultInterpolatedStringHandler.AppendFormatted<int>(DebugActionsForm.TotalPages);
			label.Text = defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00127B54 File Offset: 0x00125D54
		[return: TupleElementNames(new string[]
		{
			"name",
			"cmd"
		})]
		private static ValueTuple<string, string>[] LoadActions([TupleElementNames(new string[]
		{
			"name",
			"cmd"
		})] ValueTuple<string, string>[] {26363})
		{
			ValueTuple<string, string>[] array = new ValueTuple<string, string>[DebugActionsForm.TotalPages * DebugActionsForm.ActionsPerPage];
			for (int i = 0; i < array.Length; i++)
			{
				if (i < {26363}.Length)
				{
					array[i] = {26363}[i];
				}
				else
				{
					array[i] = new ValueTuple<string, string>(null, null);
				}
			}
			string directoryName = Path.GetDirectoryName(DebugActionsForm.filename);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			string fileName = Path.GetFileName(DebugActionsForm.filename);
			if (File.Exists(fileName) && !File.Exists(DebugActionsForm.filename))
			{
				File.Move(fileName, DebugActionsForm.filename);
			}
			if (!File.Exists(DebugActionsForm.filename))
			{
				return array;
			}
			if (DebugActionsForm.reset && DebugActionsForm.resetPage == -1)
			{
				DebugActionsForm.reset = false;
				return array;
			}
			int num = 0;
			foreach (string text in File.ReadAllLines(DebugActionsForm.filename))
			{
				if (num >= array.Length)
				{
					break;
				}
				if (!DebugActionsForm.reset || num / DebugActionsForm.TotalPages != DebugActionsForm.resetPage)
				{
					string[] array3 = text.Split('#', StringSplitOptions.None);
					array[num] = new ValueTuple<string, string>(string.Join('#', RuntimeHelpers.GetSubArray<string>(array3, Range.StartAt(1))), array3[0]);
					num++;
				}
			}
			return array;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00127C92 File Offset: 0x00125E92
		private void {26364}()
		{
			File.WriteAllLines(DebugActionsForm.filename, from {26377} in this.{26371}
			select {26377}.Item2 + "#" + {26377}.Item1);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00127CC8 File Offset: 0x00125EC8
		public static IEnumerable<string> GetDefaultActionsLines()
		{
			return new DebugActionsForm.<GetDefaultActionsLines>d__20(-2);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x00127D3D File Offset: 0x00125F3D
		[CompilerGenerated]
		private void {26365}(ClickUiEventArgs {26366})
		{
			this.{26361}(DebugActionsForm.currentPage + 1);
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00127D4C File Offset: 0x00125F4C
		[CompilerGenerated]
		private void {26367}(ClickUiEventArgs {26368})
		{
			this.{26361}(DebugActionsForm.currentPage - 1);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00127D5B File Offset: 0x00125F5B
		[CompilerGenerated]
		internal static void <OpenPage>g__SetActive|17_0(bool {26369}, Button {26370})
		{
			{26370}.Opacity = ({26369} ? 1f : 0.3f);
			{26370}.AllowMouseInput = {26369};
		}

		// Token: 0x04001FFE RID: 8190
		private static readonly Rectangle actionsPanel = new Rectangle(1214, 254, 241, 300);

		// Token: 0x04001FFF RID: 8191
		private static readonly Rectangle actionButton = new Rectangle(951, 43, 231, 33);

		// Token: 0x04002000 RID: 8192
		private static readonly string filename = Path.Combine(LocalSettings.WosbDir, "debugcommands.txt");

		// Token: 0x04002001 RID: 8193
		private static bool reset = false;

		// Token: 0x04002002 RID: 8194
		private static int resetPage = -1;

		// Token: 0x04002003 RID: 8195
		private static int currentPage = 0;

		// Token: 0x04002004 RID: 8196
		[TupleElementNames(new string[]
		{
			"name",
			"cmd"
		})]
		private readonly ValueTuple<string, string>[] {26371} = new ValueTuple<string, string>[]
		{
			new ValueTuple<string, string>("В порт", "tp port"),
			new ValueTuple<string, string>("В форт", "tp fort"),
			new ValueTuple<string, string>("+500 хп", "ship heal 500"),
			new ValueTuple<string, string>("-500 хп", "ship damage 500"),
			new ValueTuple<string, string>("Режим бога", "debug godmode"),
			new ValueTuple<string, string>("Дебаг инфо", "debug info"),
			new ValueTuple<string, string>("Дебаг режим корабля", "debug mode"),
			new ValueTuple<string, string>("День", "time set day"),
			new ValueTuple<string, string>("Ночь", "time set night"),
			new ValueTuple<string, string>("Выйти из аккаунта", "account exit"),
			new ValueTuple<string, string>("Все команды", "help"),
			new ValueTuple<string, string>("Захват порта", "port capture"),
			new ValueTuple<string, string>("Старт ПБ", "port pb begin"),
			new ValueTuple<string, string>("Уничтожить ПБ", "port pb destroy"),
			new ValueTuple<string, string>("Цельность 3", "ship integrity 3"),
			new ValueTuple<string, string>("Паруса -10%", "ship sail 10"),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>("Редактор ассетов", "edit assets"),
			new ValueTuple<string, string>("Редактор абордажа", "edit boarding"),
			new ValueTuple<string, string>("Редактор пушек", "edit cannons"),
			new ValueTuple<string, string>("Редактор экипажа", "edit crew"),
			new ValueTuple<string, string>("Редактор дизайна", "edit design"),
			new ValueTuple<string, string>("Редактор объектов", "edit objects"),
			new ValueTuple<string, string>("Редактор кораблей", "edit ships"),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>("Отчет (текстуры)", "report textures"),
			new ValueTuple<string, string>("Render Targets", "debug rendertargets"),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null),
			new ValueTuple<string, string>(null, null)
		};

		// Token: 0x04002005 RID: 8197
		private readonly Button {26372};

		// Token: 0x04002006 RID: 8198
		private readonly Button {26373};

		// Token: 0x04002007 RID: 8199
		private readonly Label {26374};
	}
}
