using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FaceDetect;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestUseFaceDetectLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private FaceDetection _faceDetection;

        public MainPage()
        {
            this.InitializeComponent();
            this._faceDetection = new FaceDetection();
            this._faceDetection.OnDetectFace += new FaceDetection.FaceDetectedHandler(this.GetFacePosition);
            this._faceDetection.StartDetect();
        }


        private void GetFacePosition(object sender, FaceDetectEventArgs e)
        {
            Debug.WriteLine("Get face position");
            Debug.WriteLine(e.ResultFrame.Count);
            Debug.WriteLine("******************");
        }
    }
}
