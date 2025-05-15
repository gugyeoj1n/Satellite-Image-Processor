using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Satellite_Image_Processor.Core
{
    public static class MetadataExtractor
    {
        public static void PrintBasicMetadata( string path )
        {
            try
            {
                var info = new FileInfo( path );
                WaitUntilFileIsReady( path );

                Console.WriteLine( $"  ├ 용량       : {info.Length / ( 1024.0 * 1024.0 ):F2} MB" );
                Console.WriteLine( $"  ├ 확장자     : {info.Extension}" );
                Console.WriteLine( $"  ├ 생성 시각  : {info.CreationTime}" );

                if ( info.Extension is ".jpg" or ".jpeg" or ".png" or ".bmp" )
                {
                    using var img = Image.FromFile( path );
                    Console.WriteLine( $"  └ 해상도     : {img.Width} x {img.Height}" );
                }
                else
                {
                    Console.WriteLine( $"  └ 해상도     : (지원되지 않는 형식)" );
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"[오류] 메타데이터 추출 실패: {ex.Message}" );
            }
        }

        private static void WaitUntilFileIsReady( string path )
        {
            while ( true )
            {
                try
                {
                    using FileStream stream = File.Open( path, FileMode.Open, FileAccess.Read, FileShare.None );
                    if ( stream.Length > 0 )
                        break;
                }
                catch { }

                Thread.Sleep( 200 );
            }
        }
    }
}
