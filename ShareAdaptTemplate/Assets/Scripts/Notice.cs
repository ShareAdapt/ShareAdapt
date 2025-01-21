using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notice : MonoBehaviour
{
    // Public field, used to set the text value during Instantiate
    public string noticeText;

    void Start()
    {
        // set Text of the notice
        Transform canvas = this.transform.Find("Canvas");

        if (canvas != null)
        {
            var textMeshPro = canvas.Find("NoticeText").GetComponent< TMP_Text>();

            if (textMeshPro != null)
            {
                textMeshPro.text = noticeText;
            }
            else
            {
                Debug.LogError("TextMeshPro组件未找到！");
            }
        }
        else
        {
            Debug.LogError("Canvas组件未找到！");
        }

        // Start the coroutine, and destroy the notice after five seconds.
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        // wait
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
