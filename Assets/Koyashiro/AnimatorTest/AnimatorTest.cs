using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

public class AnimatorTest : UdonSharpBehaviour
{
    /// <summary>
    /// 制御対象のParameter名
    /// </summary>
    private readonly string _parameterName = "Parameter1";

    /// <summary>
    /// 制御対象のAnimator
    /// </summary>
    private Animator _animator;

    void Start()
    {
        // StartメソッドでAnimatorを取得しておく
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// ピックアップしたときの処理
    /// </summary>
    public override void OnPickup()
    {
        // OwnerでなければOwnerを取得
        if (!Networking.IsOwner(Networking.LocalPlayer, gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
    }

    /// <summary>
    /// ピックアップ状態でトリガーを引いたときの処理
    /// </summary>
    public override void OnPickupUseDown()
    {
        switch (_animator.GetInteger(_parameterName))
        {
            case 0:
                // 0 → 1
                SendCustomNetworkEvent(NetworkEventTarget.All, "SetParameterTo1");
                break;
            case 1:
                // 1 → 2
                SendCustomNetworkEvent(NetworkEventTarget.All, "SetParameterTo2");
                break;
            case 2:
                // 2 → 3
                SendCustomNetworkEvent(NetworkEventTarget.All, "SetParameterTo3");
                break;
            case 3:
                // 3 → 4
                SendCustomNetworkEvent(NetworkEventTarget.All, "SetParameterTo4");
                break;
            case 4:
                // 4 → 5
                SendCustomNetworkEvent(NetworkEventTarget.All, "SetParameterTo5");
                break;
            case 5:
                // 5 → 0
                SendCustomNetworkEvent(NetworkEventTarget.All, "SetParameterTo0");
                break;
        }
    }

    #region Parameter Set Methods
    /// <summary>
    /// Parametorを0に更新するメソッド
    /// </summary>
    private void SetParameterTo0()
    {
        _animator.SetInteger(_parameterName, 0);
    }

    /// <summary>
    /// Parametorを1に更新するメソッド
    /// </summary>
    private void SetParameterTo1()
    {
        _animator.SetInteger(_parameterName, 1);
    }

    /// <summary>
    /// Parametorを2に更新するメソッド
    /// </summary>
    private void SetParameterTo2()
    {
        _animator.SetInteger(_parameterName, 2);
    }

    /// <summary>
    /// Parametorを3に更新するメソッド
    /// </summary>
    private void SetParameterTo3()
    {
        _animator.SetInteger(_parameterName, 3);
    }

    /// <summary>
    /// Parametorを4に更新するメソッド
    /// </summary>
    private void SetParameterTo4()
    {
        _animator.SetInteger(_parameterName, 4);
    }

    /// <summary>
    /// Parametorを5に更新するメソッド
    /// </summary>
    private void SetParameterTo5()
    {
        _animator.SetInteger(_parameterName, 5);
    }
    #endregion
}
