//--------------------------------------------------------------------------------
/*
 * This Script is used for collecing Data generated/User Input
 * 
 * Used in Start Scene, attached to Empty GameObject "DataCollector"
 * Require 2 InputField variables : User ID, Video ID(1 / 2 / 3 / 4 ...depends on how many videos are there)
 * which can be found under Canvas GameObject in Hierarchy.
 * Among the 2 InputFields, only User ID's GameObject need to be active.
 */
//--------------------------------------------------------------------------------

using System.IO;    // For Directory, File, StreamWriter
using UnityEngine;  // Default Unity Script (MonoBehaviour, Application, GameObject, Header, Tooltip, HideInInspector, DontDestroyOnLoad, Debug, Time)
using UnityEngine.UI; // For InputField
using UnityEngine.SceneManagement; // For SceneManager

public class DataCollector : MonoBehaviour
{
    // For other scripts to access DataCollector
    public static DataCollector Instance;

    [Tooltip("Time interval to collect Headset Position & Rotation. Default: 1.0f")]
    public float dataRecordInterval = 1f;

    [Header("Under Canvas")]

    // User ID InputField
    public InputField inputField;

    // Video ID InputField
    public InputField VideoIdInputField;

    public InputField genderinput;
    // For holding SteamVR Camera Component
    [HideInInspector]
    public GameObject user;

    // For converting User Input into respective data
    public string dataID;
    [HideInInspector]
    public int VideoID;
    [HideInInspector]
    public string currentFolderPath;

    public string gender;
    // For Recording Head Movement Data
    [HideInInspector]
    public bool startRecording = false;
    private float time = 0f;

    public GameObject button;

    static int number=4;
    // Enum for different Data type to record
    public enum DataToRecord
    {
        HeadMovement,
        RatingVideo
    }

    // Runs before Start()
    private void Awake()
    {
        // Allow DataCollector Instance to be alive after change scene
        DontDestroyOnLoad(this);
    }

    // Runs at the start of first frame
    private void Start()
    {
        // Set this instance of DataCollector to allow other scripts to access its variables and data
        Instance = this;

        // Allow input of User ID
        inputField.Select();
        //save the previous videoID
        number = PlayerPrefs.GetInt("video");
    }

    // Runs at every frame
    private void Update()
    {
        // Start Recording when bool is set to true
        if (startRecording)
        {
            time += Time.deltaTime;

            // Record Head Movement Data every data Record Interval(Default: 1.0f)
            if (time > dataRecordInterval)
            {
                // Reset timer
                time = 0;

                // Write generated data into csv file
                PushData(GenerateData());
            }
        }
    }

    // Returns generated Head Movement Data string
    private string GenerateData()
    {
        string data = "";

        // Get Time Information into data string
        data += System.DateTime.Now.ToString("HH");
        data += ":";
        data += System.DateTime.Now.ToString("mm");
        data += ":";
        data += System.DateTime.Now.ToString("ss");
        data += ":";
        data += System.DateTime.Now.ToString("FFF");

        // Seperator
        data += ",";

        // Get Headset Position in vector 3 format
        string posstr = user.GetComponent<SteamVR_Camera>().head.transform.position.ToString("F3");

        // Change , to . to prevent Position data to be seperated
        data += ChangeLetters(posstr, ',', '.');

        // Seperator
        data += ",";

        // Get Headset Rotation in vector 3 format
        string rotstr = user.GetComponent<SteamVR_Camera>().head.transform.rotation.ToString("F3");

        // Change , to . to prevent Position data to be seperated
        data += ChangeLetters(rotstr, ',', '.');

        return data;
    }

    // Edit the current file by adding the new text
    private void PushData(string text)
    {
        // Open csv file at the path return from GetPath()
        StreamWriter sw = File.AppendText(GetCSVPath(DataToRecord.HeadMovement, currentFolderPath));

        // Write onto the file
        sw.WriteLine(text);

        // Close the file
        sw.Close();
    }

    // Returns the fodler path being used to store the csv files
    private string GetFolderPath()
    {
        string Folder = "/Data/";

        // If the folder path already exists, create a new folder with a duplicate number
        string filePath = Application.dataPath + Folder + dataID;
        int duplicateCounts = 0;
        while (true)
        {
            if (Directory.Exists(filePath))
            {
                ++duplicateCounts;
                filePath = Application.dataPath + Folder + dataID + "(" + duplicateCounts.ToString() + ")";
            }
            else
                break;
        }
        return filePath;
    }

    // Duplicate or Create new folder
    private void CreateFolder()
    {
        currentFolderPath = GetFolderPath();
        // Create new folder and csv files
        Directory.CreateDirectory(currentFolderPath);
        CreateCSV(DataToRecord.HeadMovement, currentFolderPath);
        CreateCSV(DataToRecord.RatingVideo, currentFolderPath);
    }

    // Returns the file path being used to store the data
    public string GetCSVPath(DataToRecord dataToRecord, string folderPath)
    {
        string File = "";

        switch (dataToRecord)
        {
            case DataToRecord.HeadMovement:
                File += "/HeadMovement";
                break;
            case DataToRecord.RatingVideo:
                File += "/Gender";
                break;
        }

        return folderPath + File + ".csv";
    }

    // Create new CSV
    private void CreateCSV(DataToRecord dataToRecord, string folderPath)
    {
        // Create new CSV and Write in Data Headers on first line
        StreamWriter output = File.CreateText(GetCSVPath(dataToRecord, folderPath));
        switch (dataToRecord)
        {
            case DataToRecord.HeadMovement:
                output.WriteLine("Time, Position, Rotation");
                break;
            case DataToRecord.RatingVideo:
                output.WriteLine("Gender, Condition");
                break;
        }
        output.Close();
    }

    // Change "letter" in "str" to "toBeLetter"
    private string ChangeLetters(string str, char letter, char toBeLetter)
    {
        char[] ret = str.ToCharArray();
        for (int i = 0; i < ret.Length; ++i)
        {
            if (ret[i] == letter)
            {
                ret[i] = toBeLetter;
            }
        }
        return new string(ret);
    }

    //--------------------------------------------------------------------------------
    //                                  PUBLIC FUNCTIONS
    //--------------------------------------------------------------------------------

    // Link to User ID InputField OnEndEdit()
    // This Registers User ID and use it to create csv file.
    // Then Set Video ID InputField to active and allow its input.
    public void ProceedToSelectVideo()
    {
        // If no text, dont let them proceed
        if (inputField.text == null)
            return;

        // Register User ID
        dataID = inputField.text;

        // Create Folder and CSV for user based on input
        CreateFolder();

        // Allow input of Video ID
        VideoIdInputField.gameObject.SetActive(true);
        VideoIdInputField.Select();

        //allow gender input
        genderinput.gameObject.SetActive(true);
        genderinput.Select();

        button.gameObject.SetActive(true);
    }
    //this function is to write in the gender to excel file if the person did not click the "blind" button
    public void proceedtoselectvideo2()
    {
        if (inputField.text == null)
            return;

        gender = genderinput.text;
        PlayerPrefs.SetString("gender", gender);
    }
    // Link to Video ID InputField OnEndEdit()
    // This Registers Video ID, which is used to determine the video file to play.
    // Then Set bool to true so can start Recording Head Movement Data.
    // And Change Scene to Main Scene (Watch video)
    public void Start360MovieTheatre()
    {

        // Try Parse VideoID Input into int and put in VideoID
        if (VideoIdInputField.text == null || !int.TryParse(VideoIdInputField.text, out VideoID))
        {
            // If no text, dont let them proceed
            Debug.LogWarning("VideoID Input cannot be parse into int.");
            return;
        }
        // Start recording Head Movement
        startRecording = true;
        StreamWriter sw = File.AppendText(GetCSVPath(DataCollector.DataToRecord.RatingVideo, currentFolderPath));
        string url = gender;
        string rating = VideoID.ToString();

        // Write onto the file
        sw.WriteLine(url + "," + rating);

        // Close the file
        sw.Close();
        // Change to MainScene
        SceneManager.LoadScene("MainScene");
    }
    //pardon my naming convention but the function below is for the "blind" button 
    //for example if the VideoID is originally 1 and the person clicked on the "blind" button Video ID will be 2.
    //and the VideoID will be 2 even if the program is closed until the next user click on the "blind" button then the VideoID will become 3
    public void randomstuff()
    {
        int something;
        startRecording = true;
        //if you want to run 4conditions set the enable this line of code
        if(number==4)
        {
            number = 0;
        }
        //if you want to run 3conditions enable this line of code

        //if (number == 7)
        //{
        //    number = 4;
        //}

        number++;
        PlayerPrefs.SetInt("video", number);
        something = PlayerPrefs.GetInt("video");
        VideoID = something;
        SceneManager.LoadScene("MainScene");
        //write the gender and videoid into the excel file.
        StreamWriter sw = File.AppendText(GetCSVPath(DataCollector.DataToRecord.RatingVideo, currentFolderPath));
        string url = gender;
        string rating = VideoID.ToString();

        // Write onto the file
        sw.WriteLine(url + "," + rating);

        // Close the file
        sw.Close();

        Debug.Log(VideoID);
    }
}