using System;
using UdonSharp;
using UnityEngine;

namespace Name
{
    public class Clock : UdonSharpBehaviour
    {
        /// <summary>
        /// 7SegDisp�I�u�W�F�N�g�z��i6�������j
        /// </summary>
        private GameObject[] _7segDisps;

        /// <summary>
        /// MeshRenderer�W���O�z��i6���� * 7�Z�O�����g���j
        /// </summary>
        private MeshRenderer[][] _meshRenderers;

        /// <summary>
        /// �����̃Z�O�����g��`�i0~9�j
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
        /// �t�B�[���h�̏�����
        /// </summary>
        void Start()
        {
            _7segDisps = new GameObject[]
            {
                // ���i�\�̈ʁj
                this.transform.Find("7SegDisp1").gameObject,
                // ���i��̈ʁj
                this.transform.Find("7SegDisp2").gameObject,
                // ���i�\�̈ʁj
                this.transform.Find("7SegDisp3").gameObject,
                // ���i��̈ʁj
                this.transform.Find("7SegDisp4").gameObject,
                // �b�i�\�̈ʁj
                this.transform.Find("7SegDisp5").gameObject,
                // �b�i��̈ʁj
                this.transform.Find("7SegDisp6").gameObject
            };

            _meshRenderers = new MeshRenderer[6][];

            for (int i = 0; i < _7segDisps.Length; i++)
            {
                var transform = _7segDisps[i].transform;
                _meshRenderers[i] = new MeshRenderer[7]
                {
                    // �Z�O�����g1
                    transform.Find("Segment1").GetComponent<MeshRenderer>(),
                    // �Z�O�����g2
                    transform.Find("Segment2").GetComponent<MeshRenderer>(),
                    // �Z�O�����g3
                    transform.Find("Segment3").GetComponent<MeshRenderer>(),
                    // �Z�O�����g4
                    transform.Find("Segment4").GetComponent<MeshRenderer>(),
                    // �Z�O�����g5
                    transform.Find("Segment5").GetComponent<MeshRenderer>(),
                    // �Z�O�����g6
                    transform.Find("Segment6").GetComponent<MeshRenderer>(),
                    // �Z�O�����g7
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
        /// �����̍X�V����
        /// </summary>
        void Update()
        {
            // ���ݎ������擾
            var now = DateTime.Now;

            // ���A���A�b�ɕ���
            var numbers = new int[]
            {
                now.Hour / 10,
                now.Hour % 10,
                now.Minute / 10,
                now.Minute % 10,
                now.Second / 10,
                now.Second % 10
            };

            // 6���������[�v
            for (int i = 0; i < _7segDisps.Length; i++)
            {
                var meshRenderers = _meshRenderers[i];
                var number = numbers[i];

                var numberCode = _numberCodes[number];

                // �Z�O�����g�����[�v
                for (int j = 0; j < meshRenderers.Length; j++)
                {
                    var meshRender = meshRenderers[j];
                    meshRender.material = numberCode[j] ? _activeMaterial : _inactiveMaterial;
                }
            };
        }
    }
}


