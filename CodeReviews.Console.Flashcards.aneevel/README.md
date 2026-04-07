## Design Decisions

- I thought a lot about what a general solution for error/exception handling should look like; I weighed a couple of options, including a global exception/error handler, but ultimately decided to log errors at the repository level and return a status code (either -1 or an empty/null value depending on case) because I didn't want to couple the reaction to an error occurring. The service should have no say in how the controller responds to an error, but it is relevant to it's purpose at a system level and thus it should inform the system of an error but leave the response to the controller.

## Out of Scope

There were a few things I consciously acknowledged as out of scope for this project in order to keep the learning manageable. There are many things that could be improved about the application, but I learn best by breaking up new techniques/knowledge into bite-sized pieces. 

- The "Options" pattern is something I will be using in the future. I understand conceptually that this is the industry standard way to bind configuration settings to type-safe classes at runtime. For now, I did utilize a "DatabaseSettings" class to provide some type safety, but notably it does not have fallback options. An improperly set up appsettings.json file will cause problems with this application that it is not equipped to handle.
- More log sinks. I considered the addition of some sort of "access" or "information" sink as well as utilizing a Spectre sink, which would be great in a true production environment to see when successful actions took place. I didn't see much benefit in doing it at this time because it's conceptually not any different than the error logging, but it would be a fantastic idea in a real application.
- Use of Entity Framework; I understand this will be a requirement in future projects, so I held off for now. My raw SQL skills are already good from my professional work, and I'm familiar with utilizing ORMs in CakePHP, so I think this will be an easy transition in future projects.
- 