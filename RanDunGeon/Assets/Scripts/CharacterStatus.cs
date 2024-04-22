using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public float DefaultHp;
    public float DefaultAd;
    public float DefaultDefense;
    public float DefaultCriPercent;
    public float DefaultCriDamage;

    public float Hp;
    public float Ad;
    public float Defense;
    public float DownDamage;
    public float CriPercent;
    public float CriDamage;

    public float HpBuff;
    public float AdBuff;
    public float DefenseBuff;
    public float CriPercentBuff;
    public float CriDamageBuff;

    public float HpdDebuff;
    public float AdDebuff;
    public float DefenseDebuff;
    public float CriPercentDebuff;
    public float CriDamageDebuff;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Hp = DefaultHp * HpBuff * HpdDebuff;

        Ad = DefaultAd * AdBuff * AdDebuff;

        Defense = DefaultDefense * DefenseBuff * DefenseDebuff;

        CriPercent = DefaultCriPercent + CriPercentBuff - CriPercentDebuff;

        CriDamage = DefaultCriDamage + CriDamageBuff - CriDamageDebuff;
    }
}
