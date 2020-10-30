using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

// C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -out:"C:\Program Files (x86)\Steam\steamapps\common\Enter the Gungeon\BepInEx\plugins\GungeonPlugin.dll" -target:library GungeonPlugin.cs -reference:BepInEx.dll -reference:Assembly-CSharp.dll -reference:UnityEngine.dll -reference:BepInEx.Harmony.dll -reference:UnityEngine.CoreModule.dll -reference:0Harmony.dll

namespace GungeonPlugin {
	
	[BepInPlugin("GungeonPlugin", "Gungeon Plugin", "")]
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
		}
	}
	
	[HarmonyPatch(typeof(BraveInput), "ControllerFakeSemiAutoCooldown", MethodType.Getter)]
	class SemiPatch {
		
		[HarmonyPostfix]
		static void Postfix(ref float __result) {
			__result = 0f;
		}
	}
	
	[HarmonyPatch(typeof(PlayerController), "AdjustInputVector")]
	class CardinalPatch {
		
		[HarmonyPostfix]
		static void PostFix(Vector2 ___rawInput, ref Vector2 __result) {
			__result = ___rawInput;
		}
	}
}
