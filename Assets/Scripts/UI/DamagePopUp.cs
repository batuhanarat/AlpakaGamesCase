using Game.Managers;
using TMPro;
using UnityEngine;


public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp Create(Transform transform, float damageAmount, bool isCritical)
    {
        var damagePopUp = ServiceProvider.AssetLib.GetAsset<DamagePopUp>(AssetType.DAMAGEPOPUP);

        Quaternion originalRotation = damagePopUp.transform.rotation;
        damagePopUp.transform.SetParent(transform, false);
        damagePopUp.transform.rotation = originalRotation;
        damagePopUp.Setup(damageAmount,isCritical);

        return damagePopUp;
    }
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private static int _sortingOrder;
    private TextMeshPro _textMesh;
    private float _disappearCountDownInSeconds;
    private Color _textColor;
    private Vector3 _moveVector;

    void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    private void Setup(float damageAmount, bool isCriticalHit)
    {
        _textMesh.SetText(damageAmount.ToString());
        if(isCriticalHit) {
            _textMesh.fontSize = 3.5f;
            _textColor = Utilities.GetColorFromString("DE1920");
        } else {
            _textMesh.fontSize = 2.5f;
            _textColor = Utilities.GetColorFromString("EEAD0F");
        }
        _textMesh.color = _textColor;
        _disappearCountDownInSeconds = DISAPPEAR_TIMER_MAX;
        _moveVector = new Vector3(1,1);
        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;
    }

    private void Update()
    {
        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= _moveVector * 0.2f * Time.deltaTime;

        if(_disappearCountDownInSeconds > DISAPPEAR_TIMER_MAX * 0.5f){
            float increaseScale = 1f;
            transform.localScale += Vector3.one * increaseScale * Time.deltaTime;
        }  else {
            float decreaseScale = 1f;
            transform.localScale -= Vector3.one * decreaseScale * Time.deltaTime;
        }
        _disappearCountDownInSeconds -= Time.deltaTime;
        if(_disappearCountDownInSeconds < 0) {
            float disappearSpeed = 3f;
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            if(_textColor.a <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
