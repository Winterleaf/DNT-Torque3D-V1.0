//-----------------------------------------------------------------------------
// Torque
// Copyright GarageGames, LLC 2011
//-----------------------------------------------------------------------------

// Set the name of our application
$appName = "FPS Example";

// The directory it is run from
$defaultGame = "scripts";

// Set profile directory
$Pref::Video::ProfilePath = "core/profile";
 //enableWinConsole(true);

function createCanvas(%windowTitle)
{
   //error("--------------------->createCanvas");
   if ($isDedicated)
   {
      GFXInit::createNullDevice();
      return true;
   }

   // Create the Canvas
   %foo = new GuiCanvas(Canvas);
   
   // Set the window title
   if (isObject(Canvas))
      Canvas.setWindowTitle(getEngineName() @ " - " @ $appName);
   
   return true;
}

// Display the optional commandline arguements
$displayHelp = false;

// Use these to record and play back crashes
//saveJournal("editorOnFileQuitCrash.jrn");
//playJournal("editorOnFileQuitCrash.jrn", false);

//------------------------------------------------------------------------------
// Check if a script file exists, compiled or not.
function isScriptFile(%path)
{
   //error("--------------------->isScriptFile " @ %path);
   if( isFile(%path @ ".dso") || isFile(%path) )
      return true;
   
   return false;
}

//------------------------------------------------------------------------------
// Process command line arguments

dnEval("main.Init_ParseArgs()");
//exec("core/parseArgs.cs");

$isDedicated = false;
$dirCount = 2;
$userDirs = $defaultGame @ ";art;levels";

// load tools scripts if we're a tool build
if (isToolBuild())
    $userDirs = "tools;" @ $userDirs;


// Parse the executable arguments with the standard
// function from core/main.cs

defaultParseArgs();


if($dirCount == 0) {
      $userDirs = $defaultGame;
      $dirCount = 1;
}

//-----------------------------------------------------------------------------
// Display a splash window immediately to improve app responsiveness before
// engine is initialized and main window created
if (!$isDedicated)
   displaySplashWindow();


//-----------------------------------------------------------------------------
// The displayHelp, onStart, onExit and parseArgs function are overriden
// by mod packages to get hooked into initialization and cleanup.

function onStart()
{
error("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!-------------------------------->Default onStart()");
   // Default startup function
}

function onExit()
{
   error("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!-------------------------------->Default onExit()");
   // OnExit is called directly from C++ code, whereas onStart is
   // invoked at the end of this file.
}

function parseArgs()
{
   // Here for mod override, the arguments have already
   // been parsed.
}

function compileFiles(%pattern)
{  
//   error("-------------------------->compileFiles()");
   %path = filePath(%pattern);

   %saveDSO    = $Scripts::OverrideDSOPath;
   %saveIgnore = $Scripts::ignoreDSOs;
   
   $Scripts::OverrideDSOPath  = %path;
   $Scripts::ignoreDSOs       = false;
   %mainCsFile = makeFullPath("main.cs");

   for (%file = findFirstFileMultiExpr(%pattern); %file !$= ""; %file = findNextFileMultiExpr(%pattern))
   {
      // we don't want to try and compile the primary main.cs
      if(%mainCsFile !$= %file)      
         compile(%file, true);
   }

   $Scripts::OverrideDSOPath  = %saveDSO;
   $Scripts::ignoreDSOs       = %saveIgnore;
   
}

if($compileAll)
{
   echo(" --- Compiling all files ---");
   compileFiles("*.cs");
   compileFiles("*.gui");
   compileFiles("*.ts");  
   echo(" --- Exiting after compile ---");
   quit();
}

if($compileTools)
{
   echo(" --- Compiling tools scritps ---");
   compileFiles("tools/*.cs");
   compileFiles("tools/*.gui");
   compileFiles("tools/*.ts");  
   echo(" --- Exiting after compile ---");
   quit();
}

package Help {
   function onExit() {
      // Override onExit when displaying help
   }
};

function displayHelp() {
   activatePackage(Help);

      // Notes on logmode: console logging is written to console.log.
      // -log 0 disables console logging.
      // -log 1 appends to existing logfile; it also closes the file
      // (flushing the write buffer) after every write.
      // -log 2 overwrites any existing logfile; it also only closes
      // the logfile when the application shuts down.  (default)

   error(
      "Torque Demo command line options:\n"@
      "  -log <logmode>         Logging behavior; see main.cs comments for details\n"@
      "  -game <game_name>      Reset list of mods to only contain <game_name>\n"@
      "  <game_name>            Works like the -game argument\n"@
      "  -dir <dir_name>        Add <dir_name> to list of directories\n"@
      "  -console               Open a separate console\n"@
      "  -show <shape>          Deprecated\n"@
      "  -jSave  <file_name>    Record a journal\n"@
      "  -jPlay  <file_name>    Play back a journal\n"@
      "  -jDebug <file_name>    Play back a journal and issue an int3 at the end\n"@
      "  -help                  Display this help message\n"
   );
}


//--------------------------------------------------------------------------

// Default to a new logfile each session.
if( !$logModeSpecified )
{
   if( $platform !$= "xbox" && $platform !$= "xenon" )
      setLogMode(6);
}

// Get the first dir on the list, which will be the last to be applied... this
// does not modify the list.
nextToken($userDirs, currentMod, ";");

// Execute startup scripts for each mod, starting at base and working up
function loadDir(%dir)
{
   pushback($userDirs, %dir, ";");
   if (isScriptFile(%dir @ "/main.cs"))
      {
      //echo("----------->Executing file '" @ %dir @ "/main.cs'");         
      exec(%dir @ "/main.cs");
      }
}


function loadDirs(%dirPath)
{
   %dirPath = nextToken(%dirPath, token, ";");
   if (%dirPath !$= "")
      loadDirs(%dirPath);
   echo("----------->Executing file '" @ %token @ "/main.cs'");
   if(exec(%token @ "/main.cs") != true)
   {
      error("Error: Unable to find specified directory: " @ %token );
      $dirCount--;
   }
}


//Right here is where it load scripts/main.cs
//in the future, when all the code is converted
//You will have a script inject here.
echo("-------------------------------->Loading Directory '" @ $userDirs @ "'---------------------------");
dnEval("main.LoadMainInit()");
//error("Done Calling LoadMainInit");
loadDirs($userDirs);
//echo("-------------------------------->Done Loading Directory '" @ $userDirs @ "'---------------------------");

if($dirCount == 0) {
   enableWinConsole(true);
   error("Error: Unable to load any specified directories");
   //quit();
}

// Parse the command line arguments
echo("--------- Parsing Arguments ---------");
parseArgs();

// Either display the help message or startup the app.
if ($displayHelp) {
   enableWinConsole(true);
   displayHelp();
   quit();
}
else {
   onStart();
   echo("Engine initialized...");
   
   // Auto-load on the 360
   if( $platform $= "xenon" )
   {
      %mission = "levels/Deathball Desert.mis";
      
      echo("Xbox360 Autoloading level: '" @ %mission @ "'");
      
      
      if ($pref::HostMultiPlayer)
         %serverType = "MultiPlayer";
      else
         %serverType = "SinglePlayer";

      createAndConnectToLocalServer( %serverType, %mission );
   }
}

// Display an error message for unused arguments
for ($i = 1; $i < $Game::argc; $i++)  {
   if (!$argUsed[$i])
      error("Error: Unknown command line argument: " @ $Game::argv[$i]);
}

// Automatically start up the appropriate eidtor, if any
if ($startWorldEditor) {
   Canvas.setCursor("DefaultCursor");
   Canvas.setContent(EditorChooseLevelGui);
} else if ($startGUIEditor) {
   Canvas.setCursor("DefaultCursor");
   Canvas.setContent(EditorChooseGUI);
}


