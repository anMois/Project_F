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
    [SerializeField] private AudioSource stageClearSound;
    [SerializeField] private AudioSource storeOpenSound;
    [SerializeField] private AudioSource teleportSound;
    [SerializeField] private AudioSource clearBoxSound;

    [Header("버튼")]
    [SerializeField] private AudioSource buttonClickSound;

    [Header("도감")]
    [SerializeField] private AudioSource bookOpenSound;

    [Header("보스 스테이지")]
    [SerializeField] private AudioSource bossAppearSound;
    [SerializeField] private AudioSource bossDieSound;
    [SerializeField] private AudioSource bossAttackSound1;
    [SerializeField] private AudioSource bossAttackSound2;
    [SerializeField] private AudioSource bossAttackSound3;
    [SerializeField] private AudioSource bossLandPatternSound1;
    [SerializeField] private AudioSource bossLandPatternSound2;


    [Header("일반, 엘리트 스테이지")]
    [Header("고블린")]
    [SerializeField] private AudioSource goblinDieSound;
    [SerializeField] private AudioSource goblinChaseSound;
    [SerializeField] private AudioSource goblinHitSound;
    [SerializeField] private AudioSource goblinAttackSound;
    [SerializeField] private AudioSource xiamenGoblinAttackSound;

    [Header("골렘")]
    [SerializeField] private AudioSource golemDieSound;
    [SerializeField] private AudioSource golemHitSound;
    [SerializeField] private AudioSource golemAttackSound;

    [Header("눈알몬스터")]
    [SerializeField] private AudioSource eyeMonsterAttackSound;
    [SerializeField] private AudioSource eyeMonsterDieSound;
    [SerializeField] private AudioSource eyeMonsterHitSound;

    [Header("개구리몬스터")]
    [SerializeField] private AudioSource frogAttackSound;
    [SerializeField] private AudioSource frogDieSound;
    [SerializeField] private AudioSource frogHitSound;

    [Header("뱀 인간")]
    [SerializeField] private AudioSource snakeHumanAttackSound;
    [SerializeField] private AudioSource snakeHumanDieSound;
    [SerializeField] private AudioSource snakeHumanHitSound;




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
    /// 스테이지 클리어 소리
    /// </summary>
    public void StageClearSound()
    {
        stageClearSound.PlayOneShot(stageClearSound.clip);
    }

    /// <summary>
    /// 상점 열고 닫는 소리
    /// </summary>
    public void StoreOpenSound()
    {
        storeOpenSound.PlayOneShot(storeOpenSound.clip);
    }

    /// <summary>
    /// 포탈 생성 소리
    /// </summary>
    public void TeleportSound()
    {
        teleportSound.PlayOneShot(teleportSound.clip);
    }

    /// <summary>
    /// 클리어 박스 생성 소리
    /// </summary>
    public void ClearBoxSound()
    {
        clearBoxSound.PlayOneShot(clearBoxSound.clip);
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

    /// <summary>
    /// 보스 첫 등장 시 나오는 소리
    /// </summary>
    public void BossAppearSound()
    {
        bossAppearSound.PlayOneShot(bossAppearSound.clip);
    }

    /// <summary>
    /// 보스 죽을 때 나오는 소리
    /// </summary>
    public void BossDieSound()
    {
        bossDieSound.PlayOneShot(bossDieSound.clip);
    }

    /// <summary>
    /// 보스 일반 공격 1
    /// </summary>
    public void BossAttackSound1()
    {
        bossAttackSound1.PlayOneShot(bossAttackSound1.clip);
    }

    /// <summary> 
    /// 보스 일반 공격 2
    /// </summary>

    public void BossAttackSound2()
    {
        bossAttackSound2.PlayOneShot(bossAttackSound2.clip);
    }

    /// <summary>
    /// 보스 일반 공격 3
    /// </summary>
    public void BossAttackSound3()
    {
        bossAttackSound3.PlayOneShot(bossAttackSound3.clip);
    }

    /// <summary>
    /// 보스 특수 공격 1
    /// </summary>
    public void BossLandPatternSound1()
    {
        bossLandPatternSound1.PlayOneShot(bossLandPatternSound1.clip);
    }

    /// <summary>
    /// 보스 특수 공격 2,3
    /// </summary>
    public void BossLandPatternSound2()
    {
        bossLandPatternSound2.PlayOneShot(bossLandPatternSound2.clip);
    }

    /// <summary>
    /// 고블린 죽는 소리
    /// </summary>
    public void GoblinDieSound()
    {
        goblinDieSound.PlayOneShot(goblinDieSound.clip);
    }

    /// <summary>
    /// 고블린 추격 소리
    /// </summary>
    public void GoblinChaseSound()
    {
        goblinChaseSound.PlayOneShot(goblinChaseSound.clip);
    }

    /// <summary>
    /// 고블린 피격 소리
    /// </summary>
    public void GoblinHitSound()
    {
        goblinHitSound.PlayOneShot(goblinHitSound.clip);
    }

    /// <summary>
    /// 고블린 공격 소리
    /// </summary>
    public void GoblinAttackSound()
    {
        goblinAttackSound.PlayOneShot(goblinAttackSound.clip);
    }

    /// <summary>
    /// 샤먼 고블린 공격 소리
    /// </summary>
    public void XiamenGoblinAttackSound()
    {
        xiamenGoblinAttackSound.PlayOneShot(xiamenGoblinAttackSound.clip);
    }

    /// <summary>
    /// 골렘 죽는 소리
    /// </summary>
    public void GolemDieSound()
    {
        golemDieSound.PlayOneShot(golemDieSound.clip);
    }

    /// <summary>
    /// 골렘 피격 소리
    /// </summary>
    public void GolemHitSound()
    {
        golemHitSound.PlayOneShot(golemHitSound.clip);
    }

    /// <summary>
    /// 골렘 공격 소리
    /// </summary>
    public void GolemAttackSound()
    {
        golemAttackSound.PlayOneShot(golemAttackSound.clip);
    }

    /// <summary>
    /// 눈알 몬스터 공격 소리
    /// </summary>
    public void EyeMonsterAttackSound()
    {
        eyeMonsterAttackSound.PlayOneShot(eyeMonsterAttackSound.clip);
    }

    /// <summary>
    /// 눈알 몬스터 죽는 소리
    /// </summary>
    public void EyeMonsterDieSound()
    {
        eyeMonsterDieSound.PlayOneShot(eyeMonsterDieSound.clip);
    }

    /// <summary>
    /// 눈알 몬스터 피격 소리
    /// </summary>
    public void EyeMonsterHitSound()
    {
        eyeMonsterHitSound.PlayOneShot(eyeMonsterHitSound.clip);
    }

    /// <summary>
    /// 개구리형 몬스터 공격 소리
    /// </summary>
    public void FrogAttackSound()
    {
        frogAttackSound.PlayOneShot(frogAttackSound.clip);
    }

    /// <summary>
    /// 개구리형 몬스터 죽는 소리
    /// </summary>
    public void FrogDieSound()
    {
        frogDieSound.PlayOneShot(frogDieSound.clip);
    }

    /// <summary>
    /// 개구리형 몬스터 피격 소리
    /// </summary>
    public void FrogHitSound()
    {
        frogHitSound.PlayOneShot(frogHitSound.clip);
    }

    /// <summary>
    /// 뱀인간 공격 소리
    /// </summary>
    public void SnakeHumanAttackSound()
    {
        snakeHumanAttackSound.PlayOneShot(snakeHumanAttackSound.clip);
    }

    /// <summary>
    /// 뱀인간 죽는 소리
    /// </summary>
    public void SnakeHumanDieSound()
    {
        snakeHumanDieSound.PlayOneShot(snakeHumanDieSound.clip);
    }

    /// <summary>
    /// 벰인간 피격 소리
    /// </summary>
    public void SnakeHumanHitSound()
    {
        snakeHumanHitSound.PlayOneShot(snakeHumanHitSound.clip);
    }
}
