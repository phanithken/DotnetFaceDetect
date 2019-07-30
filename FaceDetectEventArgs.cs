using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.FaceAnalysis;

namespace FaceDetect
{
    public class FaceDetectEventArgs: EventArgs
    {
        public IReadOnlyList<DetectedFace> ResultFrame { get; private set; }

        public FaceDetectEventArgs(IReadOnlyList<DetectedFace> resultFrame)
        {
            this.ResultFrame = resultFrame;
        }
    }
}
