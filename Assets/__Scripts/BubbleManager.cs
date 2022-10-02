using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleManager : MonoBehaviour
{
    public TMP_Text bubbleCountText;
    public TMP_Text bubbleCountBackText;

    public int bubbleCount;

    private bool preparingBubble;
    private bool bubbleReady;

    // Update is called once per frame
    void Update()
    {
        bubbleCountText.text = $"You have {bubbleCount} bubbles.";
        bubbleCountBackText.text = $"<mark=#000000>{bubbleCountText.text}</mark>";

        if (!preparingBubble && Input.GetKeyDown(KeyCode.B))
        {
            preparingBubble = true;
            StartCoroutine(BubbleRoutine(Random.Range(4f, 5f)));
        }

        if (bubbleReady && Input.GetKeyUp(KeyCode.B))
        {
            bubbleCount++;
            bubbleReady = false;
        }
    }

    private IEnumerator BubbleRoutine(float time)
    {
        float timer = 0f;
        while (Input.GetKey(KeyCode.B))
        {
            if (timer >= time)
            {
                bubbleReady = true;
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        preparingBubble = false;
    }
}
