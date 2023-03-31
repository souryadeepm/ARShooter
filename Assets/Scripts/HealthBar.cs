using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image Healthbar;

    private void OnEnable()
    {
        GetComponent<Health>().OnHealthchanged += HealthChanged;
    }

    private void HealthChanged(float newFillAmount)
    {
        StartCoroutine(LerpHealthbarRoutine(newFillAmount));
    }
    private IEnumerator LerpHealthbarRoutine(float newFillamount)
    {
        float elapsedTime;
        float duration = 1;

        elapsedTime = 0.0f;
        while(elapsedTime < duration)
        {
            Healthbar.fillAmount = Mathf.Lerp(Healthbar.fillAmount, newFillamount, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
        Healthbar.fillAmount = newFillamount;
    }

   
    private void OnDisable()
    {
        GetComponent<Health>().OnHealthchanged -= HealthChanged;    
    }




}
