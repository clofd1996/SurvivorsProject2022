using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEXPBar : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image playerEXPBarground;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0.3f, 0);

        float expRatio = (float)player.currentExp / player.expToLevel;

        playerEXPBarground.transform.localScale = new Vector3(expRatio, 1, 1);
    }
}
