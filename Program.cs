using System;
using System.IO;

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
        Console.WriteLine( $"\n[감지됨] 파일 생성: {e.Name}" );
    }
}
