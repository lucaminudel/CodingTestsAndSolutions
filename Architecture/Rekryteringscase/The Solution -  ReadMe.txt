README
=======

1-Setup

2-Execution

3-Implementation




1-Setup
---------
This application has been written and tested on Windows.
It has been tested with Firefox and IE7.

In order to run the application you need to download and install:
Ruby 1.9.3-p194: http://rubyforge.org/frs/download.php/76054/rubyinstaller-1.9.3-p194.exe

Also there Gems are required:
- sinatra (1.3.2)
- sinatra-reloader (0.5.0)
- watir-webdriver (0.6.1)

To install:
C:\> gem install bundler
C:\> cd moveit
C:\moveit> bundle install


The acceptance tests run on Firefox.
A functioning version of Firefox is also required.





2-Execution
-------------
From the command line:

- rake run 
  to lunch the web-server and the web-app on port 4567.
  then you can open the browser at http://localhost:4567

- rake run_prod
  to lunch the web-server and the web-app on port 80.
  then you can open the browser at http://localhost:4567

- rake test 
  to run the unit tests and check that they pass

- rake atest 
  to run the acceptance tests and check that they pass.
  OBS! you need to run 'rake run' first from another console to start the web app.

- rake db_reser 
  create the folders where offers and customers are persisted





3-Implementation
------------------
The implementation has some characteristic of a real Enterprise Application.

The input is verified also on the server for security reasons.

The info persisted is normalized.
The code deal properly with locking, concurrency and concurrency conflicts.

The web application can be hot-deployed because it does not make use of Session objects 
and so can run for example in load-balancing on different production web servers.

Globar error catching is implemented.

The design of the code make it more easy to:

- replace an external system with a different implementation for example replace the current
  persistence implementation with a RDBMS or a db on the cloud.

- make changes to the domain objects, for example evolve the price calculation, this because the dependencies are
  limited and organized and every object have a well defined responsibility

- configure the system to run on different environments (dev PC, beta for acceptance tests or
  unit tests for manual tests, production, ...) even if more external systems are integrated/added
  because objects instantiation and dependencies injections are organized in the code

- test the system with unit tests, integration tests and acceptance tests.

