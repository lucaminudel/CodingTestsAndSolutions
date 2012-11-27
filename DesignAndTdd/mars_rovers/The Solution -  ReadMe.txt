

== EXECUTE

Use 'rake' or 'rake test' to execute the tests.
Use 'rake run' to execute the app.





== IMPLEMENTATION NOTES

Implemented with Ruby 1.9.3-p194 on Windows32 platform.

I'm a beginner with Ruby, I expect the solution to function correctly and
the design to be simple and flexible.
I think is possible that I missed some Ruby convention and not used some common idiom.

I chosen Ruby to compare this solution with the solution of a very similar problem I
implemented with Visual Studio 2005 and C# 2.0 in 2009:
- https://github.com/lucaminudel/CodingTestsAndSolutions/tree/master/CodingTest2





== ASSUMPTIONS ABOUT REQUIREMENTS

Input errors are caught, basic diagnostic message is provided.

Error conditions like
- deployment and movement outside the plateau
- deployment and movement over the previous deployed rover
are detected in advance.
The action is then prevented to avoid damage and an error message is displayed.
Also the operation is aborted.

Giving the user the possibility to continue after an error is desirable, the
error make the rest of the provided input useless so this require to visualize
the current state and ask the user to enter the remaining commands.
This is better done with an interactive kind of input that is very different to
the current batch mode and so requires major changes to the requirements.
This feature is a possibility suggested for the next version.





== NOTES ABOUT THE DESIGN

Some simplification I made that seems a good balance for this app at the
current moment:
- the input commands LRM are sent directly to the rover without a mapping
  of the input into another format. And the same is true for the heading.
  This is a coupling between 2 distinct concepts, easy to decouple in the future.
 
- the message in the exception is sometime displayed directly to the user instead
  of providing a different message eventually with extra info related to the context.
 
- the MarsRoversApp act as controller between the input/output and the rovers
  but the view and the model are not explicit defined in the code
 

The run-time dependencies are:

- CommandLineParser -> PointOfCompass

- NasaRover -> PointOfCompass
- NasaRover -> MarsPlateu
- NasaRover -> occupied_spots

- MarsRoversApp -> CommandLineParser
- MarsRoversApp -> PointOfCompass
- MarsRoversApp -> occupied_spots
- MarsRoversApp -> NasaRover

