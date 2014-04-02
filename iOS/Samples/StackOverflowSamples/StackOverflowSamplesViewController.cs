using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Runtime.InteropServices;
using MonoTouch.CoreImage;
using System.Linq;
using System.Collections.Generic;

using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Web;
using SimplyMobile.Text;

namespace StackOverflowSamples
{
    public partial class StackOverflowSamplesViewController : UIViewController
    {
        public StackOverflowSamplesViewController () : base ("StackOverflowSamplesViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            // Perform any additional setup after loading the view, typically from a nib.

            var filter = CreateFilter ();

            if (filter != null)
            {
                System.Diagnostics.Debug.WriteLine(filter.ValueForKey (new NSString ("inputCubeDimension")));
            }

            var serializer = new SimplyMobile.Text.JsonNet.JsonSerializer ();

            var userResponse = serializer.Deserialize<ServiceResponse<TestUser>> (
                TestUser.DummyResponse);

            if (userResponse.Status == "SUCCESS")
            {
                System.Diagnostics.Debug.WriteLine (userResponse.Value.Id);
                System.Diagnostics.Debug.WriteLine (userResponse.Value.Name);
                System.Diagnostics.Debug.WriteLine (userResponse.Value.Number);
            }
        }

        private static CIFilter CreateFilter()
        {
            //const unsigned int size = 64;
            const uint size = 64;

            const float minHueAngle = 0f; // change these
            const float maxHueAngle = 1f; // two

            //float *cubeData = (float *)malloc (size * size * size * sizeof (float) * 4);
            var cubeData = new float[size * size * size * 4];
            //float rgb[3], hsv[3], *c = cubeData;
            float[] rgb = new float[3], hsv = new float[3];

            // Populate cube with a simple gradient going from 0 to 1
//          for (int z = 0; z < size; z++){
//              rgb[2] = ((double)z)/(size-1); // Blue value
//              for (int y = 0; y < size; y++){
//                  rgb[1] = ((double)y)/(size-1); // Green value
//                  for (int x = 0; x < size; x ++){
//                      rgb[0] = ((double)x)/(size-1); // Red value
//                      // Convert RGB to HSV
//                      // You can find publicly available rgbToHSV functions on the Internet
//                      rgbToHSV(rgb, hsv);
//                      // Use the hue value to determine which to make transparent
//                      // The minimum and maximum hue angle depends on
//                      // the color you want to remove
//                      float alpha = (hsv[0] > minHueAngle && hsv[0] < maxHueAngle) ? 0.0f: 1.0f;
//                      // Calculate premultiplied alpha values for the cube
//                      c[0] = rgb[0] * alpha;
//                      c[1] = rgb[1] * alpha;
//                      c[2] = rgb[2] * alpha;
//                      c[3] = alpha;
//                      c += 4; // advance our pointer into memory for the next color value
//                  }
//              }
//          }
            NSData data;
            unsafe
            {
                fixed (float* ptr = cubeData)
                {
                    float* c = ptr;
                    for (var z = 0; z < size; z++)
                    {
                        rgb [2] = z / (size - 1); // Blue value
                        for (int y = 0; y < size; y++)
                        {
                            rgb [1] = y / (size - 1); // Green value
                            for (int x = 0; x < size; x++)
                            {
                                rgb [0] = x / (size - 1); // Red value
                                // Convert RGB to HSV
                                // You can find publicly available rgbToHSV functions on the Internet
                                ColorSpace.RGBtoHSV (rgb [0], rgb [1], rgb [2], out hsv [0], out hsv [1], out hsv [2]);
                                // Use the hue value to determine which to make transparent
                                // The minimum and maximum hue angle depends on
                                // the color you want to remove
                                float alpha = (hsv [0] > minHueAngle && hsv [0] < maxHueAngle) ? 0.0f : 1.0f;
                                // Calculate premultiplied alpha values for the cube
                                c[0] = rgb [0] * alpha;
                                c[1] = rgb [1] * alpha;
                                c[2] = rgb [2] * alpha;
                                c[3] = alpha;
                                c += 4;
                            }
                        }
                    }
                    // Create memory with the cube data
                    //          NSData *data = [NSData dataWithBytesNoCopy:cubeData
                    //              length:cubeDataSize
                    //              freeWhenDone:YES];
//                  IntPtr pointer = (IntPtr)ptr;
//                  data = NSData.FromBytesNoCopy (pointer, (uint)cubeData.Length, true);
                }
            }

            // Create memory with the cube data
            //          NSData *data = [NSData dataWithBytesNoCopy:cubeData
            //              length:cubeDataSize
            //              freeWhenDone:YES];
            var bytes = new List<byte> ();
            Array.ForEach(cubeData, a => bytes.AddRange(BitConverter.GetBytes(a)));
            data = NSData.FromArray(bytes.ToArray());
//          CIColorCube *colorCube = [CIFilter filterWithName:@"            CIColorCube"];
            var colorCube = CIFilter.FromName("CIColorCube");
//          [colorCube setValue:@(size) forKey:@"           inputCubeDimension"];
            colorCube.SetValueForKey (NSObject.FromObject(size), new NSString("inputCubeDimension"));
// Set data for cube
//          [colorCube setValue:data forKey:@"          inputCubeData"];
            colorCube.SetValueForKey (data, new NSString ("inputCubeData"));
            return colorCube;
        }


    }

    // courtesy of http://lotsacode.wordpress.com/2010/03/11/hsvtorgb-and-rgbtohsv-in-c/
    public static class ColorSpace
    {
        // Expects and returns values in the range 0 to 1
        public static void HSVtoRGB(double h, double s, double v, out double r, out double g, out double b)
        {
            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                double varH = h * 6;
                double varI = Math.Floor(varH);
                double var1 = v * (1 - s);
                double var2 = v * (1 - (s * (varH - varI)));
                double var3 = v * (1 - (s * (1 - (varH - varI))));

                if (varI == 0)
                {
                    r = v;
                    g = var3;
                    b = var1;
                }
                else if (varI == 1)
                {
                    r = var2;
                    g = v;
                    b = var1;
                }
                else if (varI == 2)
                {
                    r = var1;
                    g = v;
                    b = var3;
                }
                else if (varI == 3)
                {
                    r = var1;
                    g = var2;
                    b = v;
                }
                else if (varI == 4)
                {
                    r = var3;
                    g = var1;
                    b = v;
                }
                else
                {
                    r = v;
                    g = var1;
                    b = var2;
                }
            }
        }

        // Expects and returns values in the range 0 to 1
        public static void RGBtoHSV(float r, float g, float b, out float h, out float s, out float v)
        {
            var varMin = Math.Min(r, Math.Min(g, b));
            var varMax = Math.Max(r, Math.Max(g, b));
            var delMax = varMax - varMin;

            v = varMax;

            if (delMax == 0)
            {
                h = 0;
                s = 0;
            }
            else
            {
                var delR = (((varMax - r) / 6) + (delMax / 2)) / delMax;
                var delG = (((varMax - g) / 6) + (delMax / 2)) / delMax;
                var delB = (((varMax - b) / 6) + (delMax / 2)) / delMax;

                s = delMax / varMax;

                if (r == varMax)
                {
                    h = delB - delG;
                }
                else if (g == varMax)
                {
                    h = (1.0f / 3) + delR - delB;
                }
                else //// if (b == varMax) 
                {
                    h = (2.0f / 3) + delG - delR;
                }

                if (h < 0)
                {
                    h += 1;
                }

                if (h > 1)
                {
                    h -= 1;
                }
            }
        }
    }
}

