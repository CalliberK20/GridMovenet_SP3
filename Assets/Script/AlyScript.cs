using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlyScript : MonoBehaviour
{
    public FollowCam followCam;
    [Space]
    public TextMeshPro caughtText;
    public GameObject textParent;
    public Vector2 offset;
    [Space]
    public float textSpeed = 0.3f;
    [TextArea(3, 5)]
    public string[] wordsToTalk;

    private Manager manager;
    private bool isCaught = false;
    private int current = 0;
    private bool isCurrentTyping = false;

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
        manager.inConversation = true;

        textParent.SetActive(true);
        textParent.transform.position = transform.position + new Vector3(offset.x, offset.y);

        Transform regTarget = followCam.CurrentTarget();
        followCam.NewTarget(transform);

        current = 0;
        while (current < wordsToTalk.Length)
        {
            caughtText.text = "";
            StartCoroutine(TextDelay(wordsToTalk[current]));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && !isCurrentTyping);
            current++;
        }
        
        textParent.SetActive(false);
        followCam.NewTarget(regTarget);

        manager.inConversation = false;
        isCaught = false;
    }

    IEnumerator TextDelay(string toTalk)
    {
        isCurrentTyping = true;
        foreach (char c in toTalk)
        {
            caughtText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isCurrentTyping = false;
    }

}
