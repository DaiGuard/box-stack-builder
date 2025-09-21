using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;

public class BoxSpawnManager : MonoBehaviour
{
    public static BoxSpawnManager instance { get; private set; }

    private Vector3 boxSize = new Vector3(0.503f, 0.335f, 0.183f);
    private float shellThickness = 0.0175f;
    private float stackOverlap = 0.012f;

    [SerializeField] private Transform parentTransform;
    [SerializeField] TMP_InputField boxSizeWInput;
    [SerializeField] TMP_InputField boxSizeLInput;
    [SerializeField] TMP_InputField boxSizeHInput;
    [SerializeField] TMP_InputField shellThicknessInput;
    [SerializeField] TMP_InputField stackOverlapInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBox()
    {
        boxSize.x = float.Parse(boxSizeWInput.text) * 0.001f;
        boxSize.y = float.Parse(boxSizeLInput.text) * 0.001f;
        boxSize.z = float.Parse(boxSizeHInput.text) * 0.001f;
        shellThickness = float.Parse(shellThicknessInput.text) * 0.001f;
        stackOverlap = float.Parse(stackOverlapInput.text) * 0.001f;

        var boxObject = CreateBoxObject(boxSize, shellThickness, stackOverlap);
        boxObject.transform.SetParent(parentTransform, false);
    }

    public GameObject CreateBoxObject(Vector3 boxSize, float shellThickness, float stackOverlap)
    {
        var boxObject = new GameObject("Box");
        var meshFilter = boxObject.AddComponent<MeshFilter>();
        var meshRenderer = boxObject.AddComponent<MeshRenderer>();
        var meshCollider = boxObject.AddComponent<MeshCollider>();

        meshFilter.mesh = CreateBoxMesh(boxSize, shellThickness, stackOverlap);
        meshCollider.sharedMesh = meshFilter.mesh;
        meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        meshRenderer.material.color = new Color(0.5f, 0.5f, 1.0f, 1.0f);

        boxObject.transform.position = Vector3.zero;
        boxObject.transform.rotation = Quaternion.identity;

        return boxObject;
    }
    
    private Mesh CreateBoxMesh(Vector3 boxSize, float shellThickness, float stackOverlap)
    {
        var mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            // 外側頂点
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(boxSize.x, 0.0f, 0.0f),
            new Vector3(boxSize.x, boxSize.y, 0.0f),
            new Vector3(0.0f, boxSize.y, 0.0f),

            new Vector3(0.0f, 0.0f, boxSize.z),
            new Vector3(boxSize.x, 0.0f, boxSize.z),
            new Vector3(boxSize.x, boxSize.y, boxSize.z),
            new Vector3(0.0f, boxSize.y, boxSize.z),

            // 内側頂点
            new Vector3(shellThickness, shellThickness, shellThickness),
            new Vector3(boxSize.x-shellThickness, shellThickness, shellThickness),
            new Vector3(boxSize.x-shellThickness, boxSize.y-shellThickness, shellThickness),
            new Vector3(shellThickness, boxSize.y-shellThickness, shellThickness),

            new Vector3(shellThickness, shellThickness, boxSize.z),
            new Vector3(boxSize.x-shellThickness, shellThickness, boxSize.z),
            new Vector3(boxSize.x-shellThickness, boxSize.y-shellThickness, boxSize.z),
            new Vector3(shellThickness, boxSize.y-shellThickness, boxSize.z),

            // 重なり頂点
            new Vector3(shellThickness, shellThickness, 0.0f),
            new Vector3(boxSize.x-shellThickness, shellThickness, 0.0f),
            new Vector3(boxSize.x-shellThickness, boxSize.y-shellThickness, 0.0f),
            new Vector3(shellThickness, boxSize.y-shellThickness, 0.0f),

            new Vector3(shellThickness, shellThickness, -stackOverlap),
            new Vector3(boxSize.x-shellThickness, shellThickness, -stackOverlap),
            new Vector3(boxSize.x-shellThickness, boxSize.y-shellThickness, -stackOverlap),
            new Vector3(shellThickness, boxSize.y-shellThickness, -stackOverlap),
        };

        // 三角形の定義
        int[] triangles = new int[]
        {
            //　外側
            0, 1, 5,  0, 5, 4, // Front
            1, 2, 6,  1, 6, 5, // Right
            2, 3, 7,  2, 7, 6, // Back
            3, 0, 4,  3, 4, 7, // Left

            // 内側
            8, 13, 9,     8, 12, 13, // Front
            9, 14, 10,    9, 13, 14, // Right
            10, 15, 11,  10, 14, 15, // Back 
            11, 12, 8,   11, 15, 12, // Left

            // 上面
            4, 5, 13,  4, 13, 12,
            5, 6, 14,  5, 14, 13,
            6, 7, 15,  6, 15, 14,
            7, 4, 12,  7, 12, 15,

            // 下面
            8, 9, 10,  8, 10, 11,

            // 底面
            0, 17, 1, 0, 16, 17,
            1, 18, 2, 1, 17, 18,
            2, 19, 3, 2, 18, 19,
            3, 16, 0, 3, 19, 16,

            // 重なり部
            16, 21, 17, 16, 20, 21,
            17, 22, 18, 17, 21, 22,
            18, 23, 19, 18, 22, 23,
            19, 20, 16, 19, 23, 20,

            // 最低面
            20, 22, 21, 20, 23, 22
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
