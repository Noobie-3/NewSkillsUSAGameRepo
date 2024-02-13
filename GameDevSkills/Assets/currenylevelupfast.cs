using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currenylevelupfast : MonoBehaviour
{ public float i;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { i += 1;
       
        GetComponent<Text>().text = Mathf.RoundToInt(i).ToString();
    }
}
