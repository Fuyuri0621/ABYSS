using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBattle : MonoBehaviour
{

    public WeaponConfig currentWeaponConfig;

    [SerializeField] List<WeaponConfig> weaponList;

     bool isOnNeceTime;

    ComboConfig currentComboConfig;

    public int currentDamageRate;
    public float currentknockbackrate;

    int lightAttackIdx = 0;
    int heavyAttackIdx = 0;

    float stopcomboTimer;

    [SerializeField] const float animationFadeTime = 0.1f;
    Animator animator;
    PlayerMovent playerMovent;
    Transform currentweaponUI;
    Transform canvas;
    void Start()
    {
        canvas = transform.Find("Canvas");
        currentweaponUI = transform.Find("Canvas/currentweapon");
        animator = GetComponent<Animator>();
        playerMovent = GetComponent<PlayerMovent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > stopcomboTimer && (lightAttackIdx != 0 || heavyAttackIdx != 0))
        {
            StopCombo();


        }
        if (Time.time > stopcomboTimer&& isOnNeceTime)
        {
            ReleasePose();
        }


    }

    public void OnLightAttack(InputValue value)
    {
        if (isOnNeceTime) { return; }
        NormalAttack(true);
    }

    public void OnHeavyAttack(InputValue value)
    {
        if (isOnNeceTime) { return; }

        NormalAttack(false);
    }

    public void NormalAttack(bool isLight)
    {
        List<ComboConfig> configs = isLight ? currentWeaponConfig.lightComboConfig : currentWeaponConfig.heavyComboConfig;
        int comboIdx = isLight ? lightAttackIdx : heavyAttackIdx;
        StartCoroutine(PlayCombo(configs[comboIdx]));




        if (comboIdx >= configs.Count - 1)
        {
            comboIdx = 0;
        }
        else
        {
            comboIdx++;
        }

        if (isLight)
        {
            lightAttackIdx = comboIdx;
          

            if (heavyAttackIdx != 0)
            {
                lightAttackIdx = 1; heavyAttackIdx = 0;
            }
        }
        else //hvyatk
        {
            if (lightAttackIdx != 0)
            { comboIdx = 1; lightAttackIdx = 0; }

            heavyAttackIdx = comboIdx;
        }
    }

    IEnumerator PlayCombo(ComboConfig comboConfig)
    {
        stopcomboTimer=Time.time+1.2f;

        isOnNeceTime = true;
        playerMovent.AllowMoving(false);
        currentComboConfig = comboConfig;


        currentDamageRate = comboConfig.damagerate;
        currentknockbackrate = comboConfig.knockbackrate;

        animator.Play(comboConfig.animatorStateName);

       
        yield break;
    }

    void StopCombo()
    {
        lightAttackIdx = 0;
        heavyAttackIdx = 0;
    }
    public void ReleasePose() 
    {
        isOnNeceTime = false;
        playerMovent.AllowMoving(true);
    }

    public void OnSwitchWeapon()
    {
        animator.Play("switchweapon", 1);
        if (GameManager.Instance.datacontainer.craftedWeapon.Count == 1) return;


        int i = weaponList.FindIndex(x => x.name == currentWeaponConfig.name) + 1;

        if (i >= weaponList.Count) { i = 0; }

        while (GameManager.Instance.datacontainer.craftedWeapon.Find(x => x == weaponList[i].name) == null) 
        {  i++; if (i >= weaponList.Count) { i = 0; } }


        currentWeaponConfig = weaponList[i];
        Debug.Log("switch weapon"+ weaponList[i].name+i);
    }
    
    public void SwitchWeaponUI()
    {
        Transform a = currentweaponUI.GetChild(0);
        a.transform.SetParent(canvas, false);
        a.gameObject.SetActive(false);
       
        Transform b = transform.Find("Canvas/"+currentWeaponConfig.name);
        b.transform.SetParent(currentweaponUI, false);
        b.gameObject.SetActive(true);
    }

    [SerializeField ] GameObject shoot;
    public void Shoot()
    {
        GameObject g = Instantiate(shoot);
        g.transform.position = transform.position+Vector3.up;
        g.transform.localScale =transform.localScale;
    }
}
