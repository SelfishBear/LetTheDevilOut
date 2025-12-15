using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public enum GameRound
    {
        Round1,
        Round2,
        Round3,
        End
    }

    public class DanceMiniGame : MonoBehaviour
    {
        [Header("Rounds Settings")] [SerializeField]
        private int _round1Duration = 40;

        [SerializeField] private int _round2Duration = 50;
        [SerializeField] private int _round3Duration = 60;

        [Header("Audio Settings")]
        [SerializeField] private AudioSource _audioSource;

        [Header("UI Settings")] 
        [SerializeField] private TextMeshProUGUI _currentRoundText;
        [SerializeField] private TextMeshProUGUI _roundTimerText;
        [SerializeField] private TextMeshProUGUI _countDownText;
        [SerializeField] private Slider _progressSlider;

        [Header("Gameplay Settings")]
        [SerializeField] private int _countdownFrom = 5;
        [SerializeField] private float _clickAmountUp = 0.1f;
        [SerializeField] private float _killerPushAmount = 0.05f;
        [SerializeField] private float _killerPushRate = 0.5f;

        private GameRound _gameRound;
        private bool _isReady;
        private float _killerPushTimer;
        private IInputService _inputService;
        private InputSystem_Actions _inputActions;
        private int _roundsWon;

        private void Start()
        {
            // _inputService = AllServices.Container.Single<IInputService>();
            // _inputActions = _inputService.GetPlayerInputActions();
            
            if (_progressSlider != null)
            {
                _progressSlider.minValue = 0f;
                _progressSlider.maxValue = 1f;
                _progressSlider.value = 0.5f;
            }
            
            PlayMusicForCurrentRound();
            StartCountdown();
        }

        private void Update()
        {
            if (!_isReady) return;

            HandlePlayerInput();
            HandleKillerPush();
        }

        private void HandlePlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _progressSlider != null)
            {
                _progressSlider.value = Mathf.Clamp01(_progressSlider.value + _clickAmountUp);
            }
        }

        private void HandleKillerPush()
        {
            _killerPushTimer += Time.deltaTime;

            if (_killerPushTimer >= _killerPushRate)
            {
                if (_progressSlider != null)
                {
                    _progressSlider.value = Mathf.Clamp01(_progressSlider.value - _killerPushAmount);
                }
                _killerPushTimer = 0f;
            }
        }
        
        private void SetKillerPushRate(float newRate)
        {
            _killerPushRate = newRate;
        }

        private void StartNewRound(GameRound gameRound)
        {
            _gameRound = gameRound;
            switch (_gameRound)
            {
                case GameRound.Round1:
                    _currentRoundText.text = "Round 1 / 3";
                    SetKillerPushRate(0.05f);
                    StartCoroutine(RoundCountingRoutine(_round1Duration, GameRound.Round2));
                    break;
                case GameRound.Round2:
                    _currentRoundText.text = "Round 2 / 3";
                    SetKillerPushRate(0.03f);
                    StartCoroutine(RoundCountingRoutine(_round2Duration, GameRound.Round3));
                    break;
                case GameRound.Round3:
                    _currentRoundText.text = "Round 3 / 3";
                    SetKillerPushRate(0.027f);
                    StartCoroutine(RoundCountingRoutine(_round3Duration, GameRound.End));
                    break;
                case GameRound.End:
                    CheckWinCondition();
                    break;
            }
        }

        private void CheckWinCondition()
        {
            if (_roundsWon >= 2)
            {
                _countDownText.color = Color.green;
                _countDownText.text = "You Win!";
            }
            else
            {
                _countDownText.color = Color.red;
                _countDownText.text = "You Lose!";
                StartCoroutine(RestartSessionRoutine());
            }
        }

        private IEnumerator RestartSessionRoutine()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private IEnumerator RoundCountingRoutine(int duration, GameRound round)
        {
            for (int i = duration; i >= 0; i--)
            {
                _roundTimerText.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }

            CheckRoundWinCondition();
            StartNewRound(round);
        }

        private void CheckRoundWinCondition()
        {
            if (_progressSlider != null && _progressSlider.value >= 0.5f)
            {
                _roundsWon++;
            }
        }

        private void StartCountdown()
        {
            StartCoroutine(CountdownCoroutine());
        }

        private IEnumerator CountdownCoroutine()
        {
            _countDownText.text = "Get Ready!";
            yield return new WaitForSeconds(1f);

            for (int i = _countdownFrom; i >= 0; i--)
            {
                _countDownText.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }

            _countDownText.text = "GO!";
            _isReady = true;

            yield return new WaitForSeconds(1f);
            _countDownText.text = "";
            StartNewRound(GameRound.Round1);
        }

        private void PlayMusicForCurrentRound()
        {
            _audioSource.Play();
        }
    }
}