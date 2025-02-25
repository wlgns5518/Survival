using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}


[System.Serializable]
public class Condition
{
    [HideInInspector]
    public float curValue;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayrate;
    public Image uiBar;

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue+amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

}

public class PlayerConditions : MonoBehaviour, IDamagable
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;

    public float noHurgerHealthDecay;

    public UnityEvent onTakeDamage;


    // Start is called before the first frame update
    void Start()
    {
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        stamina.curValue = stamina.startValue;
    }

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.decayrate * Time.deltaTime);
        stamina.Add(stamina.regenRate * Time.deltaTime);

        if(hunger.curValue ==.0f)
            health.Subtract(noHurgerHealthDecay * Time.deltaTime);

        if (health.curValue == .0f)
            Die();

        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        stamina.uiBar.fillAmount = stamina.GetPercentage();


    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    public bool UseStamina(float amount)
    {
        if (stamina.curValue < amount)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
    public void Die()
    {
        Debug.Log("플레이어 다이");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }
}
