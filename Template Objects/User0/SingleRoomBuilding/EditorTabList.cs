
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
    public partial class SingleRoomBuilding 
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
                                        Label="RoofOffset",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="RoofOffset",
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
                                        Label="Height",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="Height",
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
                                        Label="Length",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="Length",
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
                                        Label="Thickness",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="Thickness",
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
                                        Label="HorizontalScale",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="HorizontalScale",
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
                                        Label="VerticalScale",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="VerticalScale",
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
                    new EditorRow()
                    {
                         EditorColumnList=new List<EditorColumn>()
                         {
                             new EditorColumn()
                             {
                                  ColumnType=EditorColumn.eColumnType.Label,
                                   Label="Front Door",
                                   Width=2
                             },
                             new EditorColumn()
                             {
                                 ColumnType=EditorColumn.eColumnType.Control,
                                  PropertyName="Door",
                                  ControlName="RectangleWithOffset",
                                  Width=8
                             }
                         }

                    }
						}
				}	
		};

	}
}
