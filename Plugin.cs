using _Scripts.Singletons;
using _Scripts.Spider;
using BepInEx;
using BepInEx.Unity.Mono;
using UnityEngine;
using UnityEngine.UIElements;

namespace Speeder;

[BepInPlugin("com.unusualotter.speeder", "speeder", "1.0.0")]
public class Plugin : BaseUnityPlugin {
    Rect windowRect;
    float speedMult = 1;
    float timeMult = 1;
    float jumpMult = 1;
    float sprintMult = 1;

    string speedBox = "1";
    string timeBox = "1";
    string jumpBox = "1";
    string sprintBox = "1";
    private void Awake() {
        // Plugin startup logic
        Logger.LogInfo($"Speeder has loaded!");

        windowRect = new Rect(0, 0, 300, 500);
    }

    void Update() {
        float.TryParse(timeBox, out timeMult);
        float.TryParse(speedBox, out speedMult);
        float.TryParse(jumpBox, out jumpMult);
        float.TryParse(sprintBox, out sprintMult);

        if (Singleton<GameController>.Instance && Singleton<GameController>.Instance.Player) {
            BodyMovement player = Singleton<GameController>.Instance.Player;
            Time.timeScale = 1f * timeMult;
            player.movementSpeed = 10f * speedMult;
            player.verticalSpeed = 2f * jumpMult;
            player.movementBoostFactor = 1.5f * sprintMult;
        }
    }

    void OnGUI() {
        windowRect = GUILayout.Window(5849, windowRect, SpeederUI, "Speeder");
    }

    void SpeederUI(int windowID) {
        GUILayout.Label("Speed Multiplier");
        speedBox = GUILayout.TextField(speedBox);

        GUILayout.Label("Time Multiplier");
        timeBox = GUILayout.TextField(timeBox);

        GUILayout.Label("Sprint Multiplier");
        sprintBox = GUILayout.TextField(sprintBox);

        GUILayout.Label("Jump Multiplier");
        jumpBox = GUILayout.TextField(jumpBox);

        GUI.DragWindow();
    }
}
