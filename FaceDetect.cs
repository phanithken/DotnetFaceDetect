using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.Core;
using Windows.Media.MediaProperties;
using Windows.UI.Xaml.Controls;

namespace FaceDetect
{

    public class FaceDetection
    {

        // custom delegate and event for face detect
        public delegate void FaceDetectedHandler(object sender, FaceDetectEventArgs e);
        public event FaceDetectedHandler OnDetectFace;

        // required dependencies for face detect
        private FaceDetectionEffect _faceDetectionEffect;
        private MediaCapture _mediaCapture;


        public async void StartDetect()
        {
            var captureElement = new CaptureElement();
            this._mediaCapture = new MediaCapture();
            await this._mediaCapture.InitializeAsync();
            captureElement.Source = _mediaCapture;
            await _mediaCapture.StartPreviewAsync();
            // start detect face after start the camera
            this.DetectFace();
        }

        // face detection process
        private async void DetectFace()
        {
            var faceDetectionDefinition = new FaceDetectionEffectDefinition();
            faceDetectionDefinition.DetectionMode = FaceDetectionMode.HighPerformance;
            faceDetectionDefinition.SynchronousDetectionEnabled = false;
            this._faceDetectionEffect = (FaceDetectionEffect) await
                this._mediaCapture.AddVideoEffectAsync(faceDetectionDefinition, MediaStreamType.VideoPreview);
            this._faceDetectionEffect.FaceDetected += FaceDetectionEffect_FaceDetected;
            this._faceDetectionEffect.DesiredDetectionInterval = TimeSpan.FromMilliseconds(33);
            this._faceDetectionEffect.Enabled = true;
        }

        // method to handle face postion detection
        private void FaceDetectionEffect_FaceDetected(FaceDetectionEffect sender, FaceDetectedEventArgs args)
        {
            var detectedFaces = args.ResultFrame.DetectedFaces;
            FaceDetectEventArgs argument = new FaceDetectEventArgs(detectedFaces);

            if (OnDetectFace == null) return;
            OnDetectFace(this, argument);
        }

    }
}
