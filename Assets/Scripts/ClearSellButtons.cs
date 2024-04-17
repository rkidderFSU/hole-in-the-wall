using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSellButtons : MonoBehaviour
{
    private Button button;
    private GameManager m;
    private CameraController cam;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        m = GameObject.Find("Game Manager").GetComponent<GameManager>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m.gameEnded || !cam.followPlayer)
        {
            button.enabled = false;
        }
        else
        {
            button.enabled = true;
        }
    }
}
