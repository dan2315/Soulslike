using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Text _messageText;
    [SerializeField] private Text _buttonRetryText;
    [SerializeField] private Text _buttonExitText;

    [SerializeField] private Button _buttonRetry;
    [SerializeField] private Button _buttonExit;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show(string message, string buttonRetryMessage, string buttonExitMessage)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _messageText.text = message;
        _buttonRetryText.text = buttonRetryMessage;
        _buttonExitText.text = buttonExitMessage;

        _buttonRetry.onClick.AddListener(() => SceneManager.LoadScene("Main"));
        _buttonExit.onClick.AddListener(Application.Quit);

        gameObject.SetActive(true);
    }
}