using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{

    [SerializeField] Slider altimeter;
    [SerializeField] Button startButton;
    [SerializeField] TextMeshProUGUI sBtn;
    [SerializeField] TextMeshProUGUI cd;
    [SerializeField] TextMeshProUGUI speed;

    PlayerController player;
    GameManager GM;
    GameObject ground;
    [SerializeField] string startText = "Tap To Jump";


    [SerializeField] GameObject onPlayCanvas;
    [SerializeField] GameObject onWaitCanvas;

    private void Start()
    {
        onPlayCanvas.SetActive(false);
        onWaitCanvas.SetActive(true);
        sBtn.SetText(startText);

        player = PlayerController.Instance;
        GM = GameManager.Instance;
        ground = GameObject.FindGameObjectWithTag("Ground");


    }

    public void OnClick()
    {

        onPlayCanvas.SetActive(true);
        onWaitCanvas.SetActive(false);

        
        StartCoroutine("waiter");
        
        cd.SetText("");
        cd.enabled = false;
        GM.play();
        play();

    }

    IEnumerator waiter()
    {
        for (int i = 3; i >= 0; i--)
        {
            cd.SetText("" + i + "");
            yield return new WaitForSeconds(3f);
        }
    }

    public void play()
    {
        altimeter.maxValue = GM.vh;
        StartCoroutine("updateRoutine");
    }

    IEnumerator updateRoutine()
    {
        while (true)
        {
            altimeter.value = GM.vh - player.getPosition().y;
            speed.SetText("" + player.getSpeed());
            yield return null;
        }
    }
}
