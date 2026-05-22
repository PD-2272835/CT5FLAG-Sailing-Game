using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashSequence : MonoBehaviour
{
    public float startScale = 1f;
    public float endScale = 1.3f;
    public GameObject[] Logos;
    public Image fog;

    void Start()
    {
        foreach (GameObject logo in Logos)
        {
            logo.gameObject.SetActive(false);
        }

        StartCoroutine(PopInLogo(Logos, 0, 2.1f, startScale, endScale));
    }



    public IEnumerator PopInLogo(GameObject[] obj, int index, float duration, float start, float end)
    {
        obj[index].gameObject.SetActive(true);
        float factor = 1f / duration;
        float currentScale;
        Image image = obj[index].GetComponent<Image>();

        for (float time = 0f; time <= duration; time += Time.deltaTime * factor)
        {
            float t = time / duration; //ensure t is within the range 0-1

            //https://easings.net/
            float progress = 1 - Mathf.Pow(1 - t, 5); //easeOutQuint
            //float progress = Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2)); //easeOutCirc
            //float progress = 1 - Mathf.Pow(1 - time, 3); //ease out cubic


            currentScale = Mathf.Lerp(start, end, progress);
            obj[index].transform.localScale = new Vector3(currentScale, currentScale, currentScale); //ease the scale to pop logo in
            fog.color = new Color(0f,0f,0f,1-t); //TODO: maybe change the fade in on a different easing?
            yield return null;
        }


        obj[index].transform.localScale = new Vector3(endScale, endScale, endScale);
        obj[index].gameObject.SetActive(false);


        if (index < obj.Length - 1) StartCoroutine(PopInLogo(obj, index + 1, duration, start, end));
        else
        {
            Debug.Log(SceneManager.GetSceneByName("TitleScreen"));
            SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
        }
    }
}
