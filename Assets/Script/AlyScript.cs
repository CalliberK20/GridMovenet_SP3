using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlyScript : MonoBehaviour
{
    public TextMeshPro caughtText;
    public GameObject textParent;
    public Vector2 offset;
    [Space]
    public float textSpeed = 0.3f;
    [TextArea(3, 5)]
    public string[] wordsToTalk;

    private Manager manager;
    private bool isCaught = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(!isCaught)
            {
                StartCoroutine(Caught());
            }
        }
    }


    IEnumerator Caught()
    {
        manager.SetCounter(1);
        isCaught = true;

        textParent.SetActive(true);
        textParent.transform.position = transform.position + new Vector3(offset.x, offset.y);

        int rand = Random.Range(0, wordsToTalk.Length);
        caughtText.text = "";
        StartCoroutine(TextDelay(wordsToTalk[rand]));

        yield return new WaitForSeconds(3);
        textParent.SetActive(false);

/*        transform.position = manager.SpawnRandom();
*/
        isCaught = false;
    }

    IEnumerator TextDelay(string toTalk)
    {
        foreach(char c in toTalk)
        {
            caughtText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

}
