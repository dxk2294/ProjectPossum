using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDialog : MonoBehaviour
{
    public FinalizedDialogScript ActiveScript;
    public int ActiveScriptIndex = 0;

    public void BeginScript(FinalizedDialogScript script)
    {
        ActiveScript = script;
        ActiveScriptIndex = 0;
        CameraDialogFrame.SetActive(true);

        DisplayNextScriptLine();
    }

    public void DisplayNextScriptLine ()
    {
        int speakerIndex = ActiveScript.dialogScript.script[ActiveScriptIndex].speaker;
        SpeakerName.text = ActiveScript.speakers[speakerIndex].FirstName;
        SpeakerDialog.text = ActiveScript.dialogScript.script[ActiveScriptIndex].dialog;

        ActiveScriptIndex += 1;

        if (ActiveScriptIndex >= ActiveScript.dialogScript.script.Count)
        {
            FinishScript();
        }
    }

    public void FinishScript()
    {
        ActiveScript = null;
        ActiveScriptIndex = 0;
        CameraDialogFrame.SetActive(false);
    }

    public Camera SpeakerCam;
    public TextMeshProUGUI SpeakerName;
    public TextMeshProUGUI SpeakerDialog;

    public GameObject CameraDialogFrame;
    public GameObject CameraTarget;

    public RectTransform CharacterDialogArrow;

    public float CharacterDialogArrowSpeed = 1.0f;
    public float CharacterDialogArrowDistance = -7.5f;
    private float _characterDialogArrowStartingY;

    private void Start()
    {
        _characterDialogArrowStartingY = CharacterDialogArrow.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        SpeakerCam.transform.position = new Vector3(CameraTarget.transform.position.x,
            CameraTarget.transform.position.y, SpeakerCam.transform.position.z);

        CharacterDialogArrow.position = new Vector2(CharacterDialogArrow.position.x,
            _characterDialogArrowStartingY + (Mathf.Sin(Time.time * CharacterDialogArrowSpeed) * CharacterDialogArrowDistance) + CharacterDialogArrowDistance / 2.0f);
    }
}
