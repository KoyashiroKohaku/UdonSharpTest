using System;
using UdonSharp;
using UnityEngine;

namespace Name
{
    public class Clock : UdonSharpBehaviour
    {
        /// <summary>
        /// 7SegDispオブジェクト配列（6文字分）
        /// </summary>
        private GameObject[] _7segDisps;

        /// <summary>
        /// MeshRendererジャグ配列（6文字 * 7セグメント分）
        /// </summary>
        private MeshRenderer[][] _meshRenderers;

        /// <summary>
        /// 数字のセグメント定義（0~9）
        /// </summary>
        private bool[][] _numberCodes;

        /// <summary>
        /// Active Material
        /// </summary>
        [SerializeField]
        private readonly Material _activeMaterial;

        /// <summary>
        /// Inactive Material
        /// </summary>
        [SerializeField]
        private readonly Material _inactiveMaterial;

        /// <summary>
        /// フィールドの初期化
        /// </summary>
        void Start()
        {
            _7segDisps = new GameObject[]
            {
                // 時（十の位）
                this.transform.Find("7SegDisp1").gameObject,
                // 時（一の位）
                this.transform.Find("7SegDisp2").gameObject,
                // 分（十の位）
                this.transform.Find("7SegDisp3").gameObject,
                // 分（一の位）
                this.transform.Find("7SegDisp4").gameObject,
                // 秒（十の位）
                this.transform.Find("7SegDisp5").gameObject,
                // 秒（一の位）
                this.transform.Find("7SegDisp6").gameObject
            };

            _meshRenderers = new MeshRenderer[6][];

            for (int i = 0; i < _7segDisps.Length; i++)
            {
                var transform = _7segDisps[i].transform;
                _meshRenderers[i] = new MeshRenderer[7]
                {
                    // セグメント1
                    transform.Find("Segment1").GetComponent<MeshRenderer>(),
                    // セグメント2
                    transform.Find("Segment2").GetComponent<MeshRenderer>(),
                    // セグメント3
                    transform.Find("Segment3").GetComponent<MeshRenderer>(),
                    // セグメント4
                    transform.Find("Segment4").GetComponent<MeshRenderer>(),
                    // セグメント5
                    transform.Find("Segment5").GetComponent<MeshRenderer>(),
                    // セグメント6
                    transform.Find("Segment6").GetComponent<MeshRenderer>(),
                    // セグメント7
                    transform.Find("Segment7").GetComponent<MeshRenderer>()
                };
            }

            _numberCodes = new bool[10][]
            {
                // 0
                new bool[7] { true, true, true, true, true, true, false },
                // 1
                new bool[7] { false, true, true, false, false, false, false },
                // 2
                new bool[7] { true, true, false, true, true, false, true },
                // 3
                new bool[7] { true, true, true, true, false, false, true },
                // 4
                new bool[7] { false, true, true, false, false, true, true },
                // 5
                new bool[7] { true, false, true, true, false, true, true },
                // 6
                new bool[7] { true, false, true, true, true, true, true },
                // 7
                new bool[7] { true, true, true, false, false, false, false },
                // 8
                new bool[7] { true, true, true, true, true, true, true },
                // 9
                new bool[7] { true, true, true, true, false, true, true }
            };
        }

        /// <summary>
        /// 時刻の更新処理
        /// </summary>
        void Update()
        {
            // 現在時刻を取得
            var now = DateTime.Now;

            // 時、分、秒に分離
            var numbers = new int[]
            {
                now.Hour / 10,
                now.Hour % 10,
                now.Minute / 10,
                now.Minute % 10,
                now.Second / 10,
                now.Second % 10
            };

            // 6文字分ループ
            for (int i = 0; i < _7segDisps.Length; i++)
            {
                var meshRenderers = _meshRenderers[i];
                var number = numbers[i];

                var numberCode = _numberCodes[number];

                // セグメント分ループ
                for (int j = 0; j < meshRenderers.Length; j++)
                {
                    var meshRender = meshRenderers[j];
                    meshRender.material = numberCode[j] ? _activeMaterial : _inactiveMaterial;
                }
            };
        }
    }
}


