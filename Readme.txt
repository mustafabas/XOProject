﻿Project Assessment:
    Cross Exchange is an arbitrarily trading game developed by a startup in a very short span of time called “Super Traders”. 

The purpose of the application is to educate users on the terminology used in the trading of shares.

Notes:
    
- The project registers share and allows admins to update the price of the share on an hourly basis; the share registered 
should have unique Symbol to identify it and should be all capital letters with 3 characters. 
   
 - The rate of the share should be exactly 2 decimal digits. 
   
 - The frontend application is excluded from the current scope.
 It is a separate, fully-functional application handled by another team, and we do not want to modify it.

Tasks:

   
 1) For a given symbol of share, get the statistics for that particular share calculating the maximum, minimum, average and sum of price for all the trades that happened for that share. 
   
 Group statistics individually for all BUY trades and SELL trades separately. 

    Your goal is to complete the above task. 
The API specifications are already there in the code as agreed with the frontend team. 

   
 2) There are a few bugs in the application that we would like you to fix. 
Even though the project might not be in a great structure, please do not spend your valuable time on structural modifications - just focus on fixing the bugs.

   
 3) Increase unit test coverage to reach code coverage up to 60%, achieving more than 60% will only consume your valuable time without any extra score.
    
  
  4) Implement best coding practices for the code provided. Review and fix the code such that there are no code smells, vulnerabilities and the code is in line with best coding guidelines for C#.
    

  
  PLEASE NOTE THAT ALL OF THE TASKS LISTED ABOVE ARE MANDATORY.

  
  We will evaluate your submission on the following parameters:
        - Implementation of the new feature
        - Bug fixes
        - Unit Tests
        - Code quality

    Prerequisites:
         
- GIT
         - VS2017
         - .NET Core 2.0
         - SQL Server 2012+


   
Development Environment:
     
   Cross Exchange application:
        
        - Modify the database connection string as per your instance and authentication.
        
- On any terminal move to the “XOProject” folder (the folder containing the “XO-Project.csproj" file) and execute these commands:

        dotnet restore
        dotnet build
        dotnet ef database update

       
 - Now you can call the API using any tool, like Postman, Curl, etc 
        
        - To check code coverage, execute the batch script:
        coverage.bat

   
Production Environment:
     
   This is how we are going to run and evaluate your submission, so please make sure to run below steps before submitting your answer.

      
  1) Make sure to run unit tests, check code coverage, ensure the application is compiling and all dependencies are included.
       
 2) Zip the entire codebase as XoProject_<yournamehere>.zip (removing /bin and /obj directories).
        
4) Store your file in a shared location where Crossover team can access and download it for evaluation. and add your sharable link in the answer field of this question.


