using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleQuantity : MonoBehaviour
{
    private const int QUANTITY_MODIFIER = 5;
    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = this.GetComponentInChildren<TMP_InputField>();
    }

    public void DecreaseQuantity()
    {
        int newValue = int.Parse(_inputField.text) - QUANTITY_MODIFIER;
        _inputField.SetTextWithoutNotify(newValue.ToString());
    }

    public void IncreaseQuantity()
    {
        int newValue = int.Parse(_inputField.text) + QUANTITY_MODIFIER;
        _inputField.SetTextWithoutNotify(newValue.ToString());
    }
}
