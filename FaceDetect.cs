using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.Core;
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
        private CaptureElement _captureElement;

        // start face detection
        public async void StartDetect()
        {
            this._captureElement = new CaptureElement();
            this._mediaCapture = new MediaCapture();
            await this._mediaCapture.InitializeAsync();
            this._captureElement.Source = _mediaCapture;
            await this._mediaCapture.StartPreviewAsync();

            // start detect face after start the camera
            this.DetectFace();
        }

        // stop face detection
        public async void StopDetect()
        {
            this._faceDetectionEffect.Enabled = false;
            this._faceDetectionEffect.FaceDetected -= FaceDetectionEffect_FaceDetected;
            this._faceDetectionEffect = null;
            await this._mediaCapture.ClearEffectsAsync(MediaStreamType.VideoPreview);

            // uninnitialize camera
            await this.CleanupCameraAsync();
        }

        private async Task CleanupCameraAsync()
        {
            if (this._mediaCapture != null)
            {
                await this._mediaCapture.StopPreviewAsync();
            }

            this._captureElement.Source = null;
            this._mediaCapture.Dispose();
            this._mediaCapture = null;
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
