This directory contains class definitions for templates which will run in Javascript in edit handlers.

These class definitions will be swept into TypeScript files for export into Javascript, for use in edit handlers. 
We do this so that the typescript handler has access to the raw types without having to port any support code for the classes.

These are partial classes and contain only what is essential for a javascript version of this class, which includes

- properties
- possibly a constructor, if properties must be set to inital values. 
- a CopyFrom routine,  which will allow javascript to make JS object versions of these classes.
- methods that are required in Javascript and that will safely port from C# to Typescript to Javascript

There should not be any inheritance or interface information in these classes. The class definitions in this directory 
will be stripped of anything like inheritance so that they will appear to be stand alone classes, so these classes cannot
implement overrides from a subclass.

The edit cycle for templates will be to serialize the properties of the template object that we want to edit into JSON and 
present it to the editing page. After  changes the object will be sent back in and deserialized. This will result 
in very low touching by the web site.

By breaking the classes into different files we an keep full access for .net code, including the documentation, and still export
what will be needed by an edit handler.

