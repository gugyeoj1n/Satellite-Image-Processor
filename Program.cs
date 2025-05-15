using System;
using System.IO;
using OpenCvSharp;

class Program
{
    static void Main( )
    {
        Console.WriteLine( "=== 위성 영상 수신 감지 시스템 ===" );

        string watchPath = Path.Combine( Environment.CurrentDirectory, "Incoming" );
        if ( !Directory.Exists( watchPath ) )
        {
            Directory.CreateDirectory( watchPath );
            Console.WriteLine( $"폴더 생성됨: {watchPath}" );
        }

        using var watcher = new FileSystemWatcher( watchPath );
        watcher.Filter = "*.*";
        watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;

        watcher.Created += OnFileCreated;
        watcher.EnableRaisingEvents = true;

        Console.WriteLine( $"감시 시작: {watchPath}" );
        Console.WriteLine( "위성 영상 파일을 폴더에 넣어보세요..." );
        Console.WriteLine( "종료하려면 아무 키나 누르세요." );
        Console.ReadKey( );
    }

    static void OnFileCreated( object sender, FileSystemEventArgs e )
    {
        try
        {
            var fileInfo = new FileInfo( e.FullPath );

            WaitUntilFileIsReady( e.FullPath );

            Console.WriteLine( $"\n[파일 감지 완료] {fileInfo.Name}" );
            Console.WriteLine( $"  ├ 경로       : {fileInfo.FullName}" );
            Console.WriteLine( $"  ├ 용량       : {fileInfo.Length / ( 1024.0 * 1024.0 ):F2} MB" );
            Console.WriteLine( $"  ├ 확장자     : {fileInfo.Extension}" );
            Console.WriteLine( $"  ├ 생성 시각  : {fileInfo.CreationTime}" );

            // 해상도 출력 (가능한 경우)
            if ( fileInfo.Extension is ".jpg" or ".jpeg" or ".png" or ".bmp" )
            {
                using var image = System.Drawing.Image.FromFile( e.FullPath );
                Console.WriteLine( $"  └ 해상도     : {image.Width} x {image.Height}" );
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
        ProcessImage( e.FullPath );
    }

    static void WaitUntilFileIsReady( string path )
    {
        while ( true )
        {
            try
            {
                using FileStream stream = File.Open( path, FileMode.Open, FileAccess.Read, FileShare.None );
                if ( stream.Length > 0 )
                    break;
            }
            catch
            {
                
            }

            Thread.Sleep( 200 );
        }
    }

    static void ProcessImage( string path )
    {
        try
        {
            string processedDir = Path.Combine( Environment.CurrentDirectory, "Processed" );
            if ( !Directory.Exists( processedDir ) )
                Directory.CreateDirectory( processedDir );

            var src = Cv2.ImRead( path, ImreadModes.Color );
            if ( src.Empty( ) )
            {
                Console.WriteLine( "[오류] 이미지를 불러올 수 없습니다." );
                return;
            }

            Mat gray = new( );
            Cv2.CvtColor( src, gray, ColorConversionCodes.BGR2GRAY );

            Mat thresholded = new( );
            Cv2.Threshold( gray, thresholded, 180, 255, ThresholdTypes.Binary );

            Mat equalized = new( );
            Cv2.EqualizeHist( thresholded, equalized );

            string filename = Path.GetFileNameWithoutExtension( path );
            string outPath = Path.Combine( processedDir, filename + "_processed.png" );
            Cv2.ImWrite( outPath, equalized );

            Console.WriteLine( $"[전처리 완료] → {outPath}" );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"[오류] 전처리 중 실패: {ex.Message}" );
        }
    }
}
