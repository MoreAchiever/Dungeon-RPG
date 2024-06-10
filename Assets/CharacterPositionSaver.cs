using UnityEngine;

public class CharacterPositionSaver : MonoBehaviour
{
    private string positionKey = "CharacterPosition";

    private void Start()
    {
        LoadCharacterPosition();
    }

    private void OnDestroy()
    {
        SaveCharacterPosition();
    }

    private void SaveCharacterPosition()
    {
        // Save the character's position
        PlayerPrefs.SetFloat(positionKey + "X", transform.position.x);
        PlayerPrefs.SetFloat(positionKey + "Y", transform.position.y);
        PlayerPrefs.Save();
    }

    private void LoadCharacterPosition()
    {
        if (PlayerPrefs.HasKey(positionKey + "X") && PlayerPrefs.HasKey(positionKey + "Y"))
        {
            // Load the character's position
            float posX = PlayerPrefs.GetFloat(positionKey + "X");
            float posY = PlayerPrefs.GetFloat(positionKey + "Y");
            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
