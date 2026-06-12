using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Audio;
using TheraEngine.Collections;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000529 RID: 1321
	internal class LocalSettings : ITKSerializable
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0010BF5B File Offset: 0x0010A15B
		public static string WosbDir
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Wosb";
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0010BF6E File Offset: 0x0010A16E
		private static string FilePath
		{
			get
			{
				return Path.Combine(LocalSettings.WosbDir, "settings.tkformatter");
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0010BF7F File Offset: 0x0010A17F
		private static string FilePathTest
		{
			get
			{
				return Path.Combine(LocalSettings.WosbDir, "settings_testClient.tkformatter");
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0010BF90 File Offset: 0x0010A190
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x0010BF98 File Offset: 0x0010A198
		public bool FullscreenEnabled
		{
			get
			{
				return this.{25642};
			}
			set
			{
				if (this.{25642} != value)
				{
					this.{25642} = value;
					Global.Game.ChangeFullscreenMode(value);
				}
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0010BFB5 File Offset: 0x0010A1B5
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0010BFBD File Offset: 0x0010A1BD
		public bool VerticalSyncEnabled
		{
			get
			{
				return this.{25643};
			}
			set
			{
				this.{25643} = value;
				Global.Game.GetGraphicsManager.SynchronizeWithVerticalRetrace = value;
				Global.Game.DiviceApplyChanges();
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0010BFE0 File Offset: 0x0010A1E0
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x0010BFE8 File Offset: 0x0010A1E8
		public LocalSettings.DrawFrequency MonitorFrequency
		{
			get
			{
				return this.{25627};
			}
			set
			{
				this.{25627} = value;
				Global.Game.ResetElapsedTime();
				if (value == LocalSettings.DrawFrequency.fps60)
				{
					Global.Game.GameTime.TargetFPS = 60;
					return;
				}
				Global.Game.GameTime.TargetFPS = 144;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0010C024 File Offset: 0x0010A224
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0010C02C File Offset: 0x0010A22C
		public float SoundVolume
		{
			get
			{
				return this.{25639};
			}
			set
			{
				this.{25639} = value;
				SoundManager.Volume = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0010C03B File Offset: 0x0010A23B
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0010C043 File Offset: 0x0010A243
		public float MusicVolume
		{
			get
			{
				return this.{25640};
			}
			set
			{
				this.{25640} = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0010C04C File Offset: 0x0010A24C
		public float VideoVolume
		{
			get
			{
				return Math.Max(this.{25639}, this.{25640});
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x0010C05F File Offset: 0x0010A25F
		// (set) Token: 0x06001D8B RID: 7563 RVA: 0x0010C08B File Offset: 0x0010A28B
		public float AmbientVolume
		{
			get
			{
				if (!Global.Game.IsActive && !{19413}.IsOpen)
				{
					return Math.Min(this.{25641}, 0.001f);
				}
				return this.{25641};
			}
			set
			{
				this.{25641} = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x0010C094 File Offset: 0x0010A294
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x0010C09C File Offset: 0x0010A29C
		public bool EnableBasicEffects
		{
			get
			{
				return this.{25628};
			}
			set
			{
				this.{25628} = value;
				Global.Render.UpdateRenderTargets();
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0010C0AF File Offset: 0x0010A2AF
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0010C0B7 File Offset: 0x0010A2B7
		public bool RendererSsaoAndRefractions
		{
			get
			{
				return this.{25629};
			}
			set
			{
				this.{25629} = value;
				Global.Render.UpdateRenderTargets();
				this.LowFpsToolTipWasShown = false;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0010C0D1 File Offset: 0x0010A2D1
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x0010C0D9 File Offset: 0x0010A2D9
		public bool RendererDynamicReflections
		{
			get
			{
				return this.{25630};
			}
			set
			{
				this.{25630} = value;
				Global.Render.UpdateRenderTargets();
				this.LowFpsToolTipWasShown = false;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0010C0F3 File Offset: 0x0010A2F3
		// (set) Token: 0x06001D93 RID: 7571 RVA: 0x0010C0FB File Offset: 0x0010A2FB
		public LocalSettings.RendererShadows ShadowsQuality
		{
			get
			{
				return this.{25631};
			}
			set
			{
				this.{25631} = value;
				Global.Render.UpdateRenderTargets();
				this.LowFpsToolTipWasShown = false;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0010C115 File Offset: 0x0010A315
		// (set) Token: 0x06001D95 RID: 7573 RVA: 0x0010C11D File Offset: 0x0010A31D
		public LocalSettings.RendererAntialias AntialiasingQuality
		{
			get
			{
				return this.{25632};
			}
			set
			{
				this.{25632} = value;
				Global.Render.UpdateRenderTargets();
				this.LowFpsToolTipWasShown = false;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0010C137 File Offset: 0x0010A337
		// (set) Token: 0x06001D97 RID: 7575 RVA: 0x0010C13F File Offset: 0x0010A33F
		public bool ImprovedPostProcess
		{
			get
			{
				return this.{25633};
			}
			set
			{
				this.{25633} = value;
				Global.Render.UpdateRenderTargets();
				this.LowFpsToolTipWasShown = false;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0010C159 File Offset: 0x0010A359
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x0010C161 File Offset: 0x0010A361
		public bool LongViewDistance
		{
			get
			{
				return this.{25634};
			}
			set
			{
				this.{25634} = value;
				Global.Game.WorldInstance.ToggleVisibleDistance();
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0010C179 File Offset: 0x0010A379
		// (set) Token: 0x06001D9B RID: 7579 RVA: 0x0010C181 File Offset: 0x0010A381
		public Point MouseControlInversion
		{
			get
			{
				return this.{25646};
			}
			set
			{
				this.{25646} = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0010C18C File Offset: 0x0010A38C
		public Tlist<SavedShipEquipment> SavedEquipment
		{
			get
			{
				ServerList.ServerInfo currentServer = Global.GetCurrentServer();
				string key = ((currentServer != null) ? currentServer.Id : null) ?? "none";
				SavedShipEquipmentCollection savedShipEquipmentCollection;
				if (!this.{25635}.TryGetValue(key, out savedShipEquipmentCollection))
				{
					this.{25635}.Add(key, savedShipEquipmentCollection = new SavedShipEquipmentCollection());
				}
				return savedShipEquipmentCollection.Items;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0010C1DD File Offset: 0x0010A3DD
		// (set) Token: 0x06001D9E RID: 7582 RVA: 0x0010C1E5 File Offset: 0x0010A3E5
		public bool ShowSailesInPort
		{
			get
			{
				return this.{25636};
			}
			set
			{
				this.{25636} = value;
				if (Global.Player != null)
				{
					Global.Player.UpdateSailClotting();
				}
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0010C1FF File Offset: 0x0010A3FF
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x0010C207 File Offset: 0x0010A407
		public LocalSettings.GlobalUiScale UiScale
		{
			get
			{
				return this.{25637};
			}
			set
			{
				this.{25637} = value;
				Global.Game.AdaptiveUiExtraScale = ((value == LocalSettings.GlobalUiScale.Normal) ? 1f : ((value == LocalSettings.GlobalUiScale.IncreaseDouble) ? 1.2f : ((value == LocalSettings.GlobalUiScale.Increase) ? 1.1f : 0.9f)));
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x0010C23F File Offset: 0x0010A43F
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x0010C247 File Offset: 0x0010A447
		public float MinimapScale
		{
			get
			{
				return this.{25638};
			}
			set
			{
				if (this.{25638} != value)
				{
					this.{25638} = value;
					if ({19891}.CurrentInstance != null)
					{
						{19891}.CurrentInstance.RemoveFromContainer();
						new {19891}();
					}
				}
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0010C270 File Offset: 0x0010A470
		public bool FoodConsumtionFilterActive
		{
			get
			{
				return this.FoodConsumptionFilter.Any((GSILocalPair {25648}) => {25648}.Count > 0 && Global.Player.UsedShipPlayer.PrivateResourcesOfHold[{25648}.ID] > 0);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0010C29C File Offset: 0x0010A49C
		public LogbookController Logbook
		{
			get
			{
				ServerList.ServerInfo currentServer = Global.GetCurrentServer();
				string key = ((currentServer != null) ? currentServer.Id : null) ?? "none";
				LogbookController result;
				if (!this.LogbookDict.TryGetValue(key, out result))
				{
					this.LogbookDict.Add(key, result = new LogbookController());
				}
				return result;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0010C2E8 File Offset: 0x0010A4E8
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x0010C2EF File Offset: 0x0010A4EF
		public TimeFormat PreferredTime
		{
			get
			{
				return LocalizedDateTime.DefaultTimeFormat;
			}
			set
			{
				LocalizedDateTime.DefaultTimeFormat = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x0010C2F7 File Offset: 0x0010A4F7
		// (set) Token: 0x06001DA8 RID: 7592 RVA: 0x0010C2FF File Offset: 0x0010A4FF
		public bool EnableDragDrop
		{
			get
			{
				return this.{25644};
			}
			set
			{
				this.{25644} = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0010C308 File Offset: 0x0010A508
		// (set) Token: 0x06001DAA RID: 7594 RVA: 0x0010C310 File Offset: 0x0010A510
		public ControlLayout ControlLayout { get; private set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0010C319 File Offset: 0x0010A519
		public string ShipControlSchemeName
		{
			get
			{
				return this.kb_ds_Forward.KeyToString + this.kb_ds_Left.KeyToString + this.kb_ds_Backward.KeyToString + this.kb_ds_Right.KeyToString;
			}
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0010C34C File Offset: 0x0010A54C
		public void LoadProperty()
		{
			this.FullscreenEnabled = this.FullscreenEnabled;
			this.SoundVolume = this.SoundVolume;
			this.UiScale = this.UiScale;
			this.MonitorFrequency = this.MonitorFrequency;
			this.Loaded = true;
			Global.Render.UpdateRenderTargets();
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0010C434 File Offset: 0x0010A634
		public void SetControlLayoutSpecifics(ControlLayout {25597})
		{
			this.ControlLayout = {25597};
			if ({25597} == ControlLayout.Azerty)
			{
				this.{25612}();
				return;
			}
			this.{25611}();
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0010C450 File Offset: 0x0010A650
		private void {25598}()
		{
			this.kb_KeyShowMouse = new KeynameHolder(Keys.LeftControl, new Buttons[]
			{
				Buttons.LeftStick
			});
			this.kb_Escape = new KeynameHolder(Keys.Escape, new Buttons[]
			{
				Buttons.Back
			});
			this.kb_Action = new KeynameHolder(Keys.Space, new Buttons[]
			{
				Buttons.A
			});
			this.kb_Mortar_ModifierKey = new KeynameHolder(Keys.LeftShift, new Buttons[]
			{
				Buttons.RightShoulder
			});
			this.kb_Mortar_Whole = new KeynameHolder(Keys.D1, new Buttons[]
			{
				Buttons.RightTrigger
			});
			this.kb_Mortar_Splinter = new KeynameHolder(Keys.D2, new Buttons[]
			{
				Buttons.LeftTrigger
			});
			this.kb_SwithPanelFalkonet = new KeynameHolder(Keys.D7, new Buttons[]
			{
				Buttons.DPadLeft
			})
			{
				GamepadBlockKey = new Buttons?(Buttons.X)
			};
			this.kb_SwithPanelPowderKeg = new KeynameHolder(Keys.D8, new Buttons[]
			{
				Buttons.DPadRight
			})
			{
				GamepadBlockKey = new Buttons?(Buttons.X)
			};
			this.kb_SwithPanelCannonBall_Gamepad = new KeynameHolder(Keys.None, new Buttons[]
			{
				Buttons.DPadUp
			})
			{
				GamepadBlockKey = new Buttons?(Buttons.X)
			};
			this.kb_SimpleShot = new KeynameHolder(Keys.D1, Array.Empty<Buttons>());
			this.kb_FireShot = new KeynameHolder(Keys.D2, Array.Empty<Buttons>());
			this.kb_AntisailShot = new KeynameHolder(Keys.D3, Array.Empty<Buttons>());
			this.kb_AnticrewShot = new KeynameHolder(Keys.D4, Array.Empty<Buttons>());
			this.kb_OpenDebugPanel = new KeynameHolder(Keys.F1, Array.Empty<Buttons>());
			this.kb_FreeMode = new KeynameHolder(Keys.F4, Array.Empty<Buttons>());
			this.kb_FreeCamera = new KeynameHolder(Keys.F5, Array.Empty<Buttons>());
			this.kb_Screenshoot = new KeynameHolder(Keys.F11, Array.Empty<Buttons>());
			this.kb_ChatEnter = new KeynameHolder(Keys.Enter, Array.Empty<Buttons>());
			this.kb_SwitchCascadeGunMode = new KeynameHolder(Keys.LeftAlt, Array.Empty<Buttons>());
			this.kb_OpenLogbook = new KeynameHolder(Keys.Tab, Array.Empty<Buttons>());
			this.kb_ds_Backward = new KeynameHolder(Keys.S, new Buttons[]
			{
				Buttons.LeftThumbstickDown
			});
			this.kb_ds_Right = new KeynameHolder(Keys.D, new Buttons[]
			{
				Buttons.LeftThumbstickRight
			});
			this.kb_OpenHold = new KeynameHolder(Keys.T, new Buttons[]
			{
				Buttons.Y
			});
			this.kb_Accept = new KeynameHolder(Keys.Y, new Buttons[]
			{
				Buttons.B
			});
			this.kb_Undo = new KeynameHolder(Keys.N, Array.Empty<Buttons>());
			this.kb_Mending = new KeynameHolder(Keys.R, new Buttons[]
			{
				Buttons.DPadDown,
				Buttons.X
			});
			this.kb_Item1 = new KeynameHolder(Keys.X, new Buttons[]
			{
				Buttons.DPadLeft,
				Buttons.X
			});
			this.kb_Item2 = new KeynameHolder(Keys.C, new Buttons[]
			{
				Buttons.DPadUp,
				Buttons.X
			});
			this.kb_Item3 = new KeynameHolder(Keys.V, new Buttons[]
			{
				Buttons.DPadRight,
				Buttons.X
			});
			this.kb_ItemExtra = new KeynameHolder(Keys.F, new Buttons[]
			{
				Buttons.DPadDown,
				Buttons.X
			});
			this.kb_ShipPerk = new KeynameHolder(Keys.J, new Buttons[]
			{
				Buttons.DPadDown,
				Buttons.X
			});
			this.kb_Spyglass = new KeynameHolder(Keys.E, new Buttons[]
			{
				Buttons.RightStick
			});
			this.kb_ThrowPowderKeg = new KeynameHolder(Keys.B, new Buttons[]
			{
				Buttons.LeftShoulder
			});
			this.kb_ChatHide = new KeynameHolder(Keys.P, Array.Empty<Buttons>());
			this.kb_OpenQuestPanel = new KeynameHolder(Keys.L, Array.Empty<Buttons>());
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0010C7D0 File Offset: 0x0010A9D0
		public static LocalSettings LoadOrCreate()
		{
			LocalSettings result;
			try
			{
				LocalSettings localSettings = DeltaStream.UnboxingFromFileTK<LocalSettings>(LocalSettings.FilePath);
				if (localSettings != null)
				{
					result = localSettings;
				}
				else
				{
					localSettings = new LocalSettings();
					((ITKSerializable)localSettings).PreInit();
					result = localSettings;
				}
			}
			catch (Exception {25356})
			{
				Helpers.SendError({25356}, "LoadSettings", true, false);
				LocalSettings localSettings2 = new LocalSettings();
				((ITKSerializable)localSettings2).PreInit();
				result = localSettings2;
			}
			return result;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0010C82C File Offset: 0x0010AA2C
		public void OnSave(bool {25599})
		{
			string path = LocalSettings.FilePath;
			try
			{
				Directory.CreateDirectory(LocalSettings.WosbDir);
				if ({25599})
				{
					DeltaStream.BoxingTk(this, LocalSettings.holder);
					ThreadPool.QueueUserWorkItem(delegate(object {25650})
					{
						try
						{
							DeltaStream.InternalFileWriter(path, LocalSettings.holder, true);
						}
						catch (Exception)
						{
						}
					});
				}
				else
				{
					DeltaStream.BoxingToFileTK(this, path, LocalSettings.holder, true);
				}
			}
			catch (Exception {25356})
			{
				Helpers.SendError({25356}, "SaveSettings", true, false);
			}
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0010C8AC File Offset: 0x0010AAAC
		public void Test()
		{
			this.{25598}();
			this.SetControlLayoutSpecifics(ControlLayout.Qwerty);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0010C8BC File Offset: 0x0010AABC
		internal void ResetForNewAccount()
		{
			this.AutomoveToStorage = true;
			this.AutomoveToStorageMendRes = false;
			this.EnableGameTooltips = true;
			this.DisableTrackingForQuests.Clear();
			this.SavedEquipment.Clear();
			this.ShowAmmoCountUi = false;
			this.SelectedCannonBalls = new SelectedCannonBalls();
			this.SelectedFalkonetsID = 13;
			this.SelectedPowderKegs = 1;
			this.SelectedMortarBallsID = 6;
			this.WindArrowMode = 2;
			this.DraggablesPositions = new Dictionary<string, Vector2>();
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0010C930 File Offset: 0x0010AB30
		public void ResetDragables(Action {25600} = null)
		{
			Global.Game.GetInterfaceManager.ClearAll();
			this.DraggablesPositions = new Dictionary<string, Vector2>();
			if (Global.Player.IsPortEntry)
			{
				Global.Game.ScenePort.ReloadPort();
			}
			else
			{
				Global.Game.SceneGame.CreateInterface();
			}
			if ({25600} != null)
			{
				{25600}();
			}
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0010C98C File Offset: 0x0010AB8C
		public void SetChatOnlyMyLanguageDefaultValue()
		{
			this.ChatOnlyMyLanguage = PlatformTuning.ChatOnlyMyLanguageDefault;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0010C99C File Offset: 0x0010AB9C
		void ITKSerializable.{25601}()
		{
			this.{25598}();
			this.SetControlLayoutSpecifics(ControlLayout.Qwerty);
			this.{25627} = LocalSettings.DrawFrequency.fps60;
			this.HighDetailing = false;
			this.{25629} = false;
			if (Rand.Chanse(50f))
			{
				this.SharpWater = true;
				this.LightWater = false;
				this.ProgressiveFresnel = false;
			}
			else
			{
				this.SharpWater = false;
				this.LightWater = false;
				this.ProgressiveFresnel = true;
			}
			this.{25630} = true;
			this.{25631} = LocalSettings.RendererShadows.Off;
			this.{25632} = LocalSettings.RendererAntialias.Off;
			this.{25633} = false;
			this.HeatDeformations = false;
			this.CrewMode = LocalSettings.CrewDisplayMode.OnlyMyShip;
			this.PostprocessorType = LocalSettings.Postprocessor.Basic;
			this.ForceCameraEffect = true;
			this.VoicesMode = LocalSettings.ShipVoices.Old;
			this.{25634} = false;
			this.SailTransparancy = LocalSettings.SailTransparancyMode.WhenZoom;
			this.{25636} = false;
			this.RightMouseAction = RightMouseKeyAction.SingleCannonGun;
			this.ShowAmmoCountUi = false;
			this.BrightNight = false;
			this.{25642} = true;
			this.{25643} = false;
			this.DisableSmoothCamera = false;
			this.SightIndex = 1;
			this.Automending = true;
			this.AutomoveToStorage = true;
			this.AutomoveToStorageMendRes = false;
			this.NoHideSight = true;
			this.{25628} = true;
			this.AutoPaySalarySpecialUnits = false;
			this.{25638} = ((Engine.MonitorWidth > 1700) ? 1.5f : 1.1f);
			this.AutoFoodConsumtionOnlyOnNorth = true;
			this.EnableGameTooltips = true;
			this.BigChat = false;
			this.BigChatFont = false;
			this.{25635} = new Dictionary<string, SavedShipEquipmentCollection>();
			this.FavoriteSpecialUnits = new Tlist<byte>();
			this.FavoriteShips = new Tlist<byte>();
			this.IsAutoHoldCrewManagementEnabled = false;
			this.SpyglassAnimation = true;
			this.IsFirstLaunch = true;
			this.HorizonTilt = true;
			this.SoundVolume = 0.4f;
			this.{25640} = 0.7f;
			this.{25641} = 0.9f;
			this.MouseSensetivity = 1f;
			this.SaveLoginData = true;
			this.GammaSetting = 0f;
			this.WindArrowMode = 2;
			this.{25637} = ((Engine.MonitorWidth >= 1920) ? LocalSettings.GlobalUiScale.Increase : LocalSettings.GlobalUiScale.Normal);
			this.Language = new LanguageSettings();
			this.SelectedCannonBalls = new SelectedCannonBalls();
			this.SelectedFalkonetsID = 13;
			this.SelectedPowderKegs = 1;
			this.SelectedMortarBallsID = 6;
			this.DeathController = new DeathController();
			this.GamemodeDoublones = 0;
			this.NotSelectedWorldMapicons = {22887}.NotSelectedByDefault.Clone();
			this.DisableTrackingForQuests = new Tlist<int>();
			this.MinimapCompas = true;
			this.FloraQuality = LocalSettings.FloraQualityMode.Normal;
			this.SavedServerID = string.Empty;
			this.PreferredTime = TimeFormat.PreferLocalTime;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00003100 File Offset: 0x00001300
		void ITKSerializable.{25602}()
		{
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0010CC04 File Offset: 0x0010AE04
		[return: Nullable(2)]
		public SavedShipEquipment GetEquipmentSave(int {25603}, PlayerShipDynamicInfo {25604})
		{
			return this.SavedEquipment.FirstOrDefault((SavedShipEquipment {25651}) => (int){25651}.SlotIndex == {25603} && {25651}.ShipInfoId == {25604}.CraftFrom.ID);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0010CC3C File Offset: 0x0010AE3C
		void ITKSerializable.{25605}(TKWriterExtern {25606})
		{
			short {10975} = 204;
			Point mouseControlInversion = this.MouseControlInversion;
			{25606}.WriteStruct<Point>({10975}, mouseControlInversion);
			{25606}.WriteStruct<float>(45, this.MouseSensetivity);
			{25606}.Write(3, this.IsFirstLaunch);
			{25606}.WriteStruct<float>(5, this.{25639});
			{25606}.Write(12, this.{25642});
			{25606}.Write(13, this.{25643});
			{25606}.Write(15, this.SaveLoginData);
			{25606}.WriteStruct<float>(24, this.{25640});
			{25606}.WriteStruct<int>(25, this.SightIndex);
			{25606}.Write(27, this.Automending);
			{25606}.Write(29, this.AutomoveToStorage);
			{25606}.WriteEnum(30, this.SailTransparancy);
			{25606}.WriteStruct<float>(33, this.GammaSetting);
			{25606}.Write(34, this.NoHideSight);
			{25606}.WriteStruct<int>(36, this.SightIndex);
			{25606}.Write(38, this.{25628});
			{25606}.Write(39, this.AutomoveToStorageMendRes);
			{25606}.Write(40, this.{25636});
			{25606}.WriteStruct<float>(42, this.{25641});
			{25606}.WriteEnum(46, this.CrewMode);
			{25606}.Write(47, this.HighDetailing);
			{25606}.Write(48, this.{25629});
			{25606}.Write(50, this.{25630});
			{25606}.WriteEnum(51, this.{25631});
			{25606}.WriteEnum(52, this.{25632});
			{25606}.Write(53, this.{25633});
			{25606}.Write(54, this.HeatDeformations);
			{25606}.Write(56, this.LowFpsToolTipWasShown);
			{25606}.WriteByte(57, (byte)this.RightMouseAction);
			{25606}.Write(58, this.ForceCameraEffect);
			{25606}.WriteStruct<int>(59, this.WindArrowMode);
			{25606}.WriteStruct<int>(61, this.LaunchCount);
			{25606}.Write(62, this.LightWater);
			{25606}.Write(63, this.ShowAmmoCountUi);
			{25606}.WriteByte(65, (byte)this.VoicesMode);
			{25606}.Write(67, this.{25634});
			{25606}.WriteStruct<int>(71, this.SelectedFalkonetsID);
			{25606}.WriteStruct<int>(72, this.SelectedMortarBallsID);
			{25606}.WriteStruct<int>(73, this.SelectedPowderKegs);
			{25606}.Write(78, this.HorizonTilt);
			{25606}.WriteIMP<DeathController>(80, this.DeathController);
			{25606}.WriteByte(81, (byte)this.PostprocessorType);
			{25606}.Write(82, this.SpyglassAnimation);
			{25606}.WriteStruct<ushort>(83, this.SavedGuildVersion);
			{25606}.WriteContent(84, new Action<WriterExtern>(this.{25613}));
			{25606}.WriteStruct<float>(142, this.AutoFoodConsumtion);
			{25606}.WriteByte(86, (byte)this.{25637});
			{25606}.WriteStruct<int>(87, this.SavedGuildAnnouncmentVersion);
			{25606}.WriteContent(225, new Action<WriterExtern>(this.{25615}));
			{25606}.WriteIMP<GSI>(89, this.FoodConsumptionFilter);
			{25606}.WriteByte(90, (byte)this.{25627});
			short {10975}2 = 91;
			int value = this.GamemodeDoublones.Value;
			{25606}.WriteStruct<int>({10975}2, value);
			{25606}.Write(92, this.AutoPaySalarySpecialUnits);
			{25606}.WriteByte32bitSize(93, this.FavoriteSpecialUnits.ToArray());
			{25606}.Write(94, this.AutoFoodConsumtionOnlyOnNorth);
			{25606}.WriteContent(96, new Action<WriterExtern>(this.{25617}));
			{25606}.Write(97, this.SharpWater);
			{25606}.WriteByte(98, (byte)this.PreferredCurrency);
			{25606}.WriteByte(99, (byte)this.PreferredTime);
			{25606}.WriteStruct<float>(200, this.{25638});
			{25606}.Write(201, this.BrightNight);
			{25606}.WriteByte(212, (byte)this.FloraQuality);
			{25606}.Write(228, this.PreferredPayRegion);
			{25606}.WriteEnum(202, this.kb_OpenQuestPanel.Key);
			{25606}.WriteEnum(203, this.ControlLayout);
			{25606}.WriteEnum(100, this.kb_KeyShowMouse.Key);
			{25606}.WriteEnum(101, this.kb_Screenshoot.Key);
			{25606}.WriteEnum(102, this.kb_ds_Forward.Key);
			{25606}.WriteEnum(103, this.kb_ds_Backward.Key);
			{25606}.WriteEnum(104, this.kb_ds_Left.Key);
			{25606}.WriteEnum(105, this.kb_ds_Right.Key);
			{25606}.WriteEnum(107, this.kb_Mending.Key);
			{25606}.WriteEnum(108, this.kb_ChatEnter.Key);
			{25606}.WriteEnum(109, this.kb_Escape.Key);
			{25606}.WriteEnum(110, this.kb_Accept.Key);
			{25606}.WriteEnum(111, this.kb_Undo.Key);
			{25606}.WriteEnum(112, this.kb_Spyglass.Key);
			{25606}.WriteEnum(113, this.kb_FreeMode.Key);
			{25606}.WriteEnum(114, this.kb_Action.Key);
			{25606}.WriteEnum(115, this.kb_Item1.Key);
			{25606}.WriteEnum(116, this.kb_Item2.Key);
			{25606}.WriteEnum(117, this.kb_Item3.Key);
			{25606}.WriteEnum(119, this.kb_FreeCamera.Key);
			{25606}.WriteEnum(120, this.kb_SendGroupCommand.Key);
			{25606}.WriteEnum(121, this.kb_Falkonet.Key);
			{25606}.WriteEnum(122, this.kb_Map.Key);
			{25606}.WriteEnum(123, this.kb_SwitchCascadeGunMode.Key);
			{25606}.WriteEnum(124, this.kb_ThrowPowderKeg.Key);
			{25606}.WriteEnum(133, this.kb_SwithPanelPowderKeg.Key);
			{25606}.WriteEnum(127, this.kb_Mortar_ModifierKey.Key);
			{25606}.WriteEnum(129, this.kb_ChatHide.Key);
			{25606}.WriteEnum(131, this.kb_OpenHold.Key);
			{25606}.WriteEnum(134, this.kb_SwithPanelFalkonet.Key);
			{25606}.WriteEnum(136, this.kb_ItemExtra.Key);
			{25606}.WriteEnum(137, this.kb_SwithPanelCannonBall_Gamepad.Key);
			{25606}.WriteEnum(138, this.kb_OpenDebugPanel.Key);
			{25606}.Write(139, this.ShowNicknames);
			{25606}.Write(140, this.IsAutoHoldCrewManagementEnabled);
			{25606}.Write(143, this.EnableGameTooltips);
			{25606}.Write(144, this.BigChat);
			{25606}.Write(145, this.BigChatFont);
			{25606}.WriteIMP<LsSavedCache>(247, this.LsSavedCacheData);
			{25606}.WriteByte(148, byte.MaxValue);
			{25606}.WriteIMP<LanguageSettings>(149, this.Language);
			{25606}.WriteEnum(150, this.kb_OpenLogbook.Key);
			{25606}.WriteEnum(205, this.kb_SimpleShot.Key);
			{25606}.WriteEnum(206, this.kb_FireShot.Key);
			{25606}.WriteEnum(207, this.kb_AntisailShot.Key);
			{25606}.WriteEnum(208, this.kb_AnticrewShot.Key);
			{25606}.WriteContent(151, new Action<WriterExtern>(this.{25619}));
			{25606}.Write(211, this.MinimapCompas);
			{25606}.Write(213, this.SavedServerID);
			{25606}.Write(215, this.ChatOnlyMyLanguage);
			{25606}.Write(216, this.HalfTransparentSailDesign);
			{25606}.WriteStruct<int>(217, this.BoardingEducationStatus);
			{25606}.WriteContent(218, new Action<WriterExtern>(this.{25621}));
			{25606}.WriteContent(219, new Action<WriterExtern>(this.{25623}));
			{25606}.WriteEnum(220, this.kb_ShipPerk.Key);
			{25606}.WriteByte32bitSize(221, this.FavoriteShips.ToArray());
			{25606}.Write(222, this.DisableSmoothCamera);
			{25606}.WriteDict<Vector2>(223, this.DraggablesPositions);
			{25606}.Write(224, this.EnableDragDrop);
			{25606}.Write<SavedCredentials>(226, this.AuthCreds);
			{25606}.Write(229, this.ProgressiveFresnel);
			short {10975}3 = 230;
			value = this.SelectedCannonBalls.Value;
			{25606}.WriteStruct<int>({10975}3, value);
			{25606}.WriteContent(232, new Action<WriterExtern>(this.{25625}));
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0010D614 File Offset: 0x0010B814
		void ITKSerializable.{25607}(short {25608}, WriterExtern {25609}, out bool {25610})
		{
			{25610} = true;
			switch ({25608})
			{
			case 3:
				{25609}.ReadBoolean(out this.IsFirstLaunch);
				return;
			case 5:
				{25609}.ReadStruct<float>(out this.{25639});
				return;
			case 7:
			{
				byte b = {25609}.ReadByte();
				this.{25631} = ((b == 0) ? LocalSettings.RendererShadows.Off : ((b == 1) ? LocalSettings.RendererShadows.Medium : LocalSettings.RendererShadows.High));
				if (b == 3)
				{
					this.{25629} = true;
					return;
				}
				return;
			}
			case 9:
			{
				byte b = {25609}.ReadByte();
				this.HighDetailing = (b > 0);
				return;
			}
			case 10:
			{
				byte b = {25609}.ReadByte();
				this.{25632} = ((b > 0) ? LocalSettings.RendererAntialias.Txaa : LocalSettings.RendererAntialias.Off);
				return;
			}
			case 12:
				{25609}.ReadBoolean(out this.{25642});
				return;
			case 13:
				{25609}.ReadBoolean(out this.{25643});
				return;
			case 15:
				{25609}.ReadBoolean(out this.SaveLoginData);
				return;
			case 16:
				{25609}.ReadString(out LocalSettings.old_LastLogin, null);
				return;
			case 17:
				{25609}.ReadString(out LocalSettings.old_LastPassword, null);
				return;
			case 19:
			{
				byte b = {25609}.ReadByte();
				this.{25630} = (b > 0);
				return;
			}
			case 21:
			{
				byte b = {25609}.ReadByte();
				this.{25633} = (b > 0);
				this.HeatDeformations = (b > 0);
				return;
			}
			case 24:
				{25609}.ReadStruct<float>(out this.{25640});
				return;
			case 25:
				{25609}.ReadStruct<int>(out this.SightIndex);
				return;
			case 27:
				{25609}.ReadBoolean(out this.Automending);
				return;
			case 29:
				{25609}.ReadBoolean(out this.AutomoveToStorage);
				return;
			case 30:
				{25609}.ReadEnum<LocalSettings.SailTransparancyMode>(out this.SailTransparancy);
				return;
			case 33:
				{25609}.ReadStruct<float>(out this.GammaSetting);
				return;
			case 34:
				{25609}.ReadBoolean(out this.NoHideSight);
				return;
			case 36:
				{25609}.ReadStruct<int>(out this.SightIndex);
				return;
			case 38:
				{25609}.ReadBoolean(out this.{25628});
				return;
			case 39:
				{25609}.ReadBoolean(out this.AutomoveToStorageMendRes);
				return;
			case 40:
				{25609}.ReadBoolean(out this.{25636});
				return;
			case 42:
				{25609}.ReadStruct<float>(out this.{25641});
				return;
			case 45:
				{25609}.ReadStruct<float>(out this.MouseSensetivity);
				return;
			case 46:
				{25609}.ReadEnum<LocalSettings.CrewDisplayMode>(out this.CrewMode);
				return;
			case 47:
				{25609}.ReadBoolean(out this.HighDetailing);
				this.FloraQuality = (this.HighDetailing ? LocalSettings.FloraQualityMode.High : LocalSettings.FloraQualityMode.Normal);
				return;
			case 48:
				{25609}.ReadBoolean(out this.{25629});
				return;
			case 50:
				{25609}.ReadBoolean(out this.{25630});
				return;
			case 51:
				{25609}.ReadEnum<LocalSettings.RendererShadows>(out this.{25631});
				return;
			case 52:
				{25609}.ReadEnum<LocalSettings.RendererAntialias>(out this.{25632});
				return;
			case 53:
				{25609}.ReadBoolean(out this.{25633});
				return;
			case 54:
				{25609}.ReadBoolean(out this.HeatDeformations);
				return;
			case 55:
			{
				bool flag;
				{25609}.ReadBoolean(out flag);
				if (flag)
				{
					this.PostprocessorType = LocalSettings.Postprocessor.Tonemap1;
					return;
				}
				return;
			}
			case 56:
				{25609}.ReadBoolean(out this.LowFpsToolTipWasShown);
				return;
			case 57:
				this.RightMouseAction = (RightMouseKeyAction){25609}.ReadByte();
				return;
			case 58:
				{25609}.ReadBoolean(out this.ForceCameraEffect);
				return;
			case 59:
				{25609}.ReadStruct<int>(out this.WindArrowMode);
				return;
			case 61:
				{25609}.ReadStruct<int>(out this.LaunchCount);
				this.LaunchCount++;
				return;
			case 62:
				{25609}.ReadBoolean(out this.LightWater);
				return;
			case 63:
				{25609}.ReadBoolean(out this.ShowAmmoCountUi);
				return;
			case 65:
				this.VoicesMode = (LocalSettings.ShipVoices){25609}.ReadByte();
				return;
			case 66:
			{
				byte b2 = {25609}.ReadByte();
				this.{25638} = ((b2 == 0) ? 1.1f : ((b2 == 1) ? 1.3f : 1.5f));
				return;
			}
			case 67:
				{25609}.ReadBoolean(out this.{25634});
				return;
			case 71:
				{25609}.ReadStruct<int>(out this.SelectedFalkonetsID);
				return;
			case 72:
				{25609}.ReadStruct<int>(out this.SelectedMortarBallsID);
				return;
			case 73:
				{25609}.ReadStruct<int>(out this.SelectedPowderKegs);
				return;
			case 78:
				{25609}.ReadBoolean(out this.HorizonTilt);
				return;
			case 80:
				{25609}.ReadIMPS<DeathController>(out this.DeathController);
				return;
			case 81:
				this.PostprocessorType = (LocalSettings.Postprocessor){25609}.ReadByte();
				return;
			case 82:
				{25609}.ReadBoolean(out this.SpyglassAnimation);
				return;
			case 83:
				{25609}.ReadStruct<ushort>(out this.SavedGuildVersion);
				return;
			case 84:
			{
				byte b3 = {25609}.ReadByte();
				this.UnreadGamepedia = new Tlist<EducationOnboarding>((int)b3);
				for (int i = 0; i < (int)b3; i++)
				{
					long num;
					{25609}.ReadStruct<long>(out num);
					Tlist<EducationOnboarding> unreadGamepedia = this.UnreadGamepedia;
					EducationOnboarding educationOnboarding = (EducationOnboarding)num;
					unreadGamepedia.Add(educationOnboarding);
				}
				return;
			}
			case 85:
			{
				bool flag2;
				{25609}.ReadBoolean(out flag2);
				this.AutoFoodConsumtion = (flag2 ? 0f : 0.7f);
				return;
			}
			case 86:
				this.{25637} = (LocalSettings.GlobalUiScale){25609}.ReadByte();
				return;
			case 87:
				{25609}.ReadStruct<int>(out this.SavedGuildAnnouncmentVersion);
				return;
			case 88:
			{
				LogbookController value;
				{25609}.ReadIMPS<LogbookController>(out value);
				this.LogbookDict.Add("ru1", value);
				return;
			}
			case 89:
				{25609}.ReadIMPS<GSI>(out this.FoodConsumptionFilter);
				return;
			case 90:
				this.{25627} = (LocalSettings.DrawFrequency){25609}.ReadByte();
				return;
			case 91:
			{
				int {11354};
				{25609}.ReadStruct<int>(out {11354});
				this.GamemodeDoublones = {11354};
				return;
			}
			case 92:
				{25609}.ReadBoolean(out this.AutoPaySalarySpecialUnits);
				return;
			case 93:
			{
				byte[] collection;
				{25609}.ReadBytes32bitSize(out collection);
				this.FavoriteSpecialUnits = new Tlist<byte>(collection);
				return;
			}
			case 94:
				{25609}.ReadBoolean(out this.AutoFoodConsumtionOnlyOnNorth);
				return;
			case 96:
			{
				byte b3 = {25609}.ReadByte();
				this.NotSelectedWorldMapicons = new Tlist<{22887}.Id>((int)b3);
				for (int j = 0; j < (int)b3; j++)
				{
					Tlist<{22887}.Id> notSelectedWorldMapicons = this.NotSelectedWorldMapicons;
					{22887}.Id id = ({22887}.Id){25609}.ReadByte();
					notSelectedWorldMapicons.Add(id);
				}
				return;
			}
			case 97:
				{25609}.ReadBoolean(out this.SharpWater);
				return;
			case 98:
				this.PreferredCurrency = (Currency){25609}.ReadByte();
				return;
			case 99:
				this.PreferredTime = (TimeFormat){25609}.ReadByte();
				return;
			case 100:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_KeyShowMouse = new KeynameHolder({25390}, this.kb_KeyShowMouse.GamepadKeys);
				return;
			}
			case 101:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Screenshoot = new KeynameHolder({25390}, this.kb_Screenshoot.GamepadKeys);
				return;
			}
			case 102:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_ds_Forward = new KeynameHolder({25390}, this.kb_ds_Forward.GamepadKeys);
				return;
			}
			case 103:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_ds_Backward = new KeynameHolder({25390}, this.kb_ds_Backward.GamepadKeys);
				return;
			}
			case 104:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_ds_Left = new KeynameHolder({25390}, this.kb_ds_Left.GamepadKeys);
				return;
			}
			case 105:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_ds_Right = new KeynameHolder({25390}, this.kb_ds_Right.GamepadKeys);
				return;
			}
			case 107:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Mending = new KeynameHolder({25390}, this.kb_Mending.GamepadKeys);
				return;
			}
			case 108:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_ChatEnter = new KeynameHolder({25390}, this.kb_ChatEnter.GamepadKeys);
				return;
			}
			case 109:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Escape = new KeynameHolder({25390}, this.kb_Escape.GamepadKeys);
				return;
			}
			case 110:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Accept = new KeynameHolder({25390}, this.kb_Accept.GamepadKeys);
				return;
			}
			case 111:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Undo = new KeynameHolder({25390}, this.kb_Undo.GamepadKeys);
				return;
			}
			case 112:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Spyglass = new KeynameHolder({25390}, this.kb_Spyglass.GamepadKeys);
				return;
			}
			case 113:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_FreeMode = new KeynameHolder({25390}, this.kb_FreeMode.GamepadKeys);
				return;
			}
			case 114:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Action = new KeynameHolder({25390}, this.kb_Action.GamepadKeys);
				return;
			}
			case 115:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Item1 = new KeynameHolder({25390}, this.kb_Item1.GamepadKeys);
				return;
			}
			case 116:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Item2 = new KeynameHolder({25390}, this.kb_Item2.GamepadKeys);
				return;
			}
			case 117:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Item3 = new KeynameHolder({25390}, this.kb_Item3.GamepadKeys);
				return;
			}
			case 119:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_FreeCamera = new KeynameHolder({25390}, this.kb_FreeCamera.GamepadKeys);
				return;
			}
			case 120:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_SendGroupCommand = new KeynameHolder({25390}, this.kb_SendGroupCommand.GamepadKeys);
				return;
			}
			case 121:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Falkonet = new KeynameHolder({25390}, this.kb_Falkonet.GamepadKeys);
				return;
			}
			case 122:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Map = new KeynameHolder({25390}, this.kb_Map.GamepadKeys);
				return;
			}
			case 123:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_SwitchCascadeGunMode = new KeynameHolder({25390}, this.kb_SwitchCascadeGunMode.GamepadKeys);
				return;
			}
			case 124:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_ThrowPowderKeg = new KeynameHolder({25390}, this.kb_ThrowPowderKeg.GamepadKeys);
				return;
			}
			case 127:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_Mortar_ModifierKey = new KeynameHolder({25390}, this.kb_Mortar_ModifierKey.GamepadKeys);
				return;
			}
			case 129:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_ChatHide = new KeynameHolder({25390}, this.kb_ChatHide.GamepadKeys);
				return;
			}
			case 131:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_OpenHold = new KeynameHolder({25390}, this.kb_OpenHold.GamepadKeys);
				return;
			}
			case 133:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_SwithPanelPowderKeg = new KeynameHolder({25390}, this.kb_SwithPanelPowderKeg.GamepadKeys);
				return;
			}
			case 134:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_SwithPanelFalkonet = new KeynameHolder({25390}, this.kb_SwithPanelFalkonet.GamepadKeys);
				return;
			}
			case 136:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_ItemExtra = new KeynameHolder({25390}, this.kb_ItemExtra.GamepadKeys);
				return;
			}
			case 137:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_SwithPanelCannonBall_Gamepad = new KeynameHolder({25390}, this.kb_SwithPanelCannonBall_Gamepad.GamepadKeys);
				return;
			}
			case 138:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_OpenDebugPanel = new KeynameHolder({25390}, this.kb_OpenDebugPanel.GamepadKeys);
				return;
			}
			case 139:
				{25609}.ReadBoolean(out this.ShowNicknames);
				return;
			case 140:
				{25609}.ReadBoolean(out this.IsAutoHoldCrewManagementEnabled);
				return;
			case 142:
				{25609}.ReadStruct<float>(out this.AutoFoodConsumtion);
				return;
			case 143:
				{25609}.ReadBoolean(out this.EnableGameTooltips);
				return;
			case 144:
				{25609}.ReadBoolean(out this.BigChat);
				return;
			case 145:
				{25609}.ReadBoolean(out this.BigChatFont);
				return;
			case 148:
			{
				byte b4 = {25609}.ReadByte();
				if (b4 != 255)
				{
					this.Language.CurrentGameLocale = new Locale?((Locale)b4);
					return;
				}
				return;
			}
			case 149:
			{
				LanguageSettings languageSettings;
				{25609}.ReadIMPS<LanguageSettings>(out languageSettings);
				if (languageSettings.LauncherLocale != null || languageSettings.CurrentGameLocale != null)
				{
					this.Language = languageSettings;
					return;
				}
				return;
			}
			case 150:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_OpenLogbook = new KeynameHolder({25390}, this.kb_OpenLogbook.GamepadKeys);
				return;
			}
			case 151:
				{25609}.ReadTlist(out this.DisableTrackingForQuests);
				return;
			case 200:
				{25609}.ReadStruct<float>(out this.{25638});
				return;
			case 201:
				{25609}.ReadBoolean(out this.BrightNight);
				return;
			case 202:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_OpenQuestPanel = new KeynameHolder({25390}, this.kb_OpenQuestPanel.GamepadKeys);
				return;
			}
			case 203:
			{
				ControlLayout controlLayout;
				{25609}.ReadEnum<ControlLayout>(out controlLayout);
				this.ControlLayout = controlLayout;
				return;
			}
			case 204:
				{25609}.ReadStruct<Point>(out this.{25646});
				return;
			case 205:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_SimpleShot = new KeynameHolder({25390}, Array.Empty<Buttons>());
				return;
			}
			case 206:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_FireShot = new KeynameHolder({25390}, Array.Empty<Buttons>());
				return;
			}
			case 207:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_AntisailShot = new KeynameHolder({25390}, Array.Empty<Buttons>());
				return;
			}
			case 208:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_AnticrewShot = new KeynameHolder({25390}, Array.Empty<Buttons>());
				return;
			}
			case 211:
				{25609}.ReadBoolean(out this.MinimapCompas);
				return;
			case 212:
				this.FloraQuality = (LocalSettings.FloraQualityMode){25609}.ReadByte();
				return;
			case 213:
				{25609}.ReadString(out this.SavedServerID, null);
				if (!string.IsNullOrEmpty(LocalSettings.old_LastLogin))
				{
					this.AuthCreds.OldInit(this.SavedServerID, LocalSettings.old_LastLogin, LocalSettings.old_LastPassword);
					return;
				}
				return;
			case 215:
				{25609}.ReadBoolean(out this.ChatOnlyMyLanguage);
				return;
			case 216:
				{25609}.ReadBoolean(out this.HalfTransparentSailDesign);
				return;
			case 217:
				{25609}.ReadStruct<int>(out this.BoardingEducationStatus);
				return;
			case 218:
				{25609}.ReadTlist(out this.TestServerRememberedAuth);
				return;
			case 219:
				{25609}.ReadTlistImps(out this.TrackingNotes);
				this.TrackingNotes.RemoveAll((LogbookTrackingNote {25649}) => {25649}.GameVersion != Version.GameVersion.BuildingNumber);
				return;
			case 220:
			{
				Keys {25390};
				{25609}.ReadEnum<Keys>(out {25390});
				this.kb_ShipPerk = new KeynameHolder({25390}, this.kb_ShipPerk.GamepadKeys);
				return;
			}
			case 221:
			{
				byte[] collection2;
				{25609}.ReadBytes32bitSize(out collection2);
				this.FavoriteShips = new Tlist<byte>(collection2);
				return;
			}
			case 222:
				{25609}.ReadBoolean(out this.DisableSmoothCamera);
				return;
			case 223:
				{25609}.ReadDict(out this.DraggablesPositions);
				return;
			case 224:
				{25609}.ReadBoolean(out this.{25644});
				return;
			case 225:
			{
				byte b5 = {25609}.ReadByte();
				for (int k = 0; k < (int)b5; k++)
				{
					string key;
					{25609}.ReadString(out key, null);
					LogbookController value2;
					{25609}.ReadIMPS<LogbookController>(out value2);
					this.LogbookDict.Add(key, value2);
				}
				return;
			}
			case 226:
				{25609}.ReadITKS<SavedCredentials>(out this.AuthCreds);
				return;
			case 227:
			{
				SavedShipEquipmentCollection savedShipEquipmentCollection = new SavedShipEquipmentCollection();
				{25609}.ReadTlistImps(out savedShipEquipmentCollection.Items);
				this.{25635}.Add("ru1", savedShipEquipmentCollection);
				this.{25635}.Add("eu1", savedShipEquipmentCollection);
				this.{25635}.Add("na1", savedShipEquipmentCollection);
				return;
			}
			case 228:
				{25609}.ReadString(out this.PreferredPayRegion, null);
				return;
			case 229:
				{25609}.ReadBoolean(out this.ProgressiveFresnel);
				return;
			case 230:
			{
				int {2298};
				{25609}.ReadStruct<int>(out {2298});
				this.SelectedCannonBalls = new SelectedCannonBalls({2298});
				return;
			}
			case 231:
			{
				SavedShipEquipmentCollection savedShipEquipmentCollection2 = new SavedShipEquipmentCollection();
				{25609}.ReadTlistImps(out savedShipEquipmentCollection2.Items);
				this.{25635}.Add("ru1", savedShipEquipmentCollection2);
				this.{25635}.Add("eu1", savedShipEquipmentCollection2);
				this.{25635}.Add("na1", savedShipEquipmentCollection2);
				{25610} = false;
				return;
			}
			case 232:
				{25609}.ReadDictImps(out this.{25635});
				return;
			case 247:
				{25609}.ReadIMPS<LsSavedCache>(out this.LsSavedCacheData);
				return;
			}
			{25610} = false;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0010E678 File Offset: 0x0010C878
		[CompilerGenerated]
		private void {25611}()
		{
			this.kb_ds_Forward = new KeynameHolder(Keys.W, new Buttons[]
			{
				Buttons.LeftThumbstickUp
			});
			this.kb_ds_Left = new KeynameHolder(Keys.A, new Buttons[]
			{
				Buttons.LeftThumbstickLeft
			});
			this.kb_Map = new KeynameHolder(Keys.M, new Buttons[]
			{
				Buttons.Start
			});
			this.kb_Falkonet = new KeynameHolder(Keys.Q, new Buttons[]
			{
				Buttons.LeftTrigger
			});
			this.kb_SendGroupCommand = new KeynameHolder(Keys.Z, Array.Empty<Buttons>());
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0010E700 File Offset: 0x0010C900
		[CompilerGenerated]
		private void {25612}()
		{
			this.kb_ds_Forward = new KeynameHolder(Keys.Z, new Buttons[]
			{
				Buttons.LeftThumbstickUp
			});
			this.kb_ds_Left = new KeynameHolder(Keys.Q, new Buttons[]
			{
				Buttons.LeftThumbstickLeft
			});
			this.kb_Map = new KeynameHolder(Keys.OemSemicolon, new Buttons[]
			{
				Buttons.Start
			});
			this.kb_Falkonet = new KeynameHolder(Keys.A, new Buttons[]
			{
				Buttons.LeftTrigger
			});
			this.kb_SendGroupCommand = new KeynameHolder(Keys.W, Array.Empty<Buttons>());
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0010E78C File Offset: 0x0010C98C
		[NullableContext(1)]
		[CompilerGenerated]
		private void {25613}(WriterExtern {25614})
		{
			{25614}.WriteByte((byte)this.UnreadGamepedia.Size);
			for (int i = 0; i < this.UnreadGamepedia.Size; i++)
			{
				{25614}.WriteStruct<long>((long)this.UnreadGamepedia.Array[i]);
			}
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0010E7D8 File Offset: 0x0010C9D8
		[NullableContext(1)]
		[CompilerGenerated]
		private void {25615}(WriterExtern {25616})
		{
			{25616}.WriteByte((byte)this.LogbookDict.Count);
			foreach (KeyValuePair<string, LogbookController> keyValuePair in this.LogbookDict)
			{
				{25616}.Write(keyValuePair.Key, null);
				{25616}.Write<LogbookController>(keyValuePair.Value);
			}
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0010E854 File Offset: 0x0010CA54
		[NullableContext(1)]
		[CompilerGenerated]
		private void {25617}(WriterExtern {25618})
		{
			{25618}.WriteByte((byte)this.NotSelectedWorldMapicons.Size);
			for (int i = 0; i < this.NotSelectedWorldMapicons.Size; i++)
			{
				{25618}.WriteByte((byte)this.NotSelectedWorldMapicons.Array[i]);
			}
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0010E89C File Offset: 0x0010CA9C
		[NullableContext(1)]
		[CompilerGenerated]
		private void {25619}(WriterExtern {25620})
		{
			{25620}.WriteTlist(this.DisableTrackingForQuests);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0010E8AA File Offset: 0x0010CAAA
		[NullableContext(1)]
		[CompilerGenerated]
		private void {25621}(WriterExtern {25622})
		{
			{25622}.WriteTlist(this.TestServerRememberedAuth);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0010E8B8 File Offset: 0x0010CAB8
		[NullableContext(1)]
		[CompilerGenerated]
		private void {25623}(WriterExtern {25624})
		{
			{25624}.WriteTlistImps(this.TrackingNotes);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0010E8C6 File Offset: 0x0010CAC6
		[NullableContext(1)]
		[CompilerGenerated]
		private void {25625}(WriterExtern {25626})
		{
			{25626}.WriteDictImps(this.{25635});
		}

		// Token: 0x04001CF2 RID: 7410
		private LocalSettings.DrawFrequency {25627};

		// Token: 0x04001CF3 RID: 7411
		public bool DisableSmoothCamera;

		// Token: 0x04001CF4 RID: 7412
		public LocalSettings.CrewDisplayMode CrewMode;

		// Token: 0x04001CF5 RID: 7413
		private bool {25628};

		// Token: 0x04001CF6 RID: 7414
		public bool HighDetailing;

		// Token: 0x04001CF7 RID: 7415
		public LocalSettings.FloraQualityMode FloraQuality;

		// Token: 0x04001CF8 RID: 7416
		private bool {25629};

		// Token: 0x04001CF9 RID: 7417
		public bool LightWater;

		// Token: 0x04001CFA RID: 7418
		public bool SharpWater;

		// Token: 0x04001CFB RID: 7419
		public bool ProgressiveFresnel;

		// Token: 0x04001CFC RID: 7420
		private bool {25630};

		// Token: 0x04001CFD RID: 7421
		private LocalSettings.RendererShadows {25631};

		// Token: 0x04001CFE RID: 7422
		private LocalSettings.RendererAntialias {25632};

		// Token: 0x04001CFF RID: 7423
		private bool {25633};

		// Token: 0x04001D00 RID: 7424
		public bool HeatDeformations;

		// Token: 0x04001D01 RID: 7425
		private bool {25634};

		// Token: 0x04001D02 RID: 7426
		public LocalSettings.Postprocessor PostprocessorType;

		// Token: 0x04001D03 RID: 7427
		public float MouseSensetivity;

		// Token: 0x04001D04 RID: 7428
		public bool IsFirstLaunch;

		// Token: 0x04001D05 RID: 7429
		public bool SaveLoginData;

		// Token: 0x04001D06 RID: 7430
		public int SightIndex;

		// Token: 0x04001D07 RID: 7431
		public bool NoHideSight;

		// Token: 0x04001D08 RID: 7432
		public int WindArrowMode;

		// Token: 0x04001D09 RID: 7433
		public bool Automending;

		// Token: 0x04001D0A RID: 7434
		public bool AutomoveToStorage;

		// Token: 0x04001D0B RID: 7435
		public bool AutomoveToStorageMendRes;

		// Token: 0x04001D0C RID: 7436
		public bool IsAutoHoldCrewManagementEnabled;

		// Token: 0x04001D0D RID: 7437
		public bool AutoPaySalarySpecialUnits;

		// Token: 0x04001D0E RID: 7438
		private Dictionary<string, SavedShipEquipmentCollection> {25635};

		// Token: 0x04001D0F RID: 7439
		public Tlist<byte> FavoriteSpecialUnits;

		// Token: 0x04001D10 RID: 7440
		public Tlist<byte> FavoriteShips;

		// Token: 0x04001D11 RID: 7441
		public SelectedCannonBalls SelectedCannonBalls;

		// Token: 0x04001D12 RID: 7442
		public int SelectedPowderKegs;

		// Token: 0x04001D13 RID: 7443
		public int SelectedMortarBallsID;

		// Token: 0x04001D14 RID: 7444
		public int SelectedFalkonetsID;

		// Token: 0x04001D15 RID: 7445
		private bool {25636};

		// Token: 0x04001D16 RID: 7446
		public LocalSettings.SailTransparancyMode SailTransparancy;

		// Token: 0x04001D17 RID: 7447
		public float GammaSetting;

		// Token: 0x04001D18 RID: 7448
		public bool LowFpsToolTipWasShown;

		// Token: 0x04001D19 RID: 7449
		public bool ForceCameraEffect;

		// Token: 0x04001D1A RID: 7450
		public LocalSettings.ShipVoices VoicesMode;

		// Token: 0x04001D1B RID: 7451
		public int LaunchCount;

		// Token: 0x04001D1C RID: 7452
		public bool ShowAmmoCountUi;

		// Token: 0x04001D1D RID: 7453
		public bool BrightNight;

		// Token: 0x04001D1E RID: 7454
		public bool EnableAutoFalkonets = true;

		// Token: 0x04001D1F RID: 7455
		public bool HalfTransparentSailDesign;

		// Token: 0x04001D20 RID: 7456
		private LocalSettings.GlobalUiScale {25637};

		// Token: 0x04001D21 RID: 7457
		private float {25638};

		// Token: 0x04001D22 RID: 7458
		public bool MinimapCompas;

		// Token: 0x04001D23 RID: 7459
		public bool ShowNicknames = true;

		// Token: 0x04001D24 RID: 7460
		public bool HorizonTilt;

		// Token: 0x04001D25 RID: 7461
		public bool SpyglassAnimation;

		// Token: 0x04001D26 RID: 7462
		public DeathController DeathController;

		// Token: 0x04001D27 RID: 7463
		public ushort SavedGuildVersion;

		// Token: 0x04001D28 RID: 7464
		public int SavedGuildAnnouncmentVersion;

		// Token: 0x04001D29 RID: 7465
		public Tlist<EducationOnboarding> UnreadGamepedia = new Tlist<EducationOnboarding>();

		// Token: 0x04001D2A RID: 7466
		public float AutoFoodConsumtion;

		// Token: 0x04001D2B RID: 7467
		public bool AutoFoodConsumtionOnlyOnNorth;

		// Token: 0x04001D2C RID: 7468
		public LsSavedCache LsSavedCacheData = new LsSavedCache();

		// Token: 0x04001D2D RID: 7469
		public GSI FoodConsumptionFilter = new GSI();

		// Token: 0x04001D2E RID: 7470
		public RTI GamemodeDoublones;

		// Token: 0x04001D2F RID: 7471
		public Dictionary<string, LogbookController> LogbookDict = new Dictionary<string, LogbookController>();

		// Token: 0x04001D30 RID: 7472
		public Tlist<LogbookTrackingNote> TrackingNotes = new Tlist<LogbookTrackingNote>();

		// Token: 0x04001D31 RID: 7473
		public Tlist<{22887}.Id> NotSelectedWorldMapicons;

		// Token: 0x04001D32 RID: 7474
		public Currency PreferredCurrency;

		// Token: 0x04001D33 RID: 7475
		public string PreferredPayRegion = "";

		// Token: 0x04001D34 RID: 7476
		public bool EnableGameTooltips;

		// Token: 0x04001D35 RID: 7477
		public bool BigChat;

		// Token: 0x04001D36 RID: 7478
		public bool BigChatFont;

		// Token: 0x04001D37 RID: 7479
		public bool ChatOnlyMyLanguage;

		// Token: 0x04001D38 RID: 7480
		public Tlist<int> DisableTrackingForQuests;

		// Token: 0x04001D39 RID: 7481
		public string SavedServerID;

		// Token: 0x04001D3A RID: 7482
		public int BoardingEducationStatus;

		// Token: 0x04001D3B RID: 7483
		public Tlist<string> TestServerRememberedAuth = new Tlist<string>();

		// Token: 0x04001D3C RID: 7484
		public Dictionary<string, Vector2> DraggablesPositions = new Dictionary<string, Vector2>();

		// Token: 0x04001D3D RID: 7485
		public SavedCredentials AuthCreds = new SavedCredentials();

		// Token: 0x04001D3E RID: 7486
		private float {25639};

		// Token: 0x04001D3F RID: 7487
		private float {25640};

		// Token: 0x04001D40 RID: 7488
		private float {25641};

		// Token: 0x04001D41 RID: 7489
		private bool {25642} = true;

		// Token: 0x04001D42 RID: 7490
		private bool {25643};

		// Token: 0x04001D43 RID: 7491
		private bool {25644};

		// Token: 0x04001D44 RID: 7492
		public bool Loaded;

		// Token: 0x04001D45 RID: 7493
		[CompilerGenerated]
		private ControlLayout {25645};

		// Token: 0x04001D46 RID: 7494
		public KeynameHolder kb_KeyShowMouse;

		// Token: 0x04001D47 RID: 7495
		public KeynameHolder kb_Screenshoot;

		// Token: 0x04001D48 RID: 7496
		public KeynameHolder kb_ds_Forward;

		// Token: 0x04001D49 RID: 7497
		public KeynameHolder kb_ds_Backward;

		// Token: 0x04001D4A RID: 7498
		public KeynameHolder kb_ds_Left;

		// Token: 0x04001D4B RID: 7499
		public KeynameHolder kb_ds_Right;

		// Token: 0x04001D4C RID: 7500
		public KeynameHolder kb_OpenDebugPanel;

		// Token: 0x04001D4D RID: 7501
		public KeynameHolder kb_Mending;

		// Token: 0x04001D4E RID: 7502
		public KeynameHolder kb_OpenHold;

		// Token: 0x04001D4F RID: 7503
		public KeynameHolder kb_ChatEnter;

		// Token: 0x04001D50 RID: 7504
		public KeynameHolder kb_ChatHide;

		// Token: 0x04001D51 RID: 7505
		public KeynameHolder kb_Escape;

		// Token: 0x04001D52 RID: 7506
		public KeynameHolder kb_Accept;

		// Token: 0x04001D53 RID: 7507
		public KeynameHolder kb_Undo;

		// Token: 0x04001D54 RID: 7508
		public KeynameHolder kb_Spyglass;

		// Token: 0x04001D55 RID: 7509
		public KeynameHolder kb_FreeMode;

		// Token: 0x04001D56 RID: 7510
		public KeynameHolder kb_Action;

		// Token: 0x04001D57 RID: 7511
		public KeynameHolder kb_Item1;

		// Token: 0x04001D58 RID: 7512
		public KeynameHolder kb_Item2;

		// Token: 0x04001D59 RID: 7513
		public KeynameHolder kb_Item3;

		// Token: 0x04001D5A RID: 7514
		public KeynameHolder kb_ItemExtra;

		// Token: 0x04001D5B RID: 7515
		public KeynameHolder kb_ShipPerk;

		// Token: 0x04001D5C RID: 7516
		public KeynameHolder kb_FreeCamera;

		// Token: 0x04001D5D RID: 7517
		public KeynameHolder kb_SendGroupCommand;

		// Token: 0x04001D5E RID: 7518
		public KeynameHolder kb_Falkonet;

		// Token: 0x04001D5F RID: 7519
		public KeynameHolder kb_Map;

		// Token: 0x04001D60 RID: 7520
		public KeynameHolder kb_SwitchCascadeGunMode;

		// Token: 0x04001D61 RID: 7521
		public KeynameHolder kb_ThrowPowderKeg;

		// Token: 0x04001D62 RID: 7522
		public KeynameHolder kb_Mortar_ModifierKey;

		// Token: 0x04001D63 RID: 7523
		public KeynameHolder kb_Mortar_Whole;

		// Token: 0x04001D64 RID: 7524
		public KeynameHolder kb_Mortar_Splinter;

		// Token: 0x04001D65 RID: 7525
		public KeynameHolder kb_SwithPanelPowderKeg;

		// Token: 0x04001D66 RID: 7526
		public KeynameHolder kb_SwithPanelFalkonet;

		// Token: 0x04001D67 RID: 7527
		public KeynameHolder kb_SwithPanelCannonBall_Gamepad;

		// Token: 0x04001D68 RID: 7528
		public KeynameHolder kb_SimpleShot;

		// Token: 0x04001D69 RID: 7529
		public KeynameHolder kb_FireShot;

		// Token: 0x04001D6A RID: 7530
		public KeynameHolder kb_AntisailShot;

		// Token: 0x04001D6B RID: 7531
		public KeynameHolder kb_AnticrewShot;

		// Token: 0x04001D6C RID: 7532
		public KeynameHolder kb_OpenQuestPanel;

		// Token: 0x04001D6D RID: 7533
		public KeynameHolder kb_OpenLogbook;

		// Token: 0x04001D6E RID: 7534
		public RightMouseKeyAction RightMouseAction;

		// Token: 0x04001D6F RID: 7535
		public LanguageSettings Language;

		// Token: 0x04001D70 RID: 7536
		private Point {25646} = new Point(1, 1);

		// Token: 0x04001D71 RID: 7537
		private static BufferedDataHolder holder = new BufferedDataHolder(new byte[393210], 0);

		// Token: 0x04001D72 RID: 7538
		private static string old_LastLogin;

		// Token: 0x04001D73 RID: 7539
		private static string old_LastPassword;

		// Token: 0x0200052A RID: 1322
		public struct GraphicsOptions
		{
			// Token: 0x06001DC5 RID: 7621 RVA: 0x0010E8D4 File Offset: 0x0010CAD4
			public void Apply(LocalSettings {25647})
			{
				{25647}.HighDetailing = this.ImprovedMaterials;
				{25647}.RendererSsaoAndRefractions = this.RendererSsaoAndRefractions;
				{25647}.{25634} = this.longViewDistance;
				{25647}.RendererDynamicReflections = this.RendererDynamicReflections;
				{25647}.ShadowsQuality = this.ShadowsQuality;
				{25647}.AntialiasingQuality = this.AntialiasingQuality;
				{25647}.ImprovedPostProcess = this.ImprovedPostProcess;
				{25647}.HeatDeformations = this.HeatDeformations;
				{25647}.EnableBasicEffects = this.BasicEffects;
				{25647}.CrewMode = this.CrewMode;
				{25647}.FloraQuality = this.FloraQuality;
			}

			// Token: 0x04001D74 RID: 7540
			public bool ImprovedMaterials;

			// Token: 0x04001D75 RID: 7541
			public bool RendererSsaoAndRefractions;

			// Token: 0x04001D76 RID: 7542
			public bool longViewDistance;

			// Token: 0x04001D77 RID: 7543
			public bool RendererDynamicReflections;

			// Token: 0x04001D78 RID: 7544
			public LocalSettings.RendererShadows ShadowsQuality;

			// Token: 0x04001D79 RID: 7545
			public LocalSettings.RendererAntialias AntialiasingQuality;

			// Token: 0x04001D7A RID: 7546
			public bool ImprovedPostProcess;

			// Token: 0x04001D7B RID: 7547
			public bool HeatDeformations;

			// Token: 0x04001D7C RID: 7548
			public bool BasicEffects;

			// Token: 0x04001D7D RID: 7549
			public LocalSettings.CrewDisplayMode CrewMode;

			// Token: 0x04001D7E RID: 7550
			public LocalSettings.Postprocessor PostprocessorType;

			// Token: 0x04001D7F RID: 7551
			public LocalSettings.FloraQualityMode FloraQuality;

			// Token: 0x04001D80 RID: 7552
			public static readonly LocalSettings.GraphicsOptions Minimal = new LocalSettings.GraphicsOptions
			{
				ImprovedMaterials = false,
				RendererSsaoAndRefractions = false,
				longViewDistance = false,
				RendererDynamicReflections = false,
				ShadowsQuality = LocalSettings.RendererShadows.Off,
				AntialiasingQuality = LocalSettings.RendererAntialias.Off,
				ImprovedPostProcess = false,
				HeatDeformations = false,
				BasicEffects = false,
				CrewMode = LocalSettings.CrewDisplayMode.None,
				PostprocessorType = LocalSettings.Postprocessor.Basic,
				FloraQuality = LocalSettings.FloraQualityMode.Low
			};

			// Token: 0x04001D81 RID: 7553
			public static readonly LocalSettings.GraphicsOptions Low = new LocalSettings.GraphicsOptions
			{
				ImprovedMaterials = true,
				RendererSsaoAndRefractions = false,
				longViewDistance = false,
				RendererDynamicReflections = true,
				ShadowsQuality = LocalSettings.RendererShadows.Off,
				AntialiasingQuality = LocalSettings.RendererAntialias.Txaa,
				ImprovedPostProcess = false,
				HeatDeformations = false,
				BasicEffects = true,
				CrewMode = LocalSettings.CrewDisplayMode.None,
				PostprocessorType = LocalSettings.Postprocessor.Tonemap2AndHdr,
				FloraQuality = LocalSettings.FloraQualityMode.Normal
			};

			// Token: 0x04001D82 RID: 7554
			public static readonly LocalSettings.GraphicsOptions Middle = new LocalSettings.GraphicsOptions
			{
				ImprovedMaterials = true,
				RendererSsaoAndRefractions = false,
				longViewDistance = false,
				RendererDynamicReflections = true,
				ShadowsQuality = LocalSettings.RendererShadows.Medium,
				AntialiasingQuality = LocalSettings.RendererAntialias.Txaa,
				ImprovedPostProcess = false,
				HeatDeformations = true,
				BasicEffects = true,
				CrewMode = LocalSettings.CrewDisplayMode.OnlyMyShip,
				PostprocessorType = LocalSettings.Postprocessor.Tonemap2AndHdr,
				FloraQuality = LocalSettings.FloraQualityMode.High
			};

			// Token: 0x04001D83 RID: 7555
			public static readonly LocalSettings.GraphicsOptions High = new LocalSettings.GraphicsOptions
			{
				ImprovedMaterials = true,
				RendererSsaoAndRefractions = true,
				longViewDistance = true,
				RendererDynamicReflections = true,
				ShadowsQuality = LocalSettings.RendererShadows.High,
				AntialiasingQuality = LocalSettings.RendererAntialias.Txaa,
				ImprovedPostProcess = true,
				HeatDeformations = true,
				BasicEffects = true,
				CrewMode = LocalSettings.CrewDisplayMode.AllShips,
				PostprocessorType = LocalSettings.Postprocessor.Tonemap2AndHdr,
				FloraQuality = LocalSettings.FloraQualityMode.High
			};
		}

		// Token: 0x0200052B RID: 1323
		public enum SailTransparancyMode
		{
			// Token: 0x04001D85 RID: 7557
			NoUse,
			// Token: 0x04001D86 RID: 7558
			WhenZoom,
			// Token: 0x04001D87 RID: 7559
			Always,
			// Token: 0x04001D88 RID: 7560
			InBattle
		}

		// Token: 0x0200052C RID: 1324
		public enum CrewDisplayMode
		{
			// Token: 0x04001D8A RID: 7562
			None,
			// Token: 0x04001D8B RID: 7563
			OnlyMyShip,
			// Token: 0x04001D8C RID: 7564
			AllShips
		}

		// Token: 0x0200052D RID: 1325
		public enum RendererShadows
		{
			// Token: 0x04001D8E RID: 7566
			Off,
			// Token: 0x04001D8F RID: 7567
			Medium,
			// Token: 0x04001D90 RID: 7568
			High
		}

		// Token: 0x0200052E RID: 1326
		public enum RendererAntialias
		{
			// Token: 0x04001D92 RID: 7570
			Off,
			// Token: 0x04001D93 RID: 7571
			Txaa,
			// Token: 0x04001D94 RID: 7572
			Fsaa
		}

		// Token: 0x0200052F RID: 1327
		public enum ShipVoices : byte
		{
			// Token: 0x04001D96 RID: 7574
			Disabled,
			// Token: 0x04001D97 RID: 7575
			BackgroundOnly,
			// Token: 0x04001D98 RID: 7576
			Young,
			// Token: 0x04001D99 RID: 7577
			Old
		}

		// Token: 0x02000530 RID: 1328
		public enum Postprocessor : byte
		{
			// Token: 0x04001D9B RID: 7579
			Basic,
			// Token: 0x04001D9C RID: 7580
			Tonemap1,
			// Token: 0x04001D9D RID: 7581
			Tonemap2AndHdr
		}

		// Token: 0x02000531 RID: 1329
		public enum GlobalUiScale : byte
		{
			// Token: 0x04001D9F RID: 7583
			Normal,
			// Token: 0x04001DA0 RID: 7584
			Increase,
			// Token: 0x04001DA1 RID: 7585
			Decrease,
			// Token: 0x04001DA2 RID: 7586
			IncreaseDouble
		}

		// Token: 0x02000532 RID: 1330
		public enum DrawFrequency : byte
		{
			// Token: 0x04001DA4 RID: 7588
			fps60,
			// Token: 0x04001DA5 RID: 7589
			fps144
		}

		// Token: 0x02000533 RID: 1331
		public enum FloraQualityMode : byte
		{
			// Token: 0x04001DA7 RID: 7591
			Low,
			// Token: 0x04001DA8 RID: 7592
			Normal,
			// Token: 0x04001DA9 RID: 7593
			High
		}
	}
}
