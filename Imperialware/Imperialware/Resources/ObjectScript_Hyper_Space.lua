--==================================================================================================
--*************************************** Hyperspace Script ****************************************
--************************************** Written by Imperial ***************************************

-- ****************** This script is allowed to be used as template for other mods *****************
--==================================================================================================


require("PGStateMachine")
require("PGStoryMode")
--=================== Definitions ===================
function Definitions()  

   -- This defines how fast the "OnUpdate" states will refresh, 0.1 means 1 second and 0.05 is half a second..
   -- Without writing this variable, the game uses a stock value of 0.05 to refresh twice a second 
   ServiceRate = 0.1

   -- Every State has to be innitiated here or it wont work
	Define_State("State_Init", State_Init)      
   Define_State("State_Inactive", State_Inactive) 
	Define_State("State_Active", State_Active)  
                                                                                            
   
   -- Counter starts at 0, the ability stops working when the counter reached "maximal_usage_limit"
   ability_used_counter = 0
   maximal_usage_limit = 200   
   Auto_Jumped = false
   Autojump_Escape = false
   Hyperspace_Is_Inhibited = false
   
   -- This HAS TO be the same as in the xml file (but with ?.0 decimal value) or it can crash script
   Ability_Cooldown = 120.0
   
   -- Name of the Engine Hardpoint of a unit: So it can be destroyed after jumping 3x and overheating:
   -- Name_of_Engine_Hardpoint = "HP_Cheating_Dummy"
   
   
   -- These are the sound effects used by the 1st ability
   --Hyper_Jump_Out = "Hyper_Jump_Out"
   --Hyper_Jump_In = "Hyper_Jump_In"
   --Hyper_Drive_Blown = "Hyper_Drive_Blown"
   Hyperspace_Jump_Target = "Hyperspace_Jump_Target"
                       
end


--=================== State_Init ===================
function State_Init(message)
	if message == OnEnter then
   
      -- If we are not in space battle mode, exit the script to prevent bugs:
      if Get_Game_Mode() ~= "Space" then
			ScriptExit()
		end
      
             
      if Object.Get_Type() == Find_Object_Type("Hyperspace_Huge")
       or Object.Get_Type() == Find_Object_Type("Hyperspace_Large") 
        or Object.Get_Type() == Find_Object_Type("Hyperspace_Medium")       
         or Object.Get_Type() == Find_Object_Type("Hyperspace_Small") then                  
      
         Sleep(4)
         Object.Despawn()
         ScriptExit()   
      end
      
      
      
       -- This decides whether the unit uses its Hyper Jump to jump away or to jump towards enemys
      local Decide = GameRandom(1,2)
      if Decide == 1 then
         Tactics = true
      elseif Decide == 2 then       
         Tactics = false
      end    
      
            
      ability_1 = "WEAKEN_ENEMY"
       
      Object.Hide(true)
      -- IMPORTANT: Have to wait or for rebel it will autoactivate ability and cause scriptfreeze, no idea why..  
      Sleep (2.0)
       
      
      -- Generating hyperspace window when the unit enters a map     
      -- The Size of the Particle used for the Hyperspace Window is depending on unit class. If you want
      -- bigger or smaller particle for a xml just change its <CategoryMask> according to this:
      if Object.Is_Category("Capital") then
           Hyperspace_Window = {"Hyperspace_Large"}
         elseif Object.Is_Category("Frigate") then
           Hyperspace_Window = {"Hyperspace_Medium"} 
         else Hyperspace_Window = {"Hyperspace_Small"}  
      end       
                
      -- Spawning Hyperspace Particle when entering Level, used "SpawnList" instead of "Create_Generic_Object" 
      -- So the particle spawns behind the ship, instead of right into it: 
      SpawnList(Hyperspace_Window, Object, Object.Get_Owner(), true, true)  
           
      -- Now that Hyper window showed up we can unhide
      Object.Hide(false) 
      
      
      -- Exiting for all units without Hyperjump Ability               
      if not Object.Has_Ability(ability_1) then
      	ScriptExit()
      end
      
      -- IMPORTANT: Have to wait or it will autoactivate ability and cause scriptcrash, no idea why..  
      Sleep(3)
                                   
      Set_Next_State("State_Inactive")    
  end
end
--================================= Abilities =================================

function State_Inactive(message)
	if message == OnUpdate then Sleep(GameRandom(1,2))
	  
     
       --================== Interdict Ability Check ==================== 
       -- Re-Searching for all ships (the first line counts only enemy units, the 2nd one checks all bigger units on the field)
       -- All_Enemy_Units = Find_All_Objects_Of_Type(Object.Get_Owner().Get_Enemy())  
       All_Enemy_Units = Find_All_Objects_Of_Type("Corvette | Frigate | Capital") 
              
       -- For all Enemy Units, do
       for each, Unit in pairs(All_Enemy_Units) do
          if Unit.Is_Ability_Active("Interdict") then
             -- Then the Ability was inhibited by a Interdict Ability
             Hyperspace_Is_Inhibited = true 
             Object.Reset_Ability_Counter() 
                     
          -- Otherwise the Ability can fire    
          elseif Unit.Is_Ability_Ready("Interdict") then
             Hyperspace_Is_Inhibited = false            
          end    
       end
       --================== AI Auto Activation ====================              
       -- If the Owner of this Unit is not Human (=AI) and a Enemy is nearby
       if not Object.Get_Owner().Is_Human() and Object.Has_Ability(ability_1)         
        -- And maximal usage wasnt reached
         and ability_used_counter < maximal_usage_limit 
          and Hyperspace_Is_Inhibited == false then  
   
                           
         if Tactics then  
            -- Starting Thread for Auto Hyperjump  
            Auto_Hyperjump_Thread = Create_Thread("Auto_Hyperjump")                   
         -- The Escape jump happens if the unit got too much damage  
         elseif Tactics == false and Object.Get_Hull() < 0.8 then
            Autojump_Escape = true
            Auto_Hyperjump_Thread = Create_Thread("Auto_Hyperjump") 
         end                                                 
      end  
      --================== Ability 1 ==================== 
      -- Wont work unless the unit has the required ability ready to run!
      if Object.Has_Ability(ability_1) and not Object.Is_Ability_Ready(ability_1)	
        -- As long the first variable is still smaller or the same as the second variable the effect thread starts.	  
         and ability_used_counter < maximal_usage_limit and Hyperspace_Is_Inhibited == false then 
            -- Using the state of active ability:
            Set_Next_State("State_Active")                       
         end  
       
   end
end		

--=================== State_Active ===================
--We will remain in this state until the cooldown of the ability is over
function State_Active(message)
   if message == OnEnter then
          
     
      -- Gathering position and owner fraction of the ship:
      local position_before_jump = Object.Get_Position()  
      local Unit_Owner = Object.Get_Owner()
      
      -- Get position of the spawned object 
      Target_Position = Find_First_Object(Hyperspace_Jump_Target) 
      
      -- Calculating Distance to the Target, 
      Distance_to_Target = Object.Get_Distance(Target_Position)
      
      if Distance_to_Target > 3700 and Auto_Jumped == false then
         -- View the ship jumping away if it is jumping out of sight.
         Point_Camera_At(Object)  
      end 

      -- Preventing further damage that might destroy the ship
      Object.Make_Invulnerable(true)
         
      -- Spawn marker of the position on the minimap for 1 sec:
      Add_Radar_Blip(Target_Position, "Marker_on_Radar")       
      
      -- Spawning a Hyperspace out particle inplace of the ship: 
      Hyperspace_Particle_01 = SpawnList(Hyperspace_Window, position_before_jump, Unit_Owner, true, true)
    
      -- Sending the ship towards its target so it has the right xy axis rotation
      Object.Move_To(Target_Position)

      -- Waiting until the jump out was shown 
      Sleep(0.8)
    
      -- Rotating the ship towards the point where it falls out of hyperspace
      Object.Face_Immediate(Target_Position)
      
      -- Teleporting the ship where the player pointed it to go:
      Object.Teleport(Target_Position)
    
      Remove_Radar_Blip("Marker_on_Radar")   
      
      -- Playing sounds for Hyperspace in (Unfortunately this leads often to script crashes, especially with 2x gamespeed)           
      -- Object.Play_SFX_Event(Hyper_Jump_Out)
      
      local position_after_jump = Object.Get_Position()      
           
      -- Temporaly hiding the hull of the spawned ship so the hyperspace effect is the only visible thing:
      Hide_Object(Object, 1)
      -- Unselect the unit so the select box wont show up.
      Object.Set_Selectable(false)  
      
      Sleep(0.8) 
      -- Showing where the ship came out if it is outside of the screen borders.  
      if Distance_to_Target > 3700 and Auto_Jumped == false then   
         Point_Camera_At(Object)   
      end    
      
      -- Spawning a Hyperspace in window to the landing coordinates of the ship:
      Hyperspace_Particle_02 = SpawnList(Hyperspace_Window, position_before_jump, Unit_Owner, true, true)
      
      -- Playing sound for Hyperspace out
      -- Object.Play_SFX_Event(Hyper_Jump_In)
             
      -- Wait again a bit until the hyperspace window was seen:
      Sleep(1.0)
      
      -- Making the ship visible again:
      Hide_Object(Object, 0)
      
      -- Backup function of the line above, for the case it fails the first time
      Sleep(0.1) 
      Hide_Object(Object, 0)
         
      Object.Make_Invulnerable(false) 
      
      -- Selecting the Unit again
      Object.Set_Selectable(true) 
      Unit_Owner.Select_Object(Object)                        
      
      -- Increasing ability count by 1 (relevant for maximal usement)
      ability_used_counter = ability_used_counter + 1      
      
      -- Disabled in order to keep the cooldown time  
      -- Re-enabeling the ability for next usement, as long the maximal usage limit wasnt reached.
      -- if ability_used_counter < maximal_usage_limit then
        -- Object.Reset_Ability_Counter()
      -- end 
      
      if Auto_Jumped then
         Auto_Jumped = false
         
         -- Following the target
         Object.Move_To(Enemy_Units[Random_Number_2])  
         
         -- This adds a fake cooldown time if the AI autojumped, it also CRASHES THE SCRIPT for any other then 1 unit      
         Sleep (120.0)        
      end
      
      Sleep (Ability_Cooldown + 1)   
        
      -- Setting the ability back to inactive state   
      Set_Next_State("State_Inactive")
   end
end

--======================= Threads: AI Auto Hyperjump =======================
-- When triggered each drone will be deactivated:
function Auto_Hyperjump ()
   
   -- innitiating variable
   Enemy_Units = {}
         
   -- Re-Searching for the fleet
   local All_Big_Units = Find_All_Objects_Of_Type("Corvette | Frigate | Capital")        
              
   -- For all units do
   for each,Unit in pairs(All_Big_Units) do
      -- If exist and is a enemy unit 
      if TestValid(Unit) and Unit.Get_Owner() ~= Object.Get_Owner() then   
         -- Then we will insert it into the table "Enemy_Units"
         table.insert(Enemy_Units, Unit)   
      end 
   end 
   
   local Enemys_in_Fleet = table.getn(Enemy_Units)  
   
   -- Choosing a random number, where Enemy_Units is the maximal number to use
   Random_Number_2 = GameRandom(1,Enemys_in_Fleet)
   
   
   if Autojump_Escape == false then  
   -- Creating a Hyperspace Particle on the positon of a random Enemy so we can start with State_Ability_1_A to jump there
   Target_Position = Create_Generic_Object(Hyperspace_Jump_Target, Enemy_Units[Random_Number_2], Object.Get_Owner())
  
   -- Waiting abit so the player units get some distance
   Sleep (GameRandom(16,28))
   end
   
   if Autojump_Escape then
      Own_Units = {}
      
      -- For all units do
      for each,Unit in pairs(All_Big_Units) do
        -- If exist and is a enemy unit 
         if TestValid(Unit) and Unit.Get_Owner() == Object.Get_Owner() 
           -- And the ship wont like to jump into itself ^^ 
            and Unit ~= Object then   
            -- Then we will insert it into the table "Enemy_Units"
            table.insert(Own_Units, Unit)   
         end 
      end 
      
      local Fleet_Size = table.getn(Own_Units) 
      Random_Number_2 = GameRandom(1,Fleet_Size)
      
      Target_Position = Create_Generic_Object(Hyperspace_Jump_Target, Own_Units[Random_Number_2], Object.Get_Owner())
      Autojump_Escape = false
   end
   
   Auto_Jumped = true
   
   
   -- Using state A of 1st ability:
   Set_Next_State("State_Active")
   
end 		

--================================= End of File =================================