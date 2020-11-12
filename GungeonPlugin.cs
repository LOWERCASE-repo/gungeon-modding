using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

// C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -out:"C:\Program Files (x86)\Steam\steamapps\common\Enter the Gungeon\BepInEx\plugins\GungeonPlugin.dll" -target:library GungeonPlugin.cs -reference:BepInEx.dll -reference:Assembly-CSharp.dll -reference:UnityEngine.dll -reference:BepInEx.Harmony.dll -reference:UnityEngine.CoreModule.dll -reference:0Harmony.dll

namespace GungeonPlugin {
	
	[BepInPlugin("GungeonPlugin", "Gungeon Plugin", "1.0")]
	public class GungeonPlugin : BaseUnityPlugin {
		
		void Awake() {
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
		}
	}
	
	
	[HarmonyPatch(typeof(Gun), "Update")]
	class GunPatch {
		
		[HarmonyPostfix]
		static void Postfix(Gun __instance) {
			if (__instance.ClipShotsRemaining == 0) __instance.Reload();
			if (__instance.HasChargedProjectileReady) __instance.CeaseAttack();
			__instance.CurrentAmmo = __instance.GetBaseMaxAmmo() + 5;
		}
	}
	
	[HarmonyPatch(typeof(PlayerController), "DoConsumableBlank")]
	class BlankPatch {
		
		[HarmonyPostfix]
		static void Prefix(PlayerController __instance) {
			if (__instance.Blanks <= 0) __instance.Blanks++;
		}
	}
	
	// [HarmonyPatch(typeof(Gun), "InfiniteAmmo", MethodType.Getter)]
	// class AmmoPatch {
	//
	// 	[HarmonyPostfix]
	// 	static void Postfix(ref bool __result) {
	// 		__result = true;
	// 	}
	// }
	
	[HarmonyPatch(typeof(BraveInput), "ControllerFakeSemiAutoCooldown", MethodType.Getter)]
	class SemiPatch {
		
		[HarmonyPostfix]
		static void Postfix(ref float __result) {
			__result = 0f;
		}
	}
}
