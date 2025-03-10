using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class _playerStats : MonoBehaviour
{
    [Header("UI References")]
    public Image playerHealth;
    public Image SpecialCooldown;
    public Image CastCooldown;
    public TextMeshProUGUI  CoinCount;
    _attackController attack;
    public float coins;
    public bool isAlive;
    [Header("Player Stats")]
    public float _maxHealth = 100f;
    float _currentHealth;

    void Awake()
    {
        isAlive = true;
        _currentHealth = _maxHealth;
        attack = GetComponent<_attackController>();
    }
    void Update()
    {
        if(isAlive == false) StartCoroutine(Die());
        playerHealth.fillAmount = _currentHealth / 100;
        if(Input.GetButtonDown("Fire2")) CastCooldown.fillAmount = 0f;
        if(Input.GetButtonDown("Special")) SpecialCooldown.fillAmount = 0f;
        if(SpecialCooldown.fillAmount <= 1)
        {
            SpecialCooldown.fillAmount += Time.fixedDeltaTime/4f;
        }
        if(CastCooldown.fillAmount <= 1)
        {
            CastCooldown.fillAmount += Time.fixedDeltaTime/2f;
        }
        if(Input.GetKeyDown(KeyCode.C)) coins += 50;
        CoinCount.text = coins.ToString();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= damage) isAlive =false;
    }

    IEnumerator Die()
    {
        //Play Death Animation;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        //End Game;
    }
}
