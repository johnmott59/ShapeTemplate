# ShapeTemplate
The ShapeTemplate library will help with creating complex shapes via the Meshola API

This code creates a DLL in .NET, written in C#

The Meshola API takes XML as input and returns mesh data as output for a mesh consumer. 
The output formats are:

1. FBX files for import into modeling programs
2. JSON format for creating scenes in ThreeJS   (TBD)
3. C# Declarations of meshes for use in programs that want mesh data. (TBD)

The role of the template is to collect parameters and render XML, which is then transmitted to the API for conversion into a mesh.
The template library will be free, open source with an Unlicense, and will become one of the sources of code for generating meshes.

Because the interface to Meshola API is XML this library is not required, nor is the use of these classes required. Any language
or process that can generate XML can be used. The templates are a way to organize and define a complex shape that has properties that are modifiable and generate the correct
XML. 

Version 1.0 Aug 31, 2017
John Mott
