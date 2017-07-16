- If you happen to be a steam user this application currently won't launch the Game (I will add steam compability later).

-/ Installation:
Just start the Imperialware.exe from anywhere on your disc, please make sure that "Imperialware_Installation.zip" 
is in the same directory when it starts first time, or it will install in minimal mode without some Dashboards and 
the Library images of mods will be in less good quality.

Please have a look at my video tutorial Series (over 2 hours of detailed explanation) to learn program usage:



In the "settings" tab you can scroll down to "Imperialware Folder Path", there you can type anytime a new diretctory path and press the "Set" button
in order to move the program directory to a new place.

Steam Users currently need to click "game\eaw and foc Game Path" in order to type the path to their game 
into the "Folder Path" boxes. Which will mostly be something like
C:\Program Files (x86)\Steam\steamapps\common\Star Wars Empire at War\corruption



-/ First of all: Setting a Mod

- Go to the "Mods" tab at the top
- If you can't see any mod for selection in the selection box at the left side, you can choose whether the eaw or the foc checkbox.
I assume you already got any mod installed, just choose its Name and then please hit the "Set Mod" button.
- Note: You can select the "No Mod" option to start the vanilla eaw or foc game or even auto apply mods to the EAW Terrain Editor.

-/ Launching the selected Mod:
- Go to "Launch" tab and have a look at the text at the bottom left corner:
- At the very bottom is the used Game: EAW or FOC
- One level over it is the selected Mod
- One level higer then the mod, is the loaded Addon inside of that Mod (structured like a pyramid)

- It will launch for CD and GOG version users, but currently Launching is not supported for Steam users (working on it), but the Cheats 
and everything else indeed work for Steam version as well.
- Now if you hit the big "Launch" button, you start what ever text you see there.
- If you hit "Mod only" it will launch only the Mod, despite of a Addon being visible over it.
- If you hit "Addon + Mod" it will aviously load the Mod and its Addon, this is useful if the last launch was Mod only.
- Note: If you don't use them you can hide the 2 small buttons in the Settings/User Interface/Show Mod Launch Button or Addon Launch Button
- You can't currently launch any mods for the Steam version, not sure about Gog versions, here I'll need feedback for Gog versions of EAW.



-/ Cheating:
The following 4 buttons overwrite all cheats that were made before:
Destroy, Teleport, Spawn and Give (Money), please be aware that when you click one of these buttons it will delete all previously programmed cheats
and replace them for a new cheat. So these cheats overwrite eachother and also themselves.

- Click the "Manage" tab.
- Click the Combobox "Faction" then choose any faction. The program wil automatically find all factions in the selected mod, you can also just leave
"Human_Player" which is a universal variable and lets you always spawn units on your side of the field on any planet.

- At the selection for the planets the simplest way is to select the "Build Planet" option, 
- Optionally: You could also choose a certain target planet: Select "Target Planet" option and hit "Galaxy" of the Segment Button,
then choose any GC and select any target planet from the list then hit the "Select Button".
- Note: You can also select any GC Map or Planet and press "Destroy", once you build the Empire dummy ingame the specified planet or Galaxy will explode.

- Click the Units Combobox and choose "SpaceUnit", "UniqueUnit" or any other type.
- There you can choose the ingame Unit Names (any unit you desire to cheat) just hit the "+" button
in oder to add it to the spawn list below.
- Once you selected enough ships for cheating just click once the "Spawn" button. --> Caution, this will cut the spawnlist away to use it, 
so if you wish to spawn again you need to repeat the unit selection process each time.
- Note: Dont "Give" Credits money, or it will overwrite the spawn cheat. Because each cheat overwrites its predecessor!


- If the game was running by now please restart the game now (only for first time using Imperialware of each new mod) then
- Select the "Launch" tag and click the "Launch" button.

- Once you are ingame, please search for the "Empire" symbol with the name "Load from Hyperspace".
Build that unit on the planet where you want the units to spawn.

- You can use it to cheat spawn anything and in any quantity, I still suggest to not cheat too much because if the game is to easy its no longer fun.




-/ Editing/Creating Units:

- Similar to the Cheating process, you just open the "Manage" tab and select any unit type in the Units dropdown Box.
- Then you highlight any Unit in the list below and click whether "Edit" (full UI support) or "Edit Xml" (Integrated XMl Editor = WIP)
- If you chose "Edit" that Unit xml will automatially be found in the Xml directory of the Selected Mod 
and it can be edited via the UI Settings below.
- Note: The UI is not fully finished, Hardpoints and many other tags for Projectiles are still to be added to the UI.
- Imperialware will automatically add ".alo" as extansion to the model Tag, and ".tga" to textures which means you can always leave the extension away in Imperialware. 
Further if you click the ".alo" it will open the chosen model in the Alamo editor for you. (.alo files need to be assigned to that alo viewer in 
C:\Program Files\Imperialware\Misc\Modding_Tools in order to work)
- clicking on the ".tga" and ".dat" text will open the Mtd editor and dat editor. The Art section is used here as central hub to access the petroglyph modding tools.


- Once you're done editing it you can finally scroll down to the bottom of the Manage tab 
- Note: Before saving you have the option (please only 1x when creating that xml) to select whether "GameObjectFiles.xml" or "HardpointDataFiles.xml" Checkbox
that will automatically append the name of the new .xml to whether one or both of these .xmls for the sake of your comfort.
As all new Unit xmls need to be registered inside of these two files in order to be recognized by the game.

- Then you're ready to "Save" or "Save As" using whether the buttons at the very bottom of the Manage UI or the two small ones at the very top left and right side.
- You can also directly hit "Test Ingame", that causes Imperialware to Launch the game and to apply your new unit as
Cheated one, you only need to build the Empire Symbol ingame in order to get it for test purposes.




-/ Editing Mods in Map Editor:

- In the "Mods" tag you can select any of your installed mods and hit "Map Editor".
- If "Settings/Copy into Art directory" is not checked it will only move the Model and Texture directory (and Xml) from the mod 
into the directory of the Vanilla Game and start that mod in the Map editor. (before you needed a extra copy of that mod on your disc)
Then once any mod is launched using Imperialware, the program will move all directories back to their original mod.

- If "Settings/Copy into Art directory" is checked it will copy the whole Mod directory (safer but needs more discspace) only the first time into the area for Map editor.



-/ Using Modmods (Addons)
- ModMods are submods for bigger mods, often created by other persons then the original develloper of the host mod.
- In the "Apps" tab you can toggle between "Mods" and "Addons" mode, the UI will show one of them at a time.
- If you select any app you can click the "?" sign, which starts your browser and leads you to the download section on Moddb
of the selected object.
- After downlaoding Apps you can hit "Load" (make sure to be in Addon mode) and browse the directory of your addon
Imperialware is supposed to apply that addon to its respective Host mod (depending on Imperialware Directory/Addons/Addon_Name/Info.txt)

-/ Installing new Mods:
To get (well rated) new mods you can select them in the "Apps" tag while in Mods mode and hit the "?" sign, after downloading them
You can click the "Game" tab and choose the "Game Path" of whether EAW or FOC, then you hit "Open" 
- It will lead you into the mods directory of that game where you simply can drop the downloaded games.
- Note: 
The "Savegames" Open button brings you into the savegame directory of your Windows account, there Imperialware generates a Cache for the last 10 days of playing
(it stores always only the first session of each day). If your game happens to screw your Saves you can hit Open and choose EAW or FOC Backup directory.
- In the "Game" tab there is also a checkbox under that savegame direcotry path that allows you to use a seperate directory for each mod, which means you don't longer 
need to bother with savegames merged from different mods.

- In the Game tab you also can choose costumized Dashboards and a alternative color for the Vanilla Game UI.





- Please give me feedback so I can improve this application.

Enjoy ^^




