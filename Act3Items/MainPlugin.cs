using BepInEx;
using BepInEx.Configuration;
using DiskCardGame;
using System;
using System.Collections.Generic;

namespace Act3Items {
	[BepInPlugin(GUID,Name,Version)]
	public class MainPlugin : BaseUnityPlugin {

		internal const string GUID = "io.github.TeamDoodz.Act3Items";
		internal const string Name = "Act3Items";
		internal const string Version = "1.0.0";

		public static List<string> AllItems = new List<string>() { 
			"BombRemote",
			"Battery",
			"ShieldGenerator"
		};

		public static Dictionary<string, string> NewDescriptions = new Dictionary<string, string>() {
			{"BombRemote", "Mrs. Bomb's Remote. Using it, you may fill the battlefield with Explode Bots." },
			{"Battery", "An extra battery. It will replenish your energy to its maximum." },
			{"ShieldGenerator", "This powerful device can shield your creatures from attacks." }
		};

		public static Dictionary<string, string> NewRulebookDescriptions = new Dictionary<string, string>() {
			{"BombRemote", "An explode bot is placed on all empty slots on the board (excluding the ones in my queue)." },
			{"Battery", "Your energy count will be replenished to its current maximum." },
			{"ShieldGenerator", "All cards on your side of the board gain the Nano Armor sigil." }
		};

		private void Awake() {
			Logger.LogWarning("Act3Items has only been tested in the Kaycee's Mod Beta. Although it may work in the base game, don't expect any support for it.");

			foreach (var item in ItemsUtil.AllConsumables) { // iterate through all items
				Logger.LogDebug($"Name: {item.name} - Display Name: {item.rulebookName}"); // log info abt item
				if (AllItems.Contains(item.name)) { // if this item is one of the act 3 ones
					item.regionSpecific = false; // make it non-region specific, this will allow it to be accessed in act 1
					item.rulebookCategory = AbilityMetaCategory.Part1Rulebook; // make it appear in act 1 rulebook
					if (Config.Bind("General", "ChangeQuips", true, "Change the text that Leshy says when you find the item for the first time.").Value) {
						// if allowed to, change the quip for the item
						item.description = NewDescriptions[item.name];
					}
					if (Config.Bind("General", "ChangeRulebookDesc", true, "Change the rulebook description for the item.").Value) {
						// if allowed to, change the description of the item
						item.rulebookDescription = NewRulebookDescriptions[item.name];
					}
					Logger.LogMessage($"Made item {item.name} usable in act 1"); // :)
				}
			}
		}

	}
}
