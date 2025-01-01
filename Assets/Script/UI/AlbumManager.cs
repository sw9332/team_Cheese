using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumManager : MonoBehaviour
{
    public GameObject Album;
    public GameObject InGameUI;

    private PlayerControl playerControl;
    private InventoryManager inventoryManager;
    private DialogueManager dialogueManager;

    [SerializeField] List<GameObject> imageObjects = new(); // Image ���� GameObject ����Ʈ
    [SerializeField] List<Image> albumImages = new(); // Image ������Ʈ ����Ʈ
    [SerializeField] Image presentImage;

    void AlbumUI_Open_Close()
    {
        if (Input.GetKeyDown(KeyCode.E) && inventoryManager.Camera && !Album.activeSelf && !dialogueManager.dialogue_continue)
        {
            Album.SetActive(true);
            playerControl.isMove = false;
            InGameUI.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.E) && inventoryManager.Camera && Album.activeSelf)
        {
            Album.SetActive(false);
            playerControl.isMove = true;
            InGameUI.SetActive(true);
        }
    }

    // It functions as named 
    GameObject FindInactiveObjectByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects) if (obj.name == name) return obj;
        return null;
    }

    // �ڵ����� image ���� components���� ����Ʈ�� �߰�
    void addImageInformationInLists()
    {
        // Set parent object as "images" in Canvas -> CameraAlbumUI -> images
        GameObject imagesParent = FindInactiveObjectByName("images");

        if (imagesParent != null)
        {
            Transform[] childrenTransforms = imagesParent.GetComponentsInChildren<Transform>(true);

            foreach (Transform child in childrenTransforms)
                if (child != imagesParent.transform)
                {
                    imageObjects.Add(child.gameObject);

                    Image img = child.GetComponent<Image>();
                    if (img != null) albumImages.Add(img);
                }
        }
    }

    // �̹��� ������Ʈ�� �׿� �ش��ϴ� Image ������Ʈ�� Ʃ�÷� ��ȯ
    (GameObject, Image) getImageInformation(string imageName)
    {
        int objIndex = imageObjects.FindIndex(x => x.name.Equals(imageName));
        GameObject obj = imageObjects[objIndex];

        int imgIndex = albumImages.FindIndex(x => x.name.Equals(imageName));
        Image img = albumImages[imgIndex];

        return (obj, img);
    }

    // �̹����� �ı��ϴ� �Լ�
    public void destroyImage(string imageName)
    {
        (GameObject objImage, Image img) = getImageInformation(imageName);

        if (objImage != null && img != null)
        {
            deleteImageInLists(objImage, img);
            Destroy(objImage); // ���� GameObject ����

            // 0��° �̹����� ������ ��, ���� 0��°�� �� �̹��� Ȱ��ȭ
            if (imageObjects.Count > 0)
            {
                ActivateImageAt(0); // ���� 0��°�� �� �̹��� Ȱ��ȭ
                presentImage = albumImages[0]; // ���� 0��°�� �� �̹����� presentImage�� ����
            }

            else presentImage = null; // �̹����� ���ٸ� presentImage�� null�� ����
        }
    }

    // ����Ʈ���� �̹��� ���� ����
    void deleteImageInLists(GameObject objImage, Image img)
    {
        imageObjects.Remove(objImage); // �̹��� ������Ʈ ����Ʈ���� ����
        albumImages.Remove(img); // �̹��� ����Ʈ���� ����
    }

    // Ư�� �ε����� �̹����� Ȱ��ȭ�ϰ� �������� ��Ȱ��ȭ�ϴ� �Լ�
    void ActivateImageAt(int index)
    {
        for (int i = 0; i < imageObjects.Count; i++)
            if (i == index) imageObjects[i].SetActive(true);  // �ش� �ε����� ������Ʈ Ȱ��ȭ
            else imageObjects[i].SetActive(false); // ������ ��Ȱ��ȭ
    }

    // null reference ����
    void removeNullImagesFromLists()
    {
        for (int i = imageObjects.Count - 1; i >= 0; i--)
            if (imageObjects[i] == null)
            {
                imageObjects.RemoveAt(i);
                albumImages.RemoveAt(i);
            }
    }

    // ���� Ȱ��ȭ�� �̹��� ����
    public void switchImage(string imageName)
    {
        (GameObject objImage, Image img) = getImageInformation(imageName);
        if (img != null) presentImage = img; // ���� ǥ�õǴ� �̹����� ����
    }


    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();

        addImageInformationInLists(); // �̹��� ���� ����Ʈ�� �ڵ� �߰�

        if (imageObjects.Count > 0)
        {
            ActivateImageAt(0); // ó���� 0��° ������Ʈ�� Ȱ��ȭ
            presentImage = albumImages[0]; // �⺻���� ù ��° �̹��� ����
        }
    }

    void Update()
    {
        AlbumUI_Open_Close();
        removeNullImagesFromLists(); // �� �����Ӹ��� null �̹��� üũ �� ����

        // MiniGame.isChange�� true�� �� �̹��� ���� �� ���� �ʱ�ȭ
        if (MiniGame.isImageChange && imageObjects.Count > 0)
        {
            destroyImage(imageObjects[0].name); // 0��° �̹��� ����
            MiniGame.isImageChange = false; // ���¸� false�� ��ȯ
        }
    }
}