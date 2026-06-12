using System;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004A1 RID: 1185
	internal static class PowerupItemEffectHelper
	{
		// Token: 0x06001A03 RID: 6659 RVA: 0x000E7D6C File Offset: 0x000E5F6C
		internal static void ApplyClient(Ship {24425}, PowerupItemInfo {24426}, int {24427}, bool {24428})
		{
			if ({24427} == Global.Player.uID && {24425} == Global.Player)
			{
				if ({24428})
				{
					Session.Account.PowerupItemExtraSlot = byte.MaxValue;
				}
				else if (({24426}.ServerEffect != PowerupItemServerEffect.CaptureNearNpc || !Session.Game.NoUseCaptureNpcPowerupItem) && !PowerupItemsGenerator.SkillsIds.Contains({24426}.Index))
				{
					Session.Account.PowerupItemsAtStorage.AddOrRemove({24426}.Index, -1);
				}
			}
			if ({24425}.IsDestroyed)
			{
				return;
			}
			{24426}.StartOrApply({24425});
			if ({24425} == Global.Player)
			{
				if ({24426}.Index == 31)
				{
					Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Horn, 0.03f, 1f);
				}
				if ({24426}.Index == 11 || {24426}.Index == 12)
				{
					Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIDeploy, 0.03f, 0.75f);
				}
				else
				{
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.ItemUse, {24425}.Position3D, 1f, false);
				}
			}
			else
			{
				new ShowPowerupItemFSEffect({24425}, {24426}.Icon, {24426}.Icon.Bounds);
			}
			if ({24427} != Global.Player.uID && {24425} == Global.Player)
			{
				{19994}.Me({19988}.Info, Local.PowerupItemEffectHelper_0({24426}.Name), Array.Empty<object>());
				Session.Account.PowerupItemAddExternalBlock({24426}, Math.Max({24426}.Cooldown.Value / 2, 20));
			}
			switch ({24426}.Index)
			{
			case 0:
			case 1:
			case 2:
				new MendingpackSFEffect({24425}, Color.Lime);
				return;
			case 3:
			case 4:
			case 5:
			case 6:
				new MendingpackSFEffect({24425}, Color.Cyan);
				return;
			case 7:
			case 8:
			case 9:
				new MendingpackSFEffect({24425}, Color.Violet);
				return;
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
			case 17:
				break;
			case 15:
			case 16:
				new VuduShieldFSEffect({24425}, Color.White, (float)({24426}.WorkTime.Value * 1000));
				return;
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 32:
				new ExplosionWaveFlashEffect({24425}.Position3D, 2f, Color.Red * 0.2f, 0.75f);
				return;
			case 28:
				if ({24425} != Global.Player || {24427} == Global.Player.uID)
				{
					new SignalRocketFSEffect({24425}, Color.Blue);
					return;
				}
				break;
			case 29:
				if ({24425} != Global.Player || {24427} == Global.Player.uID)
				{
					new SignalRocketFSEffect({24425}, Color.Red);
					return;
				}
				break;
			case 30:
				if ({24425} != Global.Player || {24427} == Global.Player.uID)
				{
					new SignalRocketFSEffect({24425}, Color.Yellow);
					return;
				}
				break;
			case 31:
				new MendingpackSFEffect({24425}, Color.Gray);
				break;
			default:
				return;
			}
		}
	}
}
