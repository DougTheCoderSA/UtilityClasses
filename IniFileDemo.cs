static void Main(string[] args)
{
    IniFile ini = new IniFile();

    // Load and existing file
    // ini.Load("C:\\temp\\ini2open.ini");

    ini.AddSection("TEST_SECTION").AddKey("Key1").Value = "Value1";
    ini.AddSection("TEST_SECTION").AddKey("Key2").Value = "Value2";
    ini.AddSection("TEST_SECTION").AddKey("Key3").Value = "Value3";
    ini.AddSection("TEST_SECTION").AddKey("Key4").Value = "Value4";
    ini.AddSection("TEST_SECTION").AddKey("Key5").Value = "Value5";
    ini.AddSection("TEST_SECTION").AddKey("Key6").Value = "Value6";
    ini.AddSection("TEST_SECTION").AddKey("Key7").Value = "Value7";

    ini.AddSection("TEST_SECTION_2").AddKey("Key1").Value = "Value1";
    ini.AddSection("TEST_SECTION_2").AddKey("Key2").Value = "Value2";
    ini.AddSection("TEST_SECTION_2").AddKey("Key3").Value = "Value3";
    ini.AddSection("TEST_SECTION_2").AddKey("Key4").Value = "Value4";
    ini.AddSection("TEST_SECTION_2").AddKey("Key5").Value = "Value5";
    ini.AddSection("TEST_SECTION_2").AddKey("Key6").Value = "Value6";
    ini.AddSection("TEST_SECTION_2").AddKey("Key7").Value = "Value7";

    // Key Rename Test
    Trace.Write("Key Rename Key1 -> KeyTemp Test: ");
    if (ini.RenameKey("TEST_SECTION", "Key1", "KeyTemp"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Section Rename Test
    Trace.Write("Test section rename TEST_SECTION -> TEST_SECTION_3: ");
    if (ini.RenameSection("TEST_SECTION", "TEST_SECTION_3"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Check Key Rename Post Section Rename
    Trace.Write("Test TEST_SECTION_3 rename key KeyTemp -> Key1: ");
    if (ini.RenameKey("TEST_SECTION_3", "KeyTemp", "Key1"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Check Section Rename Post Section Rename
    Trace.Write("Test section rename TEST_SECTION_3 -> TEST_SECTION: ");
    if (ini.RenameSection("TEST_SECTION_3", "TEST_SECTION"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Check Key Rename Key1 -> Key2 where Key2 exists
    Trace.Write("Test TEST_SECTION key rename Key1 -> Key2 where Key2 exists: ");
    if (ini.RenameKey("TEST_SECTION", "Key2", "Key1"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Check Key Rename
    Trace.Write("Test TEST_SECTION key rename Key2 -> Key2Renamed: ");
    if (ini.RenameKey("TEST_SECTION", "Key2", "Key2Renamed"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Test rename other section
    Trace.Write("Test section rename TEST_SECTION_2 -> TEST_SECTION_1 : ");
    if (ini.RenameSection("TEST_SECTION_2", "TEST_SECTION_1"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Check remove key
    Trace.Write("Test TEST_SECTION_1 remove key Key1: ");
    if (ini.GetSection("TEST_SECTION_1").RemoveKey("Key1"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Check remove key no exist
    Trace.Write("Test TEST_SECTION_1 remove key Key1: ");
    if (ini.GetSection("TEST_SECTION_1").RemoveKey("Key1"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Check remove section
    Trace.Write("Test remove section TEST_SECTION_1: ");
    if (ini.RemoveSection("TEST_SECTION_1"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    // Check remove section no exist
    Trace.Write("Test remove section TEST_SECTION_1: ");
    if (ini.RemoveSection("TEST_SECTION_1"))
        Trace.WriteLine("Pass");
    else
        Trace.WriteLine("Fail");

    //Save the INI
    ini.Save("C:\\temp\\test.ini");
}