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
        volume = GetComponent<Volume>(); //����������GetComponent
        // out�Ǳ����
        volume.profile.TryGet(out vignette); //��volume���profile�� ����vignette���override
        volume.profile.TryGet(out coloradjustments); //��volume���profile�� ����coloradjustments���override
        volume.profile.TryGet(out blur); //��volume���profile�� ����depth of field���override        
    }

    // Update is called once per frame
    private void Update()
    {
        float intensity = 0.5f * (1 - player.GetHpRatio());
        if (Bool.isButtomOn == true)
        {
            vignette.intensity.Override(intensity);
        }   

        if (pausepanel.activeInHierarchy == true && Bool.isButtomOn == true) // ����ͣ��弤��ʱ���Ѿ�ͷģ���Ľ��������80
        {
            blur.focalLength.Override(80);
        }

        else if (pausepanel.activeInHierarchy == false) // ����ͣ��弤��ʱ���Ѿ�ͷģ���Ľ��������1
        {
            blur.focalLength.Override(1);
        }


        if (player.playerHP <= 0 && Bool.isButtomOn == true) 
        {
            coloradjustments.saturation.Override(-100); //�ڰ׻���
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
