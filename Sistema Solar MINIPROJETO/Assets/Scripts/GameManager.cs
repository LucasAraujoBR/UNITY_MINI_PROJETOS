using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject inputNomeJogador1; // Referência ao campo de nome do Jogador 1
    [SerializeField] public GameObject inputNomeJogador2; // Referência ao campo de nome do Jogador 2
    [SerializeField] public TextMeshProUGUI TextAreaJogador1; 
    [SerializeField] public TextMeshProUGUI TextAreaJogador2; 
    [SerializeField] public GameObject MenuPrincipal;      // Referência ao menu inicial
    [SerializeField] public GameObject NomeJogador;      // Referência ao menu inicial
    [SerializeField] public Button Voltar;       // Botão para confirmar o início do jogo
    [SerializeField] public Button Jogar;       // Botão para confirmar o início do jogo

    private int numJogadores = 1;       // Número de jogadores selecionados (padrão 1)

    public void Start()
    {
        // Configuração inicial: Mostrar menu inicial com apenas o campo do Jogador 1
        MenuPrincipal.SetActive(true);
        NomeJogador.SetActive(false);
    }

    public void Player1()
    {
      

        // Configuração inicial: Mostrar menu inicial com apenas o campo do Jogador 1
        MenuPrincipal.SetActive(false);
        NomeJogador.SetActive(true);
        inputNomeJogador2.SetActive(false);
        TextAreaJogador2.enabled = false;
        Voltar.interactable = true;
        Jogar.interactable = true;
        numJogadores = 1;
        TextAreaJogador1.ClearMesh();
        TextAreaJogador2.ClearMesh();
    }

    public void Player2()
    {
        // Configuração inicial: Mostrar menu inicial com apenas o campo do Jogador 1
        MenuPrincipal.SetActive(false);
        NomeJogador.SetActive(true);
        TextAreaJogador2.enabled = true;
        inputNomeJogador2.SetActive(true);
        Voltar.interactable = true;
        Jogar.interactable = true;
        numJogadores = 2;
        TextAreaJogador1.ClearMesh();
        TextAreaJogador2.ClearMesh(); 
    }

    public void VoltarMenu()
    {
        // Configuração inicial: Mostrar menu inicial com apenas o campo do Jogador 1
        MenuPrincipal.SetActive(true);
        NomeJogador.SetActive(false);
    }

    public void SelecionarNumeroDeJogadores(int numero)
    {
        numJogadores = numero;

        // Ativar ou desativar os campos de nome com base na escolha
        if (numJogadores == 1)
        {
            inputNomeJogador1.SetActive(true);
            inputNomeJogador2.SetActive(false);
        }
        else if (numJogadores == 2)
        {
            inputNomeJogador1.SetActive(true);
            inputNomeJogador2.SetActive(true);
        }
    }

    public void StartGame()
    {
        // Capturar os nomes dos jogadores
        string nomeJogador1 = inputNomeJogador1.GetComponent<TMP_InputField>().text;
        string nomeJogador2 = inputNomeJogador2.GetComponent<TMP_InputField>().text;

        // Salvar os nomes para uso na próxima cena
        PlayerPrefs.SetString("NomeJogador1", nomeJogador1);

        if (numJogadores == 2)
        {
            PlayerPrefs.SetString("NomeJogador2", nomeJogador2);
        }

        // Garantir que os dados sejam salvos antes da troca de cena
        PlayerPrefs.Save();

        // Carregar a cena correta com base no número de jogadores
        if (numJogadores == 1)
        {
            SceneManager.LoadSceneAsync("SampleScene"); // Cena para 1 jogador
        }
        else if (numJogadores == 2)
        {
            SceneManager.LoadSceneAsync("SampleScene"); // Cena para 2 jogadores
        }
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
