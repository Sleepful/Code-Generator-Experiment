# Code Generator

Just a little tool I made to automate the coding of general CRUD components that had 3 input fields, the tool generates code that can be
integrated into a codebase, this codebase is not included here, but it was a fun exercise to increase productivity,
so I'm leaving the code here for curiousity's sake.

The script is written in python at `app.py`, I'd run it with `start.sh` and clean the output with `clean.sh`

So basically what this did, is create a CRUD component with 3 input fields, the name of the
component is customizable inside the python script, this would generate the code needed for a
C# backend, with its respective Entities for the database, stored procedure, and migrations. 
It would also generate the needed code for an Angular front-end with bootstrap.

# Running it

* You go into `app.py` and customize the names of the component (such as the name of access permission)
* You run `start.sh`
* The script replaces the variable names inside each template on the `\Template\` directory
* Your output is in an `\Output\`, now, depending on the file, you can copy and paste the output into the codebase, without having to type code :) 
