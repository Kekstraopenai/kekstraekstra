using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Data;
using Common.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using TheraEngine;
using TheraEngine.Assets.Audio;
using TheraEngine.Collections;
using TheraEngine.Core;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using WorldOfSeaBattles;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.GameSystems
{
	// Token: 0x020004BA RID: 1210
	internal sealed class SoundManager : GameSceneSystem
	{
		// Token: 0x06001A8A RID: 6794 RVA: 0x000EBAB4 File Offset: 0x000E9CB4
		public override void Initialize(ContentManager {24693})
		{
			ThreadPool.QueueUserWorkItem(delegate(object {24752})
			{
				MediaPlayer.Volume = 0f;
			});
			base.Initialize({24693});
			this.{24722} = new Dictionary<GameDynamicSoundName, ISoundEffect3D>(100);
			this.{24723} = new Dictionary<GameStaticSoundName, ISoundEffect>(100);
			try
			{
				this.{24696}({24693});
				this.{24694}();
				this.SelfTesting();
				this.{24743} = true;
			}
			catch (Exception ex)
			{
				if (!ex.StackTrace.Contains("SoundEffect..ctor"))
				{
					throw;
				}
				this.{24743} = false;
			}
			Global.Game.GetInterfaceManager.GettingInteropFocus += this.{24714};
			UiControl.ClickButtonEffectHandler += this.{24716};
			CheckboxControl.CheckEffectHandler += this.{24718};
			CheckboxControl.UncheckEffectHandler += this.{24720};
			this.{24742} = new Dictionary<GameStaticSoundName, Stopwatch>(100);
			this.{24741} = new LinkedDictionrary<GameDynamicSoundName, SoundManager.SoundLimits>(100);
			this.{24699}(120f, 2, 7f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.Slamming_Big,
				GameDynamicSoundName.Slamming_Small,
				GameDynamicSoundName.CannonBallFireHitWater
			});
			this.{24699}(250f, 3, 0f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.WoodImpact,
				GameDynamicSoundName.ConcreteImpact,
				GameDynamicSoundName.PowderKegExplosion,
				GameDynamicSoundName.PowderKegWick
			});
			this.{24699}(120f, 2, 0f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.BoardingHook
			});
			this.{24699}(100f, 2, 0f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.MortarsGun
			});
			this.{24699}(100f, 2, 0f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.MortarsGunKeg
			});
			this.{24699}(50f, 2, 0f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.MortarHitWater
			});
			this.{24699}(50f, 2, 0f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.MortarHitShip
			});
			this.{24699}(50f, 2, 0f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.ShipExplosion,
				GameDynamicSoundName.TowerDestruct
			});
			this.{24699}(200f, 2, 10f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.ShipSailClose,
				GameDynamicSoundName.ShipSailDeploy
			});
			this.{24699}(20f, 2, 0f, new GameDynamicSoundName[]
			{
				GameDynamicSoundName.MarineGun_Near,
				GameDynamicSoundName.MarineGun_Far
			});
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000EBCEC File Offset: 0x000E9EEC
		private void {24694}()
		{
			string dir_sounds_war = PathContent.dir_sounds_war;
			string dir_sounds_world = PathContent.dir_sounds_world;
			string dir_sounds_effects = PathContent.dir_sounds_effects;
			using (SoundLoaderParallel soundLoaderParallel = new SoundLoaderParallel("Sounds"))
			{
				soundLoaderParallel.SetChilDir("phisycs");
				this.{24722}.Add(GameDynamicSoundName.WoodImpact, soundLoaderParallel.LoadSound3DSet("volumetric_impact_wood", 1, 5, 0.7f));
				this.{24722}.Add(GameDynamicSoundName.ConcreteImpact, soundLoaderParallel.LoadSound3DSet("volumetric_impact_concrete", 1, 3, 0.8f));
				this.{24722}.Add(GameDynamicSoundName.WoodImpactLong, soundLoaderParallel.LoadSound3DSet("volumetric_impact_long", 1, 1, 0.8f));
				this.{24722}.Add(GameDynamicSoundName.CannonsGun_Lite, soundLoaderParallel.LoadSound3DSet("cannongun_far", 1, 3, 1f));
				this.{24722}.Add(GameDynamicSoundName.CannonsGun_DistSingle, soundLoaderParallel.LoadSound3DSet("cg_dist", 1, 4, 1f));
				this.{24722}.Add(GameDynamicSoundName.CannonsGun_HeavySingle, soundLoaderParallel.LoadSound3DSet("cg_heavy", 1, 3, 1f));
				this.{24722}.Add(GameDynamicSoundName.CannonsGun_BombardSingle, soundLoaderParallel.LoadSound3DSet("cg_triple", 1, 2, 1f));
				this.{24722}.Add(GameDynamicSoundName.CannonsGunFar_Assist, soundLoaderParallel.LoadSound3DSet("cg_assist_far", 1, 2, 1f));
				this.{24722}.Add(GameDynamicSoundName.FireBig, soundLoaderParallel.LoadSound3D("fire_big"));
				this.{24722}.Add(GameDynamicSoundName.Slamming_Small, soundLoaderParallel.LoadSound3DSet("slamming", 1, 2, 0.5f));
				this.{24722}.Add(GameDynamicSoundName.Slamming_Big, soundLoaderParallel.LoadSound3DSet("slamming_big", 1, 3, 0.5f));
				this.{24722}.Add(GameDynamicSoundName.WoodCreak, soundLoaderParallel.LoadSound3DSet("wood", 1, 4, 0.6f));
				this.{24722}.Add(GameDynamicSoundName.ShipExplosion, soundLoaderParallel.LoadSound3DSet("ship_explosion", 1, 2, 1f));
				this.{24722}.Add(GameDynamicSoundName.TowerDestruct, soundLoaderParallel.LoadSound3D("tower_destructnear"));
				this.{24722}.Add(GameDynamicSoundName.MarineGun_Near, soundLoaderParallel.LoadSound3DSet("marine_near", 1, 1, 0.8f));
				this.{24722}.Add(GameDynamicSoundName.MarineGun_Far, soundLoaderParallel.LoadSound3DSet("marine_far", 1, 2, 1f));
				this.{24722}.Add(GameDynamicSoundName.CannonballFlyLow, soundLoaderParallel.LoadSound3D("cannonball_fly_low"));
				this.{24722}.Add(GameDynamicSoundName.CannonballFlyMiddle, soundLoaderParallel.LoadSound3D("cannonball_fly_mid"));
				this.{24722}.Add(GameDynamicSoundName.CannonballFlyHight, soundLoaderParallel.LoadSound3D("cannonball_fly_hi"));
				this.{24722}.Add(GameDynamicSoundName.MortarsGun, soundLoaderParallel.LoadSound3DSet("mortar_shot", 1, 3, 1f));
				this.{24722}.Add(GameDynamicSoundName.MortarsGunKeg, soundLoaderParallel.LoadSound3D("mortar_shot_keg"));
				this.{24722}.Add(GameDynamicSoundName.MortarsGunFar, soundLoaderParallel.LoadSound3DSet("mortar_shot_far", 1, 2, 1f));
				this.{24722}.Add(GameDynamicSoundName.MortarHitWater, soundLoaderParallel.LoadSound3D("mortar_damage"));
				this.{24722}.Add(GameDynamicSoundName.MortarHitShip, soundLoaderParallel.LoadSound3D("mortar_damage_direct_ship"));
				this.{24722}.Add(GameDynamicSoundName.PowderKegWick, soundLoaderParallel.LoadSound3D("powderKegEffect"));
				this.{24722}.Add(GameDynamicSoundName.PowderKegExplosion, soundLoaderParallel.LoadSound3D("powderKegExpl"));
				this.{24722}.Add(GameDynamicSoundName.BoardingHook, soundLoaderParallel.LoadSound3D("chairEffect"));
				this.{24722}.Add(GameDynamicSoundName.Harpoon, soundLoaderParallel.LoadSound3D("harpoon"));
				this.{24722}.Add(GameDynamicSoundName.SnowImpact, soundLoaderParallel.LoadSound3DSet("snow_hit", 1, 3, 1f));
				this.{24722}.Add(GameDynamicSoundName.Firegun, soundLoaderParallel.LoadSound3DSet("firegun", 1, 1, 0.8f));
				this.{24722}.Add(GameDynamicSoundName.CannonBallFire, soundLoaderParallel.LoadSound3DSet("cball_fire", 1, 3, 0.6f));
				this.{24722}.Add(GameDynamicSoundName.CannonBallFireHitWater, soundLoaderParallel.LoadSound3D("cball_fire_hitwater").SetVolume(0.8f));
				this.{24722}.Add(GameDynamicSoundName.CannonBallFireHitShip, soundLoaderParallel.LoadSound3D("cball_fire_hitship"));
				this.{24722}.Add(GameDynamicSoundName.CannonBallChain, soundLoaderParallel.LoadSound3DSet("cball_chain", 1, 4, 1f));
				this.{24722}.Add(GameDynamicSoundName.MortarShotTrail, soundLoaderParallel.LoadSound3D("mortar_shot_trail"));
				this.{24723}.Add(GameStaticSoundName.AmbientTowerDestruct, soundLoaderParallel.LoadSound("tower_destructdist", 1f));
				soundLoaderParallel.SetChilDir("effects");
				this.{24722}.Add(GameDynamicSoundName.Vudushield, soundLoaderParallel.LoadSound3D("vudushield"));
				this.{24722}.Add(GameDynamicSoundName.ItemUse, soundLoaderParallel.LoadSound3D("itemUse").SetVolume(0.9f));
				this.{24722}.Add(GameDynamicSoundName.ShipSailDeploy, soundLoaderParallel.LoadSound3DSet("sail_open", 1, 2, 1f));
				this.{24722}.Add(GameDynamicSoundName.ShipSailClose, soundLoaderParallel.LoadSound3DSet("sail_close", 1, 2, 1f));
				this.{24722}.Add(GameDynamicSoundName.BoardingBegin, soundLoaderParallel.LoadSound3D("boarding_sfx"));
				this.{24722}.Add(GameDynamicSoundName.BoardingContinue, soundLoaderParallel.LoadSound3D("boarding_sfx_continue"));
				this.{24722}.Add(GameDynamicSoundName.DisasterBell, soundLoaderParallel.LoadSound3D("disaster_bell"));
				this.{24723}.Add(GameStaticSoundName.GiveDrop, soundLoaderParallel.LoadSound("bonusLoot", 1f).SetVolume(0.3f));
				this.{24723}.Add(GameStaticSoundName.Money1, soundLoaderParallel.LoadSound("bonusGive", 1f).SetVolume(0.3f));
				this.{24723}.Add(GameStaticSoundName.Money2, soundLoaderParallel.LoadSound("bonusGiveSmall", 1f).SetVolume(0.4f));
				this.{24723}.Add(GameStaticSoundName.BoardingMove, soundLoaderParallel.LoadSound("boardingMove", 1f));
				this.{24723}.Add(GameStaticSoundName.ToHold, soundLoaderParallel.LoadSound("to_hold", 1f));
				this.{24723}.Add(GameStaticSoundName.Bell, soundLoaderParallel.LoadSoundSet("bell", 1, 1, 1f));
				this.{24723}.Add(GameStaticSoundName.Mapscreen, soundLoaderParallel.LoadSound("mapscreen", 1f));
				this.{24737} = soundLoaderParallel.LoadSound("my_ship_flooding", 1f);
				this.{24738} = soundLoaderParallel.LoadSound("steamEngine", 1f);
				if (CalendarEvents.IsNewYear)
				{
					this.{24723}.Add(GameStaticSoundName.Port_Asian, soundLoaderParallel.LoadSound("PortJingle_Ny\\Port_Asian", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_England, soundLoaderParallel.LoadSound("PortJingle_Ny\\Port_England", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_North, soundLoaderParallel.LoadSound("PortJingle_Ny\\Port_North", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_Pirates, soundLoaderParallel.LoadSound("PortJingle_Ny\\Port_Pirates", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_Spain, soundLoaderParallel.LoadSound("PortJingle_Ny\\Port_Spain", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_Arabic, soundLoaderParallel.LoadSound("PortJingle_Ny\\Port_Arabic", 1f).SetVolume(0.8f));
				}
				else
				{
					this.{24723}.Add(GameStaticSoundName.Port_Asian, soundLoaderParallel.LoadSound("PortJingle\\Port_Asian", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_England, soundLoaderParallel.LoadSound("PortJingle\\Port_England", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_North, soundLoaderParallel.LoadSound("PortJingle\\Port_North", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_Pirates, soundLoaderParallel.LoadSound("PortJingle\\Port_Pirates", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_Spain, soundLoaderParallel.LoadSound("PortJingle\\Port_Spain", 1f).SetVolume(0.8f));
					this.{24723}.Add(GameStaticSoundName.Port_Arabic, soundLoaderParallel.LoadSound("PortJingle\\Port_Arabic", 1f).SetVolume(0.8f));
				}
				this.{24735} = soundLoaderParallel.LoadSound("repair", 1f);
				soundLoaderParallel.SetChilDir("ui");
				this.{24723}.Add(GameStaticSoundName.UIClick, soundLoaderParallel.LoadSound("uiClick", 1f).SetVolume(0.4f));
				this.{24723}.Add(GameStaticSoundName.UISwitch, soundLoaderParallel.LoadSound("uiSwitch", 1f).SetVolume(0.6f));
				this.{24723}.Add(GameStaticSoundName.UIClotting, soundLoaderParallel.LoadSound("uiClotting", 1f).SetVolume(0.5f));
				this.{24723}.Add(GameStaticSoundName.UIDeploy, soundLoaderParallel.LoadSound("uiDeploy", 1f).SetVolume(0.5f));
				this.{24723}.Add(GameStaticSoundName.UIButton, soundLoaderParallel.LoadSound("button", 1f).SetVolume(0.8f));
				this.{24723}.Add(GameStaticSoundName.UIButtonInterop, soundLoaderParallel.LoadSound("button_interop", 1f).SetVolume(0.1f));
				this.{24723}.Add(GameStaticSoundName.UITabSwitch, soundLoaderParallel.LoadSound("tabswitch", 1f).SetVolume(0.2f));
				this.{24723}.Add(GameStaticSoundName.UIMessage, soundLoaderParallel.LoadSound("message", 1f).SetVolume(0.2f));
				this.{24723}.Add(GameStaticSoundName.UiCommonCraftUi, soundLoaderParallel.LoadSoundSet("commoncsui", 1, 2, 1f));
				this.{24723}.Add(GameStaticSoundName.AchievOrLevelUp, soundLoaderParallel.LoadSound("achievOrLevelUp", 1f).SetVolume(0.5f));
				this.{24723}.Add(GameStaticSoundName.BattleResults, soundLoaderParallel.LoadSound("battleResults", 1f));
				this.{24723}.Add(GameStaticSoundName.ItemComplete, soundLoaderParallel.LoadSound("itemComplete", 1f));
				this.{24723}.Add(GameStaticSoundName.Equip1, soundLoaderParallel.LoadSound("equip1", 1f).SetVolume(0.5f));
				this.{24723}.Add(GameStaticSoundName.OpenDecks, soundLoaderParallel.LoadSound("openDecks", 1f).SetVolume(0.4f));
				this.{24723}.Add(GameStaticSoundName.Horn, soundLoaderParallel.LoadSound("horn", 1f));
				this.{24723}.Add(GameStaticSoundName.HornTadada, soundLoaderParallel.LoadSound("tadada", 1f));
				this.{24723}.Add(GameStaticSoundName.WeaponsLoaded, soundLoaderParallel.LoadSound("cannons_reloaded", 1f).SetVolume(0.9f));
				soundLoaderParallel.SetChilDir("environment");
				this.{24723}.Add(GameStaticSoundName.Thunder, soundLoaderParallel.LoadSoundSet("thunder", 1, 2, 0.75f));
				this.{24728} = soundLoaderParallel.LoadSound("rain1", 1f);
				this.{24729} = soundLoaderParallel.LoadSound("water_running_speed_storm", 1f);
				this.{24730} = soundLoaderParallel.LoadSound("water_ambient1", 1f);
				this.{24731} = soundLoaderParallel.LoadSound("water_ambient2", 1f);
				this.{24732} = soundLoaderParallel.LoadSound("ambient_city", 1f);
				this.{24733} = soundLoaderParallel.LoadSound("ambient_city_new", 1f);
				this.{24734} = soundLoaderParallel.LoadSound("calm_ambient", 1f);
				this.{24736} = soundLoaderParallel.LoadSound("storm_ambient", 1f);
				this.{24730}.Volume = 0.3f;
				this.{24731}.Volume = 0.3f;
				this.{24728}.MultiplySoundManagerVolume = false;
				this.{24729}.MultiplySoundManagerVolume = false;
				this.{24730}.MultiplySoundManagerVolume = false;
				this.{24731}.MultiplySoundManagerVolume = false;
				this.{24732}.MultiplySoundManagerVolume = false;
				this.{24733}.MultiplySoundManagerVolume = false;
				this.{24734}.MultiplySoundManagerVolume = false;
				this.{24736}.MultiplySoundManagerVolume = false;
			}
			string str = "Sounds\\voices\\";
			Tlist<Sound> tlist = new Tlist<Sound>();
			for (int i = 1; i <= 5; i++)
			{
				Tlist<Sound> tlist2 = tlist;
				Sound sound = new Sound(Global.Game.Content.Load<SoundEffect>(str + "fire" + i.ToString()))
				{
					Volume = 0.7f
				};
				tlist2.Add(sound);
			}
			this.{24723}.Add(GameStaticSoundName.V_Fire, new SoundSet(tlist.ToArray()));
			Tlist<Sound> tlist3 = new Tlist<Sound>();
			for (int j = 1; j <= 3; j++)
			{
				Tlist<Sound> tlist4 = tlist3;
				Sound sound = new Sound(Global.Game.Content.Load<SoundEffect>(str + "vicshout_" + j.ToString()))
				{
					Volume = 0.3f
				};
				tlist4.Add(sound);
			}
			this.{24723}.Add(GameStaticSoundName.V_Victory, new SoundSet(tlist3.ToArray()));
			Tlist<Sound> tlist5 = new Tlist<Sound>();
			for (int k = 2; k <= 12; k++)
			{
				Tlist<Sound> tlist6 = tlist5;
				Sound sound = new Sound(Global.Game.Content.Load<SoundEffect>(str + "rand_sound" + k.ToString()))
				{
					Volume = 0.7f
				};
				tlist6.Add(sound);
			}
			this.{24723}.Add(GameStaticSoundName.V_RandSound, new SoundSet(tlist5.ToArray()));
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000ECA80 File Offset: 0x000EAC80
		public void UpdateVoiceEffects()
		{
			foreach (KeyValuePair<VoiceSoundEffect, ObservableTlist<Sound>> keyValuePair in this.{24724})
			{
				foreach (Sound sound in ((IEnumerable<Sound>)keyValuePair.Value))
				{
					sound.Dispose();
				}
			}
			this.{24724}.Clear();
			this.{24695}();
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000ECB18 File Offset: 0x000EAD18
		private void {24695}()
		{
			this.{24725} = Global.Settings.VoicesMode;
			this.{24726} = Global.Settings.Language.CurrentGameLocale.GetValueOrDefault(Locale.En);
			string text = (this.{24726} == Locale.Ru) ? "voices_ru" : null;
			if (this.{24725} != LocalSettings.ShipVoices.Disabled && this.{24725} != LocalSettings.ShipVoices.BackgroundOnly && text != null)
			{
				string str = (this.{24725} == LocalSettings.ShipVoices.Old) ? ("Sounds\\" + text + "\\voice2_") : ("Sounds\\" + text + "\\voice3_");
				foreach (object obj in Enum.GetValues(typeof(VoiceSoundEffect)))
				{
					VoiceSoundEffect key = (VoiceSoundEffect)obj;
					for (int i = 0; i < 3; i++)
					{
						this.{24724}.Add(key, new Sound(Global.Game.Content.Load<SoundEffect>(str + key.ToString() + i.ToString())));
					}
				}
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000ECC48 File Offset: 0x000EAE48
		private void {24696}(ContentManager {24697})
		{
			Global.Game.MusicManager.AddSet(MusicSetTag.CalmSea, new Music[]
			{
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\mba"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\mbb"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\mbc"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\mbd"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\mbe"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\mbf"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\sc"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\Lute song"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\Teaser_Trailer"))
				{
					Volume = 1f
				}
			});
			Global.Game.MusicManager.AddSet(MusicSetTag.CalmChill, new Music[]
			{
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\mpa"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\mpb"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\mpc"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\Calm down"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\Memory of the fallen"))
				{
					Volume = 1f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\calm\\Raindrops"))
				{
					Volume = 1f
				}
			});
			Global.Game.MusicManager.AddSet(MusicSetTag.Action, new Music[]
			{
				new Music({24697}.Load<Song>("Sounds\\music\\action\\act1"))
				{
					Volume = 0.4f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\action\\act2"))
				{
					Volume = 0.4f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\action\\act3"))
				{
					Volume = 0.4f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\action\\act4"))
				{
					Volume = 0.4f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\action\\act5"))
				{
					Volume = 0.4f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\action\\act6"))
				{
					Volume = 0.4f
				}
			});
			Global.Game.MusicManager.AddSet(MusicSetTag.ActionBig, new Music[]
			{
				new Music({24697}.Load<Song>("Sounds\\music\\action\\bact1"))
				{
					Volume = 0.4f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\action\\bact2"))
				{
					Volume = 0.4f
				},
				new Music({24697}.Load<Song>("Sounds\\music\\action\\bact3"))
				{
					Volume = 0.4f
				}
			});
			if (!CalendarEvents.IsNewYear)
			{
				Global.Game.MusicManager.AddSet(MusicSetTag.Entry, new Music[]
				{
					new Music({24697}.Load<Song>("Sounds\\music\\entry1"))
					{
						Volume = 0.35f
					},
					new Music({24697}.Load<Song>("Sounds\\music\\entry2"))
					{
						Volume = 0.35f
					},
					new Music({24697}.Load<Song>("Sounds\\music\\entry3"))
					{
						Volume = 0.35f
					},
					new Music({24697}.Load<Song>("Sounds\\music\\entry4"))
					{
						Volume = 0.35f
					}
				});
				return;
			}
			Global.Game.MusicManager.AddSet(MusicSetTag.Entry, new Music[]
			{
				new Music({24697}.Load<Song>("Sounds\\music\\ny_theme"))
				{
					Volume = 0.35f
				}
			});
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000ED06C File Offset: 0x000EB26C
		private static long GetDirectorySize(string {24698})
		{
			long num = 0L;
			DirectoryInfo directoryInfo = new DirectoryInfo({24698});
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				num += fileInfo.Length;
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				num += SoundManager.GetDirectorySize(directoryInfo2.FullName);
			}
			return num;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00003100 File Offset: 0x00001300
		public void SelfTesting()
		{
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000ED0D4 File Offset: 0x000EB2D4
		private void {24699}(float {24700}, int {24701}, float {24702}, params GameDynamicSoundName[] {24703})
		{
			float num = 200f;
			foreach (GameDynamicSoundName key in {24703})
			{
				for (int j = 0; j < {24701}; j++)
				{
					float {24749} = num / (float){24701} * (float)j;
					float {24750} = (j == {24701} - 1) ? float.MaxValue : (num / (float){24701} * (float)(j + 1));
					this.{24741}.Add(key, new SoundManager.SoundLimits({24700}, {24749}, {24750}, {24702}));
				}
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000ED147 File Offset: 0x000EB347
		public override void On()
		{
			base.On();
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000ED14F File Offset: 0x000EB34F
		public override void Off()
		{
			base.Off();
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000ED158 File Offset: 0x000EB358
		public override void Update(ref FrameTime {24704})
		{
			bool flag = false;
			bool flag2 = false;
			if (this.{24743})
			{
				float num = 0f;
				float num2 = 0f;
				if (this.{24725} != Global.Settings.VoicesMode || this.{24726} != Global.Settings.Language.CurrentGameLocale.GetValueOrDefault(Locale.En))
				{
					this.UpdateVoiceEffects();
				}
				if (Global.Player != null)
				{
					float num3 = float.MaxValue;
					Vector2 value = Engine.GS.Camera.Position.XZ();
					foreach (Isle isle in ((IEnumerable<Isle>)Global.Game.WorldInstance.MapVisibleObjectLayer))
					{
						float num4 = Vector2.Distance(isle.Statement.GlobalPosition, value) - isle.Statement.ModelGlobalBS.Radius;
						if (num4 < num3)
						{
							num3 = num4;
						}
					}
					if (num3 < 0f)
					{
						num3 = 0f;
					}
					float num5 = 0f;
					for (int i = 0; i < Global.Player.physicsBody.SupportUnits.Length; i++)
					{
						float lastD = Global.Player.physicsBody.SupportUnits[i].LastD;
						num5 += Math.Max(-lastD, 0f) / (1f + num5 * 0.25f) / 4f;
					}
					float num6 = Geometry.Saturate((Global.Player.NowSpeed - 4f) / 9f);
					num = Geometry.Saturate(Global.Player.NowSpeed / 10f);
					float num7 = num6 * MathHelper.Clamp(num5 * 2.6f, 0.5f, 1f) * 0.6f * Math.Max(0f, 1f - (5f + Vector3.Distance(Global.Player.Position3D, Global.Camera.Position)) / 100f);
					this.{24729}.Volume = num7 * Global.Settings.AmbientVolume * 0.7f;
					if (this.{24729}.Volume != 0f && !this.{24729}.IsPlay)
					{
						this.{24729}.Play(1f);
					}
					if (Global.Player.MapInfo.Ports.Size != 0)
					{
						float num8 = Vector2.Distance(Global.Player.NearPort.EntryPos, Global.Player.Position);
						if (num8 < 100f)
						{
							num2 = (1f - num8 / 100f) * Global.Settings.AmbientVolume;
							this.{24732}.Volume = num2 * 0.04f;
							if (!this.{24732}.IsPlay)
							{
								this.{24732}.Play(1f);
							}
						}
						else
						{
							this.{24732}.Volume = 0f;
						}
					}
					else
					{
						this.{24732}.Volume = 0f;
					}
					this.{24735}.Volume = (Global.Player.IsMendingBegin ? 0.3f : 0f);
					if (Global.Player.IsMendingBegin && !this.{24735}.IsPlay)
					{
						this.{24735}.Play(1f);
					}
					if (Global.Player.UsedShip.FirstHP.FloodingFactor > 0f)
					{
						this.{24737}.Volume = 0.5f;
						if (!this.{24737}.IsPlay)
						{
							this.{24737}.Play(1f);
						}
					}
					else
					{
						this.{24737}.Volume = 0f;
					}
					if (Global.Player.UsedShip.StaticInfo.HasSteamWheel && Global.Player.physicsBody.LastPaddlesSpeedBonus > 0f && Global.Player.physicsBody.CanUseWheel)
					{
						this.{24738}.Volume = 0.7f;
						if (!this.{24738}.IsPlay)
						{
							this.{24738}.Play(1f);
						}
					}
					else
					{
						this.{24738}.Volume = 0f;
					}
					flag = (Global.Player.UsedShip.HpFactor < 0.7f && Session.TimeFromLastReceivedDamageSec < 20f);
					flag2 = (!Global.Player.MapInfo.IsWorldmap || Global.Player.IsRunningMarchingMode);
				}
				else
				{
					this.{24732}.Volume = 0f;
				}
				this.{24733}.Volume = num2 * (0.5f + Global.Game.StaticSystem.GetSkyShader.DayOrNight * 0.5f);
				if (!this.{24733}.IsPlay && this.{24733}.Volume > 0f)
				{
					this.{24733}.Play(1f);
				}
				float num9 = Math.Max(0f, (Global.Game.StaticSystem.GetRainCurrentPower - 0.5f) / 0.5f);
				this.{24728}.Volume = Geometry.Saturate(num9 / 0.5f) * Global.Settings.AmbientVolume * 0.08f;
				if (!this.{24728}.IsPlay && this.{24728}.Volume > 0f)
				{
					this.{24728}.Play(1f);
				}
				float openWorldWinterFactor = Global.Game.StaticSystem.OpenWorldWinterFactor;
				this.{24730}.Volume = (1f - num9) * Global.Settings.AmbientVolume * 0.15f * (1f - openWorldWinterFactor * 0.7f) * (1f + (1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight) * 0.6f);
				if (!this.{24730}.IsPlay && this.{24730}.Volume > 0f)
				{
					this.{24730}.Play(1f);
				}
				this.{24731}.Volume = (1f - num9) * Global.Settings.AmbientVolume * 0.4f * openWorldWinterFactor;
				if (!this.{24731}.IsPlay && this.{24731}.Volume > 0f)
				{
					this.{24731}.Play(1f);
				}
				this.{24734}.Volume = (1f - num9) * (1f - num) * Global.Settings.AmbientVolume * 0.5f * Global.Game.StaticSystem.GetSkyShader.DayOrNight;
				if (!this.{24734}.IsPlay && this.{24734}.Volume > 0f)
				{
					this.{24734}.Play(1f);
				}
				this.{24736}.Volume = num9 * Global.Settings.AmbientVolume;
				if (!this.{24736}.IsPlay && num9 > 0f)
				{
					this.{24736}.Play(1f);
				}
				if (Global.Game.MusicManager.CurrentTrack != null)
				{
					if ({24704}.EvaluteTimerMs2(ref this.{24740}))
					{
						this.{24740} = 100f;
						if (!this.{24739})
						{
							float v = Global.Settings.MusicVolume * Global.Game.MusicManager.CurrentTrack.Volume * MathF.Sqrt(Global.Game.MusicManager.FadeVolumeMultiplier);
							this.{24739} = true;
							ThreadPool.QueueUserWorkItem(delegate(object {24753})
							{
								MediaPlayer.Volume = (({18781}.CurrentInstance == null) ? v : Global.Settings.VideoVolume);
								this.{24739} = false;
							});
						}
					}
					object currentTrackTag = Global.Game.MusicManager.CurrentTrackTag;
					if (currentTrackTag is MusicSetTag)
					{
						MusicSetTag musicSetTag = (MusicSetTag)currentTrackTag;
						if ((flag && musicSetTag != MusicSetTag.Action && musicSetTag != MusicSetTag.ActionBig) || (flag2 && musicSetTag == MusicSetTag.Entry && {18781}.CurrentInstance == null))
						{
							Global.Game.MusicManager.ScheduleDying();
						}
					}
				}
				else if (Global.Game.GetCurrentSceneName == GameSceneName.Entry)
				{
					if (Rand.Chanse(30f))
					{
						Global.Game.MusicManager.SetNextTrackFromSet(MusicSetTag.Action, 30000f, 30000f);
					}
					else
					{
						Global.Game.MusicManager.SetNextTrackFromSet(MusicSetTag.Entry, 30000f, 30000f);
					}
				}
				else if (Math.Abs(Global.Player.NowSpeed) < 1f && Session.TimeFromLastReceivedDamageSec > 80f)
				{
					if (Global.Player.IsPortEntry || Global.Player.IsEntryToPortZoneContains)
					{
						if (Global.Player.IsPortEntry)
						{
							Global.Game.MusicManager.SetNextTrackFromSet(MusicSetTag.CalmChill, Rand.Range(2.5f, 4f) * 60000f, Rand.Range(2.5f, 4f) * 60000f);
						}
					}
					else if (Rand.Chanse(66f))
					{
						Global.Game.MusicManager.SetNextTrackFromSet(MusicSetTag.CalmChill, 0f, Rand.Range(2.5f, 4f) * 60000f);
					}
					else
					{
						Global.Game.MusicManager.SetNextTrackFromSet(MusicSetTag.CalmSea, 0f, Rand.Range(2.5f, 4f) * 60000f);
					}
				}
				else if (Session.TimeFromLastReceivedDamageSec > 200f && Session.TimeFromLastSendedCBDamageSec > 200f)
				{
					if (Rand.Chanse(33f))
					{
						Global.Game.MusicManager.SetNextTrackFromSet(MusicSetTag.CalmChill, 0f, Rand.Range(1.5f, 2f) * 60000f);
					}
					else
					{
						Global.Game.MusicManager.SetNextTrackFromSet(MusicSetTag.CalmSea, 0f, Rand.Range(1.5f, 2f) * 60000f);
					}
				}
				else if (flag && Rand.Chanse(50f))
				{
					Global.Game.MusicManager.SetNextTrackFromSet(MusicSetTag.ActionBig, 0f, 5000f);
				}
				else
				{
					Global.Game.MusicManager.SetNextTrackFromSet(MusicSetTag.Action, 0f, 5000f);
				}
				GameMusicEngine.IsEnabled = (Global.Settings.MusicVolume != 0f);
			}
			{24704}.EvaluteTimerMs(ref this.{24727});
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000EDBA0 File Offset: 0x000EBDA0
		public void Play3DSound(GameDynamicSoundName {24705}, Vector3 {24706}, float {24707} = 1f, bool {24708} = false)
		{
			if (!this.{24743})
			{
				return;
			}
			ISoundEffect3D soundEffect3D;
			if (this.{24722}.TryGetValue({24705}, out soundEffect3D))
			{
				ObservableTlist<SoundManager.SoundLimits> observableTlist;
				if (this.{24741}.TryGetValue({24705}, out observableTlist))
				{
					float num = Vector3.Distance({24706}, Engine.GS.Camera.Position);
					foreach (SoundManager.SoundLimits soundLimits in ((IEnumerable<SoundManager.SoundLimits>)observableTlist))
					{
						if (num > soundLimits.StartDistance && num <= soundLimits.EndDistance && !soundLimits.Sample())
						{
							return;
						}
					}
				}
				ShipCurrentPlayer player = Global.Player;
				if (player != null && player.IsPortEntry)
				{
					{24707} *= 0.6f;
				}
				if ({24708})
				{
					float value = Vector3.Distance({24706}, Engine.GS.Camera.Position);
					float amount = 0.45f * (1f - Geometry.InverseLerp(0f, 90f, value));
					{24706} = Vector3.Lerp({24706}, Engine.GS.Camera.Position, amount);
				}
				soundEffect3D.Play(Engine.GS.Camera, {24706}, {24707}, ({24705} == GameDynamicSoundName.CannonsGunFar_Assist) ? SoundOptions.DisableDistanceDeformation : SoundOptions.None);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000EDCD8 File Offset: 0x000EBED8
		public void PlaySound(GameStaticSoundName {24709}, float {24710} = 0.03f, float {24711} = 1f)
		{
			if (!this.{24743})
			{
				return;
			}
			ISoundEffect soundEffect;
			if (this.{24723}.TryGetValue({24709}, out soundEffect))
			{
				Stopwatch stopwatch;
				if (this.{24742}.TryGetValue({24709}, out stopwatch))
				{
					if (stopwatch.Elapsed.TotalMilliseconds < (double)({24710} * 1000f))
					{
						return;
					}
					stopwatch.Restart();
				}
				else
				{
					this.{24742}.Add({24709}, Stopwatch.StartNew());
				}
				soundEffect.Play({24711});
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000EDD50 File Offset: 0x000EBF50
		public bool PlayVoice(VoiceSoundEffect {24712})
		{
			if (!this.{24743} || Global.Settings.VoicesMode <= LocalSettings.ShipVoices.BackgroundOnly || Global.Game.GetCurrentSceneName == GameSceneName.Port || Global.Player.IsDestroyed || Global.Player.UsedShip.StaticInfo.IsBalloon)
			{
				return false;
			}
			if (this.{24727} > 0f)
			{
				return false;
			}
			this.{24727} = 5000f;
			ObservableTlist<Sound> observableTlist;
			if (this.{24724}.TryGetValue({24712}, out observableTlist))
			{
				Sound sound = observableTlist.Array[Rand.RangeInt(0, observableTlist.Size)];
				sound.Volume = 0.3f;
				sound.Play(1f);
				return true;
			}
			return false;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000EDDFC File Offset: 0x000EBFFC
		public GameStaticSoundName? NearPortStyleSound()
		{
			if (Global.Player == null || !Global.Player.MapInfo.IsWorldmap)
			{
				return null;
			}
			string modelName = Global.Player.NearPort.ModelData.Model.ModelName;
			return this.GetPortEntrySoundStyle(modelName);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000EDE4C File Offset: 0x000EC04C
		public GameStaticSoundName? GetPortEntrySoundStyle(string {24713})
		{
			if ({24713}.Contains("castle"))
			{
				return new GameStaticSoundName?(GameStaticSoundName.Port_England);
			}
			if ({24713}.Contains("asian"))
			{
				return new GameStaticSoundName?(GameStaticSoundName.Port_Asian);
			}
			if ({24713}.Contains("bay"))
			{
				return new GameStaticSoundName?(GameStaticSoundName.Port_Pirates);
			}
			if ({24713}.Contains("big_1"))
			{
				return new GameStaticSoundName?(GameStaticSoundName.Port_Spain);
			}
			if ({24713}.Contains("big_2"))
			{
				return new GameStaticSoundName?(GameStaticSoundName.Port_England);
			}
			if ({24713}.Contains("medium_2"))
			{
				return new GameStaticSoundName?(GameStaticSoundName.Port_England);
			}
			if ({24713}.Contains("orange"))
			{
				return new GameStaticSoundName?(GameStaticSoundName.Port_Spain);
			}
			if ({24713}.Contains("north"))
			{
				return new GameStaticSoundName?(GameStaticSoundName.Port_North);
			}
			if ({24713}.Contains("arab"))
			{
				return new GameStaticSoundName?(GameStaticSoundName.Port_Arabic);
			}
			return null;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000EDF3F File Offset: 0x000EC13F
		[CompilerGenerated]
		private void {24714}(UiControl {24715})
		{
			this.PlaySound(GameStaticSoundName.UIButtonInterop, 0.03f, 1f);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000EDF53 File Offset: 0x000EC153
		[CompilerGenerated]
		private void {24716}(UiControl {24717})
		{
			this.PlaySound(GameStaticSoundName.UIButton, 0.03f, 1f);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000EDF53 File Offset: 0x000EC153
		[CompilerGenerated]
		private void {24718}(CheckboxControl {24719})
		{
			this.PlaySound(GameStaticSoundName.UIButton, 0.03f, 1f);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000EDF67 File Offset: 0x000EC167
		[CompilerGenerated]
		private void {24720}(CheckboxControl {24721})
		{
			this.PlaySound(GameStaticSoundName.UITabSwitch, 0.03f, 1f);
		}

		// Token: 0x040018E9 RID: 6377
		public static float WeaponsThrottlingMaxMs = 130f;

		// Token: 0x040018EA RID: 6378
		public static float WeaponsThrottlingMaxChanse = 0f;

		// Token: 0x040018EB RID: 6379
		public static float WeaponsThrottlingMinMs = 70f;

		// Token: 0x040018EC RID: 6380
		private Dictionary<GameDynamicSoundName, ISoundEffect3D> {24722};

		// Token: 0x040018ED RID: 6381
		private Dictionary<GameStaticSoundName, ISoundEffect> {24723};

		// Token: 0x040018EE RID: 6382
		private LinkedDictionrary<VoiceSoundEffect, Sound> {24724} = new LinkedDictionrary<VoiceSoundEffect, Sound>();

		// Token: 0x040018EF RID: 6383
		private LocalSettings.ShipVoices {24725};

		// Token: 0x040018F0 RID: 6384
		private Locale {24726};

		// Token: 0x040018F1 RID: 6385
		private float {24727};

		// Token: 0x040018F2 RID: 6386
		private Sound {24728};

		// Token: 0x040018F3 RID: 6387
		private Sound {24729};

		// Token: 0x040018F4 RID: 6388
		private Sound {24730};

		// Token: 0x040018F5 RID: 6389
		private Sound {24731};

		// Token: 0x040018F6 RID: 6390
		private Sound {24732};

		// Token: 0x040018F7 RID: 6391
		private Sound {24733};

		// Token: 0x040018F8 RID: 6392
		private Sound {24734};

		// Token: 0x040018F9 RID: 6393
		private Sound {24735};

		// Token: 0x040018FA RID: 6394
		private Sound {24736};

		// Token: 0x040018FB RID: 6395
		private Sound {24737};

		// Token: 0x040018FC RID: 6396
		private Sound {24738};

		// Token: 0x040018FD RID: 6397
		private bool {24739};

		// Token: 0x040018FE RID: 6398
		private float {24740} = 1000f;

		// Token: 0x040018FF RID: 6399
		private LinkedDictionrary<GameDynamicSoundName, SoundManager.SoundLimits> {24741};

		// Token: 0x04001900 RID: 6400
		private Dictionary<GameStaticSoundName, Stopwatch> {24742};

		// Token: 0x04001901 RID: 6401
		private volatile bool {24743};

		// Token: 0x020004BB RID: 1211
		private class SoundLimits
		{
			// Token: 0x06001A9F RID: 6815 RVA: 0x000EDF7B File Offset: 0x000EC17B
			public SoundLimits(float {24748}, float {24749}, float {24750}, float {24751})
			{
				this.MinimumIntervalMs = {24748};
				this.StartDistance = {24749};
				this.EndDistance = {24750};
				this.BypassChanse = {24751};
			}

			// Token: 0x06001AA0 RID: 6816 RVA: 0x000EDFAC File Offset: 0x000EC1AC
			public bool Sample()
			{
				if (this.Timeout.Elapsed.TotalMilliseconds < (double)this.MinimumIntervalMs && (this.Timeout.Elapsed.TotalMilliseconds < 1.0 || this.BypassChanse == 0f || Rand.Chanse(100f - this.BypassChanse)))
				{
					return false;
				}
				this.Timeout.Restart();
				return true;
			}

			// Token: 0x04001902 RID: 6402
			public Stopwatch Timeout = Stopwatch.StartNew();

			// Token: 0x04001903 RID: 6403
			public readonly float MinimumIntervalMs;

			// Token: 0x04001904 RID: 6404
			public readonly float StartDistance;

			// Token: 0x04001905 RID: 6405
			public readonly float EndDistance;

			// Token: 0x04001906 RID: 6406
			public readonly float BypassChanse;
		}
	}
}
