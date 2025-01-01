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

    [SerializeField] List<GameObject> imageObjects = new(); // Image 관련 GameObject 리스트
    [SerializeField] List<Image> albumImages = new(); // Image 컴포넌트 리스트
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

    // 자동으로 image 관련 components들을 리스트에 추가
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

    // 이미지 오브젝트와 그에 해당하는 Image 컴포넌트를 튜플로 반환
    (GameObject, Image) getImageInformation(string imageName)
    {
        int objIndex = imageObjects.FindIndex(x => x.name.Equals(imageName));
        GameObject obj = imageObjects[objIndex];

        int imgIndex = albumImages.FindIndex(x => x.name.Equals(imageName));
        Image img = albumImages[imgIndex];

        return (obj, img);
    }

    // 이미지를 파괴하는 함수
    public void destroyImage(string imageName)
    {
        (GameObject objImage, Image img) = getImageInformation(imageName);

        if (objImage != null && img != null)
        {
            deleteImageInLists(objImage, img);
            Destroy(objImage); // 실제 GameObject 삭제

            // 0번째 이미지가 삭제된 후, 새로 0번째가 된 이미지 활성화
            if (imageObjects.Count > 0)
            {
                ActivateImageAt(0); // 새로 0번째가 된 이미지 활성화
                presentImage = albumImages[0]; // 새로 0번째가 된 이미지를 presentImage로 설정
            }

            else presentImage = null; // 이미지가 없다면 presentImage를 null로 설정
        }
    }

    // 리스트에서 이미지 정보 삭제
    void deleteImageInLists(GameObject objImage, Image img)
    {
        imageObjects.Remove(objImage); // 이미지 오브젝트 리스트에서 제거
        albumImages.Remove(img); // 이미지 리스트에서 제거
    }

    // 특정 인덱스의 이미지를 활성화하고 나머지를 비활성화하는 함수
    void ActivateImageAt(int index)
    {
        for (int i = 0; i < imageObjects.Count; i++)
            if (i == index) imageObjects[i].SetActive(true);  // 해당 인덱스의 오브젝트 활성화
            else imageObjects[i].SetActive(false); // 나머지 비활성화
    }

    // null reference 방지
    void removeNullImagesFromLists()
    {
        for (int i = imageObjects.Count - 1; i >= 0; i--)
            if (imageObjects[i] == null)
            {
                imageObjects.RemoveAt(i);
                albumImages.RemoveAt(i);
            }
    }

    // 현재 활성화된 이미지 설정
    public void switchImage(string imageName)
    {
        (GameObject objImage, Image img) = getImageInformation(imageName);
        if (img != null) presentImage = img; // 현재 표시되는 이미지로 설정
    }


    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();

        addImageInformationInLists(); // 이미지 정보 리스트에 자동 추가

        if (imageObjects.Count > 0)
        {
            ActivateImageAt(0); // 처음에 0번째 오브젝트만 활성화
            presentImage = albumImages[0]; // 기본으로 첫 번째 이미지 설정
        }
    }

    void Update()
    {
        AlbumUI_Open_Close();
        removeNullImagesFromLists(); // 매 프레임마다 null 이미지 체크 및 제거

        // MiniGame.isChange가 true일 때 이미지 삭제 후 상태 초기화
        if (MiniGame.isImageChange && imageObjects.Count > 0)
        {
            destroyImage(imageObjects[0].name); // 0번째 이미지 삭제
            MiniGame.isImageChange = false; // 상태를 false로 전환
        }
    }
}