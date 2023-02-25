using System;
using Modding;
using UnityEngine;

namespace AutomationUtil
{
	public class Mod : ModEntryPoint
	{
		public const string GameObjectName = "PracModObject";

		public static GameObject modObject;

		public override void OnLoad()
		{
			modObject = GameObject.Find(GameObjectName);

			if (!modObject)
            {
				UnityEngine.Object.DontDestroyOnLoad(modObject = new GameObject(GameObjectName));
            }

            modObject.AddComponent<CommonController>();
        }

		public static void Log(string text)
        {
			Debug.Log("AutomationUtil:" + text);
        }

		public static void LogError(string text)
        {
			Debug.LogError("AutomationUtil:" + text);
        }

        public static bool IsAvailableScene() => AdvancedBlockEditor.Instance != null;

	}
}
