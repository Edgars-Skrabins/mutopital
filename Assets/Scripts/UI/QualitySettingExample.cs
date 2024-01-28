using UnityEngine;

public class QualitySettingExample : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            // Sets High quality settings
            SwitchQualitySettings(1);
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            // Sets Low quality settings
            SwitchQualitySettings(0);
        }
    }

    private void SwitchQualitySettings(int _changeIndex)
    {
        QualitySettings.SetQualityLevel(_changeIndex);
    }
}
