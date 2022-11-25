using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class playercamera : MonoBehaviour
{
    [SerializeField] public Transform target;


    Player player;
    Volume volume;
    Vignette vignette;
    ColorAdjustments coloradjustments;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out coloradjustments);
        
    }

    // Update is called once per frame
    private void Update()
    {
        float intensity = 0.5f * (1 - player.GetHpRatio());
        vignette.intensity.Override(intensity);


        if (player == null)
        {
            coloradjustments.saturation.Override(-100);
            return;

        }
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float cameraZ = transform.position.z;

        transform.position = new Vector3(playerX, playerY, cameraZ);
    }

    
}
