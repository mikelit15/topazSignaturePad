using System.Drawing.Imaging;

namespace topaz
{
    public partial class Form1 : Form
    {
        readonly Bitmap sign = new(Application.StartupPath + "imgs\\Sign.bmp");
        readonly Bitmap done = new(Application.StartupPath + "imgs\\Done.bmp");
        readonly Bitmap clear = new(Application.StartupPath + "imgs\\Clear.bmp");
        readonly Bitmap please = new(Application.StartupPath + "imgs\\Please.bmp");

        public Form1()
        {
            InitializeComponent();
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Call the method that was previously triggered by the button.
            cmdStart_Click(sender, e);
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            //The following code will write BMP images out to the LCD 1X5 screen
            sigPlusNET1.SetTabletState(1); //Turns tablet on to collect signature
            sigPlusNET1.SetTranslateBitmapEnable(false);

            //Images sent to the background
            sigPlusNET1.LCDSendGraphic(1, 2, -2, 0, sign);
            sigPlusNET1.LCDSendGraphic(1, 2, 199, 48, done);
            sigPlusNET1.LCDSendGraphic(1, 2, 196, 5, clear);

            sigPlusNET1.SetDisplayPenWidth(10);

            sigPlusNET1.ClearTablet();
            sigPlusNET1.ClearSigWindow(1);
            sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
            sigPlusNET1.KeyPadAddHotSpot(2, 1, 191, 0, 55, 22); //For CLEAR button
            sigPlusNET1.KeyPadAddHotSpot(3, 1, 194, 45, 48, 22); //For DONE button
            sigPlusNET1.LCDSetWindow(16, 16, 174, 44);
            sigPlusNET1.SetSigWindow(1, 16, 16, 174, 44); //Sets the area where ink is permitted in the SigPlus object
            sigPlusNET1.SetLCDCaptureMode(2);   //Sets mode so ink will not disappear after a few seconds
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //reset hardware
            sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
            sigPlusNET1.LCDSetWindow(0, 0, 240, 64);
            sigPlusNET1.SetSigWindow(1, 0, 0, 240, 64);
            sigPlusNET1.KeyPadClearHotSpotList();
            // Bitmap blank = new System.Drawing.Bitmap(240, 64);
            // sigPlusNET1.LCDSendGraphic(1, 0, 0, 0, blank);
            sigPlusNET1.SetLCDCaptureMode(1);
            sigPlusNET1.SetTabletState(0);
        }

        private void sigPlusNET1_PenUp(object sender, EventArgs e)
        {
            if (sigPlusNET1.KeyPadQueryHotSpot(2) > 0) //If the CLEAR hotspot is tapped, then...
            {
                sigPlusNET1.ClearSigWindow(1);
                sigPlusNET1.ClearTablet();
                sigPlusNET1.LCDRefresh(1, 16, 16, 175, 44); //Refresh LCD at 'CLEAR' to indicate to user that this option has been sucessfully chosen
                sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
            }
            else if (sigPlusNET1.KeyPadQueryHotSpot(3) > 0) //If the DONE hotspot is tapped, then...
            {
                sigPlusNET1.ClearSigWindow(1);

                if (sigPlusNET1.NumberOfTabletPoints() > 0)
                {
                    saveImage();
                }
                else
                {
                    sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
                    sigPlusNET1.LCDSendGraphic(0, 2, 4, 20, please);
                    System.Threading.Thread.Sleep(750);
                    sigPlusNET1.ClearTablet();
                    sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64);
                    sigPlusNET1.SetLCDCaptureMode(2);   //Sets mode so ink will not disappear after a few seconds
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            sigPlusNET1.ClearSigWindow(1);
            sigPlusNET1.ClearTablet();
            sigPlusNET1.LCDRefresh(1, 16, 16, 175, 44); //Refresh LCD at 'CLEAR' to indicate to user that this option has been sucessfully chosen
            sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            sigPlusNET1.ClearSigWindow(1);
            
            // If there are tablet points, call saveImage; otherwise, show a message on the LCD.
            if (sigPlusNET1.NumberOfTabletPoints() > 0)
            {
                saveImage();
            }
            else
            {
                MessageBox.Show("No signature captured. Please sign on the tablet before clicking Done.", 
                        "No Signature Detected", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
            }
        }

        private void saveImage()
        {
            sigPlusNET1.SetImageXSize(2000);
            sigPlusNET1.SetImageYSize(700);
            sigPlusNET1.SetJustifyY(10);
            sigPlusNET1.SetJustifyX(10);
            sigPlusNET1.SetJustifyMode(5);
            sigPlusNET1.SetImagePenWidth(86);
            sigPlusNET1.SetImageFileFormat(4);
            Image sigImage = sigPlusNET1.GetSigImage();
            string filePath = Path.Combine(Application.StartupPath, "signature.png");
            sigImage.Save(filePath, ImageFormat.Png);
            sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
            Font f = new("Arial", 9.0F, FontStyle.Regular);
            sigPlusNET1.LCDWriteString(0, 2, 35, 25, f, "Signature capture complete.");
            System.Threading.Thread.Sleep(1500);
            Application.Exit();
        }
    }
}
