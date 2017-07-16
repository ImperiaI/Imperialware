--==================================================================================================
--************************************** Written by Imperial ***************************************
--*********************************** Script for story cheating ************************************
 
-- ***************** This scripts is allowed to be used as template for other mods *****************
--==================================================================================================

--[[ 
     For Manual Cheating you ONLY need to edit 3 Values:
     - Planet_01 = Ingame name of the planet where you want your cheated units to spawn.
     - Human_Player = The fraction you want to spawn for.
     - Selected_Units = This is a Table, it expects the units you like to spawn in "quotes", with , sign after each of them.
     
     
     The easiest way is to use the "Cheating" tag of the Imperialare UI.
     
     Finally you can innitiate the files Manually:
     
     Method 1:
     You cant spawn any unit using this script, unless it is innitated. 
     First of all you have to search for the right Campaign file .xml, once you found it search there for 
     the tag of the sandbox player, which can be <Rebel_Story_Name>, <Empire_Story_Name> or <Underworld_Story_Name>. (The faction you intend to play)
     There you will find the name of the right Story_Plots_Sandbox_???.xml file, copy 
     <Lua_Script>Story_Cheating</Lua_Script>
     right under <Story_Mode_Plots> of your Story_Plots_Sandbox file. Next time you load a savegame of this Galactic Conquest map 
     or start a new game it should load the cheated units. Doesen't even matter how old the savegame is.
     But if the "Story_Name" Tag doesen't exist in the Campaign file you can create one, or if its empty.. in this case use Method 2. 
     
     
     Method 2:
     Alternatively, you can innitiate "Story_Plots_Sandbox_Cheats.xml" in the <???_Story_Name> tag of the Campaignfile.xml
     of the campaign you want to cheat. Replace the tree ??? in the tag with any fraction (Empire, Underworld or Rebel), but
     contrary to the first case, here it must not be the Sandbox player fraction (because in case there could allready exist one story plot file the game will accept only 1 of them.) 
     So make sure you attach this plot file to a opponent fraction in order to preserve your own Story_Plots file. So choose and copy the right one from here below:
    
     <Empire_Story_Name>Story_Plots_Sandbox_Cheats.xml</Empire_Story_Name>
     <Rebel_Story_Name>Story_Plots_Sandbox_Cheats.xml</Rebel_Story_Name>
     <Underworld_Story_Name>Story_Plots_Sandbox_Cheats.xml</Underworld_Story_Name>
     
     Note: If you load a savegame that all ready had Story_Plots_Sandbox_Cheats.xml innitiated in its GC File right from the beginning,
     this savegame will load the units (just like starting this new campaign). But it wont spawn anything to allrady running campaigns using the method 2, 
     so Method 1 is better because it works always.
]]

     

require("PGStoryMode")
require("PGStateMachine")

--================================= Base Definitions =================================
 function Definitions()
   
   --define all events we "listen" to and assign them a function name (State_...)
   -- StoryModeEvents = {Universal_Story_Start = State_Universal_Story_Start}
   Define_State("State_Init", State_Init)
   Define_State("State_Galactic", State_Galactic)
   Define_State("State_Tactical", State_Tactical)
            
 end
 
--================================= Innitiating  =================================

 -- Triggered right at the beginning. We can do variable setup here
 --(searching for planets wouldn't work in the definitions section because we haven't loaded the gc until now)
 
function State_Init(message)
   if message == OnEnter then
   
   
      -- Defining a Target Planet where to spawn/teleport/destroy all units
      Planet_01 = Find_First_Object("Planet_Name_01") 
      -- Starting Planet
      Planet_02 = Find_First_Object("Planet_Name_02")
      
      -- In list mode the player can select a bunch of planets
      Planet_List = {"Selection_Planets"}
       
            
      -- Which player gets the unit?      
      Human_Player = "Faction_Name"      
      
      -- Auto assigning the current Player
      if Human_Player == "Human_Player" then       
         Human_Player = Object.Get_Owner()
      else
         Human_Player = Find_Player(Human_Player)                     
      end  
      
      
      -- The value we give the Player to cheat    
      Credit_Value = "Player_Credits" 
   
      if Credit_Value ~= 0 then
         Human_Player.Give_Money(Credit_Value)
      end
    
    
      -- Need the Units to be inside of a table. PLEASE DON'T make the table or any of the content units 
      -- a local variable, or it will crash the loop and the script. Because for loops don't like that! 
      All_Units = {        
         -- Defining all Space or Ground units (innitially false), remove the false and comment to activate the variable
         "Selected_Units"
      }  
      
      -- Getting all Units
      All_Found_Units = Find_All_Objects_Of_Type("Hero | Infantry | Fighter | Bomber | Transport | Corvette | Frigate | Capital")
               
      
      -- Mode Flags, they decide about the operation!    
      Spawn = false    
      Teleport_Units = false 
      Teleport_from_Planet = false 
      Teleport_Both = false  
      Build_Planet = false 
      
      Conquer_Planets = false
      Reverse_Planet_Selection = false
      
      Destroy_Galaxy = false
      Destroy_Target_Planet = false
      Show_All_Shields = false  
        
                   
      --================ Removing all Cheat Dummy Duplicates ================                             
      All_Cheat_Dummies = Find_All_Objects_Of_Type(Object.Get_Type().Get_Name())
      
      -- Removing all units except for this one to prevent unintended extra spawns
      for each, Unit in pairs(All_Cheat_Dummies) do  
         -- If there are other units of this kind  
         if TestValid(Unit) and Unit ~= Object then  
            -- Then we will insert it into the table "Fleet_01"
            Unit.Despawn()  
         end        
      end    
          
      
      --==== Choosing Game Mode ====
      if Get_Game_Mode() == "Galactic" then
         Set_Next_State("State_Galactic")  
      elseif Get_Game_Mode() ~= "Galactic" then	
         Set_Next_State("State_Tactical")  
      end   
                     
   end
end 

--====================== Tactical Space and Ground Mode =====================
function State_Tactical(message)
   if message == OnEnter then            
      if Object.Get_Owner().Get_Enemy().Is_Human() then    
         Object.Despawn() 
      end
      
      
      if Spawn == true then       
         -- Waiting until the Dummy Hyperspaced out       
         if Get_Game_Mode() == "Space" then Sleep(5) end    
            
         -- Spawning all wanted Units at position of the Cheating dummy
         SpawnList(All_Units, Object, Human_Player, true, true)           
      end   
      
      
      if Show_All_Shields then        
         for each, Unit in pairs(All_Found_Units) do 
            if Unit ~= Object then                  
               Hide_Sub_Object(Unit, 0, "Shield")               
            end
         end      
      end
        
                      
      Object.Despawn()    
   end
end           
                                 
--================================= Glactic =================================    
-- Triggered right at the beginning. We can do variable setup here
--(searching for planets wouldn't work in the definitions section because we haven't loaded the gc until now)
       
function State_Galactic(message)
   if message == OnEnter then      
       
                     
      -- If no Planet was selected, we take the Planet where the Cheating Dummy, ohr Object spawned    
      if Build_Planet == true then       
         Planet_01 = Object.Get_Planet_Location()                         
      end   
      
      
      --================ Conquering Selected Planets ================               
      if Conquer_Planets and table.getn(Planet_List) == 0 then 
                    
                            
         -- Getting all Fighters, got to get rid of them or they will disturb the process
         All_Fighters = Find_All_Objects_Of_Type("Fighter | Bomber")
         
                                                           
         for each, Unit in pairs(All_Fighters) do 
            -- If is located on the target Planet the 1 or 2 Planets will change faction and we put all units there into a variable
            if Unit ~= nil and Unit ~= Object and Unit.Get_Planet_Location() == Planet_01 and Unit.Get_Owner() ~= Human_Player then  
               Unit.Despawn()
            end
         end
         
         -- Waiting a bit
         Sleep (0.5)
                          
         Planet_01.Change_Owner(Human_Player)                                                                                       
         
        
      --======== If the User defined a planetary list we conquer ALL selected Planets ========  
      elseif Conquer_Planets and table.getn(Planet_List) ~= 0 then 
                                                         
         for each, Planet in pairs(FindPlanet.Get_All_Planets()) do                                              
            -- Only excluded planets wont get conquered    
            if Reverse_Planet_Selection and not Is_Object_In_List(Planet_List, Planet) then
               
               -- Getting Rid of all Units on that planet, otherwise they will disturb the process
               for each, Unit in pairs(All_Found_Units) do 
                  -- If is located on the target Planet the 1 or 2 Planets will change faction and we put all units there into a variable
                  if Unit ~= nil and Unit ~= Object and Unit.Get_Planet_Location() == Planet
                   and Unit.Get_Owner() ~= Human_Player then 
                     Unit.Despawn() 
                  end
               end
               
               -- Conquering Planet                                                                                                          
               Planet.Change_Owner(Human_Player) 
            
            -- This causes to conquer only the selected planets, above it conqers all unselected ones
            elseif Reverse_Planet_Selection == false and Is_Object_In_List(Planet_List, Planet) then
            
               -- Getting Rid of all Units on that planet, otherwise they will disturb the process
               for each, Unit in pairs(All_Found_Units) do 
                  -- If is located on the target Planet the 1 or 2 Planets will change faction and we put all units there into a variable
                  if Unit ~= nil and Unit ~= Object and Unit.Get_Planet_Location() == Planet
                   and Unit.Get_Owner() ~= Human_Player then 
                     Unit.Despawn() 
                  end
               end
               
               Planet.Change_Owner(Human_Player)                                 
            end
            -- Little break between each planet conquer  
            Sleep (20.0)
         end                                                                                       
      end    
          
                          
      --================ Destroying the whole Galaxy - Muhuhaha ================               
      if Destroy_Galaxy == true then           
         for each, Planet in pairs(FindPlanet.Get_All_Planets()) do  
            if Planet ~= Object.Get_Planet_Location() then               
                                                       
               for each, Unit in pairs(All_Found_Units) do 
                  -- If is located on the target Planet it gets destroyed 
                  if TestValid(Unit) and Unit ~= Object and Unit.Get_Planet_Location() == Planet then 
                     Unit.Despawn()
                  end
               end  
                              
               -- SGMG Function: Destroy a planet in GC        
               Death_Star_Fleet_List = SpawnList({"Death_Star"}, Planet, Find_Player("Neutral"), false, false)
       
               Death_Star = Find_First_Object("Death_Star")
               Death_Star.Set_Check_Contested_Space(false)          
               Death_Star.Activate_Ability("Death_Star")
               Death_Star.Set_Check_Contested_Space(true)
               Death_Star.Despawn()  
         
               -- The Planet is useless wasteland now, careful this line could cause crashes because of Change_Owner
               Planet.Change_Owner(Find_Player("Neutral"))  
               -- Waiting abit before we destroy the next one
               Sleep (0.6)
            end
         end      
      end    
      
      --==================== Destroying Target Planet ====================     
      if Destroy_Target_Planet == true then                                      
                                                         
         for each, Unit in pairs(All_Found_Units) do 
            -- If is located on the target Planet it gets destroyed 
            if TestValid(Unit) and Unit ~= Object and Unit.Get_Planet_Location() == Planet_01 then 
               Unit.Despawn()
            end
         end  
      
         -- I am not using a function of the code above because this is more reliable!                         
         -- SGMG Function: Destroy a planet in GC              -- Neutral Player because it must not be the same Faction!
         Death_Star_Fleet_List = SpawnList({"Death_Star"}, Planet_01, Find_Player("Neutral"), false, false)
       
         Death_Star = Find_First_Object("Death_Star")
         Death_Star.Set_Check_Contested_Space(false)          
         Death_Star.Activate_Ability("Death_Star")
         Death_Star.Set_Check_Contested_Space(true)
         Death_Star.Despawn()  
         
         -- The Planet is useless wasteland now, careful this line could cause crashes because of Change_Owner
         Planet_01.Change_Owner(Find_Player("Neutral"))      
      end          
                 
      --==========================================================     
      -- Defining all Units to spawn in a table, they can be called using their [number], depending on position;
      local Fleet_01 = {} 
      
      
      for each, Unit in pairs(All_Units) do  
         -- If any value is assigned to this Unit  
         if Unit ~= false then  
            -- Then we will insert it into the table "Fleet_01"
            table.insert(Fleet_01, Unit)   
         end        
      end     
                  
      --================ Spawning selected Units ================
      if Spawn == true then
         -- Spawning the fleet (unit_list, position, faction, allow_ai_usage, delete_after_scenario) 
         local Spawned_Fleet = SpawnList(Fleet_01, Planet_01, Human_Player, true, false)
      end
                        
      
      --============= Teleporting these Units only =============
      Units_Exist = false
      
      if Teleport_Units == true then
         -- Picking up the first found object of the selected Units                          
         for each, String_Name in pairs(Fleet_01) do 
            local Found_Unit = Find_First_Object(String_Name)               
            -- If exist and the Owner of the Unit is the same as the Object Owner (Human)
            if TestValid(Found_Unit) and Found_Unit.Get_Owner() == Object.Get_Owner() then    
               Found_Unit.Despawn()
               Units_Exist = true
            end
         end
               
         -- And Respawning them on the Destination Planet, the if state is a failsafe if the unit wasn't found.
         if Units_Exist then
            Spawned_Fleet = SpawnList(Fleet_01, Planet_01, Human_Player, true, false)  
         end    
      end 
           
      --============= Teleporting from Planet_02 to Planet_01 =============
      if Teleport_from_Planet == true then          
         -- ("HeroCompany | GroundCompany | GroundInfantry | GroundVehicle | Air | Transport | Fighter | Bomber | Corvette | Frigate | Capital")     
              
         Orbital_Fleet = {}     
                                     
         -- Removing any non player Units                       
         for each, Unit in pairs(All_Found_Units) do   
            if TestValid(Unit) and Unit.Get_Owner() == Object.Get_Owner() and Unit.Get_Planet_Location() == Planet_02 then 
                  -- All Nearby Units get duplicated to the destination planet then Deleted    
                  SpawnList({Unit.Get_Type().Get_Name()}, Planet_01, Object.Get_Owner(), true, false) 
                  
                  table.insert(Orbital_Fleet, Unit)                                                                
               
               -- Need to despawn seperately using this list in oder to prevent the script form crashing
               for each, Unit in pairs(Orbital_Fleet) do   
                  Unit.Despawn()
               end                                       
            end   
         end        
      end
          
      --=========== Teleporting selected Units from Planet_02 to Planet_01 ===========
      if  Teleport_Both == true then 
      
         Orbital_Fleet = {}
         
         -- Picking all found objects of this type                         
         for each, String_Name in pairs(Fleet_01) do 
            local Found_Units = Find_All_Objects_Of_Type(String_Name)               
                                 
            -- If exist on the Start Planet, and the Owner of the Unit is the same as the Object Owner (Human)           
            for each, Unit in pairs(Found_Units) do
               if TestValid(Unit) and Unit.Get_Owner() == Object.Get_Owner() and Unit.Get_Planet_Location() == Planet_02 then                   
                  -- All Nearby Units get duplicated to the destination planet then Deleted    
                  SpawnList({Unit.Get_Type().Get_Name()}, Planet_01, Object.Get_Owner(), true, false)                   
                  table.insert(Orbital_Fleet, Unit)
                  
                  -- Using the failsafe variable from above
                  Units_Exist = true
               end
            end
                        
            -- Need to despawn seperately using this list in oder to prevent the script form crashing
            if Units_Exist then      
               for each, Unit in pairs(Orbital_Fleet) do   
                  Unit.Despawn()
               end 
            end  
         end
                   
      end
      --========================================================
                   
      
      -- Getting rid of the Cheat Dummy
      Object.Despawn()
         
      
   end
end


-- =============================== Functions ===============================
-- Test if a certain object is in a list

function Is_Object_In_List(list, obj)
  for each,String_Name in pairs(list) do
    if Find_Object_Type(String_Name) == obj.Get_Type() then
      return true
    end
  end
  return false
end


 -- =============================== End of File ===============================
