The design of the solution details every single concept and responsibilities.
Because of this is possible to change or evolve
- the command line commands
- the default and optional parameters
- the format of the different output (csv,html, plain text)
- the destination of the output (console, file)
- the content of the multiplication table
making only local changes to the code


Responsibilities
-----------------

The solution separate the different responsibilities and concepts without duplication:

- The responsibility to deal with default and optional command line parameters is separated from the responsibility to parse the command line parameters.
- The responsibility to calculate the multiplication table is separated from the responsibility to print the multiplication table on the output
- The responsibility to create the output in different format (csv, html and plaint text) is separated from the responsibility to write the output to the console or to a file
- The responsibility to to write the output in each format is separated from the responsibility to write the output in another format, still the part common to all formats are not duplicated


Dependencies
--------------

Not only the responsibilities are separated, also there are no dependencies between classes that deal with different responsibilities:

- The TableBuilders namespace have dependencies only to the abstract types defined in the root namespace
- The Commands namaspace also have dependencies only to the abstract types defined in the root namespace
- The Application is the only one that have dependencies to the TableBuilders and Commands because use them
- There are no circular dependencies between namespaces or classes







