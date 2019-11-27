using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuToGame : MonoBehaviour {
    //PatientDataList m_list;
    
    public TMPro.TMP_InputField m_inputField;
    
    public GameObject m_invalidID;

    public void CheckChangeScene()
    {
        string id = m_inputField.text;
        //PatientData pd = System.Array.Find(m_list.m_list, element => element.Id == id);
        if(id != null)
        {
            PlayerPrefs.SetString("ID", id);
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Scene0toScene1"));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ShowInvalidIdText());
        }
    }

    IEnumerator ShowInvalidIdText()
    {
        m_invalidID.SetActive(true);
        float timer = 0.0f;
        while (timer < 3.0f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_invalidID.SetActive(false);
    }

}
