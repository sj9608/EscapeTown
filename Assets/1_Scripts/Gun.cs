using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunState
    {
        Ready,      // 발사 준비됨
        Empty,      // 탄창이 빔
        Reloading   // 재장전 중
    }

    // 현재 총의 상태
    public GunState gunState { get; private set; }

    // 탄알이 발사 될 위치
    public Transform fireTransform;
    Ray ray;

    // 탄알 도착 위치
    public Transform raycastDestination;

    //// 총구 화염 효과
    //public ParticleSystem muzzleFlashEffect;
    //// 탄피 배출 효과
    //public ParticleSystem shellEjectEffect;

    // 총알 궤적을 그리기 위한 렌더러
    private LineRenderer bulletLineRenderer;

    // 총 소리 재생기
    private AudioSource gunAudioPlayer;
    // 발사 소리
    public AudioClip shotClip;
    // 재장전 소리
    public AudioClip reloadClip;

    // 공격력
    public float gunDamage = 40;
    // 사정거리
    private float fireDistance = 50f;

    // 남은 전체 탄약
    public int ammoRemain;
    // 탄창 용량
    public int magCapacity = 30;
    // 현재 탄창에 남아있는 탄약
    public int magAmmo;

    // 총알 발사 간격
    private float timeBetFire = 0.12f;
    // 재장전 소요 시간
    private float reloadTime = 1.8f;
    // 총을 마지막으로 발사한 시점
    private float lastFireTime;

    // 남은 탄약을 추가하는 메서드
    public void AddAmmo(int ammo)
    {
        ammoRemain += ammo;

        GameInformation.Instance.UpdateCurAmmo();
    }
    private void Awake()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        // 사용할 점을 두개로 변경
        bulletLineRenderer.positionCount = 2;
        // 라인 렌더러를 비활성화
        bulletLineRenderer.enabled = false;
    }
    private void Start()
    {
        ammoRemain = GameInformation.Instance.RemainAmmo;
        // 현재 탄창을 가득채우기
        magAmmo = GameInformation.Instance.CurAmmo;
        // 총의 현재 상태를 총을 쏠 준비가 된 상태로 변경
        gunState = GunState.Ready;
        // 마지막으로 총을 쏜 시점을 초기화
        lastFireTime = 0;
    }
    // 발사 시도
    public void Fire()
    {
        // 현재 상태가 발사 가능한 상태
        // && 마지막 총 발사 시점에서 timeBetFire 이상의 시간이 지남
        if (gunState == GunState.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            // 마지막 총 발사 시점을 갱신
            lastFireTime = Time.time;
            // 실제 발사 처리 실행
            Shot();
        }
    }
    // 실제 발사 처리
    private void Shot()
    {
        // 레이캐스트에 의한 충돌 정보를 저장하는 컨테이너
        RaycastHit hit;
        // 총알이 맞은 곳을 저장할 변수
        Vector3 hitPosition = Vector3.zero;

        ray.origin = fireTransform.position;
        ray.direction = raycastDestination.position - fireTransform.position;

        // 레이캐스트(시작지점, 방향, 충돌 정보 컨테이너, 사정거리, 레이어마스크)
        if (Physics.Raycast(ray, out hit, fireDistance, LayerMask.GetMask("Enemy")))
        {
            // 레이가 충돌한 위치 저장
            hitPosition = hit.point;
            Zombie zombie = hit.transform.GetComponent<Zombie>();
            // 레이가 충돌한 위치 저장
            hitPosition = hit.point;
            if (zombie)
            {
                zombie.OnDamage(gunDamage, hitPosition);
            }
            // 레이가 어떤 물체와 충돌한 경우
            //GameManager.Instance.Attack(hit.collider, damage);
        }
        else
        {
            // 레이가 다른 물체와 충돌하지 않았다면
            // 총알이 최대 사정거리까지 날아갔을때의 위치를 충돌 위치로 사용
            hitPosition = fireTransform.position +
                          fireTransform.forward * fireDistance;
        }

        // 발사 이펙트 재생 시작
        StartCoroutine(ShotEffect(hitPosition));

        // 남은 탄환의 수를 -1
        magAmmo--;

        GameInformation.Instance.UpdateCurAmmo();

        if (magAmmo <= 0)
        {
            // 탄창에 남은 탄약이 없다면, 총의 현재 상태를 Empty으로 갱신
            gunState = GunState.Empty;
        }
    }
    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        //// 총구 화염 효과 재생
        //muzzleFlashEffect.Play();
        //// 탄피 배출 효과 재생
        //shellEjectEffect.Play();

        // 총격 소리 재생
        gunAudioPlayer.PlayOneShot(shotClip);

        // 선의 시작점은 총구의 위치
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        // 선의 끝점은 입력으로 들어온 충돌 위치
        bulletLineRenderer.SetPosition(1, hitPosition);
        // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }
    // 재장전 시도
    public bool Reload()
    {
        if (gunState == GunState.Reloading|| ammoRemain <= 0 || magAmmo >= magCapacity)
        {
            // 이미 재장전 중이거나, 남은 총알이 없거나
            // 탄창에 총알이 이미 가득한 경우 재장전 할수 없다
            return false;
        }

        // 재장전 처리 시작
        StartCoroutine(ReloadRoutine());
        return true;
    }
    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 상태로 전환
        gunState = GunState.Reloading;
        // 재장전 소리 재생
        gunAudioPlayer.PlayOneShot(reloadClip);

        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(reloadTime);

        // 탄창에 채울 탄약을 계산한다
        int ammoToFill = magCapacity - magAmmo;

        // 탄창에 채워야할 탄약이 남은 탄약보다 많다면,
        // 채워야할 탄약 수를 남은 탄약 수에 맞춰 줄인다
        if (ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        // 탄창을 채운다
        magAmmo += ammoToFill;
        // 남은 탄약에서, 탄창에 채운만큼 탄약을 뺸다
        ammoRemain -= ammoToFill;

        GameInformation.Instance.UpdateCurAmmo();
        // 총의 현재 상태를 발사 준비된 상태로 변경
        gunState = GunState.Ready;
    }
}
