using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("배경음")]
    [SerializeField] private AudioSource startBGM;

    [Header("플레이어 관련")]
    [SerializeField] private AudioSource playerWalkSound;
    [SerializeField] private AudioSource playerRunSound;
    [SerializeField] private AudioSource playerJumpSound;
    [SerializeField] private AudioSource playerDashSound;
    [SerializeField] private AudioSource playerHitSound;

    [Header("라운드 관련")]
    [SerializeField] private AudioSource roundStartSound;
    [SerializeField] private AudioSource coinSound;
    [SerializeField] private AudioSource buyItemSound;
    [SerializeField] private AudioSource inventorySound;

    [Header("버튼")]
    [SerializeField] private AudioSource buttonClickSound;

    [Header("도감")]
    [SerializeField] private AudioSource bookOpenSound;



    //이전 BGM 위치 저장
    private float previousTime = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetStartBGM()
    {
        PlayBGM(startBGM);
    }

    public void PlayBGM(AudioSource bgm)
    {
        bgm.loop = true;
        bgm.Play();
        previousTime = 0f;
    }

    public void StopBGM()
    {
        previousTime = startBGM.time;
        startBGM.Stop();
    }

    /// <summary>
    /// 플레이어 걷는 소리
    /// </summary>
    public void PlayerWalkSound()
    {
        playerWalkSound.PlayOneShot(playerWalkSound.clip);
    }

    /// <summary>
    /// 플레이어 달리는 소리
    /// </summary>
    public void PlayerRunnSound()
    {
        playerRunSound.PlayOneShot(playerRunSound.clip);
    }

    /// <summary>
    /// 플레이어 점프 소리
    /// </summary>
    public void PlayerJumpSound()
    {
        playerJumpSound.PlayOneShot(playerJumpSound.clip);
    }

    /// <summary>
    /// 플레이어 대쉬 소리
    /// </summary>
    public void PlayerDashSound()
    {
        playerDashSound.PlayOneShot(playerDashSound.clip);
    }

    /// <summary>
    /// 플레이어 피격 소리
    /// </summary>
    public void PlayerHitSound()
    {
        playerHitSound.PlayOneShot(playerHitSound.clip);
    }

    /// <summary>
    /// 라운드 시작 소리
    /// </summary>
    public void RoundStartSound()
    {
        roundStartSound.PlayOneShot(roundStartSound.clip);
    }

    /// <summary>
    /// 코인 획득 소리
    /// </summary>
    public void CoinSound()
    {
        coinSound.PlayOneShot(coinSound.clip);
    }

    /// <summary>
    /// 아이템 구매 소리 - 상점에서 이용
    /// </summary>
    public void BuyItemSound()
    {
        buyItemSound.PlayOneShot(buyItemSound.clip);
    }

    /// <summary>
    /// 인벤토리 열고 닫는 소리
    /// </summary>
    public void InventorySound()
    {
        inventorySound.PlayOneShot(inventorySound.clip);
    }

    /// <summary>
    /// 버튼 클릭 소리
    /// </summary>
    public void ButtonClickSound()
    {
        buttonClickSound.PlayOneShot(buttonClickSound.clip);
    }

    /// <summary>
    /// 도감 펼치는 소리
    /// </summary>
    public void BookOpenSound()
    {
        bookOpenSound.PlayOneShot(bookOpenSound.clip);
    }
}
