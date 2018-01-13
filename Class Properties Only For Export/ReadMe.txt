This directory contains class definitions that are only the properties and possibly constructor.
These class definitions will be swept into TypeScript files
for export into Javascript, for use in edit handlers. We do this so that the typescript handler has access to the raw types without
having to port any support code for the classes

The edit cycle for templates will be to serialize the template object into JSON and present it to the editing page. After
changes the object will be sent back in and deserialized. This will result in very low touching by the web site

By breaking the classes into different files we an keep full access for .net code, including the documentation, and still export
what will be needed by an edit handler.

these class definitions will be stripped of anything like inheritance so that they will appear to be 