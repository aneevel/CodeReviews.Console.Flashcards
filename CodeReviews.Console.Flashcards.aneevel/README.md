# Flashcard Application

Console Application written with C# 14/.NET 10 using JetBrains Rider.

This is a console application which allows you to study sets of Flashcards and track said sessions. Requirements were laid out
by [The C# Academy](https://www.thecsharpacademy.com/project/14/flashcards)

## Features
- Creates a SQLServer database upon application initialization (provided one didn't already exist)
- Creates all relevant tables in database
- Allows creation of "Flashcards", objects with a Question and Answer text component.
- Allows creation of "Study Stacks", related sets of Flashcards.
- Allows creation of "Study Sessions", dedicated study instances where you go through each Flashcard in a stack and receive a score.
- Logs errors to file
- Unit tested services

## Instructions

Ensure you have an 'appsettings.json' file with the correctly configured settings to point to your database. The one I've included should work "out of the box" but if you've modified your database environment you'll need to make some changes.

## Challenges Faced When Implementing This Project

- Sticking to one project when the demands of life keep getting bigger :P
- Keeping it simple, when my tendency is to make a system as robust as possible for future use. I don't need to make every project capable of being extended indefinitely.

## What I Learned

- I really dived deep into the repository/service/controller pattern, and how it eases separation of concerns.
- This was the first project I used Dependency Injection in, and it served me very well. I enjoy the modular design DI encourages, it made unit testing a breeze, and helped me think about the difference features necessary.
- DTOs! I did manual mapping for the DTOs to get the most familiarity with them. I had to think through at each layer whether how something should be modeled, and it lead to me thinking a lot about which components of a model are necessary and when.
- More reliance on System packages. Configuration and Logging are vital services, which the C# ecosystem recognizes and provides for. I'm used to coming from a JavaScript world where almost nothing is available as standard, you have to know where to look.

## Design Decisions

- I thought a lot about what a general solution for error/exception handling should look like; I weighed a couple of options, including a global exception/error handler, but ultimately decided to log errors at the repository level and return a status code (either -1 or an empty/null value depending on case) because I didn't want to couple the reaction to an error occurring. The service should have no say in how the controller responds to an error, but it is relevant to it's purpose at a system level and thus it should inform the system of an error but leave the response to the controller.
- Due to the design of my UI as sequential steps, the easiest UI/UX choice to address backing out of actions was simply to return to the main module. In the future, I think I would like to maintain of Stack of UI States so that I can simply pop the last one off to transition to a previous state. 
- I could have made UserInput an interface, and then inherited from that to utilize Spectre Console. This would be necessary if I were testing the controller methods, but I decided not to since they are just wrappers around the core service logic. Thus, there is one concrete implementation relying on Spectre.
- Probably the most interesting decision for me was to forego the use of a many-to-many table between StudyStacks and Study Sessions. This is how I would represent this relationship when drawing out the ERD. However, I decided to forego it here because the entity relationships are simple and I only had to write a touch more SQL to get associated entities. My understanding is that EF has a simple solution for these relationships, so I'm interested to see what's in store there.

## Out of Scope

There were a few things I consciously acknowledged as out of scope for this project in order to keep the learning manageable. There are many things that could be improved about the application, but I learn best by breaking up new techniques/knowledge into bite-sized pieces. 

- The "Options" pattern is something I will be using in the future. I understand conceptually that this is the industry standard way to bind configuration settings to type-safe classes at runtime. For now, I did utilize a "DatabaseSettings" class to provide some type safety, but notably it does not have fallback options. An improperly set up appsettings.json file will cause problems with this application that it is not equipped to handle.
- More log sinks. I considered the addition of some sort of "access" or "information" sink as well as utilizing a Spectre sink, which would be great in a true production environment to see when successful actions took place. I didn't see much benefit in doing it at this time because it's conceptually not any different than the error logging, but it would be a fantastic idea in a real application.
- Use of Entity Framework; I understand this will be a requirement in future projects, so I held off for now. My raw SQL skills are already good from my professional work, and I'm familiar with utilizing ORMs in CakePHP, so I think this will be an easy transition in future projects.
- The challenge "Reports". This is an interesting challenge but I was ready to move on from this project and practice other skills.

## Technologies Used

- Microsoft.Data.SqlClient 6.1.4
- Microsoft.Extensions.Hosting 10.0.3
- Microsofot.NET.Test.Sdk 17.14.1
- Spectre.Console 0.54
- xunit.runner.visualstudio 3.1.4
- Moq 4.20.72
- Serilog 4.3.1
- Serilog.Extensions.Logging 10.0.0
- Serilog.Sinks.Console 6.1.1
- Serilog.Sinks.File 7.0.0
- System.Linq 4.3.0
- System.Linq.Expressions 4.3.0
- xunit 2.9.3