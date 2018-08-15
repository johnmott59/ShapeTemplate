using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib
{
    /*
     * The template root contains entry points needed for a template, like the data train components.
     */
    public class TemplateBaseClass : TransformableRoot
    {
        /*
            * The property data can be retrieved as a json data train, for use in export and editing
            * The property values that can be modified are packaged, each property in a 'carriage', and the 
            * carriages are combined into a 'train'. This train is then exposed so that a caller can modify
            * the properties via JSON. This is particularly useful in HTML editing, where an edit control
            * can extract a value or replace a modified value
            */

        public virtual JSONDataTrain GetJSONDataTrain()
        {
            return new JSONDataTrain();

        }

        // the property data can be loaded from a data train, for use in export and editing
        public virtual void LoadJSONDataTrain(string sDataTrain)
        {

        }
        /*
         * If this template is going to be edited in the browser then each property has to have a Javascript control assigned to it that knows how to 
         * edit it. The Javascript control wraps a UI element of some sort, which could be a checkbox, textbox, dropdown, or a GUI.
         * The control can manage a single property multiple properties simultaneously. The Javascript code for the control will know what properties to 
         * pull and restore to and from the DataTrain.
         * An object might have many properties to manage, so there can be a tabbed interface. There is always at least one tab, although there is
         * not a tabbed interface unless there are two or more tabs.
         */
        public virtual List<EditorTab> EditorTabList { get; set; } = new List<EditorTab>();

        /*
         * Retrieve an instance of the template by name. At some point we will use reflection to do this
         */
        public static TemplateBaseClass GetTemplateInstance(string name)
        {
            if (String.IsNullOrEmpty(name)) return null;

            string[] parts = name.Split(':');
            if (parts.Length != 2) return null;
            string user = parts[0];
            string templatename = parts[1];

            return GetTemplateInstance(user, templatename);

        }
        public static TemplateBaseClass GetTemplateInstance(string user, string templatename)
        {
            switch (user.ToLower())
            {
                case "user0":
                    switch (templatename.ToLower())
                    {
                        case "gearbase":
                            return new GearBase();
                        case "singleroombuilding":
                            return new SingleRoomBuilding();
                        case "straightstairs":
                            return new StraightStairs();
                        case "simplelayout":
                            return new SimpleLayout();
                    }
                    break;
            }

            return null;
        }
    }
}
