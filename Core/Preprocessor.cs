using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Satellite_Image_Processor.Core
{
    public static class Preprocessor
    {
        public static void Process( string path )
        {
            try
            {
                var src = Cv2.ImRead( path, ImreadModes.Color );
                if ( src.Empty( ) )
                {
                    Console.WriteLine( "[전처리 실패] 이미지가 비어 있음" );
                    return;
                }

                string outputDir = Path.Combine( Environment.CurrentDirectory, "Processed" );
                if ( !Directory.Exists( outputDir ) )
                    Directory.CreateDirectory( outputDir );

                Mat gray = new( );
                Cv2.CvtColor( src, gray, ColorConversionCodes.BGR2GRAY );

                Mat equalized = new( );
                Cv2.EqualizeHist( gray, equalized );

                Cv2.GaussianBlur( equalized, equalized, new Size( 5, 5 ), 0 );

                Mat binary = new( );
                Cv2.Threshold( equalized, binary, 120, 255, ThresholdTypes.Binary );

                Mat morph = new( );
                Mat kernel = Cv2.GetStructuringElement( MorphShapes.Rect, new Size( 3, 3 ) );
                Cv2.MorphologyEx( binary, morph, MorphTypes.Open, kernel );

                string filename = Path.GetFileNameWithoutExtension( path );
                string outputPath = Path.Combine( outputDir, filename + "_processed.png" );
                Cv2.ImWrite( outputPath, equalized );

                Console.WriteLine( $"[전처리 완료] → {outputPath}" );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"[전처리 오류] {ex.Message}" );
            }
        }
    }
}
