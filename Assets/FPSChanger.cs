using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSChanger : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public TMP_Dropdown fpsDropdown;

    // Start is called before the first frame update
    void OnEnable()
    {
        QualitySettings.vSyncCount = 0;
        FPSCount();
    }

    public void FPSCount()
    {
        int fps = 0;
        switch (fpsDropdown.value)
        {
            case 0:
                fps = 30;
                break;
            case 1:
                fps = 60;
                break;
            case 2:
                fps = 120;
                break;
        }

        Application.targetFrameRate = fps;
        StartCoroutine(FPS());
    }

    private IEnumerator FPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            float fps = (int)(1f / Time.deltaTime);

            fpsText.text = fps.ToString() + " FPS";
        }
    }
}
