
# Table of Contents
- [Introduction](#introduction)
- [Requirements](#requirements)
- [ERD](#EntityRelationshipDiagram)
- [Project Plan](#projectplan)


## Introduction
# ChrisSharp DiscordBot - MSSA project 
Collaboration and a way to communicate inter-personally in any environment is a necessity in the software development sector. Coming into the Microsoft Software and System Academy I thought I could get through the course being with minimal interaction. Little did i know the Software development environment is always a team effort. The course has their own way of implementing a way for students to share ideas and ask each other questions. With limited functionality through that medium I took it upon myself to create a discord server and utilize my weeks of codewars training and azure labs to create the not so ultimate discord bot. 

## Requirements

 - User Accessibility 
  1. User shall be able to utilize commands through the discord server
  2. User Shall be able to retrieve data connected to another users account
  3. User shall be able to set data to own account for others to pull
  
 - System Accessibility 
  1. System shall cache 100 messages to process
  2. System shall distinguish whether a message is intended for bot
  3. System shall asynchronously reply to user message
  4. System shall save all user data into a database
  5. System shall be able to message user directly
  6. System shall be able to allow certain commands based on permission
  
 - Software 
  1. System shall be writing in C# and ASP.NET using Discord.Net
  2. System database shall be in SQL

## Entity Relationship Diagram
![ERD](https://github.com/ChrisRuaboro/ChrisSharp/blob/master/DOCS/ERD.JPG))


## WireFrame

![Home](https://github.com/ChrisRuaboro/ChrisSharp/blob/master/DOCS/Wireframe1.JPG)![AddBot](https://github.com/ChrisRuaboro/ChrisSharp/blob/master/DOCS/Wireframe2.JPG)

## ProjectPlan
1.  Describe project experience including each phase of the  **SDLC** (Requirements, Design, Implementation & Test) and the project artifacts (**_design_** documents, requirements trace,  _**test**_ reports...)
2.  TestReport
	[TRR](https://github.com/ChrisRuaboro/ChrisSharp/blob/master/TRR.md)

3.  Solution
    -   What  **technologies** did you integrate?
    -Discord.Net
    -CSS
    -JS
    -HTML AgilityPack
     -   What are paths that you explored?
        -Storing data in text files
        -Discord.Net Documentation
        -   What are some future development ideas?
        - Add music functionality
        - Gerard's Meme generator
        - Xp System
        - Use Azure KV so no one steals my token
        - Event Logger
        - 
4.  **architecture** and  **code** walk-through
    [Code](https://github.com/ChrisRuaboro/ChrisSharp/tree/master/ChrisSharp)
5.  product  **demonstration** _(cloud-deployed)_
	 [Site](https://chrissharp.azurewebsites.net)
6.  **lessons learned**:

-   -   -   what did you do right?
			- I switched my project
			- picked something that utilized a lot of the C# step by step book
        -   what did you do wrong?
	        - I switched my project
	        - didn't start with the implementation of a Db
        -   where were you lucky?
	        - Had documentation to help me
	        - had easy to understand code and very readable
	        - Did a MVC Website but the bot was more of a learning experience
        -   what would you change / what do you know now that you wish you knew "then"?
	        - Change - Organization
	        - Proper planning in the beginning
	        - Sync Changes to github more so i have more activity


