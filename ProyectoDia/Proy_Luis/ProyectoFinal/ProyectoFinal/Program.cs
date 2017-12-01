using System;

namespace ViajesIU
{
    using ViajesIU.View;
    class MainClass
    {
        public static void Main(string[] args)
        {
            Gtk.Application.Init();
            var win = new MainWindow();
            win.ShowAll();
            Gtk.Application.Run();
        }
    }
}
