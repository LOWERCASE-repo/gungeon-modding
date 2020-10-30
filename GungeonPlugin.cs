using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace GungeonPlugin {
	
	[BepInPlugin("GungeonPlugin", "Gungeon Plugin", "1.0.0.0")]
	public class GungeonPlugin : BaseUnityPlugin {
		
		void Awake() {
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
		}
	}
	
	
	[HarmonyPatch(typeof(Gun), "Update")]
	class GunPatch {
		
		static void Postfix(Gun __instance) {
			if (__instance.ClipShotsRemaining == 0) __instance.Reload();
		}
	}
	
	[HarmonyPatch(typeof(BraveInput), "ControllerFakeSemiAutoCooldown", MethodType.Getter)]
	class SemiPatch {
		
		[HarmonyPostfix]
		static void Postfix(ref float __result) {
			__result = 0f;
		}
	}
}
