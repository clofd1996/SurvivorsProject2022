using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f ;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BaseWeapon[] weapons;
    [SerializeField] TMP_Text playerLevel;
    [SerializeField] TMP_Text CoinNumber;

    [SerializeField] TMP_Text PauseLevel;
    [SerializeField] TMP_Text PauseHP;
    [SerializeField] TMP_Text PauseCoin;
    [SerializeField] TMP_Text PauseEXP;

    public int playerHP;
    public int maxHP;

    //public bool playerIsDead = false;



    //经验相关
    internal int currentExp = 0;
    internal int totalExp = 0;
    internal int expToLevel = 5;
    internal int currentLevel = 1;

    internal void AddExp()
    {
        currentExp++;
        totalExp++;
        if (currentExp >= expToLevel)// 升级
        {
            currentExp -= expToLevel;
            expToLevel += 5;
            currentLevel++;
            //调整HP
            maxHP += 5; //最大HP增加5
            playerHP = maxHP; //把血量回满

            weapons[0].LevelUp(); //Kanata 升级
            weapons[1].LevelUp(); //Scythe 升级
        }
    }

    internal int coin = 0;
    internal void AddCoin()
    {
        coin++;
    }

    [SerializeField] Animator animator;

    

    public float GetHpRatio()
    {
        return (float)playerHP / maxHP;
    }



    bool isInvincible;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = maxHP;
        animator = GetComponent<Animator>(); //添加动画

        weapons[0].LevelUp(); //开始游戏的时候生成一把武器
                              //    StartCoroutine(SwordCoroutine());

        
    }

    

    //IEnumerator SwordCoroutine() //自动激活武器
    //{
    //   while (this != null)
    //    {
    //        weapons.SetActive(true);
    //
    //        yield return new WaitForSeconds(0.5f);

    //        weapons.SetActive(false);

    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    public bool OnDamage()
    {
        IEnumerator InvincibleCoroutine()
        {
            isInvincible = true; //无敌
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            isInvincible = false; //取消无敌
            spriteRenderer.color = Color.white;
        }

        if (!isInvincible) //如果 非无敌
        {
            StartCoroutine(InvincibleCoroutine());
            return true;
        }
        return false;
    }


    internal void Damage(int damage)
    {
        playerHP -= damage;

        if (playerHP <= 0)
        {
            //playerIsDead = true;
            TitleManager.saveData.deathCount++;
            //Destroy(gameObject); // 不destroy player了
            Time.timeScale = 0.0f; //暂停时间
            GameOverManager.GetInstance().ShowDeathScreen();
            //GetComponent<GameOverManager>().ShowDeathScreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //计算角色等级
        playerLevel.text = "Level  " + currentLevel.ToString();

        //计算角色获得金币
        CoinNumber.text = coin.ToString();

        //计算暂停界面数据
        PauseLevel.text = "Level: " + currentLevel.ToString();
        PauseHP.text = "HP: " + playerHP.ToString() + " / " + maxHP.ToString();
        PauseCoin.text = coin.ToString();
        PauseEXP.text = totalExp.ToString();


        //控制移动速度 controll speed
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(inputX, inputY) * speed * Time.deltaTime;

        if (inputX != 0)
        {
            transform.localScale = new Vector3(inputX >0 ? 1 : -1, 1, 1);
        }

        //跑步动画
        bool isRunning = false;
        if (inputX != 0 || inputY != 0)
        {
            isRunning = true;
        }
        animator.SetBool("isRunning", isRunning);


    }
}
