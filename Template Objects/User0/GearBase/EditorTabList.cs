using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ShapeTemplateLib.Templates.User0 
{

    public partial class GearBase 
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
                                        Label="Scale",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="Scale",
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
                                        Label="Tooth Count",
                                        Width=2

                                   },
                                   new EditorColumn()
                                   {
                                        ColumnType=EditorColumn.eColumnType.Control,
                                        PropertyName="ToothCount",
                                        ControlName="TextBox",
                                        Width=4
                                   }

                              }
                         }
                     }

                }

        };
    }
}
