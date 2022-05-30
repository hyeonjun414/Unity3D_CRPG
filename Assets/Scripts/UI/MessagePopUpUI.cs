using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessagePopUpUI : MonoBehaviour
{
    public delegate void Callback();
    private Callback callbackOK;
    private Callback callbackYES;
    private Callback callbackNO;

    // Define
    public enum DialogType { NOTICE = 0, WARNING = 1, CAUTION = 2 }
    public enum DialogResponse { OK, YES, NO, ERROR }

    public GameObject OneButton;
    public GameObject YesOrNoButton;

    public Text messageText;
    public Button OkButton;
    public Button YesButton;
    public Button NoButton;

    private DialogResponse thisResult;

    private void Awake()
    {

        // 기본 리스너 추가
        OkButton.onClick.AddListener(PopUpClose);
        YesButton.onClick.AddListener(PopUpClose);
        NoButton.onClick.AddListener(PopUpClose);
    }

    public void PopUp(string message)
    {
        gameObject.SetActive(true);

        messageText.text = message;

        YesOrNoButton.SetActive(false);
        OneButton.SetActive(true);
        OkButton.interactable = true;

    }

    public void PopUp(string message, DialogType type, Callback Function)
    {
        gameObject.SetActive(true);
        SetCallback(null, DialogResponse.OK);

        messageText.text = message;

        YesOrNoButton.SetActive(false);
        OneButton.SetActive(true);
        OkButton.interactable = true;

    }

    public void YesOrNoPopUp(string message, DialogType type, Callback yesButtonFuncion, Callback noButtonFuntion)
    {
        gameObject.SetActive(true);
        SetCallback(yesButtonFuncion, DialogResponse.YES);
        SetCallback(noButtonFuntion, DialogResponse.NO);

        messageText.text = message;

        OneButton.SetActive(false);
        YesOrNoButton.SetActive(true);
        YesButton.interactable = true;
        NoButton.interactable = true;
        
    }

    // 팝업이 닫힐 때, 지정해 둔 콜백 실행
    public void PopUpClose()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "OKButton":
                thisResult = DialogResponse.OK;
                callbackOK?.Invoke();
                callbackOK = null;
                break;

            case "YesButton":
                thisResult = DialogResponse.YES;
                callbackYES?.Invoke();
                callbackYES = null;
                break;

            case "NoButton":
                thisResult = DialogResponse.NO;
                callbackNO?.Invoke();
                callbackYES = null;
                break;

            default:
                thisResult = DialogResponse.ERROR;
                break;
        }

        OkButton.interactable = false;
        YesButton.interactable = false;
        NoButton.interactable = false;
        gameObject.SetActive(false);
    }

    // Callback 관련
    public void SetCallback(Callback call, DialogResponse buttontype)
    {
        switch (buttontype)
        {
            case DialogResponse.OK:
                callbackOK = call;
                break;

            case DialogResponse.YES:
                callbackYES = call;
                break;

            case DialogResponse.NO:
                callbackNO = call;
                break;

            default:
                callbackOK = call;
                callbackYES = call;
                callbackNO = call;
                break;
        }
    }

}
