using System;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace ConsoleApp54
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start...");

            // ----------------------

            var path = @"C:\ImagesColorSeparation\1\MySlide001.png";
            //var path = @"C:\ImagesColorSeparation\1\MySlide002.jpg";
            var filename = Path.GetFileName(path);


            Console.WriteLine("PATH:     {0}", path);
            Console.WriteLine("FILENAME: {0}", filename);

            Console.WriteLine("\n\n\nPress any key to continue...");
            Console.Read();
            

            // ********************************* HSV

            // Load your image here
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);

            // Convert the image to HSV color space
            Image<Hsv, byte> hsvImage = image.Convert<Hsv, byte>();

            VectorOfMat channels = new VectorOfMat();
            CvInvoke.Split(hsvImage, channels);

            // Get the individual channels
            Image<Gray, byte> hueChannel = channels[0].ToImage<Gray, byte>();
            Image<Gray, byte> saturationChannel = channels[1].ToImage<Gray, byte>();
            Image<Gray, byte> valueChannel = channels[2].ToImage<Gray, byte>();


            // Save each channel separately as PNG files
            hueChannel.Save("C:/ImagesColorSeparation/1/output/Hue_Channel.png");
            saturationChannel.Save("C:/ImagesColorSeparation/1/output/Saturation_Channel.png");
            valueChannel.Save("C:/ImagesColorSeparation/1/output/Value_Channel.png");


            // Display each channel separately
            CvInvoke.Imshow("Hue Channel", hueChannel);
            CvInvoke.Imshow("Saturation Channel", saturationChannel);
            CvInvoke.Imshow("Value Channel", valueChannel);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();

            // Merge the channels back to HSV image
            Image<Hsv, byte> mergedChannels = new Image<Hsv, byte>(hsvImage.Width, hsvImage.Height);
            CvInvoke.Merge(new VectorOfMat(hueChannel.Mat, saturationChannel.Mat, valueChannel.Mat), mergedChannels);

            mergedChannels.Save("C:/ImagesColorSeparation/1/output/Merged_Channels_Back_To_BGR.png");


            // Convert the merged channels back to Bgr for display
            Image<Bgr, byte> mergedBgrChannels = mergedChannels.Convert<Bgr, byte>();

            // Display the merged channels
            CvInvoke.Imshow("Merged Channels", mergedBgrChannels);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();

            // ********************************************* HSV



            Console.Read();
        }
    }
}
