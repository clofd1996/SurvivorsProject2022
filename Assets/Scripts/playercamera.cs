using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class playercamera : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float cameraspeed = 5f;
    [SerializeField] GameObject pausepanel;

    Player player;
    Volume volume;
    Vignette vignette;
    ColorAdjustments coloradjustments;
    DepthOfField blur;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player = GameManager.GetInstance().currentPlayer;
        target = GameManager.GetInstance().currentPlayer.transform;
        volume = GetComponent<Volume>(); //这个最基本的GetComponent
        // out是必须的
        volume.profile.TryGet(out vignette); //从volume这个profile中 调用vignette这个override
        volume.profile.TryGet(out coloradjustments); //从volume这个profile中 调用coloradjustments这个override
        volume.profile.TryGet(out blur); //从volume这个profile中 调用depth of field这个override        
    }

    // Update is called once per frame
    private void Update()
    {
        float intensity = 0.5f * (1 - player.GetHpRatio());
        if (Bool.isButtomOn == true)
        {
            vignette.intensity.Override(intensity);
        }   

        if (pausepanel.activeInHierarchy == true && Bool.isButtomOn == true) // 当暂停面板激活时，把镜头模糊的焦距调整到80
        {
            blur.focalLength.Override(80);
        }

        else if (pausepanel.activeInHierarchy == false) // 当暂停面板激活时，把镜头模糊的焦距调整到1
        {
            blur.focalLength.Override(1);
        }


        if (player.playerHP <= 0 && Bool.isButtomOn == true) 
        {
            coloradjustments.saturation.Override(-100); //黑白画面
            return;

        }
        
        if (target.tag == "Player")
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
        else if (target.tag == "red")
        {
            var targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, cameraspeed * Time.unscaledDeltaTime);
        }
    }
}
