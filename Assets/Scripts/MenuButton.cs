using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private string _buttonText;
    private string _buttonTextSelected;
    [SerializeField] private TMPro.TMP_Text _text;
    private void Awake()
    {
        _buttonText = _text.text;
    }
    public void Select() {

    }
}
