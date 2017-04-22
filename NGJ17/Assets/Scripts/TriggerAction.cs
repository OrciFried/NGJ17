using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerAction : MonoBehaviour
{
    [System.Serializable]
    public struct InfoField
    {
        public string t_info, c_name;
        public bool b_interact_active;
        public Vector2 position, wh;
    } // A struct with the elements of an infoField: text, name, pos, wh, bool

    [SerializeField]
    private InfoField[] infoFields;

    [SerializeField]
    private Text ui_text;

    [SerializeField]
    private Vector2 v2_wh_interact;

    [SerializeField]
    private string t_interact;

    private EventInput EI;

    private void Start()
    {
        EI = GetComponent<EventInput>();
        DisableText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < infoFields.Length; i++)
            if (collision.name == infoFields[i].c_name)
            {
                ShowText(t_interact, infoFields[i].position, v2_wh_interact);
                infoFields[i].b_interact_active = true;
            }
    } // Searches for the name of the triggered object in infoFields and shows the interact text. Also activated the field's ability to be interacted with.

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < infoFields.Length; i++)
            if (collision.name == infoFields[i].c_name)
            {
                DisableText();
                infoFields[i].b_interact_active = false;
            }
    } // Disables the text when exiting object as well as cancels its ability to be interacted with

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            for (int i = 0; i < infoFields.Length; i++)
                if (infoFields[i].b_interact_active)
                    ShowText(infoFields[i].t_info, infoFields[i].position, infoFields[i].wh);
    } // When e (or other input button) is pressed, the infoField whose interactable bool is active has its associated text shown

    private void ShowText(string text, Vector2 worldPosition, Vector2 scale)
    {
        ui_text.rectTransform.localPosition = worldPosition;
        ui_text.rectTransform.sizeDelta = scale;
        ui_text.enabled = true;
        ui_text.text = text;
    } // Sets respectively position-, size-, enabled- and text of the UI text

    private void DisableText()
    {
        ui_text.enabled = false;
    } // Disables the text
}