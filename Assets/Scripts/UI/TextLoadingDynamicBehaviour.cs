using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLoadingDynamicBehaviour : MonoBehaviour {
    Text text;
    private string[] textArray =
    { ".            ",
     "..           ",
     "...          ",
     "....         ",
     ".....        ",
     " .....       ",
     "  .....      ",
     "   .....     ",
     "    .....    ",
     "     .....   ",
     "      .....  ",
     "       ..... ",
     "        .....",
     "         ....",
     "          ...",
     "           ..",
     "            .",
     "             ",
    };
    private int i = 0;
    private bool isEnd = false;
	void Start () {
        text = GetComponent<Text>();
        StartCoroutine(Enumerator());
	}

    private void OnDestroy()
    {
        isEnd = true;
    }
    private IEnumerator Enumerator()
    {
        while (!isEnd)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            text.text = textArray[i];
            ++i;
            if (i == textArray.Length)
                i = 0;
        }
    }
}
