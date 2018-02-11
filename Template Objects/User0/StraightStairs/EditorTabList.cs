
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using System.Web.Script.Serialization;

namespace ShapeTemplateLib.Templates.User0 
{
    public partial class StraightStairs 
    {

        /// <summary>
        /// This contains the editor definitions needed to modify this template in the browser. This property is 
        /// only necessary if the template is to be edited in the browser, if its accessed from the API its not used.
        /// </summary>
        public override List<EditorTab> EditorTabList { get; set; } = new List<EditorTab>()
        {
                new EditorTab()
                {
                     EditorRowList=new List<EditorRow>()
                     {


                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="VerticalDistance",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="VerticalDistance",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
	
                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="HorizontalDistance",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="HorizontalDistance",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
	
                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="LateralDistance",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="LateralDistance",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
	
                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="StairCount",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="StairCount",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
	
                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="Width",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="Width",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
	
                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="Rise",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="Rise",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
	
                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="Run",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="Run",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
	
                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="LeftSideTexture",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="LeftSideTexture",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
	
                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="RightSideTexture",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="RightSideTexture",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
	
                    new EditorRow() {
                              EditorColumnList=new List<EditorColumn>()
                              {
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Label,
                                        Label="StairTexture",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="StairTexture",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         },
						}
				}	
		};

	}
}
