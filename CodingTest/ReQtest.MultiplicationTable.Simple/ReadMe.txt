The design of the solution details main concepts and responsibilities.
- command-line processing
- computation of the multiplication table
- writing output

There is a minor duplication in the different format of the output.
There is a minor dependencies between the multiplication table and the different output format

In this design the priority is the balance between simplicity and maintainability.
When future feature request will require to change or extend a responsibility, the design still make it easy to extract and extend the responsibility with minor non-local changes.
A dynamically typed language would further reduce the need of non-local changes of this design.


