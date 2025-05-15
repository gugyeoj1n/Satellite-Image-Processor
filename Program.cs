using System;
using System.IO;
using Satellite_Image_Processor.Core;

class Program
{
    static void Main( )
    {
        Console.WriteLine( "=== 아리랑 위성 영상 처리 시스템 시작 ===" );
        var watcher = new FileWatcher( );
        watcher.StartWatching( );

        Console.WriteLine( "\n종료하려면 아무 키나 누르세요." );
        Console.ReadKey( );
    }
}
