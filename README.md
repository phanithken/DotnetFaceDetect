# DotnetFaceDetect

### Usage
+ get dll and add to the project by following this guidelines:
https://qiita.com/phanithken/items/fffa6ec4b73b1fdde254

+ sample code
```
private FaceDetection _faceDetection = new FaceDetection();
_faceDetection.OnDetectFace += new FaceDetection.FaceDetectedHandler(GetFacePosition);
_faceDetection.StartDetect();

private void GetFacePosition(object sender, FaceDetectEventArgs e)
{
    // number of people
    var people = e.ResultFrame.Count
}
```
