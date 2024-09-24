using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingNumberManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += new Vector3(0, 80f * Time.deltaTime, 0);
    }

    public void SetText(string text)
    {
        GetComponent<TMP_Text>().text = text;
        if (text.Contains("-"))
        {
            GetComponent<TMP_Text>().color = Color.red;
        }
        else
        {
            GetComponent<TMP_Text>().color = Color.green;
        }
    }
}
