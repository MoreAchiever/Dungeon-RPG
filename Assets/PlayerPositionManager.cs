using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    private Transform playerTransform;
    private int lastSceneIndex;
    private int WasInBossFight;

    private void Awake()
    {
        // Find the player object in the scene by tag or name
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        // Get the last scene index from PlayerPrefs
        WasInBossFight = PlayerPrefs.GetInt("MyIntegerValue");

        // Load the player's position based on the last scene index
        if (WasInBossFight == 1)
        {
            // Player is returning to the game scene, load the saved position
            LoadPlayerPosition();
            WasInBossFight = 0;
            PlayerPrefs.SetInt("MyIntegerValue", WasInBossFight);
        }
        else
        {
            // Player is coming from a menu scene, save the current position
            SavePlayerPosition();
        }
    }

    private void Update()
    {
        SavePlayerPosition();
    }

    private void SavePlayerPosition()
    {
        // Save the player's position
        PlayerPrefs.SetFloat("PlayerPosX", playerTransform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerTransform.position.y);
        PlayerPrefs.Save();
    }

    private void LoadPlayerPosition()
    {
        // Load the player's position
        float posX = PlayerPrefs.GetFloat("PlayerPosX");
        float posY = PlayerPrefs.GetFloat("PlayerPosY");
        float posZ = PlayerPrefs.GetFloat("PlayerPosZ");
        playerTransform.position = new Vector2(posX, posY);
    }
}
