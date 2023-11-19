using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public string sceneName = "";
    [Space]
    public GameObject pointParent;
    public GameObject winnerText;
    public GameObject gameOverObj;
    [Space]
    public AudioSource loseAudio;
    [Header("Area To Spawn")]
    public float xArea;
    public float yArea;

        
    [Space ,SerializeField]
    private int mileStone = 2;
    [SerializeField]
    private int goalCount = 3;
    private int counter = 0;
    public TextMeshProUGUI counterText;

    [Space]
    [HideInInspector] public bool inConversation = false;

    private AudioSource bgAudio;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowText();

        bgAudio = GameObject.FindGameObjectWithTag("bgAudio").GetComponent<AudioSource>();
    }

    public Vector3 SpawnRandom()
    {
        float x = Random.Range(-xArea, xArea);
        float y = Random.Range(-yArea, yArea);
        return new Vector3(x / 2, y /2);
    }

    public void SetCounter(int count)
    {
        counter += count;
        if(counter >= goalCount)
        {
            pointParent.SetActive(false);
            winnerText.SetActive(true);
        }
        else
        {
            if (counter >= mileStone)
            {
                foreach (FollowTarget followTarget in FindObjectsOfType(typeof(FollowTarget)))
                {
                    followTarget.SetSpeed(0.4f);
                }
                mileStone += mileStone;
            }
            ShowText();
        }
    }

    public void SetGameOver()
    {
        gameOverObj.SetActive(true);
        StartCoroutine(ResetGame());
    }

    private void ShowText()
    {
        counterText.text = counter.ToString() + "/" + goalCount.ToString();
    }

    public void ResetCounter()
    {
        counter = 0;
        ShowText();
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(4f);
        bgAudio.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void PlayLosingAudio()
    {
        loseAudio.Play();
        bgAudio.Stop();
    }

/*    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(xArea, yArea));
    }*/
}
