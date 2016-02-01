using System.Collections.Generic;
using System.Collections;
using UnityEngine;


class LoadingScreen : MonoBehaviour
{
    public Animator SunAnimator;

    public void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3f);
        SunAnimator.SetTrigger("Sunset");


        yield return new WaitForSeconds(3f);

        DayTracker.ShowOfficeScene();
    }

}

