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

    Material material; //Material Field

    public int playerHP;
    public int maxHP;

    //public bool playerIsDead = false;



    //�������
    internal int currentExp = 0;
    internal int totalExp = 0;
    internal int expToLevel = 5;
    internal int currentLevel = 1;

    internal void AddExp()
    {
        currentExp++;
        totalExp++;
        if (currentExp >= expToLevel)// ����
        {
            currentExp -= expToLevel;
            expToLevel += 5;
            currentLevel++;
            //����HP
            maxHP += 5; //���HP����5
            playerHP = maxHP; //��Ѫ������
            //��ʼHeal�ĵ�����
            StartCoroutine(HealCoroutine());
            
            IEnumerator HealCoroutine()
            {
                // Heal����ɫShader��Ч
                material.SetInt("_HealBool", 0); //��Breach�л���Heal��Ч��false��

                yield return new WaitForSeconds(0.5f);
                material.SetInt("_HealBool", 1); //��Breach�л���Flash��Ч��true��
            }

            weapons[0].LevelUp(); //Kanata ����
            weapons[1].LevelUp(); //Scythe ����
        }
    }

    internal int coin = 0;
    internal void AddCoin()
    {
        coin++;
    }

    [SerializeField] Animator animator;

    

    public float GetHpRatio() //�������Camera��ѪЧ����
    {
        return (float)playerHP / maxHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHP = maxHP;
        animator = GetComponent<Animator>(); //��Ӷ���

        weapons[0].LevelUp(); //��ʼ��Ϸ��ʱ������һ������
                              //    StartCoroutine(SwordCoroutine());

        // Assign the material from the sprite renderer to your field
        material = spriteRenderer.material;  
    }

    bool isInvincible;

    public bool OnDamage()
    {
        IEnumerator InvincibleCoroutine()
        {
            isInvincible = true; //�޵�
            //spriteRenderer.color = Color.red;
            material.SetFloat("_Flash", 0.35f);
            yield return new WaitForSeconds(0.2f);
            isInvincible = false; //ȡ���޵�
            //spriteRenderer.color = Color.white;
            material.SetFloat("_Flash", 0);
        }

        if (!isInvincible) //��� ���޵�
        {
            StartCoroutine(InvincibleCoroutine());
            return true;
        }
        return false;
    }


    internal void Damage(int damage)
    {
        playerHP -= damage;
        OnDamage();

        if (playerHP <= 0)
        {
            TitleManager.saveData.deathCount++;
            //Destroy(gameObject); // ��destroy player��
            Time.timeScale = 0.0f; //��ͣʱ��
            GameOverManager.GetInstance().ShowDeathScreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�����ɫ�ȼ�
        playerLevel.text = "Level  " + currentLevel.ToString();

        //�����ɫ��ý��
        CoinNumber.text = coin.ToString();

        //������ͣ��������
        PauseLevel.text = "Level: " + currentLevel.ToString();
        PauseHP.text = "HP: " + playerHP.ToString() + " / " + maxHP.ToString();
        PauseCoin.text = coin.ToString();
        PauseEXP.text = totalExp.ToString();


        //�����ƶ��ٶ� controll speed
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(inputX, inputY) * speed * Time.deltaTime;

        if (inputX != 0)
        {
            transform.localScale = new Vector3(inputX >0 ? 1 : -1, 1, 1);
        }

        //�ܲ�����
        bool isRunning = false;
        if (inputX != 0 || inputY != 0)
        {
            isRunning = true;
        }
        animator.SetBool("isRunning", isRunning);


    }
}
