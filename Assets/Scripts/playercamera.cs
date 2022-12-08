using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class playercamera : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float cameraspeed = 5f;


    Player player;
    Volume volume;
    Vignette vignette;
    ColorAdjustments coloradjustments;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        volume = GetComponent<Volume>(); //这个最基本的GetComponent
        // out是必须的
        volume.profile.TryGet(out vignette); //从volume这个profile中 调用vignette这个override
        volume.profile.TryGet(out coloradjustments); //从volume这个profile中 调用coloradjustments这个override

    }

    // Update is called once per frame
    private void Update()
    {
        float intensity = 0.5f * (1 - player.GetHpRatio());
        vignette.intensity.Override(intensity);


        if (player.playerHP <= 0) 
        {
            coloradjustments.saturation.Override(-100); //黑白画面
            return;

        }
        
        if (target.tag == "Player")
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
        else if (target.tag == "red")
        {
            var targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, cameraspeed * Time.unscaledDeltaTime);
        }


    }

    
}
