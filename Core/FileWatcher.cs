using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite_Image_Processor.Core
{
    public class FileWatcher
    {
        private readonly string _watchPath = Path.Combine( Environment.CurrentDirectory, "Incoming" );
        private FileSystemWatcher _watcher; // ✔️ 필드로 선언

        public void StartWatching( )
        {
            if ( !Directory.Exists( _watchPath ) )
            {
                Directory.CreateDirectory( _watchPath );
                Console.WriteLine( $"[폴더 생성] {_watchPath}" );
            }

            _watcher = new FileSystemWatcher( _watchPath );
            _watcher.Filter = "*.*";
            _watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            _watcher.Created += OnFileCreated;
            _watcher.EnableRaisingEvents = true;

            Console.WriteLine( $"[감시 시작] {_watchPath}" );
            Console.WriteLine( "영상 파일을 폴더에 추가해보세요..." );
        }

        private void OnFileCreated( object sender, FileSystemEventArgs e )
        {
            Console.WriteLine( $"\n[감지됨] {e.Name}" );

            MetadataExtractor.PrintBasicMetadata( e.FullPath );
            Preprocessor.Process( e.FullPath );
        }
    }
}