using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.Compression;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Andropeak
{
    public partial class Form1 : Form
    {
        private readonly string[] dangerousPermissions = {
        "android.permission.READ_CALENDAR",
        "android.permission.WRITE_CALENDAR",
        "android.permission.CAMERA",
        "android.permission.READ_CONTACTS",
        "android.permission.WRITE_CONTACTS",
        "android.permission.GET_ACCOUNTS",
        "android.permission.ACCESS_FINE_LOCATION",
        "android.permission.ACCESS_COARSE_LOCATION",
        "android.permission.RECORD_AUDIO",
        "android.permission.READ_PHONE_STATE",
        "android.permission.CALL_PHONE",
        "android.permission.READ_CALL_LOG",
        "android.permission.WRITE_CALL_LOG",
        "android.permission.ADD_VOICEMAIL",
        "android.permission.USE_SIP",
        "android.permission.PROCESS_OUTGOING_CALLS",
        "android.permission.BODY_SENSORS",
        "android.permission.SEND_SMS",
        "android.permission.RECEIVE_SMS",
        "android.permission.READ_SMS",
        "android.permission.RECEIVE_WAP_PUSH",
        "android.permission.RECEIVE_MMS",
        "android.permission.READ_EXTERNAL_STORAGE",
        "android.permission.WRITE_EXTERNAL_STORAGE"
    };
        // Define a class-level variable to store the selected package location
        private string selectedPackageLocation = "";
        private string WorkingDirectory = "";
        public Form1()
        {
            InitializeComponent();
            ListenForUSBDeviceConnection();
            ListenForUSBDeviceDisconnect();
            ExecuteADBDevicesCommand();

        }
        private void ListenForUSBDeviceConnection()
        {
            // Create a management scope to query for USB device connection events
            var scope = new ManagementScope("root\\CIMV2");
            var query = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PnPEntity'");
            var watcher = new ManagementEventWatcher(scope, query);

            // Handle USB device connection and disconnection events
            watcher.EventArrived += (s, e) =>
            {
                var instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                var deviceId = (string)instance["DeviceID"]; // Assuming "DeviceID" is the property that holds the device serial number

                if ((string)e.NewEvent.ClassPath.ClassName == "__InstanceCreationEvent") // USB device connection event
                {
                    // Execute the adb devices command when the USB device is connected
                    ExecuteADBDevicesCommand();

                    // Update the label text with the connected device ID
                    UpdateUI("" + deviceId, true);
                    //HTTP Device Proxy is Unset on reconnection, because this setting stays with the device until it's unset. 
                    ExecuteADBCommand("shell settings put global http_proxy :0");

                }
            };
            // Start listening for USB device connection events
            watcher.Start();
        }

        private void ListenForUSBDeviceDisconnect()
        {
            // Create a management scope to query for USB device disconnection events
            var scope = new ManagementScope("root\\CIMV2");
            var query = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBControllerDevice'");
            var watcher = new ManagementEventWatcher(scope, query);

            // Handle USB device disconnection events
            watcher.EventArrived += (s, e) =>
            {
                var instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                var deviceId = (string)instance["Dependent"]; // Assuming "Dependent" is the property that holds the USB device ID

                // Extract the device ID from the dependent property
                var matches = Regex.Match(deviceId, @"DeviceID=""(.*?)""");
                if (matches.Success)
                {
                    var usbDeviceId = matches.Groups[1].Value;
                    // Update UI elements on the main UI thread
                    Invoke((MethodInvoker)delegate
                    {
                        UpdateUI("Device not connected!", false);
                        btnProxyOn.Visible = true;
                        btnProxyOff.Visible = false;
                        btnPortForwardOn.Visible = true;
                        btnPortForwardOff.Visible = false;
                        txtBoxPortLocal.Text = "";
                        txtBoxPortRemote.Text = "";
                        txtBoxProxySet.Text = "";
                    });
                }
            };
            // Start listening for USB device disconnection events
            watcher.Start();
        }
        private void ExecuteADBDevicesCommand()
        {
            try
            {
                // Execute the adb devices command
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "adb"; // Assuming adb is in your system PATH
                startInfo.Arguments = "devices";
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Extract connected device information
                string connectedDevices = "";
                string[] lines = output.Split('\n');
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.Contains("List of devices attached"))
                    {
                        string[] parts = line.Split('\t');
                        connectedDevices += parts[0] + Environment.NewLine;
                    }
                }

                // Check if there are connected devices
                bool isConnected = !string.IsNullOrEmpty(connectedDevices.Trim());

                // Update the label with the output of the adb devices command                              
                UpdateUI(connectedDevices, isConnected);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing ADB command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateUI(string connectedDevices, bool isConnected)
        {
            string labelText = isConnected ? "Connected to: " + connectedDevices : "Device not connected";
            Color labelColor = isConnected ? Color.Green : Color.Red;

            if (lblConnectInfo.InvokeRequired)
            {
                lblConnectInfo.Invoke((MethodInvoker)(() =>
                {
                    lblConnectInfo.Text = labelText;
                    lblConnectInfo.ForeColor = labelColor;
                    grpPortReverse.Visible = isConnected;
                    grpProxy.Visible = isConnected;
                    grpApkExtract.Visible = isConnected;
                    grpDecompile.Visible = isConnected;
                    grpAdbShell.Visible = isConnected;
                }));
            }
            else
            {
                lblConnectInfo.Text = labelText;
                lblConnectInfo.ForeColor = labelColor;
                ExecuteADBCommand("shell settings put global http_proxy :0");
            }
        }

        // Method to update the label text
        private void UpdateStatusLabel(string text, Color color)
        {
            if (lblConnectInfo.InvokeRequired)
            {
                lblConnectInfo.Invoke((MethodInvoker)(() =>
                {
                    lblConnectInfo.Text = text;
                    lblConnectInfo.ForeColor = color;
                }));
            }
            else
            {
                lblConnectInfo.Text = text;
                lblConnectInfo.ForeColor = color;
            }
        }
        private void ExecuteApktoolCommand(string command)
        {
            try
            {
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "C:\\Windows\\apktool.bat"; // Assuming apktool is in your system PATH
                    startInfo.Arguments = command;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    process.StartInfo = startInfo;

                    // Start the process without waiting for it to complete
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing Apktool command, is it in your path?: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //ExecuteADBCommand chunk is used to list and pull APKs
        private string ExecuteADBCommand(string command)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "adb"; // Assuming adb is in your system PATH
                startInfo.Arguments = command;
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                // Check if the command is for getting the proxy setting
                if (command.Contains("settings get global http_proxy"))
                {
                    // Check if the proxy setting is not ":0"
                    if (output.Trim() != ":0")
                    {
                        // Reset the http_proxy setting to ":0"
                        ExecuteADBCommand("shell settings put global http_proxy :0");
                    }
                }
                return output;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing ADB command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        private string ExecuteAaptCommand(string command)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "aapt"; // Aapt comes with Android SDK and ADB so it should already be in your $PATH
                startInfo.Arguments = command;
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return output;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing aapt command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void comboAppPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (comboAppPicker.SelectedItem != null)
            {
                // Retrieve the selected package location and store it in the class-level variable
                selectedPackageLocation = comboAppPicker.SelectedItem.ToString();
                buttonPullAPK.Visible = true;
            }
        }

        private void buttonListAPK_Click(object sender, EventArgs e)
        {

            // Execute ADB command to list installed packages
            string output = ExecuteADBCommand("shell pm list packages -f");

            // Split the output by newline to get individual lines
            string[] lines = output.Split('\n');

            // Clear the existing items in the ComboBox
            comboAppPicker.Items.Clear();

            // Filter and add the package locations to the ComboBox
            foreach (string line in lines)
            {
                // Remove any leading or trailing whitespace
                string packageLocation = line.Trim();

                // Check if the package location contains "base"
                if (packageLocation.Contains("base"))
                {
                    // Add the package location to the ComboBox
                    comboAppPicker.Items.Add(packageLocation);

                }
            }

            // If no packages were found, display a message
            if (comboAppPicker.Items.Count == 0)
            {
                comboAppPicker.Items.Add("No packages found.");
            }
        }

        private void buttonPullAPK_Click(object sender, EventArgs e)
        {
            // Check if a package location is selected
            if (!string.IsNullOrEmpty(selectedPackageLocation))
            {
                // Extract the APK file path from the selected package location
                string apkFilePath = ExtractApkFilePath(selectedPackageLocation);

                // Specify the destination folder where you want to pull the APK file
                string destinationFolder = workingDirectory;

                try
                {
                    // Execute ADB command to pull the APK file from the device to the local folder
                    string output = ExecuteADBCommand($"pull \"{apkFilePath}\" \"{destinationFolder}\"");

                    // Check if the ADB command was successful
                    if (!string.IsNullOrEmpty(output))
                    {
                        MessageBox.Show("APK file pulled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Failed to pull APK file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No package location selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to extract APK file path from the package location
        private string ExtractApkFilePath(string packageLocation)
        {
            // Define a regular expression pattern to match the APK file path
            string pattern = @"package:(.*?)\/base\.apk";

            // Match the pattern in the package location string
            Match match = Regex.Match(packageLocation, pattern);

            // Check if a match is found
            if (match.Success)
            {
                // Get the captured group value (APK file path)
                string apkFilePath = match.Groups[1].Value;

                // Return the extracted APK file path
                return apkFilePath;
            }
            else
            {
                // Return an empty string if no match is found
                return "";
            }
        }

        //This pulls the local apk files from where they were downloaded to from the device and loads them in a combobox
        private void btnDecompile_Click(object sender, EventArgs e)
        {
            string[] filePaths = Directory.GetFiles(workingDirectory, "*.apk", SearchOption.AllDirectories);
            foreach (string file in filePaths)
            {
                string apkLocation = file.Trim();
                if (apkLocation.Contains("base"))
                    comboDecompile.Items.Add(apkLocation);
                else
                    comboDecompile.Items.Add("No packages found.");
            }
        }
        private string selectedapkLocation = "";
        private void comboDecompile_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (comboDecompile.SelectedItem != null)
            {
                // Retrieve the selected package location and store it in the class-level variable
                selectedapkLocation = comboDecompile.SelectedItem.ToString();
                btnDecompileSelection.Visible = true;
                btnUnzipAPK.Visible = true;
            }
        }
        private void btnDecompileSelection_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedapkLocation))
            {
                try
                {
                    // Extract the directory path from the selected APK location
                    string directoryPath = Path.GetDirectoryName(selectedapkLocation);

                    // Create a new directory next to the APK file for the decompiled files
                    string outputFolder = Path.Combine(directoryPath, "decompiled");

                    // Create the output directory if it doesn't exist
                    if (!Directory.Exists(outputFolder))
                    {
                        Directory.CreateDirectory(outputFolder);
                    }

                    // Construct the Apktool command with the output folder
                    string apktoolCommand = $"d -f -o \"{outputFolder}\" \"{selectedapkLocation}\"";

                    // Execute the Apktool command without awaiting its completion
                    ExecuteApktoolCommand(apktoolCommand);

                    // Display a message indicating that the decompilation process has started
                    MessageBox.Show("Decompilation started. Andro Peak will not monitor progress. Check the folders manually", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No package location selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnProxyOff_Click(object sender, EventArgs e)
        {
            ExecuteADBCommand("shell settings put global http_proxy :0");
            btnProxyOn.Visible = true;
            btnProxyOff.Visible = false;
            lblUnsetMsg.Visible = false;
            txtBoxProxySet.Text = "";
        }
        private int localPort = -1;
        private int remotePort = -1;
        private void btnProxyOn_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the port numbers from the text boxes
                int proxyPort = int.Parse(txtBoxProxySet.Text);

                if (proxyPort != localPort)
                {
                    MessageBox.Show("Local ports for reverse socket and device proxy must be the same.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method early
                }

                // Check if the port numbers are valid
                if (proxyPort < 1024 || proxyPort > 65535)
                {
                    MessageBox.Show("Please enter valid TCP ports within the range 1024-65535.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method early
                }
                // Construct the ADB command with the port numbers
                string adbCommand = $"shell settings put global http_proxy 127.0.0.1:{proxyPort}";

                // Execute the ADB command
                ExecuteADBCommand(adbCommand);

                // If the command executed successfully, update UI
                btnProxyOff.Visible = true;
                btnProxyOn.Visible = false;
                lblUnsetMsg.Visible = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid port numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPortForwardOn_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the port numbers from the text boxes
                localPort = int.Parse(txtBoxPortLocal.Text);
                int remotePort = int.Parse(txtBoxPortRemote.Text);

                // Check if the port numbers are valid
                if (localPort < 1024 || localPort > 65535 || remotePort < 1024 || remotePort > 65535)
                {
                    MessageBox.Show("Please enter valid TCP ports within the range 1024-65535.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method early
                }
                // Construct the ADB command with the port numbers
                string adbCommand = $"reverse tcp:{localPort} tcp:{remotePort}";

                // Execute the ADB command
                ExecuteADBCommand(adbCommand);

                // If the command executed successfully, update UI
                btnPortForwardOff.Visible = true;
                btnPortForwardOn.Visible = false;
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid port numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPortForwardOff_Click(object sender, EventArgs e)
        {
            ExecuteADBCommand("reverse --remove-all");
            btnPortForwardOn.Visible = true;
            btnPortForwardOff.Visible = false;
            txtBoxPortLocal.Text = "";
            txtBoxPortRemote.Text = "";

        }

        private void btnWorkingDirectory_Click(object sender, EventArgs e)
        {
            string pathToFolder = workingDirectory;
            System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", pathToFolder);
        }

        private void btnAdbShell_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/k adb shell";
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = false;

                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private string workingDirectory = ""; // Variable to store the selected folder path
        private void btnWorkingDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // Store the selected folder path in the variable
                    workingDirectory = folderBrowserDialog.SelectedPath;
                    lblWorkingDirectory.Text = "";
                    lblWorkingDirectory.Text += workingDirectory;
                    btnWorkingDirectory.Visible = true;
                    grpPortReverse.Visible = true;
                    grpProxy.Visible = true;
                    grpApkExtract.Visible = true;
                    grpDecompile.Visible = true;
                    grpAdbShell.Visible = true;
                    grpDeviceScreenshot.Visible = true;

                }
            }
        }

        private void btnUnzipAPK_Click(object sender, EventArgs e)
        {
            string selectedFilePath = comboDecompile.SelectedItem?.ToString();
            if (selectedFilePath != null)
            {
                string directoryPath = Path.GetDirectoryName(selectedFilePath);
                string sourceFilePath = Path.Combine(directoryPath, "base.apk");
                string destinationFolderPath = Path.Combine(directoryPath, "unzipped");
                string destinationFilePath = Path.Combine(destinationFolderPath, "base.zip");

                // Create the destination folder if it doesn't exist
                if (!Directory.Exists(destinationFolderPath))
                {
                    Directory.CreateDirectory(destinationFolderPath);
                }

                // Check if the source file exists
                if (File.Exists(sourceFilePath))
                {
                    // Check if the destination file already exists
                    int count = 1;
                    string newDestinationFilePath = destinationFilePath;
                    while (File.Exists(newDestinationFilePath))
                    {
                        // Append a number to the file name
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(destinationFilePath);
                        string extension = Path.GetExtension(destinationFilePath);
                        newDestinationFilePath = Path.Combine(destinationFolderPath, $"{fileNameWithoutExtension}_{count}{extension}");
                        count++;
                    }

                    // Copy the source file to the destination folder and rename it
                    File.Copy(sourceFilePath, newDestinationFilePath);

                    // Check if the destination file exists after copy
                    if (File.Exists(newDestinationFilePath))
                    {
                        // Unzip the copied file
                        UnzipFile(newDestinationFilePath, destinationFolderPath);

                        MessageBox.Show("APK file copied, renamed, and unzipped successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to copy and rename APK file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Source APK file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UnzipFile(string zipFilePath, string destinationFolder)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string entryDestinationPath = Path.Combine(destinationFolder, entry.FullName);

                    // Check if the entry's destination path already exists
                    if (File.Exists(entryDestinationPath))
                    {
                        // Append a unique identifier to the file name
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(entryDestinationPath);
                        string extension = Path.GetExtension(entryDestinationPath);
                        string newFileName = $"{fileNameWithoutExtension}_{DateTime.Now.Ticks}{extension}";
                        entryDestinationPath = Path.Combine(destinationFolder, newFileName);
                    }

                    // Ensure that the directory structure exists
                    Directory.CreateDirectory(Path.GetDirectoryName(entryDestinationPath));

                    // Extract the entry to the destination path
                    entry.ExtractToFile(entryDestinationPath);
                }
            }
        }

        private void btnAppPermissions_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedapkLocation))
            {
                try
                {
                    // Extract the directory path from the selected APK location
                    string directoryPath = Path.GetDirectoryName(selectedapkLocation);

                    // Construct the aapt command with the output folder
                    string aaptCommand = $"d permissions \"{selectedapkLocation}\"";

                    // Execute the aapt command without awaiting its completion
                    string aaptOutput = ExecuteAaptCommand(aaptCommand);

                    // Mark dangerous permissions
                    string modifiedOutput = MarkDangerousPermissions(aaptOutput);

                    // Display a message showing app permissions
                    MessageBox.Show(modifiedOutput, "Information: CTRL+C to copy to Clipboard!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No package location selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string MarkDangerousPermissions(string aaptOutput)
        {
            StringBuilder sb = new StringBuilder();
            using (StringReader reader = new StringReader(aaptOutput))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string permission = line.Trim();
                    int index = permission.IndexOf("name='");
                    if (index != -1)
                    {
                        int startIndex = index + "name='".Length;
                        int endIndex = permission.IndexOf("'", startIndex);
                        if (endIndex != -1)
                        {
                            permission = permission.Substring(startIndex, endIndex - startIndex);

                            // Check if permission exists in dangerousPermissions array
                            if (dangerousPermissions.Contains(permission))
                            {
                                sb.AppendLine(permission + " - Dangerous");
                            }
                            else
                            {
                                sb.AppendLine(permission);
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }
        private bool ExecuteADBScreenshot(string command, string outputFile)
        {
            try
            {
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "adb"; // Assuming adb is in your system PATH
                    startInfo.Arguments = command;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    process.StartInfo = startInfo;

                    // Start the process
                    process.Start();

                    // Read the output as binary data and write it to the file
                    using (FileStream fs = new FileStream(outputFile, FileMode.Create))
                    {
                        process.StandardOutput.BaseStream.CopyTo(fs);
                    }

                    // Wait for the process to exit
                    process.WaitForExit();

                    // Check if the process exited successfully
                    if (process.ExitCode == 0)
                    {
                        return true; // Process completed successfully
                    }
                    else
                    {
                        return false; // Process failed
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing ADB command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Process failed
            }
        }




        private void btnDevScreenshot_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the current date and time
                string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                // Construct the filename with the current date and time
                string filename = $"screen_{timeStamp}.png";

                // Construct the full path to the file
                string filePath = Path.Combine(workingDirectory, filename);

                // Run the ADB command to capture the screen
                string adbCommand = "exec-out screencap -p";

                // Execute the ADB command and save the output to the file
                ExecuteADBScreenshot(adbCommand, filePath); // Ensure both command and outputFile are provided

                MessageBox.Show("Screenshot saved successfully to: " + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error capturing screenshot: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

