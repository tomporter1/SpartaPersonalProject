# Valorant tracker

## Project goal
To make a stat tracker for the game Valorant that will also contain wiki information about elements of the game.

## Definition of Done
The completed project is committed to GitHub that implements all of the user stories. All of the accompanying documentation has also been completed.

## Sprints
* Sprint 1
    * **Goal:** Create the Database and the Entity Framework to go with it. Alos add 3 classes to the middle layer to act as managers for the Agent, Agent Type and Map tables in the database.
    * **Sprint Outputs:** The database and the DdContext have been created. Three manager classes that contain CRUD operations on the databases. Each manager has a full set of unit tests.
    * **Sprint retrospective:** 
      * **Went well:** Making the bulk of the database via an sql query was a good idea because it was what I was most familar with so it went quicker. Due to the previous practice these operations were easy to implement as it was the same method that was used in other projects before.
      * **Problems:** There was some issues with getting the correct NuGet packages installed but this was resolved and shouldn't need to be done again in this project. Also to write the unit tests for the managers I chose not to spend ages learning about mockes as that would take up too much time. I insetad decided to undo any changes that a test did to the database at the end of the test.

| Keban board before sprint 1 | Kaban board after sprint 1 |
| ------------------------- | ------------------------- |
| <img src = "ReadMeImages/Sprint1Before.png" width = 650 height = 365.625>|<img src = "ReadMeImages/Sprint1After.png" width = 650 height = 365.625>|

* Sprint 2
  * **Goal:** Start work on the GUI that allows the user to cary out CRUD operations on the agent, agent type and map tables
  * **Sprint Outputs:** 
  * **Sprint retrospective:** 
    * **Went well:** 
    * **Problems:**  

| Keban board before sprint 2 | Kaban board after sprint 2 |
| -------------------------- | -------------------------- |
| <img src = "ReadMeImages/Sprint2Before.png" width = 650 height = 365.625>||




## Project Retrospective
